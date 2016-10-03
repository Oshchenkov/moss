﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel;
using Castle.Core.Logging;
using EC.Common.Base;
using EC.Common.Interfaces;

namespace EC.Core.Common
{
    /// <summary>
    /// Base class implementation for all variants of MLSTask.
    /// </summary>

    public class MLSTaskBase : IDisposable
    {
        /// <summary>
        /// Return the current status of the task. If the task has not actually been
        /// spawned yet (i.e. Execute() has not been called) then the status is
        /// WaitingForActivation.
        /// </summary>

        public TaskStatus Status()
        {
            var currentStatus = TaskStatus.WaitingForActivation;
            if (MyTask != null) currentStatus = MyTask.Status;
            return currentStatus;
        }

        /// <summary>
        /// Block until this task completes.
        /// </summary>

        public void Wait()
        {
            if (MyTask != null)
            {
                MyTask.Wait();
            }
        }

        /// <summary>
        /// Return the underlying C# Task.
        /// </summary>

        public Task GetUnderlyingTask()
        {
            return MyTask;
        }

        /// <summary>
        /// IoC constructor.
        /// </summary>

        public MLSTaskBase(IKernel k, ILogger l)
        {
            Kernel = k;
            Logger = l;
            SwallowExceptions = false;
        }

        /// <summary>
        /// Release resources used by this object.
        /// </summary>

        public void Dispose()
        {
            if (MyTask != null) { MyTask.Dispose(); }
        }

        /// <summary>
        /// If true, then any exceptions generated by this task will be caught and logged
        /// within the task, but not re-thrown. If false, then exceptions will be available
        /// via Wait().
        /// </summary>
        
        public bool SwallowExceptions { get; set; }

        // ---------------------------- Protected Data ---------------------------------------

        protected IKernel Kernel = null;
        protected ILogger Logger = null;
        protected Task MyTask = null;

    }

    /// <summary>
    /// TaskWithContext allows a task to be spawned that relies on IRequestContext (which almost
    /// all service level code does). The subtask uses the RequestContext of the thread spawning it. 
    /// Because of this, a thread which creates a TaskWithContext cannot exit/complete until the 
    /// TaskWithContext is complete.
    /// </summary>

    [TransientType]
    [RegisterAsType(typeof(ITaskWithContext))]

    public class TaskWithContext : MLSTaskBase, ITaskWithContext
    {
        /// <summary>
        /// Execute the given action and wait for the task to complete.
        /// </summary>
        /// <remarks>
        /// The request context of the parent thread *must* be fetched here so that it can be
        /// given to the newly spawned task. The code in SetupAndStart() is executed by the
        /// new thread, and hence does not have access to the request context of the 'parent'.
        /// </remarks>
        /// <param name="a">the code to execute</param>

        public void Execute(Action a)
        {
            var contextMgr = Kernel.Resolve<IManageRequestContexts>();
            var requestContext = contextMgr.GetContext();
            MyTask = Task.Factory.StartNew(() => SetupAndStart(a, requestContext));
        }

        /// <summary>
        /// Set up the RequestContext for the Task and the execute its main body. This
        /// code is run in the newly created thread.
        /// </summary>
        /// <remarks>
        /// For incoming WCF operations, the RequestContext is set up with a PerWCFRequest
        /// lifestyle and can simply be resolved whenever needed. However, the threads that underlie
        /// the C# Task class are not associated with a WCF request, and attempts to resolve
        /// the RequestContext fail. So, for spawned tasks we need to use the InitializeThreadRequestContext()
        /// method on the request context manager to inform it that this thread does not bind
        /// to the WCF/Web request context.
        /// <para>
        /// NOTE: It is *very* important to clear the thread local storage of the spawned
        /// thread when the Task completes. This is because the thread may be used to handle
        /// an incoming WCF operation, and if the thread local storage RequestContext is
        /// still there, it will be used instead of the RequestContext bound to the operation.
        /// (Not clearing the thread local storage caused bug #1012)
        /// </para>
        /// </remarks>
        /// <param name="a">the code to execute within the new task</param>
        /// <param name="rc">the request context of the 'parent' thread</param>

        private void SetupAndStart(Action a, IRequestContext rc)
        {
            var contextMgr = Kernel.Resolve<IManageRequestContexts>();
            contextMgr.InitializeThreadRequestContext();
            var context = contextMgr.GetContext();
            context.CopyFrom(rc);

            try
            {
                a.Invoke();
            }
            catch (Exception ex)
            {
                if (!SwallowExceptions) { throw ex; }
                Logger.Warn("TaskWithContext - Exception swallowed", ex);
            }
            finally
            {
                contextMgr.ClearThreadContext();
            }
        }

        /// <summary>
        /// IoC object creation entry point.
        /// </summary>

        public TaskWithContext(IKernel k, ILogger l) : base(k,l)
        {
        }
    }

    /// <summary>
    /// SyncTask is used to spawn a background action and then wait for it to complete.
    /// </summary>
    /// <remarks>
    /// It may seem pointless to spawn a background task only to sit and wait for it to 
    /// complete. However, this is exactly the interaction required if you want to 
    /// create a new unit of work that is independent of the one you are currently
    /// executing in.
    /// </remarks>

    [TransientType]
    [RegisterAsType(typeof(ISyncTask))]

    public class SyncTask : MLSTaskBase, ISyncTask
    {
        /// <summary>
        /// Execute the given action and wait for the task to complete.
        /// </summary>
        /// <param name="a">the code to execute</param>

        public void Execute(Action a)
        {
            MyTask = Task.Factory.StartNew(() => SetupAndStart(a));
            MyTask.Wait();
        }

        /// <summary>
        /// Invoke the code for this task, and if necessary catch and swallow exceptions.
        /// </summary>

        private void SetupAndStart(Action a)
        {
            try
            {
                a.Invoke();
            }
            catch (Exception ex)
            {
                if (!SwallowExceptions) { throw ex; }
                Logger.Warn("TaskWithContext - Exception swallowed", ex);
            }
        }

        /// <summary>
        /// IoC object creation entry point.
        /// </summary>

        public SyncTask(IKernel k, ILogger l) : base(k,l)
        {
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

using EC.Models;
using EC.Models.Database;
using EC.Constants;
using EC.Core.Common;
using EC.App_LocalResources;
using EC.Models.ViewModel;
using EC.Common.Interfaces;
using EC.Localization;
using EC.Models.ViewModels;

namespace EC.Controllers.API
{
    public class CasesController : BaseApiController
    {
        public class Filter
        {
            public int ReportFlag { get; set; }
        }

        [HttpGet]
        public object Get([FromUri] Filter filter)
        {
            var _started = DateTime.Now;

            user user = (user)HttpContext.Current.Session[ECGlobalConstants.CurrentUserMarcker];

            if (user == null || user.id == 0)
            {
                return null;
            }

            UserModel um = new UserModel(user.id);
            UsersReportIDsViewModel vmAllIDs = um.GetAllUserReportIdsLists();
            UsersUnreadReportsNumberViewModel vmUnreadReports = um.GetUserUnreadCasesNumbers(vmAllIDs);

            ///old var report_ids = um.ReportsSearchIds(um._user.company_id, filter.ReportFlag);

            List<int> report_ids = new List<int>();
            switch (filter.ReportFlag)
            {
                case 1:
                    //active
                    report_ids = vmAllIDs.all_active_report_ids;
                    break;
                case 2:
                    //completed
                    report_ids = vmAllIDs.all_completed_report_ids;
                    break;
                case 5:
                    //closed
                    report_ids = vmAllIDs.all_closed_report_ids;
                    break;
                case 3:
                    //spam
                    report_ids = vmAllIDs.all_spam_report_ids;

                    break;
                case 4:
                    //pending
                    report_ids = vmAllIDs.all_pending_report_ids;

                    break;
                default:
                    report_ids = vmAllIDs.all_report_ids;
                    break;
            }

            string investigation_status = LocalizationGetter.GetString("Investigation");
            int delay_allowed = 2;
            if (report_ids.Count > 0)
            {
                ReportModel tempRm = new ReportModel(report_ids[0]);
                investigation_status = tempRm.InvestigationStatusString();
                delay_allowed = tempRm.GetDelayAllowed();
            }
            if (investigation_status.ToLower().Contains(LocalizationGetter.GetString("Investigation").ToLower()))
                investigation_status = LocalizationGetter.GetString("Investigation");
           //////  var reports = report_ids.Select(x => new CasePreviewViewModel(x, user.id)).ToList();
            var  reports = um.ReportPreviews(report_ids, investigation_status, delay_allowed).ToList();

            string title = LocalizationGetter.GetString("ActiveCasesUp");
            title = filter.ReportFlag == 2 ? LocalizationGetter.GetString("CompletedcasesUp") : title;
            title = filter.ReportFlag == 5 ? LocalizationGetter.GetString("ClosedCasesUp") : title;
            title = filter.ReportFlag == 3 ? LocalizationGetter.GetString("SpamcasesUp") : title;
            title = filter.ReportFlag == 4 ? LocalizationGetter.GetString("NewReportsUp") : title;
            var m = new
            {
                Mode = filter.ReportFlag,

                Reports = reports,

                Counts = new
                {
                    Active = vmUnreadReports.unread_active_reports,
                    Completed = vmUnreadReports.unread_completed_reports,
                    Spam = vmUnreadReports.unread_spam_reports,
                    Closed = vmUnreadReports.unread_closed_reports,
                    Pending = vmUnreadReports.unread_pending_reports,
                },

                Title = title,

                counter = (DateTime.Now - _started).TotalMilliseconds,
            };

            return ResponseObject2Json(m);
        }
    }
}
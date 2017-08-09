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
            user user = (user)HttpContext.Current.Session[ECGlobalConstants.CurrentUserMarcker];

            if (user == null || user.id == 0)
            {
                return null;
            }

            UserModel um = new UserModel(user.id);
            var report_ids = um.ReportsSearchIds(um._user.company_id, filter.ReportFlag);
            List<int> all_active_report_ids = um.ReportsSearchIds(um._user.company_id, 1);
            List<int> completed_report_ids = um.ReportsSearchIds(um._user.company_id, 2);
            List<int> spam_report_ids = um.ReportsSearchIds(um._user.company_id, 3);
            List<int> closed_report_ids = um.ReportsSearchIds(um._user.company_id, 5);

            var reports = report_ids.Select(x => new CasePreviewViewModel(x, user.id)).ToList();

            IDateTimeHelper m_DateTimeHelper = new DateTimeHelper();
            var userIds = reports.Select(x => x.last_sender_id).ToList();

            var m = new
            {
                Mode = filter.ReportFlag,

                Reports = reports,

                ReportsAdv = reports
                    .Select(x => new {
                        id = x.report_id,
                        last_investigation_status_date = m_DateTimeHelper.ConvertDateToLongMonthString(new ReportModel(x.report_id)._last_investigation_status_date)
                    }).ToList(),

                Users = DB.user.Where(x => userIds.Contains(x.id)).ToList(),
                UserIds = userIds,

                Counts = new
                {
                    Active = UnreadReportsInProgressNumber(all_active_report_ids, user.id),
                    Completed = UnreadReportsInProgressNumber(completed_report_ids, user.id),
                    Spam = UnreadReportsInProgressNumber(spam_report_ids, user.id),
                    Closed = UnreadReportsInProgressNumber(closed_report_ids, user.id),
                },
            };

            return ResponseObject2Json(m);
        }

        private int UnreadReportsInProgressNumber(List<int> _report_ids, int user_id)
        {
            int _count = 0;

            //    select rep_id from[Marine].[dbo].[testt]  z
            //where id in (select max(id) from[Marine].[dbo].[testt] z2 group by z2.rep_id) and z.status_id = 4
            var refGroupReportLogs = (from m in DB.report_log
                                      group m by m.report_id into refGroup
                                      //   orderby refGroup.Id descending
                                      select refGroup.OrderByDescending(x => x.created_dt).FirstOrDefault());

            var refGroupReportReadDate = DB.report_user_read.Where(item => ((item.user_id == user_id)));

            DateTime dt1, dt2;
            foreach (int ID in _report_ids)
            {
                if (refGroupReportReadDate.Where(item => ((item.report_id == ID))).Count() == 0)
                {
                    dt2 = ECGlobalConstants._default_date;
                }
                else
                {
                    dt2 = refGroupReportReadDate.Where(item => ((item.report_id == ID))).Select(t => t.read_date).FirstOrDefault();
                }

                if (refGroupReportLogs.Where(item => ((item.report_id == ID))).Count() == 0)
                {
                    dt1 = ECGlobalConstants._default_date.AddDays(2);
                }
                else
                {
                    dt1 = refGroupReportLogs.Where(item => ((item.report_id == ID))).Select(t => t.created_dt).FirstOrDefault();
                }

                if (dt2 < dt1)
                    _count++;
            }
            return _count;
        }
    }
}
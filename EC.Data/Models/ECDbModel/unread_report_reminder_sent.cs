//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EC.Data.Models.ECDbModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class unread_report_reminder_sent
    {
        public int id { get; set; }
        public int report_id { get; set; }
        public System.DateTime sent_dt { get; set; }
        public System.DateTime last_update_dt { get; set; }
        public int user_id { get; set; }
    }
}

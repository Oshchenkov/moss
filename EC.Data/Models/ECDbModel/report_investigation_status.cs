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
    
    public partial class report_investigation_status
    {
        public int id { get; set; }
        public int report_id { get; set; }
        public int investigation_status_id { get; set; }
        public System.DateTime created_date { get; set; }
        public int user_id { get; set; }
        public string description { get; set; }
        public Nullable<int> approved { get; set; }
        public Nullable<int> approved_user_id { get; set; }
        public string approved_message { get; set; }
        public Nullable<int> outcome_id { get; set; }
        public string outcome_message { get; set; }
        public string outcome_other { get; set; }
        public Nullable<int> case_closure_reason_id { get; set; }
        public string case_closure_report { get; set; }
        public string executive_summary { get; set; }
        public string facts_established { get; set; }
        public string investigation_methodology { get; set; }
        public string description_outcome { get; set; }
        public string recommended_actions { get; set; }
    }
}

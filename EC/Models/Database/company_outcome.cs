//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EC.Models.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class company_outcome
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public int status_id { get; set; }
        public string outcome_en { get; set; }
        public string outcome_fr { get; set; }
        public string outcome_es { get; set; }
        public string outcome_ru { get; set; }
        public string outcome_ar { get; set; }
        public string outcome_jp { get; set; }
        public System.DateTime last_update_dt { get; set; }
        public int user_id { get; set; }
    }
}

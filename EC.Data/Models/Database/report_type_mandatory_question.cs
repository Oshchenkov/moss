//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EC.Data.Models.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class report_type_mandatory_question
    {
        public int id { get; set; }
        public int report_type_id { get; set; }
        public Nullable<int> secondary_type_id { get; set; }
        public int status_id { get; set; }
        public Nullable<byte> header_in { get; set; }
        public string question_en { get; set; }
        public string question_fr { get; set; }
        public string question_es { get; set; }
        public string question_ru { get; set; }
        public string question_ar { get; set; }
        public string question_ja { get; set; }
        public string tip_en { get; set; }
        public string tip_fr { get; set; }
        public string tip_es { get; set; }
        public string tip_ru { get; set; }
        public string tip_ar { get; set; }
        public string tip_ja { get; set; }
        public Nullable<System.DateTime> last_update_dt { get; set; }
        public Nullable<int> user_id { get; set; }
    }
}

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
    
    public partial class company_custom_question
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public Nullable<int> type_id { get; set; }
        public Nullable<int> secondary_type_id { get; set; }
        public Nullable<int> custom_question_type_id { get; set; }
        public int status_id { get; set; }
        public System.DateTime created_date { get; set; }
        public string question_title_en { get; set; }
        public string question_tip_en { get; set; }
        public string question_body_en { get; set; }
        public string question_title_fr { get; set; }
        public string question_tip_fr { get; set; }
        public string question_body_fr { get; set; }
        public string question_title_es { get; set; }
        public string question_tip_es { get; set; }
        public string question_body_es { get; set; }
        public string question_title_ru { get; set; }
        public string question_tip_ru { get; set; }
        public string question_body_ru { get; set; }
        public string question_title_ar { get; set; }
        public string question_tip_ar { get; set; }
        public string question_body_ar { get; set; }
        public int user_id { get; set; }
        public System.DateTime last_update_date { get; set; }
    }
}

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
    
    public partial class preferred_contact_method
    {
        public int id { get; set; }
        public string contact_method_en { get; set; }
        public string contact_method_fr { get; set; }
        public string contact_method_es { get; set; }
        public string contact_method_ru { get; set; }
        public string contact_method_ar { get; set; }
        public System.DateTime last_update_dt { get; set; }
        public int user_id { get; set; }
        public string code { get; set; }
    }
}

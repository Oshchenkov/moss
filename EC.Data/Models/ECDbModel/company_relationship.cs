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
    
    public partial class company_relationship
    {
        public int id { get; set; }
        public int company_id { get; set; }
        public int client_id { get; set; }
        public int status_id { get; set; }
        public string relationship_en { get; set; }
        public string relationship_fr { get; set; }
        public string relationship_es { get; set; }
        public string relationship_ru { get; set; }
        public string relationship_ar { get; set; }
        public string relationship_jp { get; set; }
    }
}

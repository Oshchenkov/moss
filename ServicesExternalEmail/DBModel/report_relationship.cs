//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServicesExternalEmail.DBModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class report_relationship
    {
        public int id { get; set; }
        public int report_id { get; set; }
        public Nullable<int> relationship_id { get; set; }
        public Nullable<int> company_relationship_id { get; set; }
        public string relationship_nm { get; set; }
        public Nullable<System.DateTime> last_update_dt { get; set; }
        public Nullable<int> user_id { get; set; }
    }
}
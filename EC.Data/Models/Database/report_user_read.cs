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
    
    public partial class report_user_read
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public int report_id { get; set; }
        public System.DateTime read_date { get; set; }
    }
}

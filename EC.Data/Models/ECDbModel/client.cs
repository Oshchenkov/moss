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
    
    public partial class client
    {
        public int id { get; set; }
        public int address_id { get; set; }
        public int status_id { get; set; }
        public string client_nm { get; set; }
        public string client_ds { get; set; }
        public System.DateTime registration_dt { get; set; }
        public string notepad_tx { get; set; }
        public System.DateTime last_update_dt { get; set; }
        public int user_id { get; set; }
    }
}

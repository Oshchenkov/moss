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
    
    public partial class poster
    {
        public int id { get; set; }
        public string poster_name { get; set; }
        public string poster_img { get; set; }
        public int poster_message_posters_id { get; set; }
        public string image_name { get; set; }
        public string image_path { get; set; }
        public int status { get; set; }
        public int company_id { get; set; }
    }
}

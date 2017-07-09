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
    
    public partial class company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public company()
        {
            this.company_department = new HashSet<company_department>();
        }
    
        public int id { get; set; }
        public int client_id { get; set; }
        public int address_id { get; set; }
        public int billing_info_id { get; set; }
        public int status_id { get; set; }
        public string company_nm { get; set; }
        public string company_code { get; set; }
        public System.DateTime registration_dt { get; set; }
        public string contact_nm { get; set; }
        public int language_id { get; set; }
        public string employee_quantity { get; set; }
        public int implicated_title_name_id { get; set; }
        public int witness_show_id { get; set; }
        public int show_location_id { get; set; }
        public int show_department_id { get; set; }
        public int default_anonymity_id { get; set; }
        public string notepad_en { get; set; }
        public string notepad_fr { get; set; }
        public string notepad_es { get; set; }
        public string notepad_ru { get; set; }
        public string notepad_ar { get; set; }
        public string path_en { get; set; }
        public string path_fr { get; set; }
        public string path_es { get; set; }
        public string path_ru { get; set; }
        public string path_ar { get; set; }
        public string alert_en { get; set; }
        public string alert_fr { get; set; }
        public string alert_es { get; set; }
        public string alert_ru { get; set; }
        public string alert_ar { get; set; }
        public System.DateTime last_update_dt { get; set; }
        public int user_id { get; set; }
        public int time_zone_id { get; set; }
        public int step1_delay { get; set; }
        public int step1_postpone { get; set; }
        public int step2_delay { get; set; }
        public int step2_postpone { get; set; }
        public int step3_delay { get; set; }
        public int step3_postpone { get; set; }
        public int step4_delay { get; set; }
        public int step4_postpone { get; set; }
        public int step5_delay { get; set; }
        public int step5_postpone { get; set; }
        public int step6_delay { get; set; }
        public int step6_postpone { get; set; }
        public string subdomain { get; set; }
        public Nullable<int> reseller_id { get; set; }
        public Nullable<int> company_invitation_id { get; set; }
        public Nullable<System.DateTime> invitation_confirmation_dt { get; set; }
        public Nullable<int> invitation_confirmation_user_id { get; set; }
        public Nullable<int> reseller { get; set; }
        public Nullable<System.Guid> guid { get; set; }
        public Nullable<System.DateTime> next_payment_date { get; set; }
        public Nullable<decimal> next_payment_amount { get; set; }
    
        public virtual address address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<company_department> company_department { get; set; }
    }
}

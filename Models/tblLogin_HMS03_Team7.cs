//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SandMax1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblLogin_HMS03_Team7
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblLogin_HMS03_Team7()
        {
            this.tblCustomer_HMS03_Team7 = new HashSet<tblCustomer_HMS03_Team7>();
            this.tblEmployee_HMS03_Team7 = new HashSet<tblEmployee_HMS03_Team7>();
        }
    
        public int LoginID { get; set; }
        public string uname { get; set; }
        public string pwd { get; set; }
        public string roles { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblCustomer_HMS03_Team7> tblCustomer_HMS03_Team7 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblEmployee_HMS03_Team7> tblEmployee_HMS03_Team7 { get; set; }
    }
}

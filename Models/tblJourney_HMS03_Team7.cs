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
    
    public partial class tblJourney_HMS03_Team7
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblJourney_HMS03_Team7()
        {
            this.tblBonusMilesRequest_HMS03_Team7 = new HashSet<tblBonusMilesRequest_HMS03_Team7>();
            this.tblPassenger_HMS03_Team7 = new HashSet<tblPassenger_HMS03_Team7>();
            this.tblPayment_HMS03_Team7 = new HashSet<tblPayment_HMS03_Team7>();
        }
    
        public int JourneyID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> ScheduleID { get; set; }
        public Nullable<decimal> TotalBookingFare { get; set; }
        public Nullable<int> NoOfPassengers_Adult { get; set; }
        public Nullable<int> NoOfPassengers_Child { get; set; }
        public string PaymentStatus { get; set; }
        public string BonusRequstStatus { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public Nullable<decimal> AdditionalBaggageCharge { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblBonusMilesRequest_HMS03_Team7> tblBonusMilesRequest_HMS03_Team7 { get; set; }
        public virtual tblCustomer_HMS03_Team7 tblCustomer_HMS03_Team7 { get; set; }
        public virtual tblSchedule_HMS03_Team7 tblSchedule_HMS03_Team7 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPassenger_HMS03_Team7> tblPassenger_HMS03_Team7 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPayment_HMS03_Team7> tblPayment_HMS03_Team7 { get; set; }
    }
}

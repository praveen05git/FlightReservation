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
    
    public partial class tblPayment_HMS03_Team7
    {
        public int TransactionID { get; set; }
        public Nullable<int> JourneyID { get; set; }
        public string CardType { get; set; }
        public Nullable<long> CardNumber { get; set; }
        public Nullable<int> CVV { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> ScheduleID { get; set; }
    
        public virtual tblCustomer_HMS03_Team7 tblCustomer_HMS03_Team7 { get; set; }
        public virtual tblJourney_HMS03_Team7 tblJourney_HMS03_Team7 { get; set; }
        public virtual tblSchedule_HMS03_Team7 tblSchedule_HMS03_Team7 { get; set; }
    }
}

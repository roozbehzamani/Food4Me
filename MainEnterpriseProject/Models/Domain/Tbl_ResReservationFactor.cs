//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MainEnterpriseProject.Models.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_ResReservationFactor
    {
        public int ID { get; set; }
        public int ResID { get; set; }
        public string UserEmail { get; set; }
        public int TableID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}

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
    
    public partial class Tbl_FlowerReservationFactorList
    {
        public int ID { get; set; }
        public int FoodID { get; set; }
        public int FoodFactorID { get; set; }
        public int FoodCount { get; set; }
    
        public virtual Tbl_Flower Tbl_Flower { get; set; }
        public virtual Tbl_FlowerReservationFactor Tbl_FlowerReservationFactor { get; set; }
    }
}

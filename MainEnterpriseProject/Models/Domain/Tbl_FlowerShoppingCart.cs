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
    
    public partial class Tbl_FlowerShoppingCart
    {
        public int ID { get; set; }
        public int FlowerCount { get; set; }
        public int FlowerID { get; set; }
        public string UserEmail { get; set; }
    
        public virtual Tbl_Flower Tbl_Flower { get; set; }
    }
}

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
    
    public partial class Tbl_FlowerComment
    {
        public int ID { get; set; }
        public int flowerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string IP { get; set; }
        public string Text { get; set; }
        public System.DateTime commentDate { get; set; }
        public bool confirm { get; set; }
        public bool read { get; set; }
        public string Phone { get; set; }
        public int Stars { get; set; }
    
        public virtual Tbl_Flower Tbl_Flower { get; set; }
    }
}

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
    
    public partial class Tbl_Restaurant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_Restaurant()
        {
            this.tab_products = new HashSet<tab_products>();
            this.Tbl_OrderFactor = new HashSet<Tbl_OrderFactor>();
        }
    
        public int ID { get; set; }
        public string resName { get; set; }
        public string resAddress { get; set; }
        public string resType { get; set; }
        public string resAvgServiceTime { get; set; }
        public double resPoints { get; set; }
        public string resLatLng { get; set; }
        public string resPhone { get; set; }
        public string resImage { get; set; }
        public string userEmail { get; set; }
        public bool resEnable { get; set; }
        public bool StudentRes { get; set; }
        public string ResBusinessLicense { get; set; }
        public int FoodAvragePrice { get; set; }
        public bool isGetOrder { get; set; }
        public string FirstTime { get; set; }
        public string SecendTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tab_products> tab_products { get; set; }
        public virtual Tab_users Tab_users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_OrderFactor> Tbl_OrderFactor { get; set; }
    }
}

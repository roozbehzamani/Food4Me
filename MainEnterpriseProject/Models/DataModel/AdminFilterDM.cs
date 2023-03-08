using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class AdminFilterDM
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public int CountFrom { get; set; }
        public int CountTo { get; set; }
        public string FoodName { get; set; }
        public int ResID { get; set; }
    }
}
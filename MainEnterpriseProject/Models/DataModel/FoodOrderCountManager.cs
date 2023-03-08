using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class FoodOrderCountManager
    {
        public string foodName { get; set; }
        public int orderCount { get; set; }
        public int TotalOrderCount { get; set; }
        public string CurrentCount { get; set; }
        public string Date { get; set; }
    }
}
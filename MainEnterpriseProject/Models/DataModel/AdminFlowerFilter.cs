using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class AdminFlowerFilter
    {
        public int ID { get; set; }
        public int ResID { get; set; }
        public string FoodName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string StartPrice { get; set; }
        public string EndPrice { get; set; }
        public string StartCount { get; set; }
        public string EndCount { get; set; }
        public List<int> lstType { get; set; }
    }
}
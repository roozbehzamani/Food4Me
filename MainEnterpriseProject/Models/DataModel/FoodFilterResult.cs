using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class FoodFilterResult
    {
        public int ID { get; set; }
        public string FoodName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Price { get; set; }
        public string Count { get; set; }
        public int Type { get; set; }
    }
}
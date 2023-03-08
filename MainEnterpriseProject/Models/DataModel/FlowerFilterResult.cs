using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class FlowerFilterResult
    {
        public int ID { get; set; }
        public string FoodName { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Price { get; set; }
        public string Count { get; set; }
        public int Type { get; set; }
    }
}
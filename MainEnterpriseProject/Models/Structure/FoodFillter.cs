using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Structure
{
    public class FoodFillter
    {
        public int id { get; set; }
        public int cost { get; set; }
        public string name { get; set; }
        public string foodImage { get; set; }
        public string restuarant { get; set; }
        public string menuName { get; set; }
        public int FoodCount { get; set; }
        public bool OrderType { get; set; }
        public string resID { get; set; }
    }
}
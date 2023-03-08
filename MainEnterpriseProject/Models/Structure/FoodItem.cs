using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Structure
{
    public class FoodItem
    {
        public int ID { get; set; }
        public int foodID { get; set; }
        public string foodItem { get; set; }
        public int itemPrice { get; set; }
        public string groupName { get; set; }
        public Boolean Used { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class FoodOption
    {
        public int ID { get; set; }
        public string foodItem { get; set; }
        public int itemPrice { get; set; }
        public int itemGroupID { get; set; }
        public bool Used { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class ShoppingCartResult
    {
        public int FoodCount { get; set; }
        public string FoodName { get; set; }
        public string UserEmail { get; set; }
        public int FoodTotalPrice { get; set; }
        public int ID { get; set; }
    }
}
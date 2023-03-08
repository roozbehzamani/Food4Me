using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Structure
{
    public class Shopping_Cart
    {
        public int Total_Price { get; set; }
        public List<Tab_Cart> lst_ShoppingCart { get; set; }
        public int count { get; set; }
    }
    public class Tab_Cart
    {
        public int ID { get; set; }
        public string Product_Name { get; set; }
        public string Count { get; set; }
        public string Price { get; set; }
        public string Single_Price { get; set; }
    }
}
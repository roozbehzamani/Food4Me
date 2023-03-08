using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Structure
{
    public class MySweetReserved
    {
        public int ID { get; set; }
        public int totalPrice { get; set; }
        public string sweetName { get; set; }
        public string orderType { get; set; }
        public string orderDate { get; set; }
        public string orderTime { get; set; }
        public string sweetImage { get; set; }
        public string sweetWeight { get; set; }
    }
}
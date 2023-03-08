using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Structure
{
    public class MyFlowerReserved
    {
        public int ID { get; set; }
        public int totalPrice { get; set; }
        public string floristName { get; set; }
        public string orderType { get; set; }
        public string orderDate { get; set; }
        public string orderTime { get; set; }
        public string floristImage { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Structure
{
    public class MyResReserved
    {
        public int ID { get; set; }
        public int totalPrice { get; set; }
        public string resName { get; set; }
        public string orderTable { get; set; }
        public string orderDate { get; set; }
        public string orderTime { get; set; }
        public string resImage { get; set; }
    }
}
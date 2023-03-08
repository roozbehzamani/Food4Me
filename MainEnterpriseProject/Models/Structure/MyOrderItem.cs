using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Structure
{
    public class MyOrderItem
    {
        public int ID { get; set; }
        public int OrderCount { get; set; }
        public string OrderName { get; set; }
        public string OrderImage { get; set; }

    }
}
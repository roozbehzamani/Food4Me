using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class OrderItems
    {
        public int Counter { get; set; }
        public string Name { get; set; }
        public string OrderDate { get; set; }
        public string OrderTime { get; set; }
        public string OrderPrice { get; set; }
        public string SNumber { get; set; }
    }
}
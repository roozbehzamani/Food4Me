using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Structure
{
    public class MyOrder
    {
        public int ID { get; set; }
        public int OrderPrice { get; set; }
        public string resName { get; set; }
        public string Status { get; set; }
        public string OrderDate { get; set; }
        public string OrderTime { get; set; }
        public string resImage { get; set; }
    }
}
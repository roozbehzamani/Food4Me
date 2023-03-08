using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class FactorItem
    {
        public int factorID { get; set; }
        public List<int> foodCount { get; set; }
        public List<int> foodID { get; set; }
    }
}
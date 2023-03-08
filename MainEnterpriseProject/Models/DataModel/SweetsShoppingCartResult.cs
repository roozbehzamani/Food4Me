using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class SweetsShoppingCartResult
    {
        public int SweetsCount { get; set; }

        public string SweetsName { get; set; }

        public string UserEmail { get; set; }

        public int SweetsTotalPrice { get; set; }

        public int ID { get; set; }
    }
}
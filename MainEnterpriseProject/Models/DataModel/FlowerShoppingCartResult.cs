using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class FlowerShoppingCartResult
    {
        public int FlowerCount { get; set; }

        public string FlowerName { get; set; }

        public string UserEmail { get; set; }

        public int FlowerTotalPrice { get; set; }

        public int ID { get; set; }
    }
}
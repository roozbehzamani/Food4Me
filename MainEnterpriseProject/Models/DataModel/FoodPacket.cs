using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class FoodPacket
    {
        public int ID { get; set; }
        public string packingDescription { get; set; }
        public string packingImage { get; set; }
        public List<int> lstFoodIDs { get; set; }
    }
}
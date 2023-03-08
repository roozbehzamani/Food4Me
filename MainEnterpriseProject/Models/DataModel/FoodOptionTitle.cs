using MainEnterpriseProject.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models
{
    public class FoodOptionTitle
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
        public List<FoodOption> lstFoodOption { get; set; }
    }
}
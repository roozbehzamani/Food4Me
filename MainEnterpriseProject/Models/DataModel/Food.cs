using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class Food
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MenuID { get; set; }
        public string Price { get; set; }
        public string Image { get; set; }
        public string Recepi { get; set; }
        public string CreateMaterial { get; set; }
        public string BakingTime { get; set; }
        public string UserEmail { get; set; }
        public int ResID { get; set; }
        public int FoodCount { get; set; }
        public bool ordertype { get; set; }
    }
}
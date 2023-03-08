using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class AddFlower
    {
        public int ID { get; set; }
        public string FlwName { get; set; }
        public string FlwPrice { get; set; }
        public string FlwType { get; set; }
        public string FlwImage { get; set; }
        public string FlwMaintenance { get; set; }
        public int FlorestID { get; set; }

    }
}
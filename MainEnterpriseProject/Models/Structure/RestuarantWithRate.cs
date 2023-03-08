using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Structure
{
    public class RestuarantWithRate
    {
        public int ID { get; set; }
        public string resName { get; set; }
        public string resAddress { get; set; }
        public string resType { get; set; }
        public string resDiscription { get; set; }
        public string resWorkTime { get; set; }
        public string resAvgServiceTime { get; set; }
        public int resPoints { get; set; }
        public bool resEconomical { get; set; }
        public bool resDiscount { get; set; }
        public bool resSuggestion { get; set; }
        public string resLatLng { get; set; }
        public string resPhone { get; set; }
        public string resImage { get; set; }
        public string userEmail { get; set; }
        public float Rate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class AddSweets
    {
        public int ID { get; set; }
        public string S_Name { get; set; }
        public string S_Type { get; set; }
        public string S_Price { get; set; }
        public string S_Description { get; set; }
        public string SImage { get; set; }
        public int ConfectionaryID { get; set; }
    }
}
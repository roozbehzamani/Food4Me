using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class FoodComment
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string text { get; set; }
        public string data { get; set; }
        public bool confirm { get; set; }
        public bool read { get; set; }
        public int cm_like { get; set; }
        public int cm_dislike { get; set; }
        public string phone { get; set; }
        public int Stars { get; set; }
    }
}
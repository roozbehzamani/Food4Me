using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Structure
{
    public class user
    {
        public string name { get; set; }
        public string email { get; set; }
        public int age { get; set; }
        public string[] topics { get; set; }
    }
}
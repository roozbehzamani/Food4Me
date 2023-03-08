using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class UserWithCredit
    {
        public int ID { get; set; }
        public string UserEmail { get; set; }
        public string Name { get; set; }
        public string SNumber { get; set; }
        public string Phone { get; set; }
        public int Credit { get; set; }
    }
}
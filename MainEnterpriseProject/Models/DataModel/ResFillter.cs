using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class ResFillter
    {
        public int ID { get; set; }
        public string resName { get; set; }
        public string resAddress { get; set; }
        public string resType { get; set; }
        public double resPoints { get; set; }
        public string resImage { get; set; }
        public int ResCommentCount { get; set; }
    }
}
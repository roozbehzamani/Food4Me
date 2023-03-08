using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Structure
{
    public class ResCommentItemList
    {
        public int ID { get; set; }
        public int FoodId { get; set; }
        public int Star { get; set; }
        public string Comment { get; set; }
    }
}
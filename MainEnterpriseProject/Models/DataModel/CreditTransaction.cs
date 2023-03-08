using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.DataModel
{
    public class CreditTransaction
    {
        public int ID { get; set; }
        public string TransactionCode { get; set; }
        public string Price { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public bool CreditType { get; set; }
    }
}
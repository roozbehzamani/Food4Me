using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class OrderRepository
    {
        DataBase db = new DataBase();

        public List<Tbl_OrderFactorItem> lstOrderFactorItems(int factorID)
        {
            List<Tbl_OrderFactorItem> q = (from a in db.Tbl_OrderFactorItem
                                           where a.FactorID.Equals(factorID)
                                           select a).ToList();

            return q;
        }

        public int TotalfactorPrice (int FactorID)
        {
            int sum = 0;
            List<Tbl_OrderFactorItem> q = (from a in db.Tbl_OrderFactorItem
                                           where a.FactorID.Equals(FactorID)
                                           select a).ToList();

            sum = q.Sum(i => i.tab_products.cost * i.FoodCount);
            return sum;
        }
        public string ResName(int id)
        {
            string q = (from a in db.Tbl_Restaurant
                        where a.ID.Equals(id)
                        select a).SingleOrDefault().resName;

            return q;
        }
    }
}
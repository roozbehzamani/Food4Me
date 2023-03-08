using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class PublicRepository
    {
        DataBase db = new DataBase();

        public int TotaldailyIncome()
        {
            DateTime NowDate = DateTime.Now.Date;
            int sum = 0;
            
            List<Tbl_OrderFactor> qFoodFactor = (from a in db.Tbl_OrderFactor
                                                 where a.Date.Equals(NowDate)
                                                 select a).ToList();

            List<Tbl_FlowerOrder> qFlowerFactor = (from a in db.Tbl_FlowerOrder
                                                   where a.Date.Equals(NowDate)
                                                   select a).ToList();

            List<Tbl_SweetOrderFactor> qSweetFactor = (from a in db.Tbl_SweetOrderFactor
                                                       where a.Date.Equals(NowDate)
                                                       select a).ToList();

            foreach (var item1 in qFoodFactor)
            {
                List<Tbl_OrderFactorItem> qFoodFactorItem = (from a in db.Tbl_OrderFactorItem
                                                             where a.FactorID.Equals(item1.ID)
                                                             select a).ToList();

                foreach (var item in qFoodFactorItem)
                {
                    sum += item.tab_products.cost;
                }
            }

            foreach (var item2 in qFlowerFactor)
            {
                List<Tbl_FlowerOrderItem> qFlowerFactorItem = (from a in db.Tbl_FlowerOrderItem
                                                               where a.ParentID.Equals(item2.ID)
                                                               select a).ToList();

                foreach (var item in qFlowerFactorItem)
                {
                    sum += Convert.ToInt32(item.Tbl_Flower.FlwPrice);
                }
            }

            foreach (var item1 in qSweetFactor)
            {
                List<Tbl_SweetOrderFactorItem> qSweetFactorItem = (from a in db.Tbl_SweetOrderFactorItem
                                                                  where a.FactorID.Equals(item1.ID)
                                                                  select a).ToList();

                foreach (var item in qSweetFactorItem)
                {
                    sum += Convert.ToInt32(item.Tbl_Sweets.S_Price);
                }
            }

            return sum;
        }
    }
}
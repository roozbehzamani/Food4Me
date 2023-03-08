using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class FlowerRepository
    {
        DataBase db = new DataBase();

        public List<Tbl_FlowerReservationFactorList> GetFlowerReserv(int id)
        {
            List<Tbl_FlowerReservationFactorList> q = (from a in db.Tbl_FlowerReservationFactorList
                                                       where a.FoodFactorID.Equals(id)
                                                     select a).ToList();

            return q;
        }

        public int GetFlowerOrderCount()
        {
            List<Tbl_FlowerOrder> q = (from a in db.Tbl_FlowerOrder
                                       select a).ToList();

            return q.Count();
        }
    }
}
using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class SweetsRepository
    {
        DataBase db = new DataBase();

        public List<Tbl_SweetReservationFactorList> GetSweetsReserv(int id)
        {
            List<Tbl_SweetReservationFactorList> q = (from a in db.Tbl_SweetReservationFactorList
                                                      where a.FoodFactorID.Equals(id)
                                                       select a).ToList();

            return q;
        }

        public int GetSweetOrderCount()
        {
            List<Tbl_SweetOrderFactor> q = (from a in db.Tbl_SweetOrderFactor
                                            select a).ToList();

            return q.Count();
        }
    }
}
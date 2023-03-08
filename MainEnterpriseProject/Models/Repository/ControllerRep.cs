using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class ControllerRep
    {
        DataBase db = new DataBase();

        public int SingleUserCredit(string email)
        {
            var q = (from a in db.Tbl_Credit
                     where a.UserEmail.Equals(email)
                     select a).ToList();

            return q.Sum(i => i.Credit);
        }
    }
}
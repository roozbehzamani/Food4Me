using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class UserRepository
    {
        DataBase db = new DataBase();

        public Tab_users GetSingleUserInfo(string userEmail)
        {
            Tab_users q = (from a in db.Tab_users
                           where a.email.Equals(userEmail)
                           select a).SingleOrDefault();

            return q;
        }

        public int StudentCount()
        {
            var q = (from a in db.Tab_users
                     where a.access.Equals("دانشجو")
                     select a).ToList();

            return q.Count();
        }

        public int DailyCredit(DateTime dt)
        {
            string MyDateStrig = dt.ToString("yyyy-MM-dd 00:00:00.000");
            var q = (from a in db.Tbl_Credit
                     where a.RootUser.Equals("U")
                     select a).ToList();

            List<Tbl_Credit> lstTblCredit = new List<Tbl_Credit>();

            foreach (var item in q)
            {
                Tbl_Credit tCredit = new Tbl_Credit();
                string date = item.Date.ToString("yyyy-MM-dd 00:00:00.000");
                if (MyDateStrig.Equals(date))
                {
                    tCredit.Credit = item.Credit;
                    tCredit.Date = item.Date;
                    tCredit.ID = item.ID;
                    tCredit.RootUser = item.RootUser;
                    tCredit.Time = item.Time;
                    tCredit.TransactionCode = item.TransactionCode;
                    tCredit.UserEmail = item.UserEmail;

                    lstTblCredit.Add(tCredit);

                }
            }


            if(lstTblCredit.Count() <= 0)
            {
                return 0;
            }
            else
            {
                return lstTblCredit.Sum(i => i.Credit);
            }

            
        }

        public int Date(DateTime dt)
        {
            int sum = 0;
            string MyDateStrig = dt.ToString("yyyy-MM-dd 00:00:00.000");

            var q = (from a in db.Tbl_OrderFactor
                     where a.Tab_users.access.Equals("دانشجو") && a.Credit.HasValue && a.Credit.Value.Equals(true)
                     select a).ToList();

            List<Tbl_OrderFactor> lstFactor = new List<Tbl_OrderFactor>();

            foreach(var item in q)
            {
                string date = item.Date.ToString("yyyy-MM-dd 00:00:00.000");
                Tbl_OrderFactor t = new Tbl_OrderFactor();

                if (MyDateStrig.Equals(date))
                {
                    t.AddressID = item.AddressID;
                    t.Comment = item.Comment;
                    t.Credit = item.Credit;
                    t.Date = item.Date;
                    t.ID = item.ID;
                    t.OrderStatus = item.OrderStatus;
                    t.ResID = item.ResID;
                    t.Time = item.Time;
                    t.UserEmail = item.UserEmail;

                    lstFactor.Add(t);
                }


            }

            if (lstFactor.Count() <= 0)
            {
                return 0;
            }
            else
            {
                foreach (var item in lstFactor)
                {
                    var qList = (from a in db.Tbl_OrderFactorItem
                                 where a.FactorID.Equals(item.ID)
                                 select a).ToList();

                    sum += qList.Sum(i => i.tab_products.cost * i.FoodCount);
                }
                return sum;
            }
        }

        public int DailyTotalSales(DateTime dt)
        {
            int sum = 0;
            string MyDateStrig = dt.ToString("yyyy-MM-dd 00:00:00.000");

            var q = (from a in db.Tbl_OrderFactor
                     select a).ToList();

            List<Tbl_OrderFactor> lstFactor = new List<Tbl_OrderFactor>();

            foreach (var item in q)
            {
                string date = item.Date.ToString("yyyy-MM-dd 00:00:00.000");
                Tbl_OrderFactor t = new Tbl_OrderFactor();

                if (MyDateStrig.Equals(date))
                {
                    t.AddressID = item.AddressID;
                    t.Comment = item.Comment;
                    t.Credit = item.Credit;
                    t.Date = item.Date;
                    t.ID = item.ID;
                    t.OrderStatus = item.OrderStatus;
                    t.ResID = item.ResID;
                    t.Time = item.Time;
                    t.UserEmail = item.UserEmail;
                    t.Delivery = item.Delivery;
                    t.CreditAmount = item.CreditAmount;

                    lstFactor.Add(t);
                }


            }

            if (lstFactor.Count() <= 0)
            {
                return 0;
            }
            else
            {
                foreach (var item in lstFactor)
                {
                    var qList = (from a in db.Tbl_OrderFactorItem
                                 where a.FactorID.Equals(item.ID)
                                 select a).ToList();

                    sum += qList.Sum(i => i.tab_products.cost * i.FoodCount);
                }
                return sum;
            }
        }
        public int DailyTotalDelivery(DateTime dt)
        {
            int sum = 0;
            string MyDateStrig = dt.ToString("yyyy-MM-dd 00:00:00.000");

            var q = (from a in db.Tbl_OrderFactor
                     where a.Delivery.HasValue && a.Delivery.Value.Equals(true)
                     select a).ToList();

            List<Tbl_OrderFactor> lstFactor = new List<Tbl_OrderFactor>();

            foreach (var item in q)
            {
                string date = item.Date.ToString("yyyy-MM-dd 00:00:00.000");
                Tbl_OrderFactor t = new Tbl_OrderFactor();

                if (MyDateStrig.Equals(date))
                {
                    t.AddressID = item.AddressID;
                    t.Comment = item.Comment;
                    t.Credit = item.Credit;
                    t.Date = item.Date;
                    t.ID = item.ID;
                    t.OrderStatus = item.OrderStatus;
                    t.ResID = item.ResID;
                    t.Time = item.Time;
                    t.UserEmail = item.UserEmail;
                    t.Delivery = item.Delivery;
                    t.CreditAmount = item.CreditAmount;

                    lstFactor.Add(t);
                }


            }

            if (lstFactor.Count() <= 0)
            {
                return 0;
            }
            else
            {
                sum = lstFactor.Count * 3000;
                return sum;
            }
        }
        public int DateCount()
        {
            var q = (from a in db.Tbl_OrderFactor
                     select a).OrderBy(i => i.Date).ToList();

            DateTime firstDate = q.First().Date;
            DateTime endDate = DateTime.Now;

            double DateCount = (endDate - firstDate).TotalDays;
            int CountDate = Convert.ToInt32(DateCount);

            return CountDate;
        }

        public List<int> lstResIDs()
        {
            var q = (from a in db.Tbl_OrderFactor
                     select a).ToList();

            List<int> lstFirstResIDs = new List<int>();
            List<int> lstSecendResIDs = new List<int>();

            foreach (var item in q)
            {
                lstFirstResIDs.Add(item.ResID);
            }

            lstSecendResIDs = lstFirstResIDs.Distinct().ToList();
            return lstSecendResIDs;
        }

        public string SingleResName(int id)
        {
            var q = (from a in db.Tbl_Restaurant
                     where a.ID.Equals(id)
                     select a).SingleOrDefault();

            return q.resName;
        }

        public int singleResSales(int id , int position)
        {
            var q = (from a in db.Tbl_OrderFactor
                     select a).OrderBy(i => i.Date).ToList();

            int sum = 0;

            DateTime firstDate = q.First().Date;
            DateTime dt = firstDate.AddDays(position);

            var qResOrders = (from a in db.Tbl_OrderFactor
                              where a.Date.Equals(dt) && a.ResID.Equals(id)
                              select a).ToList();

            foreach (var item in qResOrders)
            {
                var qItems = (from a in db.Tbl_OrderFactorItem
                              where a.FactorID.Equals(item.ID)
                              select a).ToList();

                sum += qItems.Sum(i => i.tab_products.cost * i.FoodCount);
            }

            return sum;
        }
    }
}
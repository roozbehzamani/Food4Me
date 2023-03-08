using MainEnterpriseProject.Models.DataModel;
using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class WalletRep
    {
        DataBase db = new DataBase();

        public int ShowCredit(string email)
        {
            var q = (from a in db.Tbl_Credit
                     where a.UserEmail.Equals(email)
                     select a).ToList();

            if (q.Count() <= 0)
            {
                return 0;
            }
            else
            {
                return q.Sum(i => i.Credit);
            }
        }
        public int ShowCreditWithTime(string email)
        {
            string dt = DateTime.Now.ToString("HH:mm");
            TimeSpan currentTime = TimeSpan.Parse(dt);
            TimeSpan startTime = TimeSpan.Parse("10:00");
            TimeSpan endTime = TimeSpan.Parse("20:00");

            if (currentTime >= startTime && currentTime <= endTime)
            {
                var q = (from a in db.Tbl_Credit
                         where a.UserEmail.Equals(email)
                         select a).ToList();

                if (q.Count() <= 0)
                {
                    return 0;
                }
                else
                {
                    return q.Sum(i => i.Credit);
                }
            }
            else
            {
                var q = (from a in db.Tbl_Credit
                         where a.UserEmail.Equals(email) && a.RootUser.Equals("M")
                         select a).ToList();

                if (q.Count() <= 0)
                {
                    return 0;
                }
                else
                {
                    return q.Sum(i => i.Credit);
                }
            }
        }
        public int ShowCreditOnlyM(string email)
        {
            var q = (from a in db.Tbl_Credit
                     where a.UserEmail.Equals(email) && a.RootUser.Equals("M")
                     select a).ToList();

            if (q.Count() <= 0)
            {
                return 0;
            }
            else
            {
                return q.Sum(i => i.Credit);
            }
        }
        public int TotalPrice(string userEmail)
        {
            var q = (from a in db.Tbl_ShoppingCart
                     where a.UserEmail.Equals(userEmail)
                     select a).ToList();

            if (q.Count() <= 0)
            {
                return 0;
            }
            else
            {
                return q.Sum(i => i.tab_products.cost * i.FoodCount);
            }
        }
        public List<Tbl_OptionCart> ShappingCartOptionList(int id)
        {
            List<Tbl_OptionCart> q = (from a in db.Tbl_OptionCart
                                      where a.ShoppingCartID.Equals(id)
                                      select a).ToList();

            return q;
        }
        public UseOrUnUseCredit CreditManager(bool isCredit , bool inTime , string email)
        {
            string dt = DateTime.Now.ToString("HH:mm");
            TimeSpan currentTime = TimeSpan.Parse(dt);
            TimeSpan startTime = TimeSpan.Parse("10:00");
            TimeSpan endTime = TimeSpan.Parse("20:00");
            UseOrUnUseCredit useCredit;

            if (currentTime >= startTime  && currentTime <= endTime)
            {
                if (inTime)
                {
                    if (isCredit)
                    {
                        int credit = ShowCreditOnlyM(email);
                        int totalprice = 3000;

                        var q = (from a in db.Tbl_ShoppingCart
                                 where a.UserEmail.Equals(email)
                                 select a).ToList();

                        useCredit = new UseOrUnUseCredit();

                        totalprice += q.Sum(i => i.FoodCount * i.tab_products.cost);
                        if (totalprice > credit)
                        {
                            useCredit.credit = 0;
                            useCredit.price = (totalprice - credit);
                        }
                        else if (totalprice == credit)
                        {
                            useCredit.credit = 0;
                            useCredit.price = 0;
                        }
                        else if (totalprice < credit)
                        {
                            useCredit.credit = (credit - totalprice);
                            useCredit.price = 0;
                        }
                    }
                    else
                    {
                        int credit = ShowCreditOnlyM(email);
                        int totalprice = 3000;

                        var q = (from a in db.Tbl_ShoppingCart
                                 where a.UserEmail.Equals(email)
                                 select a).ToList();

                        useCredit = new UseOrUnUseCredit();

                        totalprice += q.Sum(i => i.FoodCount * i.tab_products.cost);
                        useCredit.credit = credit;
                        useCredit.price = totalprice;
                    }
                }
                else
                {
                    if (isCredit)
                    {
                        int credit = ShowCreditWithTime(email);
                        int totalprice = 0;

                        var q = (from a in db.Tbl_ShoppingCart
                                 where a.UserEmail.Equals(email)
                                 select a).ToList();

                        useCredit = new UseOrUnUseCredit();

                        totalprice += q.Sum(i => i.FoodCount * i.tab_products.cost);
                        if (totalprice > credit)
                        {
                            useCredit.credit = 0;
                            useCredit.price = (totalprice - credit);
                        }
                        else if (totalprice == credit)
                        {
                            useCredit.credit = 0;
                            useCredit.price = 0;
                        }
                        else if (totalprice < credit)
                        {
                            useCredit.credit = (credit - totalprice);
                            useCredit.price = 0;
                        }
                    }
                    else
                    {
                        int credit = ShowCreditWithTime(email);
                        int totalprice = 0;

                        var q = (from a in db.Tbl_ShoppingCart
                                 where a.UserEmail.Equals(email)
                                 select a).ToList();

                        useCredit = new UseOrUnUseCredit();

                        totalprice += q.Sum(i => i.FoodCount * i.tab_products.cost);
                        useCredit.credit = credit;
                        useCredit.price = totalprice;
                    }
                }
            }
            else
            {
                if (isCredit)
                {
                    int credit = ShowCreditOnlyM(email);
                    int totalprice = 3000;

                    var q = (from a in db.Tbl_ShoppingCart
                             where a.UserEmail.Equals(email)
                             select a).ToList();

                    useCredit = new UseOrUnUseCredit();

                    totalprice += q.Sum(i => i.FoodCount * i.tab_products.cost);
                    if (totalprice > credit)
                    {
                        useCredit.credit = 0;
                        useCredit.price = (totalprice - credit);
                    }
                    else if (totalprice == credit)
                    {
                        useCredit.credit = 0;
                        useCredit.price = 0;
                    }
                    else if (totalprice < credit)
                    {
                        useCredit.credit = (credit - totalprice);
                        useCredit.price = 0;
                    }
                }
                else
                {
                    int credit = ShowCreditOnlyM(email);
                    int totalprice = 3000;

                    var q = (from a in db.Tbl_ShoppingCart
                             where a.UserEmail.Equals(email)
                             select a).ToList();

                    useCredit = new UseOrUnUseCredit();

                    totalprice += q.Sum(i => i.FoodCount * i.tab_products.cost);
                    useCredit.credit = credit;
                    useCredit.price = totalprice;
                }
            }
            return useCredit;
        }
        public int TimeToOrder()
        {
            string dt = DateTime.Now.ToString("HH:mm");
            TimeSpan currentTime = TimeSpan.Parse(dt);
            TimeSpan endTime = TimeSpan.Parse("23:50");
            TimeSpan startTime = TimeSpan.Parse("01:00");
            if (currentTime >= startTime && currentTime < endTime)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int ResGetOrderStatus(int id)
        {
            string dt = DateTime.Now.ToString("HH:mm");
            TimeSpan currentTime = TimeSpan.Parse(dt);

            var q = (from a in db.Tbl_Restaurant
                     where a.ID.Equals(id)
                     select a).SingleOrDefault();

            if (q.FirstTime.Split('-')[1].Equals(q.SecendTime.Split('-')[0]))
            {
                TimeSpan FirstStart = TimeSpan.Parse(q.FirstTime.Split('-')[0]);
                TimeSpan FirstEnd = TimeSpan.Parse(q.SecendTime.Split('-')[1]);

                if(currentTime >= FirstStart && currentTime <= FirstEnd)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                TimeSpan FirstStart = TimeSpan.Parse(q.FirstTime.Split('-')[0]);
                TimeSpan FirstEnd = TimeSpan.Parse(q.FirstTime.Split('-')[1]);
                TimeSpan SecendStart = TimeSpan.Parse(q.SecendTime.Split('-')[0]);
                TimeSpan SecendEnd = TimeSpan.Parse(q.SecendTime.Split('-')[1]);

                if (currentTime >= FirstStart && currentTime <= FirstEnd)
                {
                    return 1;
                }
                else
                {
                    if (currentTime >= SecendStart && currentTime <= SecendEnd)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
    }
}
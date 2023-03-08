using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class SweetsAllRep
    {
        DataBase db = new DataBase();

        public List<Tbl_Confectionary> lstAllConfectionery()
        {
            List<Tbl_Confectionary> q = (from a in db.Tbl_Confectionary
                                         select a).ToList();

            return q;
        }

        public double SweetsAverageRate(int ID)
        {
            int counter = 0;
            int sum = 0;
            double average;

            var q = (from a in db.Tbl_SweetsComment
                     select a).ToList();

            foreach (var item in q)
            {
                var qConfectionery = (from a in db.Tbl_Sweets
                                      where a.ID.Equals(item.SweetsID)
                                      select a).SingleOrDefault();

                if (qConfectionery.ConfectionaryID.Equals(ID))
                {
                    sum += item.Star;
                    counter++;
                }
            }
            if (counter != 0)
            {
                average = (double)sum / counter;
                string s = average.ToString();
                if (s.Length >= 4)
                {
                    s = s.Substring(0, 4);
                }
                average = Convert.ToDouble(s);
            }
            else
                average = 0;
            return average;
        }
        public int ConfectioneryCommentCount(int ID)
        {
            var q = (from a in db.Tbl_SweetsComment
                     select a).ToList();

            List<int> lstCommentsID = new List<int>();

            foreach (var item in q)
            {
                var qRes = (from a in db.Tbl_Sweets
                            where a.ID.Equals(item.SweetsID)
                            select a).SingleOrDefault();
                if (qRes.ConfectionaryID.Equals(ID))
                    lstCommentsID.Add(item.ID);

            }
            return lstCommentsID.Count();
        }


        public Tbl_Sweets GetDeatilSweets(int ID)
        {

            try
            {

                Tbl_Sweets q = (from a in db.Tbl_Sweets
                                where a.ID.Equals(ID)
                                select a).SingleOrDefault();

                return q;

            }
            catch
            {

                return null;
            }




        }


        public double SweetsAverageRateSingle(int ID)
        {
            int counter = 0;
            int sum = 0;
            double average;

            var q = (from a in db.Tbl_SweetsComment
                     where a.SweetsID.Equals(ID)
                     select a).ToList();

            foreach (var item in q)
            {

                sum += item.Star;
                counter++;

            }


            if (counter != 0)
            {
                average = (double)sum / counter;
                string s = average.ToString();
                if(s.Length >= 4)
                {
                    s = s.Substring(0, 4);
                }
                average = Convert.ToDouble(s);
            }
            else

                average = 0;


            return average;
        }


        public Tbl_Confectionary ConfectioneryInfo(int id)
        {
            Tbl_Confectionary q = (from a in db.Tbl_Confectionary
                                   where a.ID.Equals(id)
                                   select a).SingleOrDefault();

            return q;
        }

        public int SweetsCommentCountSingle(int ID)
        {
            var q = (from a in db.Tbl_FlowerComment
                     where a.flowerID.Equals(ID)
                     select a).ToList();


            return q.Count();
        }

        public List<Tbl_SweetsComment> lstAllComment(int ID)
        {
            List<Tbl_SweetsComment> q = (from a in db.Tbl_SweetsComment
                                         where a.SweetsID.Equals(ID)
                                         select a).ToList();

            return q;
        }
        public List<Tbl_Sweets> LstSweets(int id)
        {
            List<Tbl_Sweets> q = (from a in db.Tbl_Sweets
                                  where a.ConfectionaryID.Equals(id)
                                  select a).ToList();

            return q;
        }

        public Tbl_Confectionary ConfectionerytInfo(int id)
        {
            Tbl_Confectionary q = (from a in db.Tbl_Confectionary
                                   where a.ID.Equals(id)
                                   select a).SingleOrDefault();

            return q;
        }

        public int getCountSweets()
        {
            var q = (from a in db.Tbl_Confectionary
                     select a).ToList();

            return q.Count();
        }
        public List<Tbl_ConfectioneryUserImage> lstAllGallery(int ID)
        {
            List<Tbl_ConfectioneryUserImage> q = (from a in db.Tbl_ConfectioneryUserImage
                                                  where a.ConfectioneryID.Equals(ID)
                                                  select a).ToList();

            return q;
        }
        public Tab_users GetSingleUserInfo(string userEmail)
        {
            Tab_users q = (from a in db.Tab_users
                           where a.email.Equals(userEmail)
                           select a).SingleOrDefault();

            return q;
        }
    }
}
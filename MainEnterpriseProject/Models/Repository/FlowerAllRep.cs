using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class FlowerAllRep
    {
   
        DataBase db = new DataBase();




        public List<Tbl_Floriest> lstAllFlorist()
        {
            List<Tbl_Floriest> q = (from a in db.Tbl_Floriest
                                    select a).ToList();

            return q;
        }

        public double FlowerAverageRate(int ID)
        {
            int counter = 0;
            int sum = 0;
            double average;

            var q = (from a in db.Tbl_FlowerComment
                     select a).ToList();

            foreach (var item in q)
            {
                var qFlorist = (from a in db.Tbl_Flower
                                      where a.ID.Equals(item.flowerID)
                                      select a).SingleOrDefault();

                if (qFlorist.FlorestID.Equals(ID))
                {
                    sum += item.Stars;
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
        public int FloristCommentCount(int ID)
        {
            var q = (from a in db.Tbl_FlowerComment
                     select a).ToList();

            List<int> lstCommentsID = new List<int>();

            foreach (var item in q)
            {
                var qRes = (from a in db.Tbl_Flower
                            where a.ID.Equals(item.flowerID)
                            select a).SingleOrDefault();
                if (qRes.FlorestID.Equals(ID))
                    lstCommentsID.Add(item.ID);

            }
            return lstCommentsID.Count();
        }


        public Tbl_Flower GetDeatilFlower(int ID)
        {

            try
            {

                Tbl_Flower q = (from a in db.Tbl_Flower
                         where a.ID.Equals(ID)
                         select a).SingleOrDefault();
                
                return q;

            }
            catch
            {

                return null;
            }




        }

        public double FlowerAverageRateSingle(int ID)
        {
            int counter = 0;
            int sum = 0;
            double average;

            var q = (from a in db.Tbl_FlowerComment
                     where a.flowerID.Equals(ID)
                     select a).ToList();

            foreach (var item in q)
            {
              
                    sum += item.Stars;
                    counter++;
              
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




        public Tbl_Floriest FlowerInfo(int id)
        {
            Tbl_Floriest q = (from a in db.Tbl_Floriest
                                   where a.ID.Equals(id)
                                   select a).SingleOrDefault();

            return q;
        }

        public int FlowerCommentCountSingle(int ID)
        {
            var q = (from a in db.Tbl_FlowerComment
                     where a.flowerID.Equals(ID)
                     select a).ToList();


            return q.Count();
        }

        public List<Tbl_FlowerComment> lstAllComment(int ID)
        {
            List<Tbl_FlowerComment> q = (from a in db.Tbl_FlowerComment
                                         where a.flowerID .Equals(ID)
                                         select a).ToList();

            return q;
        }

        public Tab_users getImageUserCom(String Email)
        {
                            Tab_users q = (from a in db.Tab_users 
                                         where a.email.Equals(Email)
                                         select a).SingleOrDefault();

            return q;
        }

        public List<Tbl_Flower> LstFlower(int id)
        {
            List<Tbl_Flower> q = (from a in db.Tbl_Flower
                                  where a.FlorestID.Equals(id)
                                  select a).ToList();

            return q;
        }

        public Tbl_Floriest FloristInfo(int id)
        {
            Tbl_Floriest q = (from a in db.Tbl_Floriest
                              where a.ID.Equals(id)
                              select a).SingleOrDefault();

            return q;
        }

        public int getCountFlorist()
        {
            var q = (from a in db.Tbl_Floriest
                     select a).ToList();

            return q.Count();
        }
        public List<Tbl_FloristUserImage> lstAllGallery(int ID)
        {
            List<Tbl_FloristUserImage> q = (from a in db.Tbl_FloristUserImage
                                            where a.FloristID.Equals(ID)
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
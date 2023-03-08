using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class Rep_resturant
    {

        DataBase db = new DataBase();
        public List<Tbl_Restaurant> getResturant()
        {
            try
            {
                List<Tbl_Restaurant> qgetResturant = (from a in db.Tbl_Restaurant
                                                      select a).ToList();



                return qgetResturant;
            }
            catch
            {
                return null;
            }
        }
        public List<Tbl_RestaurantImage> GetResImage(int id)
        {
            List<Tbl_RestaurantImage> qGetResImage = (from a in db.Tbl_RestaurantImage
                                                      where a.resID.Equals(id)
                                                      select a).ToList();
            return qGetResImage;
        }
        public int commentCount(int ID)
        {
            int counter = 0;
            var q = (from a in db.tab_products
                     where a.resID.Equals(ID)
                     select a).ToList();
            foreach (var item in q)
            {
                var w = (from a in db.Tab_comments
                         where a.product_id.Equals(item.id)
                         select a).Count();
                counter += w;
            }
            return counter;
        }
        public double resAverageRate(int ID)
        {
            int counter = 0;
            int sum = 0;
            double average;

            var q = (from a in db.Tab_comments
                     select a).ToList();

            foreach (var item in q)
            {
                var qRes = (from a in db.tab_products
                            where a.id.Equals(item.product_id)
                            select a).SingleOrDefault();

                if (qRes.resID.Equals(ID))
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

        public int resCommentCount(int ID)
        {
            var q = (from a in db.Tab_comments
                     select a).ToList();

            List<int> lstCommentsID = new List<int>();

            foreach (var item in q)
            {
                var qRes = (from a in db.tab_products
                            where a.id.Equals(item.product_id)
                            select a).SingleOrDefault();
                if (qRes.resID.Equals(ID))
                    lstCommentsID.Add(item.id);

            }
            return lstCommentsID.Count();
        }
        public List<Tbl_Restaurant> getOnlyRestuarant()
        {
            try
            {
                List<Tbl_Restaurant> qgetResturant = (from a in db.Tbl_Restaurant
                                                      where a.resType.Equals("رستوران")
                                                      select a).ToList();



                return qgetResturant;
            }
            catch
            {
                return null;
            }
        }
        public List<Tbl_Restaurant> getOnlyFastFood()
        {
            try
            {
                List<Tbl_Restaurant> qgetResturant = (from a in db.Tbl_Restaurant
                                                      where a.resType.Equals("فست فود")
                                                      select a).ToList();



                return qgetResturant;
            }
            catch
            {
                return null;
            }
        }
        public List<Tbl_Restaurant> getOnlyCaffi()
        {
            try
            {
                List<Tbl_Restaurant> qgetResturant = (from a in db.Tbl_Restaurant
                                                      where a.resType.Equals("کافی شاپ")
                                                      select a).ToList();



                return qgetResturant;
            }
            catch
            {
                return null;
            }
        }
        //public List<RestuarantWithRate> sortByRate()
        //{

        //    int counter = 0;
        //    int sum = 0;
        //    float average;
        //    List<RestuarantWithRate> lstRWR = new List<RestuarantWithRate>();
        //    RestuarantWithRate rwr = new RestuarantWithRate();
        //    var e = (from a in db.Tbl_Restaurant
        //             select a).ToList();

        //    foreach (var item2 in e)
        //    {
        //        var q = (from a in db.tab_products
        //                 where a.resID.Equals(item2.ID)
        //                 select a).ToList();
        //        foreach (var item in q)
        //        {
        //            var w = (from a in db.Tab_comments
        //                     where a.product_id.Equals(item.id)
        //                     select a).ToList();
        //            foreach (var item1 in w)
        //            {
        //                counter += 1;
        //                sum += item1.Stars;
        //            }
        //        }
        //        if (counter != 0)
        //        {
        //            average = (sum / counter);
        //            rwr.ID = item2.ID;
        //            rwr.Rate = average;
        //            rwr.resAddress = item2.resAddress;
        //            rwr.resAvgServiceTime = item2.resAvgServiceTime;
        //            rwr.resDiscount = item2.resDiscount;
        //            rwr.resDiscription = item2.resDiscription;
        //            rwr.resEconomical = item2.resEconomical;
        //            rwr.resImage = item2.resImage;
        //            rwr.resLatLng = item2.resLatLng;
        //            rwr.resName = item2.resName;
        //            rwr.resPhone = item2.resPhone;
        //            rwr.resPoints = item2.resPoints;
        //            rwr.resSuggestion = item2.resSuggestion;
        //            rwr.resType = item2.resType;
        //            rwr.resWorkTime = item2.resWorkTime;
        //            rwr.userEmail = item2.userEmail;
        //            lstRWR.Add(rwr);
        //        }

        //        else
        //        {
        //            average = 0;
        //            rwr.ID = item2.ID;
        //            rwr.Rate = average;
        //            rwr.resAddress = item2.resAddress;
        //            rwr.resAvgServiceTime = item2.resAvgServiceTime;
        //            rwr.resDiscount = item2.resDiscount;
        //            rwr.resDiscription = item2.resDiscription;
        //            rwr.resEconomical = item2.resEconomical;
        //            rwr.resImage = item2.resImage;
        //            rwr.resLatLng = item2.resLatLng;
        //            rwr.resName = item2.resName;
        //            rwr.resPhone = item2.resPhone;
        //            rwr.resPoints = item2.resPoints;
        //            rwr.resSuggestion = item2.resSuggestion;
        //            rwr.resType = item2.resType;
        //            rwr.resWorkTime = item2.resWorkTime;
        //            rwr.userEmail = item2.userEmail;
        //            lstRWR.Add(rwr);
        //        }

        //    }
        //    var allResSorted = (from a in lstRWR
        //                        select a).OrderByDescending(s => s.Rate).ToList();
        //    return allResSorted;

        //}
        public List<Tbl_Restaurant> sortByBestselling()
        {
            try
            {
                List<Tbl_Restaurant> qgetResturant = (from a in db.Tbl_Restaurant
                                                      where a.resType.Equals("کافی شاپ")
                                                      select a).ToList();



                return qgetResturant;
            }
            catch
            {
                return null;
            }
        }
        public List<Tbl_Restaurant> sortByLowestSelling()
        {
            try
            {
                List<Tbl_Restaurant> qgetResturant = (from a in db.Tbl_Restaurant
                                                      where a.resType.Equals("کافی شاپ")
                                                      select a).ToList();



                return qgetResturant;
            }
            catch
            {
                return null;
            }
        }
        public List<Tbl_Restaurant> sortByMostExpensive()
        {
            try
            {
                List<Tbl_Restaurant> qgetResturant = (from a in db.Tbl_Restaurant
                                                      where a.resType.Equals("کافی شاپ")
                                                      select a).ToList();



                return qgetResturant;
            }
            catch
            {
                return null;
            }
        }
        public List<Tbl_Restaurant> sortByLowestExpensive()
        {
            try
            {
                List<Tbl_Restaurant> qgetResturant = (from a in db.Tbl_Restaurant
                                                      where a.resType.Equals("کافی شاپ")
                                                      select a).ToList();



                return qgetResturant;
            }
            catch
            {
                return null;
            }
        }
        public tab_products GetdeatilFood(int id)
        {

            try
            {

                tab_products q = (from a in db.tab_products
                                  where a.id.Equals(id)
                                  select a).SingleOrDefault();
                return q;

            }
            catch
            {

                return null;
            }




        }


        public int FoodAverageRateSingle(int ID)
        {
            int counter = 0;
            int sum = 0;
            int average;

            var q = (from a in db.Tab_comments
                     where a.product_id.Equals(ID)
                     select a).ToList();

            foreach (var item in q)
            {

                sum += item.Stars;
                counter++;

            }


            if (counter != 0)
            {
                average = sum / counter;
                
            }
            else
                average = 0;

            return average;
        }
        public Tbl_Restaurant ResturantInfo(int id)
        {
            Tbl_Restaurant q = (from a in db.Tbl_Restaurant
                                where a.ID.Equals(id)
                                select a).SingleOrDefault();

            return q;
        }
        public Tbl_Restaurant SingleResInfo(string email) {

            Tbl_Restaurant q = (from a in db.Tbl_Restaurant
                                where a.userEmail.Equals(email)
                                select a).SingleOrDefault();

            return q;
        }
        public List<Tbl_FoodAlbume> Decoration(int id)
        {
            List<Tbl_FoodAlbume> q = (from a in db.Tbl_FoodAlbume
                                      where a.foodID.Equals(id)
                                      select a).ToList();

            return q;
        }
        public List<Tbl_Packing> packing(int id)
        {
            List<Tbl_Packing> q = (from a in db.Tbl_Packing
                                   where a.foodID.Equals(id)
                                   select a).ToList();

            return q;
        }
        public List<Tbl_FoodItemGroup> OptionTitle(int foodID)
        {
            var q = (from a in db.Tbl_FoodOption
                     where a.FoodID.Equals(foodID)
                     select a).ToList();


            List<Tbl_FoodItemGroup> lstOptionTitles = new List<Tbl_FoodItemGroup>();

            foreach (var item in q)
            {
                var qOption = (from a in db.Tbl_CustomizationFood
                               where a.ID.Equals(item.OptionID)
                               select a).SingleOrDefault();

                Tbl_FoodItemGroup fig = new Tbl_FoodItemGroup();

                fig = qOption.Tbl_FoodItemGroup;

                bool flag = true;
                if (lstOptionTitles.Count() == 0)
                {
                    lstOptionTitles.Add(fig);
                }
                else
                {
                    foreach (var itemTest in lstOptionTitles)
                    {
                        if (itemTest.ID.Equals(fig.ID))
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                        lstOptionTitles.Add(fig);
                }
            }
            return lstOptionTitles;
        }
        public List<Tbl_CustomizationFood> lstFoodOption(int foodID, int titleID)
        {
            var q = (from a in db.Tbl_FoodOption
                     where a.FoodID.Equals(foodID)
                     select a).ToList();
            List<Tbl_CustomizationFood> lstOptin = new List<Tbl_CustomizationFood>();
            foreach (var item in q)
            {
                var qFoodOption = (from a in db.Tbl_CustomizationFood
                                   where a.ID.Equals(item.OptionID) && a.itemGroupID.Equals(titleID)
                                   select a).SingleOrDefault();
                if (qFoodOption != null)
                    lstOptin.Add(qFoodOption);
            }

            return lstOptin;
        }



        public int resCommentCountSingle(int ID)
        {
            var q = (from a in db.Tab_comments
                     where a.product_id.Equals(ID)
                     select a).ToList();


            return q.Count();
        }

        public List<Tab_comments> lstAllComment(int ID)
        {
            List<Tab_comments> q = (from a in db.Tab_comments
                                    where a.product_id.Equals(ID)
                                    select a).ToList();

            return q;
        }

        public int getCountRes()
        {
            var q = (from a in db.Tbl_Restaurant
                     where a.resType.Equals("رستوران")
                     select a).ToList();

            return q.Count();
        }
        public int getCountFast()
        {
            var q = (from a in db.Tbl_Restaurant
                     where a.resType.Equals("فست فود")
                     select a).ToList();

            return q.Count();
        }
        public int getCountCafe()
        {
            var q = (from a in db.Tbl_Restaurant
                     where a.resType.Equals("کافی شاپ")
                     select a).ToList();

            return q.Count();
        }
        public List<Tbl_UserImages> lstAllGallery(int ID)
        {
            List<Tbl_UserImages> q = (from a in db.Tbl_UserImages
                                      where a.ResID.Equals(ID)
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
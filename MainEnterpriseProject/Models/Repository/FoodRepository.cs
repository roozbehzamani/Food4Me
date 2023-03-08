using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class FoodRepository
    {
        DataBase db = new DataBase();

        public Tbl_Menu GetSingleMenu(int id)
        {
            Tbl_Menu q = (from a in db.Tbl_Menu
                          where a.menuID.Equals(id)
                          select a).SingleOrDefault();

            return q;
        }
        public List<Tbl_Packing> GetPacking(int id)
        {
            List<Tbl_Packing> q = (from a in db.Tbl_Packing
                                   where a.foodID.Equals(id)
                                   select a).ToList();

            return q;
        }
        public List<Tbl_FoodAlbume> GetAlbum(int id)
        {
            List<Tbl_FoodAlbume> q = (from a in db.Tbl_FoodAlbume
                                      where a.foodID.Equals(id)
                                      select a).ToList();

            return q;
        }
        public tab_products GetSingleFoodName(int id)
        {
            tab_products q = (from a in db.tab_products
                              where a.id.Equals(id)
                              select a).SingleOrDefault();

            return q;
        }
        public List<Tbl_ResReservationFactorItem> GetRestaurantReserv(int id)
        {
            List<Tbl_ResReservationFactorItem> q = (from a in db.Tbl_ResReservationFactorItem
                                                    where a.FactorID.Equals(id)
                                                    select a).ToList();

            return q;
        }
        public Tbl_ResTables GetSingleTableInfo(int id)
        {
            Tbl_ResTables q = (from a in db.Tbl_ResTables
                               where a.ID.Equals(id)
                               select a).SingleOrDefault();

            return q;
        }

        public List<Tbl_FoodReservationFactorList> GetFoodReserv(int id)
        {
            List<Tbl_FoodReservationFactorList> q = (from a in db.Tbl_FoodReservationFactorList
                                                     where a.FoodFactorID.Equals(id)
                                                     select a).ToList();

            return q;
        }
        public List<tab_products> GetResFoodlist(string Email)
        {
            int id = (from a in db.Tbl_Restaurant
                      where a.userEmail.Equals(Email)
                      select a).SingleOrDefault().ID;

            List<tab_products> q = (from a in db.tab_products
                                    where a.resID.Equals(id)
                                    select a).ToList();

            return q;
        }
        public tab_products GetResFood(int ID)
        {

            tab_products q = (from a in db.tab_products
                              where a.id.Equals(ID)
                              select a).SingleOrDefault();

            return q;
        }

        public int GetFoodOrderCount()
        {
            List<Tbl_OrderFactor> q = (from a in db.Tbl_OrderFactor
                                       select a).ToList();

            return q.Count();
        }
        public List<tab_products> getResfoodList(string userEmail)
        {


            var q = (from a in db.Tbl_Restaurant
                     where a.userEmail.Equals(userEmail)
                     select a).Single();

            List<tab_products> qList = (from a in db.tab_products
                                        where a.resID.Equals(q.ID)
                                        select a).ToList();

            return qList;
        }

        public int Satisfied(int FoodID)
        {
            int totalCommentCount = 0;

            var qTotalComment = (from a in db.Tab_comments
                                 where a.product_id.Equals(FoodID)
                                 select a).ToList();

            totalCommentCount = qTotalComment.Count();

            int totalSatisfiedCommentCount = 0;

            var qTotalSatisfiedComment = (from a in db.Tab_comments
                                          where a.product_id.Equals(FoodID) && a.Stars >= 3
                                          select a).ToList();

            totalSatisfiedCommentCount = qTotalSatisfiedComment.Count();

            if(totalCommentCount == 0)
            {
                return 0;
            }
            else
            {
                int per = (totalSatisfiedCommentCount * 100) / (totalCommentCount);
                return per;
            }
        }

        public int NonSatisfied(int FoodID)
        {
            int totalCommentCount = 0;

            var qTotalComment = (from a in db.Tab_comments
                                 where a.product_id.Equals(FoodID)
                                 select a).ToList();

            totalCommentCount = qTotalComment.Count();

            int totalSatisfiedCommentCount = 0;

            var qTotalSatisfiedComment = (from a in db.Tab_comments
                                          where a.product_id.Equals(FoodID) && a.Stars < 3
                                          select a).ToList();

            totalSatisfiedCommentCount = qTotalSatisfiedComment.Count();

            if (totalCommentCount == 0)
            {
                return 0;
            }
            else
            {
                int per = (totalSatisfiedCommentCount * 100) / (totalCommentCount);
                return per;
            }
        }
    }
}
using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class Rep_Food
    {
        DataBase db = new DataBase();
        public List<tab_products> getResFoodList(int ID)
        {
            try
            {
                List<tab_products> qgetFood = (from a in db.tab_products
                                                    where a.resID.Equals(ID)
                                                      select a).ToList();



                return qgetFood;
            }
            catch
            {
                return null;
            }
        }
        public List<Tbl_Menu> getResMenuList()
        {
            try
            {
                List<Tbl_Menu> qgeResMenuList = (from a in db.Tbl_Menu
                                               select a).ToList();



                return qgeResMenuList;
            }
            catch
            {
                return null;
            }
        }
    }
}
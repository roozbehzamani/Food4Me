using MainEnterpriseProject.Models.Domain;
using MainEnterpriseProject.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CaptchaMvc;
using CaptchaMvc.HtmlHelpers;
using MainEnterpriseProject.Models.Structure;
using MainEnterpriseProject.Models.Utility;
using Newtonsoft.Json;
using System.Web.Helpers;
using System.Globalization;
using System.Net;
using System.Drawing;
using MainEnterpriseProject.Models.DataModel;
using MainEnterpriseProject.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;

namespace MainEnterpriseProject.Controllers
{
    public class HomeController : Controller
    {
        Rep_resturant Rresturant = new Rep_resturant();
        SweetsAllRep SweetsRep = new SweetsAllRep();
        FlowerAllRep FlowerRep = new FlowerAllRep();
        WalletRep wr = new WalletRep();

        DataBase db = new DataBase();
        AllUtility u = new AllUtility();
        public JsonResult ResFilter(string filterType)
        {
            if (filterType.Equals("بیشترین امتیاز"))
            {
                var q = (from a in db.Tbl_Restaurant
                         select a).OrderByDescending(s => s.resPoints).ToList();

                List<ResFillter> resFillterList = new List<ResFillter>();

                foreach (var item in q)
                {
                    ResFillter res = new ResFillter();

                    res.ID = item.ID;
                    res.resAddress = item.resAddress;
                    res.ResCommentCount = Rresturant.resCommentCount(item.ID);
                    res.resImage = item.resImage;
                    res.resName = item.resName;
                    res.resPoints = item.resPoints;
                    res.resType = item.resType;

                    resFillterList.Add(res);
                }

                return Json(resFillterList, JsonRequestBehavior.AllowGet);
            }
            else if (filterType.Equals("گرانترین"))
            {
                var q = (from a in db.Tbl_Restaurant
                         select a).OrderByDescending(s => s.FoodAvragePrice).ToList();

                List<ResFillter> resFillterList = new List<ResFillter>();

                foreach (var item in q)
                {
                    ResFillter res = new ResFillter();

                    res.ID = item.ID;
                    res.resAddress = item.resAddress;
                    res.ResCommentCount = Rresturant.resCommentCount(item.ID);
                    res.resImage = item.resImage;
                    res.resName = item.resName;
                    res.resPoints = item.resPoints;
                    res.resType = item.resType;

                    resFillterList.Add(res);
                }

                return Json(resFillterList, JsonRequestBehavior.AllowGet);
            }
            else if (filterType.Equals("ارزان ترین"))
            {
                var q = (from a in db.Tbl_Restaurant
                         select a).OrderBy(s => s.FoodAvragePrice).ToList();

                List<ResFillter> resFillterList = new List<ResFillter>();

                foreach (var item in q)
                {
                    ResFillter res = new ResFillter();

                    res.ID = item.ID;
                    res.resAddress = item.resAddress;
                    res.ResCommentCount = Rresturant.resCommentCount(item.ID);
                    res.resImage = item.resImage;
                    res.resName = item.resName;
                    res.resPoints = item.resPoints;
                    res.resType = item.resType;

                    resFillterList.Add(res);
                }

                return Json(resFillterList, JsonRequestBehavior.AllowGet);
            }
            else if (filterType.Equals("رستوران"))
            {
                var q = (from a in db.Tbl_Restaurant
                         where a.resType.Equals("رستوران")
                         select a).ToList();

                List<ResFillter> resFillterList = new List<ResFillter>();

                foreach (var item in q)
                {
                    ResFillter res = new ResFillter();

                    res.ID = item.ID;
                    res.resAddress = item.resAddress;
                    res.ResCommentCount = Rresturant.resCommentCount(item.ID);
                    res.resImage = item.resImage;
                    res.resName = item.resName;
                    res.resPoints = item.resPoints;
                    res.resType = item.resType;

                    resFillterList.Add(res);
                }

                return Json(resFillterList, JsonRequestBehavior.AllowGet);
            }
            else if (filterType.Equals("فست فود"))
            {
                var q = (from a in db.Tbl_Restaurant
                         where a.resType.Equals("فست فود")
                         select a).ToList();

                List<ResFillter> resFillterList = new List<ResFillter>();

                foreach (var item in q)
                {
                    ResFillter res = new ResFillter();

                    res.ID = item.ID;
                    res.resAddress = item.resAddress;
                    res.ResCommentCount = Rresturant.resCommentCount(item.ID);
                    res.resImage = item.resImage;
                    res.resName = item.resName;
                    res.resPoints = item.resPoints;
                    res.resType = item.resType;

                    resFillterList.Add(res);
                }

                return Json(resFillterList, JsonRequestBehavior.AllowGet);
            }
            else if (filterType.Equals("کافی شاپ"))
            {
                var q = (from a in db.Tbl_Restaurant
                         where a.resType.Equals("کافی شاپ")
                         select a).ToList();

                List<ResFillter> resFillterList = new List<ResFillter>();

                foreach (var item in q)
                {
                    ResFillter res = new ResFillter();

                    res.ID = item.ID;
                    res.resAddress = item.resAddress;
                    res.ResCommentCount = Rresturant.resCommentCount(item.ID);
                    res.resImage = item.resImage;
                    res.resName = item.resName;
                    res.resPoints = item.resPoints;
                    res.resType = item.resType;

                    resFillterList.Add(res);
                }

                return Json(resFillterList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var q = (from a in db.Tbl_Restaurant
                         select a).ToList();

                List<ResFillter> resFillterList = new List<ResFillter>();

                foreach (var item in q)
                {
                    ResFillter res = new ResFillter();

                    res.ID = item.ID;
                    res.resAddress = item.resAddress;
                    res.ResCommentCount = Rresturant.resCommentCount(item.ID);
                    res.resImage = item.resImage;
                    res.resName = item.resName;
                    res.resPoints = item.resPoints;
                    res.resType = item.resType;

                    resFillterList.Add(res);
                }

                return Json(resFillterList, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult Menu(int ID)
        {

            var q = (from a in db.Tbl_Restaurant
                     where a.ID.Equals(ID)
                     select a).SingleOrDefault();
            return View(q);
        }
        [HttpPost]
        public ActionResult Menu(Tbl_UserImages tUserImages, string SaveChange, HttpPostedFileBase file)
        {
            string u;
            if (Session["email"] != null)
            {
                u = Session["Email"].ToString();
                tUserImages.UserEmail = u;
            }



            if (file != null)
            {

                if (file.ContentType == "image/jpeg")
                {
                    if (file.ContentLength <= 81920)
                    {
                        Random rnd = new Random();
                        string rndname = "UserImage_" + rnd.Next(1, 100000).ToString() + ".jpg";
                        string path = Path.Combine(Server.MapPath("~/Content/images/"));
                        file.SaveAs(path + rndname);
                        tUserImages.ImageName = rndname;
                    }
                    else
                    {
                        return Content("سایز زیاد است");
                    }
                }
                else
                {
                    return Content("فرمت درست نیس");
                }
            }
            else
            {
                return Content("عکس موجود نیس");
            }
            db.Tbl_UserImages.Add(tUserImages);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                ViewBag.MenugalleryResult = "S";
                var q = (from a in db.Tbl_Restaurant
                         where a.ID.Equals(tUserImages.ResID)
                         select a).SingleOrDefault();
                return View(q);
            }
            else
            {
                ViewBag.MenugalleryResult = "F";
                var q = (from a in db.Tbl_Restaurant
                         where a.ID.Equals(tUserImages.ResID)
                         select a).SingleOrDefault();
                return View(q);
            }
        }
        public JsonResult UseCredit(bool Time, bool Credit)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"] == null)
                return Json(1, JsonRequestBehavior.AllowGet);

            string email = Session["email"].ToString();

            UseOrUnUseCredit useCredit = new UseOrUnUseCredit();
            useCredit = wr.CreditManager(Credit, Time, email);

            return Json(useCredit, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UnUseCredit(string ID)
        {
            if (Session["email"] == null)
                return Json(0, JsonRequestBehavior.AllowGet);
            if (Session["access"] == null)
                return Json(1, JsonRequestBehavior.AllowGet);

            string email = Session["email"].ToString();

            int credit;
            int totalprice;

            if(TimeToUniCreditOrder() == 1)
            {
                credit = wr.ShowCredit(email);
                totalprice = 0;
            }
            else
            {
                credit = wr.ShowCreditOnlyM(email);
                totalprice = 3000;
            }

            var q = (from a in db.Tbl_ShoppingCart
                     where a.UserEmail.Equals(email)
                     select a).ToList();

            UseOrUnUseCredit useCredit = new UseOrUnUseCredit();

            totalprice += q.Sum(i => i.FoodCount * i.tab_products.cost);
            useCredit.credit = credit;
            useCredit.price = totalprice;

            return Json(useCredit, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ActiveLogin(string DialFifth, string DialFourth, string DialThird, string DialSecond, string DialFirst)
        {
            if (Session["myEmail"] != null)
            {
                int x1 = Convert.ToInt32(DialFifth);
                int x2 = Convert.ToInt32(DialFourth);
                int x3 = Convert.ToInt32(DialThird);
                int x4 = Convert.ToInt32(DialSecond);
                int x5 = Convert.ToInt32(DialFirst);

                int x = (x5 * 10000) + (x4 * 1000) + (x3 * 100) + (x2 * 10) + (x1);
                string u = Session["myEmail"].ToString();

                var q = (from a in db.Tab_users
                         where a.email.Equals(u)
                         select a).SingleOrDefault();

                if (q.activeCode.Equals(x.ToString()))
                {
                    q.SMS_Enable = true;
                    db.Tab_users.Attach(q);
                    db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return Json("با موفقیت فعال شد", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("خطا", JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    return Json("خطا", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("خطا", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Dup_Email(string email)
        {
            int x = email.IndexOf('@');
            if (x == -1)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var q = (from a in db.Tab_users
                         where a.email.Equals(email)
                         select a).SingleOrDefault();

                if (q != null)
                {
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(2, JsonRequestBehavior.AllowGet);
                }
            }

        }
        public JsonResult Dup_Phone(string phone)
        {
            var q = (from a in db.Tab_users
                     where a.mob_phone.Equals(phone)
                     select a).SingleOrDefault();

            if (q != null)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }
        public int SaveShoppingCart(string InTimeStatus , string CreditChecked, string Address, string Comment)
        {
            int pprice = 0;
            if (CreditChecked == null)
            {
                CreditChecked = "false";
            }
            if (InTimeStatus == null)
            {
                InTimeStatus = "false";
            }
            string email = Session["email"].ToString();
            var q = (from a in db.Tbl_ShoppingCart
                     where a.UserEmail.Equals(email)
                     select a).FirstOrDefault();

            int resID = q.tab_products.resID;
            Tbl_Address tAddress = new Tbl_Address();

            tAddress.Address = Address;
            tAddress.addressLatLng = "0,0";
            tAddress.addressType = "سایت";
            tAddress.userEmail = email;

            db.Tbl_Address.Add(tAddress);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                Tbl_OrderFactor tFactor = new Tbl_OrderFactor();

                tFactor.AddressID = tAddress.ID;
                tFactor.Comment = Comment;
                if (CreditChecked.Equals("true"))
                {
                    tFactor.Credit = true;
                }
                else
                {
                    tFactor.Credit = false;
                }
                if(TimeToUniCreditOrder() == 1)
                {
                    if (InTimeStatus.Equals("true"))
                    {
                        tFactor.ReceveTime = "درلحظه";
                        tFactor.Delivery = true;
                    }
                    else
                    {
                        tFactor.ReceveTime = "شام";
                        tFactor.Delivery = false;
                    }
                }
                else
                {
                    tFactor.ReceveTime = "درلحظه";
                    tFactor.Delivery = true;
                }
                tFactor.Date = DateTime.Now;
                tFactor.OrderStatus = "درحال بررسی";
                tFactor.ResID = resID;
                tFactor.Time = DateTime.Now.TimeOfDay;
                tFactor.UserEmail = email;
                tFactor.CreditAmount = "0";
                db.Tbl_OrderFactor.Add(tFactor);
                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    var resEmail = (from a in db.Tbl_Restaurant
                                    where a.ID.Equals(resID)
                                    select a).SingleOrDefault();
                    u.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", email, "فاکتور سفارش", "سفارش شما توسط " + resEmail.resName + " دریافت گردید" );
                    u.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", "zamani.roozbeh.75@gmail.com", "سفارش جدید", "شما یک سفارش جدید دارید");
                    u.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", resEmail.userEmail, "سفارش جدید", "شما یک سفارش جدید دارید");
                    var qListShoppingCart = (from a in db.Tbl_ShoppingCart
                                             where a.UserEmail.Equals(email)
                                             select a).ToList();

                    foreach (var foodItem in qListShoppingCart)
                    {
                        if (foodItem.FoodCount < foodItem.tab_products.FoodCount && foodItem.tab_products.FoodCount == -1)
                        {
                            return 0;
                        }
                    }

                    List<Tbl_OrderFactorItem> lstItems = new List<Tbl_OrderFactorItem>();

                    foreach (var item in qListShoppingCart)
                    {
                        Tbl_OrderFactorItem tItems = new Tbl_OrderFactorItem();

                        tItems.FactorID = tFactor.ID;
                        tItems.FoodCount = item.FoodCount;
                        tItems.FoodID = item.FoodID;

                        lstItems.Add(tItems);
                    }

                    db.Tbl_OrderFactorItem.AddRange(lstItems);
                    db.SaveChanges();

                    foreach (var foodItem in qListShoppingCart)
                    {
                        if (foodItem.tab_products.FoodCount != -1)
                        {
                            var qSelectedFood = (from a in db.tab_products
                                                 where a.id.Equals(foodItem.FoodID)
                                                 select a).SingleOrDefault();

                            qSelectedFood.FoodCount = qSelectedFood.FoodCount - foodItem.FoodCount;
                            db.tab_products.Attach(qSelectedFood);
                            db.Entry(qSelectedFood).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    if (CreditChecked.Equals("true"))
                    {
                        int cred;
                        if (InTimeStatus.Equals("true"))
                        {
                            cred = wr.ShowCreditOnlyM(email);
                        }
                        else
                        {
                            cred = wr.ShowCreditWithTime(email);
                        }
                        int totalPrice = 0;
                        if (TimeToUniCreditOrder() == 1)
                        {
                            if (InTimeStatus.Equals("true"))
                            {
                                totalPrice = wr.TotalPrice(email) + 3000;
                            }
                            else
                            {
                                totalPrice = wr.TotalPrice(email);
                            }
                        }
                        else
                        {
                            totalPrice = wr.TotalPrice(email) + 3000;
                        }
                        if (cred >= totalPrice)
                        {
                            int x = 0 - totalPrice;

                            int result;
                            if (InTimeStatus.Equals("true"))
                            {
                                result = AddNewCredite(email, x, "0", "درلحظه");
                            }
                            else
                            {
                                result = AddNewCredite(email, x, "0", "شام");
                            }
                            if (result == 1)
                            {
                                var qEditFactor = (from a in db.Tbl_OrderFactor
                                                   where a.ID.Equals(tFactor.ID)
                                                   select a).SingleOrDefault();

                                qEditFactor.CreditAmount = x.ToString();
                                db.Tbl_OrderFactor.Attach(qEditFactor);
                                db.Entry(qEditFactor).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            pprice = totalPrice;
                        }
                        else
                        {
                            int x = 0 - cred;

                            int result;
                            if (InTimeStatus.Equals("true"))
                            {
                                result = AddNewCredite(email, x, "0", "درلحظه");
                            }
                            else
                            {
                                result = AddNewCredite(email, x, "0", "شام");
                            }

                            if (result == 1)
                            {
                                var qEditFactor = (from a in db.Tbl_OrderFactor
                                                   where a.ID.Equals(tFactor.ID)
                                                   select a).SingleOrDefault();

                                qEditFactor.CreditAmount = x.ToString();
                                db.Tbl_OrderFactor.Attach(qEditFactor);
                                db.Entry(qEditFactor).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                            }
                            pprice = totalPrice;
                        }
                    }
                    Dargah(pprice.ToString());
                    var lstShopping = (from a in db.Tbl_ShoppingCart
                                       where a.UserEmail.Equals(email)
                                       select a).ToList();

                   

                    db.Tbl_ShoppingCart.RemoveRange(lstShopping);
                    db.SaveChanges();
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }

        }
        public JsonResult SignUp(string access, string sNumber, string name, string Email, string Phone, string Pass, string Family)
        {
            Tab_users tUser = new Tab_users();

            tUser.access = access;
            tUser.email = Email;
            tUser.enable = false;
            tUser.image = "1.jpg";
            tUser.mob_phone = Phone;
            tUser.name = name;
            tUser.password = Pass;
            tUser.Family = Family;
            tUser.SMS_Enable = false;
            tUser.SNumber = sNumber;
            Random rndom = new Random();
            tUser.activeCode = rndom.Next(10000, 99999).ToString();

            db.Tab_users.Add(tUser);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                Session["myEmail"] = Email;
                u.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", Email, "کد فعال سازی", tUser.activeCode);
                return Json("عضویت با موفقیت انجام شد", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("عضویت با خطا مواجه شد", JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult SignIn(string access, string Phone, string Pass)
        {
            if (access.Equals("دانشجو"))
            {
                var q = (from a in db.Tab_users
                         where a.SNumber.Equals(Phone) && a.password.Equals(Pass) && a.enable.Equals(true)
                         select a).SingleOrDefault();

                if (q != null)
                {
                    Session["email"] = q.email;
                    Session["access"] = q.access;
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(0, JsonRequestBehavior.AllowGet);
            }
            else if (access.Equals("کاربر عادی"))
            {
                var q = (from a in db.Tab_users
                         where a.mob_phone.Equals(Phone) && a.password.Equals(Pass) && a.enable.Equals(true)
                         select a).SingleOrDefault();

                if (q != null)
                {
                    Session["email"] = q.email;
                    Session["access"] = q.access;
                    return Json(1, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(0, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var q = (from a in db.Tab_users
                         where a.mob_phone.Equals(Phone) && a.password.Equals(Pass) && a.enable.Equals(true)
                         select a).SingleOrDefault();

                if (q != null)
                {
                    Session["email"] = q.email;
                    Session["access"] = q.access;
                    return Json(2, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ShowShoppingCart()
        {
            if (Session["email"] != null)
            {
                string email = Session["email"].ToString();
                List<ShoppingCartResult> lstShoppingCartresult = new List<ShoppingCartResult>();

                var qAll = (from a in db.Tbl_ShoppingCart
                            where a.UserEmail.Equals(email)
                            select a).ToList();

                foreach (var item in qAll)
                {
                    ShoppingCartResult cartResult = new ShoppingCartResult();

                    cartResult.FoodCount = item.FoodCount;
                    cartResult.FoodName = item.tab_products.name;
                    cartResult.UserEmail = item.UserEmail;
                    cartResult.FoodTotalPrice = item.tab_products.cost * item.FoodCount;
                    cartResult.ID = item.ID;

                    lstShoppingCartresult.Add(cartResult);
                }
                return Json(lstShoppingCartresult, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }


        }
        public JsonResult ShoppingcartCountDown(int ID)
        {
            string email = Session["email"].ToString();
            var q = (from a in db.Tbl_ShoppingCart
                     where a.ID.Equals(ID)
                     select a).SingleOrDefault();

            if (q.tab_products.FoodCount >= q.FoodCount || q.tab_products.FoodCount == -1)
            {
                q.FoodCount--;
                if (q.FoodCount == 0)
                {
                    db.Tbl_ShoppingCart.Remove(q);
                }
                else
                {
                    db.Tbl_ShoppingCart.Attach(q);
                    db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                }
                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    List<ShoppingCartResult> lstShoppingCartresult = new List<ShoppingCartResult>();

                    var qAll = (from a in db.Tbl_ShoppingCart
                                where a.UserEmail.Equals(email)
                                select a).ToList();

                    foreach (var item in qAll)
                    {
                        ShoppingCartResult cartResult = new ShoppingCartResult();

                        cartResult.FoodCount = item.FoodCount;
                        cartResult.FoodName = item.tab_products.name;
                        cartResult.UserEmail = item.UserEmail;
                        cartResult.FoodTotalPrice = item.tab_products.cost * item.FoodCount;
                        cartResult.ID = item.ID;

                        lstShoppingCartresult.Add(cartResult);
                    }
                    return Json(lstShoppingCartresult, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else if (q.tab_products.FoodCount == q.FoodCount)
            {
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
            else if (q.tab_products.FoodCount == 0)
            {
                db.Tbl_ShoppingCart.Remove(q);

                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    List<ShoppingCartResult> lstShoppingCartresult = new List<ShoppingCartResult>();

                    var qAll = (from a in db.Tbl_ShoppingCart
                                where a.UserEmail.Equals(email)
                                select a).ToList();

                    foreach (var item in qAll)
                    {
                        ShoppingCartResult cartResult = new ShoppingCartResult();

                        cartResult.FoodCount = item.FoodCount;
                        cartResult.FoodName = item.tab_products.name;
                        cartResult.UserEmail = item.UserEmail;
                        cartResult.FoodTotalPrice = item.tab_products.cost * item.FoodCount;
                        cartResult.ID = item.ID;

                        lstShoppingCartresult.Add(cartResult);
                    }
                    return Json(lstShoppingCartresult, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                q.FoodCount = q.tab_products.FoodCount.Value;
                db.Tbl_ShoppingCart.Attach(q);
                db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    List<ShoppingCartResult> lstShoppingCartresult = new List<ShoppingCartResult>();

                    var qAll = (from a in db.Tbl_ShoppingCart
                                where a.UserEmail.Equals(email)
                                select a).ToList();

                    foreach (var item in qAll)
                    {
                        ShoppingCartResult cartResult = new ShoppingCartResult();

                        cartResult.FoodCount = item.FoodCount;
                        cartResult.FoodName = item.tab_products.name;
                        cartResult.UserEmail = item.UserEmail;
                        cartResult.FoodTotalPrice = item.tab_products.cost * item.FoodCount;
                        cartResult.ID = item.ID;

                        lstShoppingCartresult.Add(cartResult);
                    }
                    return Json(lstShoppingCartresult, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List<ShoppingCartResult> lstShoppingCartresult = new List<ShoppingCartResult>();

                    var qAll = (from a in db.Tbl_ShoppingCart
                                where a.UserEmail.Equals(email)
                                select a).ToList();

                    foreach (var item in qAll)
                    {
                        ShoppingCartResult cartResult = new ShoppingCartResult();

                        cartResult.FoodCount = item.FoodCount;
                        cartResult.FoodName = item.tab_products.name;
                        cartResult.UserEmail = item.UserEmail;
                        cartResult.FoodTotalPrice = item.tab_products.cost * item.FoodCount;
                        cartResult.ID = item.ID;

                        lstShoppingCartresult.Add(cartResult);
                    }
                    return Json(lstShoppingCartresult, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult RemoveShoppingCart(int ID)
        {
            string email = Session["email"].ToString();
            var q = (from a in db.Tbl_ShoppingCart
                     where a.ID.Equals(ID)
                     select a).SingleOrDefault();

            db.Tbl_ShoppingCart.Remove(q);

            if (Convert.ToBoolean(db.SaveChanges()))
            {
                List<ShoppingCartResult> lstShoppingCartresult = new List<ShoppingCartResult>();

                var qAll = (from a in db.Tbl_ShoppingCart
                            where a.UserEmail.Equals(email)
                            select a).ToList();

                foreach (var item in qAll)
                {
                    ShoppingCartResult cartResult = new ShoppingCartResult();

                    cartResult.FoodCount = item.FoodCount;
                    cartResult.FoodName = item.tab_products.name;
                    cartResult.UserEmail = item.UserEmail;
                    cartResult.FoodTotalPrice = item.tab_products.cost * item.FoodCount;
                    cartResult.ID = item.ID;

                    lstShoppingCartresult.Add(cartResult);
                }
                return Json(lstShoppingCartresult, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult ShoppingcartCountUp(int ID)
        {
            string email = Session["email"].ToString();
            var q = (from a in db.Tbl_ShoppingCart
                     where a.ID.Equals(ID)
                     select a).SingleOrDefault();

            if (q.tab_products.FoodCount > q.FoodCount || q.tab_products.FoodCount == -1)
            {
                q.FoodCount++;

                db.Tbl_ShoppingCart.Attach(q);
                db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    List<ShoppingCartResult> lstShoppingCartresult = new List<ShoppingCartResult>();

                    var qAll = (from a in db.Tbl_ShoppingCart
                                where a.UserEmail.Equals(email)
                                select a).ToList();

                    foreach (var item in qAll)
                    {
                        ShoppingCartResult cartResult = new ShoppingCartResult();

                        cartResult.FoodCount = item.FoodCount;
                        cartResult.FoodName = item.tab_products.name;
                        cartResult.UserEmail = item.UserEmail;
                        cartResult.FoodTotalPrice = item.tab_products.cost * item.FoodCount;
                        cartResult.ID = item.ID;

                        lstShoppingCartresult.Add(cartResult);
                    }
                    return Json(lstShoppingCartresult, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else if (q.tab_products.FoodCount == q.FoodCount)
            {
                return Json(-1, JsonRequestBehavior.AllowGet);
            }
            else if (q.tab_products.FoodCount == 0)
            {
                db.Tbl_ShoppingCart.Remove(q);

                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    List<ShoppingCartResult> lstShoppingCartresult = new List<ShoppingCartResult>();

                    var qAll = (from a in db.Tbl_ShoppingCart
                                where a.UserEmail.Equals(email)
                                select a).ToList();

                    foreach (var item in qAll)
                    {
                        ShoppingCartResult cartResult = new ShoppingCartResult();

                        cartResult.FoodCount = item.FoodCount;
                        cartResult.FoodName = item.tab_products.name;
                        cartResult.UserEmail = item.UserEmail;
                        cartResult.FoodTotalPrice = item.tab_products.cost * item.FoodCount;
                        cartResult.ID = item.ID;

                        lstShoppingCartresult.Add(cartResult);
                    }
                    return Json(lstShoppingCartresult, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                q.FoodCount = q.tab_products.FoodCount.Value;
                db.Tbl_ShoppingCart.Attach(q);
                db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    List<ShoppingCartResult> lstShoppingCartresult = new List<ShoppingCartResult>();

                    var qAll = (from a in db.Tbl_ShoppingCart
                                where a.UserEmail.Equals(email)
                                select a).ToList();

                    foreach (var item in qAll)
                    {
                        ShoppingCartResult cartResult = new ShoppingCartResult();

                        cartResult.FoodCount = item.FoodCount;
                        cartResult.FoodName = item.tab_products.name;
                        cartResult.UserEmail = item.UserEmail;
                        cartResult.FoodTotalPrice = item.tab_products.cost * item.FoodCount;
                        cartResult.ID = item.ID;

                        lstShoppingCartresult.Add(cartResult);
                    }
                    return Json(lstShoppingCartresult, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List<ShoppingCartResult> lstShoppingCartresult = new List<ShoppingCartResult>();

                    var qAll = (from a in db.Tbl_ShoppingCart
                                where a.UserEmail.Equals(email)
                                select a).ToList();

                    foreach (var item in qAll)
                    {
                        ShoppingCartResult cartResult = new ShoppingCartResult();

                        cartResult.FoodCount = item.FoodCount;
                        cartResult.FoodName = item.tab_products.name;
                        cartResult.UserEmail = item.UserEmail;
                        cartResult.FoodTotalPrice = item.tab_products.cost * item.FoodCount;
                        cartResult.ID = item.ID;

                        lstShoppingCartresult.Add(cartResult);
                    }
                    return Json(lstShoppingCartresult, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public JsonResult AddShoppingCart(int foodID)
        {
            Session["cartType"] = "food";
            string email = Session["email"].ToString();
            var q = (from a in db.tab_products
                     where a.id.Equals(foodID)
                     select a).SingleOrDefault();

            var qDup = (from a in db.Tbl_ShoppingCart
                        where a.FoodID.Equals(foodID) && a.UserEmail.Equals(email)
                        select a).FirstOrDefault();

            if (qDup != null)
            {
                if (q.FoodCount <= qDup.FoodCount && q.FoodCount != -1)
                {
                    return Json(5, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    qDup.FoodCount++;

                    db.Tbl_ShoppingCart.Attach(qDup);
                    db.Entry(qDup).State = System.Data.Entity.EntityState.Modified;

                }

            }
            else
            {
                Tbl_ShoppingCart tShoppingCart = new Tbl_ShoppingCart();

                tShoppingCart.FoodCount = 1;
                tShoppingCart.FoodID = foodID;
                tShoppingCart.UserEmail = email;

                db.Tbl_ShoppingCart.Add(tShoppingCart);
            }

            if (Convert.ToBoolean(db.SaveChanges()))
            {
                List<ShoppingCartResult> lstShoppingCartresult = new List<ShoppingCartResult>();

                var qAll = (from a in db.Tbl_ShoppingCart
                            where a.UserEmail.Equals(email)
                            select a).ToList();

                foreach (var item in qAll)
                {
                    ShoppingCartResult cartResult = new ShoppingCartResult();


                    cartResult.FoodCount = item.FoodCount;
                    cartResult.FoodName = item.tab_products.name;
                    cartResult.UserEmail = item.UserEmail;
                    cartResult.FoodTotalPrice = item.tab_products.cost * item.FoodCount;
                    cartResult.ID = item.ID;


                    lstShoppingCartresult.Add(cartResult);
                }
                return Json(lstShoppingCartresult, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpGet]
        public ActionResult resRegister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult resRegister(Tbl_Restaurant tRestaurant, Tab_users tUser, string SaveChange, HttpPostedFileBase file, HttpPostedFileBase file2)
        {
            if (SaveChange.Equals("ذخیره"))
            {
                tUser.access = tRestaurant.resType;
                tUser.enable = true;
                tUser.password = "123456789";
                tUser.image = "sdfsdf";
                tUser.Family = "";

                db.Tab_users.Add(tUser);
                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    tRestaurant.resAvgServiceTime = "15";
                    tRestaurant.resPoints = 0;
                    tRestaurant.resLatLng = "34.2,45.8";
                    tRestaurant.userEmail = tUser.email;
                    tRestaurant.StudentRes = true;
                    tRestaurant.resEnable = false;

                    if (file != null && file2 != null)
                    {

                        if (file.ContentType == "image/jpeg" && file2.ContentType == "image/jpeg")
                        {
                            if (file.ContentLength <= 81920 && file2.ContentLength <= 81920)
                            {
                                Random r = new Random();
                                string ran = r.Next(100000, 999999).ToString() + ".jpg";
                                var uploadurl = "ftp://Image@185.159.152.62";
                                var uploadfilename = ran;
                                var username = "Image";
                                var password = "1741034681.Shm";
                                Stream streamObj = file.InputStream;
                                byte[] buffer = new byte[file.ContentLength];
                                streamObj.Read(buffer, 0, buffer.Length);
                                streamObj.Close();
                                streamObj = null;
                                string ftpurl = String.Format("{0}/{1}", uploadurl, uploadfilename);
                                var requestObj = FtpWebRequest.Create(ftpurl) as FtpWebRequest;
                                requestObj.Method = WebRequestMethods.Ftp.UploadFile;
                                requestObj.Credentials = new NetworkCredential(username, password);
                                Stream requestStream = requestObj.GetRequestStream();
                                requestStream.Write(buffer, 0, buffer.Length);
                                requestStream.Flush();
                                requestStream.Close();
                                requestObj = null;
                                tRestaurant.resImage = ran;

                                Random r2 = new Random();
                                string ran2 = r2.Next(100000, 999999).ToString() + ".jpg";
                                var uploadfilename2 = ran2;
                                Stream streamObj2 = file2.InputStream;
                                byte[] buffer2 = new byte[file2.ContentLength];
                                streamObj2.Read(buffer2, 0, buffer2.Length);
                                streamObj2.Close();
                                streamObj2 = null;
                                string ftpurl2 = String.Format("{0}/{1}", uploadurl, uploadfilename2);
                                var requestObj2 = FtpWebRequest.Create(ftpurl2) as FtpWebRequest;
                                requestObj2.Method = WebRequestMethods.Ftp.UploadFile;
                                requestObj2.Credentials = new NetworkCredential(username, password);
                                Stream requestStream2 = requestObj2.GetRequestStream();
                                requestStream2.Write(buffer2, 0, buffer2.Length);
                                requestStream2.Flush();
                                requestStream2.Close();
                                requestObj2 = null;
                                tRestaurant.ResBusinessLicense = ran2;
                            }
                            else
                            {
                                return Content("سایز زیاد است");
                            }
                        }
                        else
                        {
                            return Content("فرمت درست نیس");
                        }
                    }
                    else
                    {
                        return Content("عکس موجود نیس");
                    }
                    db.Tbl_Restaurant.Add(tRestaurant);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return Content("همه چی درست");
                    }
                    else
                    {
                        return Content("مشکل");
                    }
                }
                else
                {
                    return Content("یوزر ایراد دارد");
                }
            }
            else
            {
                return Content("برو گم شو هکر بیشور");
            }


        }
        public JsonResult Download(string phoneNo, string Send)
        {
            if (Send.Equals("ارسال"))
            {
                u.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", phoneNo, "لینک دانلود اپلیکیشن سفارش غذای دانشگاه صنعتی ارومیه", "http://android-application-api.ir/Home/App");
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public FilePathResult App()
        {
            return File(Server.MapPath("~/App_Data/ApkDownload/" + "app-release.apk"), "application/apk", "app-release.apk");
        }
        [HttpGet]
        public ActionResult Index()
        {
            var q = Rresturant.getResturant();
            return View(q);
        }
        public ActionResult SignOut()
        {
            Session["email"] = null;
            Session["access"] = null;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Index(Tab_users tUser, string btnSignUp)
        {
            if (btnSignUp.Equals("btnForgetPass"))
            {
                var q = (from a in db.Tab_users
                         where a.email.Equals(tUser.email) && a.mob_phone.Equals(tUser.mob_phone)
                         select a).SingleOrDefault();

                if (q != null)
                {
                    u.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", q.email, "دریافت رمز از آرین پترو ایده", q.password);
                    ViewBag.ForgetPassResult = "S";
                }
                else
                {
                    ViewBag.ForgetPassResult = "F";
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult mail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult mail(Tab_mail t)
        {
            rep_mail rmail = new rep_mail();
            if (ModelState.IsValid)
            {


                if (rmail.Insert(t))
                {
                    ViewBag.style = "color:green;";
                    ViewBag.mailMessage = " درخواست با موفقیت  ثبت شد";

                }
                else
                {

                    ViewBag.style = "color :red;";
                    ViewBag.Message = "متاسفانه درخواست ثبت نشد";

                }


            }
            else
            {

                ViewBag.style = "color :red;";
                ViewBag.Message = "تمام فیلد ها را به صورت صحیح پرنمایید";
            }

            return View(t);

        }
        public int AddCredit(string value)
        {
            if (Session["email"] != null)
            {
                string email = Session["email"].ToString();

                int val = Convert.ToInt32(value);

                Tbl_Credit tCredit = new Tbl_Credit();

                tCredit.Credit = val;
                tCredit.Date = DateTime.Now;
                tCredit.RootUser = "M";
                tCredit.Time = DateTime.Now.ToShortTimeString();
                tCredit.TransactionCode = "123";
                tCredit.UserEmail = email;

                db.Tbl_Credit.Add(tCredit);
                if (Convert.ToBoolean(db.SaveChanges()))
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
                return -1;
            }

        }
        public JsonResult SaveComment(string commentText , string commentRate , int ID)
        {
            if (Session["Email"] != null)
            {
                string email = Session["Email"].ToString();

                var qUser = (from a in db.Tab_users
                             where a.email.Equals(email)
                             select a).SingleOrDefault();

                Tab_comments tComment = new Tab_comments();

                tComment.name = qUser.name + " " + qUser.Family;
                tComment.email = qUser.email;
                tComment.ip = "1.2.3";
                tComment.data = DateTime.Now;
                tComment.confirm = false;
                tComment.read = true;
                tComment.parent_id = 1;
                tComment.read = true;
                tComment.cm_like = 0;
                tComment.cm_dislike = 0;
                tComment.phone = qUser.mob_phone;
                tComment.Stars = Convert.ToInt32(commentRate);
                if (commentText == "" || commentText == null)
                {
                    tComment.text = "(بدون نظر)";
                }
                else
                {
                    tComment.text = commentText;
                }
                tComment.product_id = ID;
                db.Tab_comments.Add(tComment);
                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    int sum = 0;
                    int count = 0;
                    var qResName = (from a in db.tab_products
                                    where a.id.Equals(tComment.product_id)
                                    select a).SingleOrDefault();

                    var qProducts = (from a in db.tab_products
                                     where a.resID.Equals(qResName.resID)
                                     select a).ToList();
                    foreach (var item in qProducts)
                    {
                        var qCommentList = (from a in db.Tab_comments
                                            where a.product_id.Equals(item.id)
                                            select a).ToList();

                        count += qCommentList.Count();
                        sum += qCommentList.Sum(s => s.Stars);
                    }
                    var qRes = (from a in db.Tbl_Restaurant
                                where a.ID.Equals(qResName.resID)
                                select a).SingleOrDefault();

                    qRes.resPoints = (sum / count);
                    db.Tbl_Restaurant.Attach(qRes);
                    db.Entry(qRes).State = System.Data.Entity.EntityState.Modified;
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        return Json(1, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(0, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult single(int ID)
        {
            return View(Rresturant.GetdeatilFood(ID));
        }
        public ActionResult Filter()
        {
            return View();
        }
        public ActionResult Resturant()
        {
            var m = Rresturant.getResturant();
            return View(m.ToList());
        }
        public int RegisterAPI(string Email, string password, string name, string mobNumber, string Access, string Snumber, string family)
        {
            int status;
            Tab_users user = new Tab_users();
            user.email = Email;
            user.password = password;
            user.access = Access;
            user.name = name;
            user.Family = family;
            user.mob_phone = mobNumber;
            user.image = "men.png";
            user.enable = false;
            user.SNumber = Snumber;
            db.Tab_users.Add(user);
            db.SaveChanges();

            var q = (from a in db.Tab_users
                     where a.email.Equals(Email)
                     select a).SingleOrDefault();

            if (q != null)
                status = 1;
            else
                status = 0;

            return status;
        }
        public int ActiveCodeAPI(string activeCode, string email)
        {
            int status = 2;
            var q = (from a in db.Tab_users
                     where a.email.Equals(email)
                     select a).SingleOrDefault();
            if (q.activeCode.Equals(activeCode))
            {
                q.enable = true;
                db.Tab_users.Attach(q);
                db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                if (db.SaveChanges() > 0)
                    status = 1;
                else
                    status = 0;
            }
            return status;

        }
        public ActionResult ForgetPasswordAPI(string Email)
        {

            var q = (from a in db.Tab_users
                     where a.email.Equals(Email)
                     select a).SingleOrDefault();

            u.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681.Sh", q.email, "دریافت رمز از آرین پترو ایده", q.password);

            return Content("ایمیل با موفقیت ارسال شد");
        }
        public JsonResult SignInAPI(string mobNumber, string password, string Access)
        {

            if (Access.Equals("دانشجو"))
            {
                var q = (from a in db.Tab_users
                         where a.SNumber.Equals(mobNumber) && a.password.Equals(password) && a.enable.Equals(true)
                         select a).SingleOrDefault();

                Tab_users t = new Tab_users();
                t.id = q.id;
                t.email = q.email;
                t.password = q.password;
                t.access = q.access;
                t.description = q.description;
                t.name = q.name;
                t.Family = q.Family;
                if (q.sex.Equals("خانوم"))
                    t.image = "women.png";
                else
                    t.image = "men.png";
                t.enable = q.enable;
                t.ncode = q.ncode;
                t.mob_phone = q.mob_phone;
                t.home_phone = q.home_phone;
                t.birth_date = q.birth_date;
                t.sex = q.sex;
                t.home_address = q.home_address;
                t.IMEI = q.IMEI;
                t.SMS_Enable = q.SMS_Enable;
                t.activeCode = q.activeCode;
                t.SNumber = q.SNumber;



                var jsondata = Json(t, JsonRequestBehavior.AllowGet);
                return jsondata;
            }
            else
            {
                var q = (from a in db.Tab_users
                         where a.mob_phone.Equals(mobNumber) && a.password.Equals(password)
                         select a).SingleOrDefault();

                Tab_users t = new Tab_users();
                t.id = q.id;
                t.email = q.email;
                t.password = q.password;
                t.access = q.access;
                t.description = q.description;
                t.name = q.name;
                t.Family = q.Family;
                if (q.sex.Equals("خانوم"))
                    t.image = "women.png";
                else
                    t.image = "men.png";
                t.enable = q.enable;
                t.ncode = q.ncode;
                t.mob_phone = q.mob_phone;
                t.home_phone = q.home_phone;
                t.birth_date = q.birth_date;
                t.sex = q.sex;
                t.home_address = q.home_address;
                t.IMEI = q.IMEI;
                t.SMS_Enable = q.SMS_Enable;
                t.activeCode = q.activeCode;
                t.SNumber = q.SNumber;



                var jsondata = Json(t, JsonRequestBehavior.AllowGet);
                return jsondata;
            }
        }
        public JsonResult RestuarantListAPI()
        {
            var q = (from a in db.Tbl_Restaurant
                     where a.resType.Equals("رستوران")
                     select a).ToList();

            List<Tbl_Restaurant> LstRestuarant = new List<Tbl_Restaurant>();
            foreach (var item in q)
            {
                Tbl_Restaurant t = new Tbl_Restaurant();
                t.ID = item.ID;
                t.resAddress = item.resAddress;
                t.resAvgServiceTime = item.resAvgServiceTime;
                t.resImage = item.resImage;
                t.resLatLng = item.resLatLng;
                t.resName = item.resName;
                t.resPhone = item.resPhone;
                t.resPoints = item.resPoints;
                t.ResBusinessLicense = item.ResBusinessLicense;
                t.resEnable = item.resEnable;
                t.StudentRes = item.StudentRes;
                t.resType = item.resType;
                t.FirstTime = item.FirstTime;
                t.FoodAvragePrice = item.FoodAvragePrice;
                t.isGetOrder = item.isGetOrder;
                t.SecendTime = item.SecendTime;

                LstRestuarant.Add(t);
                //db.Tbl_Restaurant.Add(t);
            }

            var jsondata = Json(LstRestuarant, JsonRequestBehavior.AllowGet);
            return jsondata;

        }
        public JsonResult FastFoodListAPI()
        {
            var q = (from a in db.Tbl_Restaurant
                     where a.resType.Equals("فست فود")
                     select a).ToList();

            List<Tbl_Restaurant> LstRestuarant = new List<Tbl_Restaurant>();
            foreach (var item in q)
            {
                Tbl_Restaurant t = new Tbl_Restaurant();
                t.ID = item.ID;
                t.resAddress = item.resAddress;
                t.resAvgServiceTime = item.resAvgServiceTime;
                t.resImage = item.resImage;
                t.resLatLng = item.resLatLng;
                t.resName = item.resName;
                t.resPhone = item.resPhone;
                t.resPoints = item.resPoints;
                t.StudentRes = item.StudentRes;
                t.ResBusinessLicense = item.ResBusinessLicense;
                t.resEnable = item.resEnable;
                t.resType = item.resType;
                t.userEmail = item.userEmail;
                t.FirstTime = item.FirstTime;
                t.FoodAvragePrice = item.FoodAvragePrice;
                t.isGetOrder = item.isGetOrder;
                t.SecendTime = item.SecendTime;

                LstRestuarant.Add(t);
                //db.Tbl_Restaurant.Add(t);
            }

            var jsondata = Json(LstRestuarant, JsonRequestBehavior.AllowGet);
            return jsondata;

        }
        public JsonResult CaffiShopListAPI()
        {
            var q = (from a in db.Tbl_Restaurant
                     where a.resType.Equals("کافی شاپ")
                     select a).ToList();

            List<Tbl_Restaurant> LstRestuarant = new List<Tbl_Restaurant>();
            foreach (var item in q)
            {
                Tbl_Restaurant t = new Tbl_Restaurant();
                t.ID = item.ID;
                t.resAddress = item.resAddress;
                t.resAvgServiceTime = item.resAvgServiceTime;
                t.resImage = item.resImage;
                t.resLatLng = item.resLatLng;
                t.resName = item.resName;
                t.resPhone = item.resPhone;
                t.resPoints = item.resPoints;
                t.resType = item.resType;
                t.userEmail = item.userEmail;
                t.StudentRes = item.StudentRes;
                t.ResBusinessLicense = item.ResBusinessLicense;
                t.resEnable = item.resEnable;
                t.FirstTime = item.FirstTime;
                t.FoodAvragePrice = item.FoodAvragePrice;
                t.isGetOrder = item.isGetOrder;
                t.SecendTime = item.SecendTime;

                LstRestuarant.Add(t);
                //db.Tbl_Restaurant.Add(t);
            }

            var jsondata = Json(LstRestuarant, JsonRequestBehavior.AllowGet);
            return jsondata;

        }
        public JsonResult UserListAPI()
        {
            var q = (from a in db.Tab_users
                     select a).ToList();
            List<Tab_users> LstUser = new List<Tab_users>();
            foreach (var item in q)
            {
                Tab_users t = new Tab_users();
                t.id = item.id;
                t.access = item.access;
                t.activeCode = item.activeCode;
                t.birth_date = item.birth_date;
                t.description = item.description;
                t.email = item.email;
                t.home_address = item.home_address;
                t.home_phone = item.home_phone;
                t.image = item.image;
                t.IMEI = item.IMEI;
                t.mob_phone = item.mob_phone;
                t.name = item.name;
                t.Family = item.Family;
                t.ncode = item.ncode;
                t.password = item.password;
                t.sex = item.sex;
                t.SMS_Enable = item.SMS_Enable;
                LstUser.Add(t);

            }
            var jsondata = Json(LstUser, JsonRequestBehavior.AllowGet);
            return jsondata;

        }
        public JsonResult FoodListAPI(int resId, int menuId)
        {
            List<tab_products> LstFood = new List<tab_products>();

            var q = (from a in db.tab_products
                     where a.resID.Equals(resId) && a.menuID.Equals(menuId)
                     select a).ToList();
            foreach (var item in q)
            {
                tab_products t = new tab_products();
                t.id = item.id;
                t.cost = item.cost;
                t.name = item.name;
                t.User_Email = item.User_Email;
                t.resID = item.resID;
                t.CreateMaterial = item.CreateMaterial;
                t.menuID = item.menuID;
                t.foodImage = item.foodImage;
                t.bakingTime = item.bakingTime;
                t.Recipe = item.Recipe;
                t.FoodCount = item.FoodCount;
                t.OrderType = item.OrderType;
                LstFood.Add(t);

            }
            var jsondata = Json(LstFood, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult DetailsFoodListAPI(int id)
        {
            var q = (from a in db.tab_products
                     where a.id.Equals(id)
                     select a).ToList();

            List<tab_products> LstFood = new List<tab_products>();
            foreach (var item in q)
            {
                tab_products t = new tab_products();
                t.id = item.id;
                t.cost = item.cost;
                t.name = item.name;
                t.User_Email = item.User_Email;
                t.resID = item.resID;
                t.CreateMaterial = item.CreateMaterial;
                t.menuID = item.menuID;
                t.foodImage = item.foodImage;
                t.bakingTime = item.bakingTime;
                t.Recipe = item.Recipe;
                t.FoodCount = item.FoodCount;
                t.OrderType = item.OrderType;
                LstFood.Add(t);

            }


            var jsondata = Json(LstFood, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult SpecialResFood(int foodID)
        {
            var q = (from a in db.tab_products
                     where a.id.Equals(foodID)
                     select a).SingleOrDefault();

            tab_products t = new tab_products();
            t.id = q.id;
            t.cost = q.cost;
            t.name = q.name;
            t.User_Email = q.User_Email;
            t.resID = q.resID;
            t.CreateMaterial = q.CreateMaterial;
            t.menuID = q.menuID;
            t.foodImage = q.foodImage;
            t.bakingTime = q.bakingTime;
            t.Recipe = q.Recipe;
            t.FoodCount = q.FoodCount;
            t.OrderType = q.OrderType;


            var jsondata = Json(t, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult MenuCaffiShopAPI()
        {
            var q = (from a in db.Tbl_Menu
                     where a.menuType.Equals("کافی شاپ")
                     select a).ToList();


            List<Tbl_Menu> MenuRestuarant = new List<Tbl_Menu>();
            foreach (var item in q)
            {
                Tbl_Menu t = new Tbl_Menu();
                t.menuID = item.menuID;
                t.menuTitle = item.menuTitle;
                t.menuType = item.menuType;
                t.menuImage = item.menuImage;


                MenuRestuarant.Add(t);
                //db.Tbl_Restaurant.Add(t);
            }

            var jsondata = Json(MenuRestuarant, JsonRequestBehavior.AllowGet);
            return jsondata;

        }
        public JsonResult MenuResAPI()
        {
            var q = (from a in db.Tbl_Menu
                     where a.menuType.Equals("رستوران")
                     select a).ToList();


            List<Tbl_Menu> MenuRestuarant = new List<Tbl_Menu>();
            foreach (var item in q)
            {
                Tbl_Menu t = new Tbl_Menu();
                t.menuID = item.menuID;
                t.menuTitle = item.menuTitle;
                t.menuType = item.menuType;
                t.menuImage = item.menuImage;

                MenuRestuarant.Add(t);
                //db.Tbl_Restaurant.Add(t);
            }

            var jsondata = Json(MenuRestuarant, JsonRequestBehavior.AllowGet);
            return jsondata;

        }
        public JsonResult MenuFastFoodAPI()
        {
            var q = (from a in db.Tbl_Menu
                     where a.menuType.Equals("فست فود")
                     select a).ToList();


            List<Tbl_Menu> MenuRestuarant = new List<Tbl_Menu>();
            foreach (var item in q)
            {
                Tbl_Menu t = new Tbl_Menu();
                t.menuID = item.menuID;
                t.menuTitle = item.menuTitle;
                t.menuType = item.menuType;
                t.menuImage = item.menuImage;

                MenuRestuarant.Add(t);
                //db.Tbl_Restaurant.Add(t);
            }

            var jsondata = Json(MenuRestuarant, JsonRequestBehavior.AllowGet);
            return jsondata;

        }
        public JsonResult CommentAPI(int foodID, int stars, string phone, string text)
        {
            var q = (from a in db.Tab_users
                     where a.mob_phone.Equals(phone)

                     select a).FirstOrDefault();


            Tab_comments comments = new Tab_comments();

            comments.product_id = foodID;
            comments.name = q.name + " " + q.Family;
            comments.email = q.email;
            comments.ip = "11111";
            comments.phone = phone;
            comments.cm_dislike = 10;
            comments.cm_like = 10;
            comments.confirm = false;
            comments.data = DateTime.Now;
            comments.parent_id = 1111;
            comments.read = false;
            comments.Stars = stars;
            comments.text = text;

            db.Tab_comments.Add(comments);
            db.SaveChanges();

            var jsondata = Json("1", JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult CommentListAPI(int id)
        {
            var q = (from a in db.Tab_comments
                     where a.product_id.Equals(id)
                     select a).ToList();

            List<Tab_comments> LstCommmt = new List<Tab_comments>();
            foreach (var item in q)
            {
                Tab_comments t = new Tab_comments();
                t.cm_dislike = item.cm_dislike;
                t.cm_like = item.cm_like;
                t.confirm = item.confirm;
                t.data = item.data;
                t.email = item.email;
                t.id = item.id;
                t.ip = item.ip;
                t.name = item.name;
                t.parent_id = item.parent_id;
                t.phone = item.phone;
                t.product_id = item.product_id;
                t.read = item.read;
                t.Stars = item.Stars;
                t.text = item.text;


                LstCommmt.Add(t);
                // db.Tab_comments.Add(t);
            }

            var jsondata = Json(LstCommmt, JsonRequestBehavior.AllowGet);
            return jsondata;

        }
        public ActionResult ChangePasswordAPI(string newPassword, string email)
        {


            var q = (from a in db.Tab_users
                     where a.email.Equals(email)
                     select a).SingleOrDefault();
            if (q != null)
            {

                q.password = newPassword;
                db.Tab_users.Attach(q);
                db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }
            return Content("Success");





        }
        public JsonResult BannerAPI()
        {
            List<tab_products> q = (from a in db.tab_products
                                    select a).ToList();


            List<tab_products> LstFood = new List<tab_products>();
            int c = 0;
            foreach (var item in q)
            {



                tab_products t = new tab_products();
                t.id = item.id;
                t.cost = item.cost;
                t.name = item.name;
                t.User_Email = item.User_Email;
                t.resID = item.resID;
                t.CreateMaterial = item.CreateMaterial;
                t.menuID = item.menuID;
                t.foodImage = item.foodImage;
                LstFood.Add(t);



                c++;
                if (c == 4)
                {
                    break;
                }
            }





            var jsondata = Json(LstFood, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult SignInServerAPI(string mobNumber, string password)
        {
            var q = (from a in db.Tab_users
                     where a.mob_phone.Equals(mobNumber) && a.password.Equals(password)
                     select a).FirstOrDefault();
            var w = (from a in db.Tbl_Restaurant
                     where a.userEmail.Equals(q.email)
                     select a).FirstOrDefault();
            Tbl_Restaurant res = new Tbl_Restaurant();
            res.ID = w.ID;
            res.resAddress = w.resAddress;
            res.resAvgServiceTime = w.resAvgServiceTime;
            res.resImage = w.resImage;
            res.resLatLng = w.resLatLng;
            res.resName = w.resName;
            res.resPhone = w.resPhone;
            res.resPoints = w.resPoints;
            res.resType = w.resType;
            res.userEmail = w.userEmail;
            res.StudentRes = w.StudentRes;
            res.resEnable = w.resEnable;
            res.ResBusinessLicense = w.ResBusinessLicense;
            res.FirstTime = w.FirstTime;
            res.FoodAvragePrice = w.FoodAvragePrice;
            res.isGetOrder = w.isGetOrder;
            res.SecendTime = w.SecendTime;


            var jsondata = Json(res, JsonRequestBehavior.AllowGet);
            return jsondata;

        }
        public JsonResult ServerUser(string mobNumber, string password)
        {
            var q = (from a in db.Tab_users
                     where a.mob_phone.Equals(mobNumber) && a.password.Equals(password)
                     select a).ToList();

            List<Tab_users> LstUser = new List<Tab_users>();
            foreach (var item in q)
            {
                Tab_users t = new Tab_users();
                t.id = item.id;
                t.access = item.access;
                t.activeCode = item.activeCode;
                t.birth_date = item.birth_date;
                t.description = item.description;
                t.email = item.email;
                t.home_address = item.home_address;
                t.home_phone = item.home_phone;
                t.image = item.image;
                t.IMEI = item.IMEI;
                t.mob_phone = item.mob_phone;
                t.name = item.name;
                t.Family = item.Family;
                t.ncode = item.ncode;
                t.password = item.password;
                t.sex = item.sex;
                t.SMS_Enable = item.SMS_Enable;
                LstUser.Add(t);

            }
            var jsondata = Json(LstUser, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public float Rating(int id)
        {
            int sum = 0;
            float rate;
            var q = (from a in db.Tab_comments
                     where a.product_id.Equals(id)
                     select a).ToList();

            foreach (var item in q)
            {
                sum += item.Stars;
            }

            rate = (sum / (q.Count()));

            return rate;
        }
        public string DeleteFood(int id)
        {
            var q = (from a in db.tab_products
                     where a.id.Equals(id)
                     select a).SingleOrDefault();
            db.tab_products.Remove(q);
            if (db.SaveChanges() <= 0)
                return "Error";
            else
                return "Success";
        }
        public void FoodServerListAPI(int id, int idMenu, string name, int cost, string full_text, string title, string type, int discount, string createMaterial, string foodImage)
        {



            tab_products t = new tab_products();
            t.id = id;
            t.cost = cost;
            t.name = name;
            t.User_Email = "moazamshabnam@gmail.com";
            t.resID = id;
            t.CreateMaterial = createMaterial;
            t.menuID = idMenu;
            t.foodImage = foodImage;
            db.tab_products.Add(t);
            db.SaveChanges();


        }
        public string EditFood(int id, string title, int cost, string name, string type, string full_text, int discount, string createMaterial, string foodImage)
        {
            var q = (from a in db.tab_products
                     where a.id.Equals(id)
                     select a).SingleOrDefault();
            q.cost = cost;
            q.CreateMaterial = createMaterial;
            q.foodImage = foodImage;
            q.name = name;

            db.tab_products.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (db.SaveChanges() <= 0)
                return "Error";
            else
                return "Success";

        }
        public float ResRating(int id)
        {
            int sum = 0, count = 0;
            float rate;
            var q = (from a in db.tab_products
                     where a.resID.Equals(id)
                     select a).ToList();
            foreach (var item in q)
            {
                var w = (from a in db.Tab_comments
                         where a.product_id.Equals(item.id)
                         select a).ToList();
                foreach (var item1 in w)
                {
                    sum += item1.Stars;
                    count += 1;
                }
            }
            rate = sum / count;
            return rate;
        }
        public String InsertUserAddressAPI(string userEmail, string address, string addressType, string latLng, string apartmentName, string floor, string unit)
        {
            string status;
            if (userEmail == null || address == null || addressType == null || latLng == null)
            {
                status = "Failed";
            }
            else
            {
                var q = (from a in db.Tab_users
                         where a.email.Equals(userEmail)
                         select a).SingleOrDefault();
                Tbl_Address tAddress = new Tbl_Address();
                tAddress.Address = address;
                tAddress.addressLatLng = latLng;
                tAddress.userEmail = q.email;
                tAddress.Floor = floor;
                tAddress.apartmentName = apartmentName;
                tAddress.Unit = unit;
                tAddress.addressType = addressType;
                db.Tbl_Address.Add(tAddress);
                if (db.SaveChanges() > 0)
                    status = "Success";
                else
                    status = "Failed";
            }
            return status;
        }
        public JsonResult ShowUserAddressAPI(string userPhone, string userEmail)
        {
            if (userPhone == null)
            {
                var jsondata = Json(0, JsonRequestBehavior.AllowGet);
                return jsondata;
            }
            else
            {
                var q = (from a in db.Tbl_Address
                         where a.Tab_users.mob_phone.Equals(userPhone) && a.userEmail.Equals(userEmail)
                         select a).ToList();
                List<Tbl_Address> lstAddress = new List<Tbl_Address>();
                foreach (var item in q)
                {
                    Tbl_Address tAddress = new Tbl_Address();
                    tAddress.Address = item.Address;
                    tAddress.addressLatLng = item.addressLatLng;
                    tAddress.ID = item.ID;
                    tAddress.userEmail = item.userEmail;
                    tAddress.addressType = item.addressType;
                    tAddress.apartmentName = item.apartmentName;
                    tAddress.Floor = item.Floor;
                    tAddress.Unit = item.Unit;
                    lstAddress.Add(tAddress);
                }
                var jsondata = Json(lstAddress, JsonRequestBehavior.AllowGet);
                return jsondata;
            }

        }
        public String DeleteUserAddressAPI(int id)
        {
            string status;
            if (id == null)
            {
                status = "Failed";
            }
            else
            {
                var q = (from a in db.Tbl_Address
                         where a.ID.Equals(id)
                         select a).SingleOrDefault();

                db.Tbl_Address.Remove(q);
                if (db.SaveChanges() > 0)
                    status = "Success";
                else
                    status = "Failed";
            }
            return status;

        }
        public String EditUserAddressAPI(int id, string address, string addressType, string addressLatLng, string apartmentName, string floor, string unit)
        {
            string status;
            if (id == null || address == null || addressType == null || addressLatLng == null)
            {
                status = "Failed";
            }
            else
            {
                var q = (from a in db.Tbl_Address
                         where a.ID.Equals(id)
                         select a).SingleOrDefault();
                q.Address = address;
                q.addressLatLng = addressLatLng;
                q.addressType = addressType;
                q.apartmentName = apartmentName;
                q.Floor = floor;
                q.Unit = unit;
                db.Tbl_Address.Attach(q);
                db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                if (db.SaveChanges() > 0)
                    status = "Success";
                else
                    status = "Failed";
            }
            return status;
        }
        public JsonResult ShowAlbumAPI(int id)
        {
            var q = (from a in db.Tbl_FoodAlbume
                     where a.foodID.Equals(id)
                     select a).ToList();
            List<Tbl_FoodAlbume> lstFoodAlbum = new List<Tbl_FoodAlbume>();
            foreach (var item in q)
            {
                Tbl_FoodAlbume tAlbum = new Tbl_FoodAlbume();
                tAlbum.albumImage = item.albumImage;
                tAlbum.albumName = item.albumName;
                tAlbum.foodID = item.foodID;
                tAlbum.ID = item.ID;
                lstFoodAlbum.Add(tAlbum);
            }
            var jsondata = Json(lstFoodAlbum, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public String EditUserProfileAPI(string email, string name, string sex, string birthDate, string family)
        {
            string status;
            var q = (from a in db.Tab_users
                     where a.email.Equals(email)
                     select a).SingleOrDefault();
            q.birth_date = birthDate;
            q.name = name;
            q.Family = family;
            q.sex = sex;
            db.Tab_users.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (db.SaveChanges() > 0)
                status = "Success";
            else
                status = "Failed";
            return status;
        }
        public JsonResult ShowUserProfileAPI(string userEmail)
        {
            var q = (from a in db.Tab_users
                     where a.email.Equals(userEmail)
                     select a).SingleOrDefault();

            Tab_users t = new Tab_users();
            t.id = q.id;
            t.access = q.access;
            t.activeCode = q.activeCode;
            t.birth_date = q.birth_date;
            t.description = q.description;
            t.email = q.email;
            t.home_address = q.home_address;
            t.home_phone = q.home_phone;
            if (q.sex.Equals("خانوم"))
                t.image = "women.png";
            else
                t.image = "men.png";
            t.IMEI = q.IMEI;
            t.mob_phone = q.mob_phone;
            t.name = q.name;
            t.Family = q.Family;
            t.ncode = q.ncode;
            t.password = q.password;
            t.sex = q.sex;
            t.SMS_Enable = q.SMS_Enable;
            var jsondata = Json(t, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult ShowFoodItemGroupAPI()
        {
            var q = (from a in db.Tbl_FoodItemGroup
                     select a).ToList();
            List<Tbl_FoodItemGroup> lstItemGroup = new List<Tbl_FoodItemGroup>();
            foreach (var item in q)
            {
                Tbl_FoodItemGroup tItemGroup = new Tbl_FoodItemGroup();
                tItemGroup.ID = item.ID;
                tItemGroup.GroupName = item.GroupName;
                lstItemGroup.Add(tItemGroup);
            }
            var jsondata = Json(lstItemGroup, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult ShowFoodPackingAPI(int foodID)
        {
            var q = (from a in db.Tbl_Packing
                     where a.foodID.Equals(foodID)
                     select a).ToList();
            List<Tbl_Packing> lstPacking = new List<Tbl_Packing>();
            foreach (var item in q)
            {
                Tbl_Packing tPacking = new Tbl_Packing();
                tPacking.foodID = item.foodID;
                tPacking.ID = item.ID;
                tPacking.packingDescription = item.packingDescription;
                tPacking.packingImage = item.packingImage;
                lstPacking.Add(tPacking);
            }
            var jsondata = Json(lstPacking, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult ShowResMapAPI()
        {
            var q = (from a in db.Tbl_Restaurant
                     select a).ToList();

            List<Tbl_Restaurant> LstRestuarant = new List<Tbl_Restaurant>();
            foreach (var item in q)
            {
                Tbl_Restaurant t = new Tbl_Restaurant();
                t.ID = item.ID;
                t.resAddress = item.resAddress;
                t.resAvgServiceTime = item.resAvgServiceTime;
                t.resImage = item.resImage;
                t.resLatLng = item.resLatLng;
                t.resName = item.resName;
                t.resPhone = item.resPhone;
                t.resPoints = item.resPoints;
                t.StudentRes = item.StudentRes;
                t.ResBusinessLicense = item.ResBusinessLicense;
                t.resEnable = item.resEnable;
                t.resType = item.resType;
                t.userEmail = item.userEmail;
                t.FirstTime = item.FirstTime;
                t.FoodAvragePrice = item.FoodAvragePrice;
                t.isGetOrder = item.isGetOrder;
                t.SecendTime = item.SecendTime;


                LstRestuarant.Add(t);
                //db.Tbl_Restaurant.Add(t);
            }

            var jsondata = Json(LstRestuarant, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AllFloristAPI()
        {
            var q = (from a in db.Tbl_Floriest
                     select a).ToList();

            List<Tbl_Floriest> LstRestuarant = new List<Tbl_Floriest>();
            foreach (var item in q)
            {
                Tbl_Floriest t = new Tbl_Floriest();
                t.ID = item.ID;
                t.FAddress = item.FAddress;
                t.FImage = item.FImage;
                t.FLatLng = item.FLatLng;
                t.FName = item.FName;
                t.FPhone = item.FPhone;
                t.userEmail = item.userEmail;

                LstRestuarant.Add(t);
            }

            var jsondata = Json(LstRestuarant, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult SortPriceTopToDown()
        {
            var q = (from a in db.tab_products
                     select a).OrderByDescending(s => s.cost).ToList();
            List<FoodFillter> LstFood = new List<FoodFillter>();
            foreach (var item in q)
            {
                FoodFillter t = new FoodFillter();
                t.id = item.id;
                t.cost = item.cost;
                t.name = item.name;
                t.foodImage = item.foodImage;
                var w = (from a in db.Tbl_Restaurant
                         where a.ID.Equals(item.resID)
                         select a).SingleOrDefault();
                t.restuarant = w.resName;
                var e = (from a in db.Tbl_Menu
                         where a.menuID.Equals(item.menuID)
                         select a).SingleOrDefault();
                t.menuName = e.menuTitle;
                LstFood.Add(t);

            }
            var jsondata = Json(LstFood, JsonRequestBehavior.AllowGet);
            return jsondata;

        }
        public JsonResult SortPriceDownToTop()
        {
            var q = (from a in db.tab_products
                     select a).OrderBy(s => s.cost).ToList();
            List<FoodFillter> LstFood = new List<FoodFillter>();
            foreach (var item in q)
            {
                FoodFillter t = new FoodFillter();
                t.id = item.id;
                t.cost = item.cost;
                t.name = item.name;
                t.foodImage = item.foodImage;
                var w = (from a in db.Tbl_Restaurant
                         where a.ID.Equals(item.resID)
                         select a).SingleOrDefault();
                t.restuarant = w.resName;
                var e = (from a in db.Tbl_Menu
                         where a.menuID.Equals(item.menuID)
                         select a).SingleOrDefault();
                t.menuName = e.menuTitle;
                LstFood.Add(t);

            }
            var jsondata = Json(LstFood, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult ResSearch(string resName)
        {
            var q = (from a in db.Tbl_Restaurant
                     where a.resName.Equals(resName)
                     select a).ToList();

            List<Tbl_Restaurant> LstRestuarant = new List<Tbl_Restaurant>();
            foreach (var item in q)
            {
                Tbl_Restaurant t = new Tbl_Restaurant();
                t.ID = item.ID;
                t.resAddress = item.resAddress;
                t.resAvgServiceTime = item.resAvgServiceTime;
                t.resImage = item.resImage;
                t.resLatLng = item.resLatLng;
                t.resName = item.resName;
                t.resPhone = item.resPhone;
                t.resPoints = item.resPoints;
                t.StudentRes = item.StudentRes;
                t.ResBusinessLicense = item.ResBusinessLicense;
                t.resEnable = item.resEnable;
                t.resType = item.resType;
                t.userEmail = item.userEmail;
                t.FirstTime = item.FirstTime;
                t.FoodAvragePrice = item.FoodAvragePrice;
                t.isGetOrder = item.isGetOrder;
                t.SecendTime = item.SecendTime;
                LstRestuarant.Add(t);

            }

            var jsondata = Json(LstRestuarant, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult ResFoodAPI()
        {
            var q = (from a in db.tab_products
                     where a.menuID.Equals(7) || a.menuID.Equals(8) || a.menuID.Equals(9)
                     select a).ToList();

            List<FoodFillter> LstFood = new List<FoodFillter>();
            foreach (var item in q)
            {
                FoodFillter t = new FoodFillter();
                t.id = item.id;
                t.cost = item.cost;
                t.name = item.name;
                t.FoodCount = item.FoodCount.Value;
                t.OrderType = item.OrderType.Value;
                t.foodImage = item.foodImage;
                var w = (from a in db.Tbl_Restaurant
                         where a.ID.Equals(item.resID)
                         select a).SingleOrDefault();
                t.restuarant = w.resName;
                t.resID = w.ID.ToString();
                var e = (from a in db.Tbl_Menu
                         where a.menuID.Equals(item.menuID)
                         select a).SingleOrDefault();
                t.menuName = e.menuTitle;
                LstFood.Add(t);

            }
            var jsondata = Json(LstFood, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult FastFoodFilterApi()
        {
            var q = (from a in db.tab_products
                     where a.menuID.Equals(1) || a.menuID.Equals(2) || a.menuID.Equals(3) || a.menuID.Equals(4) || a.menuID.Equals(5) || a.menuID.Equals(6)
                     select a).ToList();

            List<FoodFillter> LstFood = new List<FoodFillter>();
            foreach (var item in q)
            {
                FoodFillter t = new FoodFillter();
                t.id = item.id;
                t.cost = item.cost;
                t.name = item.name;
                t.FoodCount = item.FoodCount.Value;
                t.OrderType = item.OrderType.Value;
                t.foodImage = item.foodImage;
                var w = (from a in db.Tbl_Restaurant
                         where a.ID.Equals(item.resID)
                         select a).SingleOrDefault();
                t.restuarant = w.resName;
                var e = (from a in db.Tbl_Menu
                         where a.menuID.Equals(item.menuID)
                         select a).SingleOrDefault();
                t.menuName = e.menuTitle;
                LstFood.Add(t);

            }
            var jsondata = Json(LstFood, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult CaffiShopFoodAPI()
        {
            var q = (from a in db.tab_products
                     where a.menuID.Equals(10) || a.menuID.Equals(11) || a.menuID.Equals(12) || a.menuID.Equals(14) || a.menuID.Equals(15) || a.menuID.Equals(16)
                     select a).ToList();

            List<FoodFillter> LstFood = new List<FoodFillter>();
            foreach (var item in q)
            {
                FoodFillter t = new FoodFillter();
                t.id = item.id;
                t.cost = item.cost;
                t.name = item.name;
                t.FoodCount = item.FoodCount.Value;
                t.OrderType = item.OrderType.Value;
                t.foodImage = item.foodImage;
                var w = (from a in db.Tbl_Restaurant
                         where a.ID.Equals(item.resID)
                         select a).SingleOrDefault();
                t.restuarant = w.resName;
                var e = (from a in db.Tbl_Menu
                         where a.menuID.Equals(item.menuID)
                         select a).SingleOrDefault();
                t.menuName = e.menuTitle;
                LstFood.Add(t);

            }
            var jsondata = Json(LstFood, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public ActionResult nearRestuarant()
        {
            return View();
        }
        public JsonResult ShowFlorestList()
        {
            var q = (from a in db.Tbl_Floriest
                     select a).ToList();
            List<Tbl_Floriest> lstPacking = new List<Tbl_Floriest>();
            foreach (var item in q)
            {
                Tbl_Floriest tFloriest = new Tbl_Floriest();
                tFloriest.FAddress = item.FAddress;
                tFloriest.FImage = item.FImage;
                tFloriest.FLatLng = item.FLatLng;
                tFloriest.FName = item.FName;
                tFloriest.FPhone = item.FPhone;
                tFloriest.userEmail = item.userEmail;
                tFloriest.ID = item.ID;

                lstPacking.Add(tFloriest);
            }
            var jsondata = Json(lstPacking, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult ShowFlowerList(int id)
        {
            var q = (from a in db.Tbl_Flower
                     where a.FlorestID.Equals(id) && a.FlwType.Equals("شاخه گل")
                     select a).ToList();
            List<Tbl_Flower> lstPacking = new List<Tbl_Flower>();
            foreach (var item in q)
            {
                Tbl_Flower tFlower = new Tbl_Flower();
                tFlower.FlwColor = item.FlwColor;
                tFlower.FlorestID = item.FlorestID;
                tFlower.FlwMaintenance = item.FlwMaintenance;
                tFlower.FlwName = item.FlwName;
                tFlower.FlwPrice = item.FlwPrice;
                tFlower.FlwType = item.FlwType;
                tFlower.FlwImage = item.FlwImage;
                tFlower.ID = item.ID;

                lstPacking.Add(tFlower);
            }
            var jsondata = Json(lstPacking, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult FirstShowFlowerList(int id)
        {
            var q = (from a in db.Tbl_Flower
                     where a.FlorestID.Equals(id) && a.FlwType.Equals("دسته گل")
                     select a).ToList();
            List<Tbl_Flower> lstPacking = new List<Tbl_Flower>();
            foreach (var item in q)
            {
                Tbl_Flower tFlower = new Tbl_Flower();
                tFlower.FlwColor = item.FlwColor;
                tFlower.FlorestID = item.FlorestID;
                tFlower.FlwMaintenance = item.FlwMaintenance;
                tFlower.FlwName = item.FlwName;
                tFlower.FlwPrice = item.FlwPrice;
                tFlower.FlwType = item.FlwType;
                tFlower.FlwImage = item.FlwImage;
                tFlower.ID = item.ID;

                lstPacking.Add(tFlower);
            }
            var jsondata = Json(lstPacking, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult SecendShowFlowerList(int id)
        {
            var q = (from a in db.Tbl_Flower
                     where a.FlorestID.Equals(id) && a.FlwType.Equals("مصنوعی")
                     select a).ToList();
            List<Tbl_Flower> lstPacking = new List<Tbl_Flower>();
            foreach (var item in q)
            {
                Tbl_Flower tFlower = new Tbl_Flower();
                tFlower.FlwColor = item.FlwColor;
                tFlower.FlorestID = item.FlorestID;
                tFlower.FlwMaintenance = item.FlwMaintenance;
                tFlower.FlwName = item.FlwName;
                tFlower.FlwPrice = item.FlwPrice;
                tFlower.FlwType = item.FlwType;
                tFlower.FlwImage = item.FlwImage;
                tFlower.ID = item.ID;

                lstPacking.Add(tFlower);
            }
            var jsondata = Json(lstPacking, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult TirthShowFlowerList(int id)
        {
            var q = (from a in db.Tbl_Flower
                     where a.FlorestID.Equals(id) && a.FlwType.Equals("گلدان")
                     select a).ToList();
            List<Tbl_Flower> lstPacking = new List<Tbl_Flower>();
            foreach (var item in q)
            {
                Tbl_Flower tFlower = new Tbl_Flower();
                tFlower.FlwColor = item.FlwColor;
                tFlower.FlorestID = item.FlorestID;
                tFlower.FlwMaintenance = item.FlwMaintenance;
                tFlower.FlwName = item.FlwName;
                tFlower.FlwPrice = item.FlwPrice;
                tFlower.FlwType = item.FlwType;
                tFlower.FlwImage = item.FlwImage;
                tFlower.ID = item.ID;

                lstPacking.Add(tFlower);
            }
            var jsondata = Json(lstPacking, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult FourShowFlowerList(int id)
        {
            var q = (from a in db.Tbl_Flower
                     where a.FlorestID.Equals(id) && a.FlwType.Equals("درختچه")
                     select a).ToList();
            List<Tbl_Flower> lstPacking = new List<Tbl_Flower>();
            foreach (var item in q)
            {
                Tbl_Flower tFlower = new Tbl_Flower();
                tFlower.FlwColor = item.FlwColor;
                tFlower.FlorestID = item.FlorestID;
                tFlower.FlwMaintenance = item.FlwMaintenance;
                tFlower.FlwName = item.FlwName;
                tFlower.FlwPrice = item.FlwPrice;
                tFlower.FlwType = item.FlwType;
                tFlower.FlwImage = item.FlwImage;
                tFlower.ID = item.ID;

                lstPacking.Add(tFlower);
            }
            var jsondata = Json(lstPacking, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult ShowFlowerDetails(int id)
        {
            var q = (from a in db.Tbl_Flower
                     where a.ID.Equals(id)
                     select a).SingleOrDefault();

            Tbl_Flower tFloriest = new Tbl_Flower();
            tFloriest.FlorestID = q.FlorestID;
            tFloriest.FlwColor = q.FlwColor;
            tFloriest.FlwImage = q.FlwImage;
            tFloriest.FlwMaintenance = q.FlwMaintenance;
            tFloriest.FlwName = q.FlwName;
            tFloriest.FlwPrice = q.FlwPrice;
            tFloriest.FlwType = q.FlwType;
            tFloriest.ID = q.ID;

            var jsondata = Json(tFloriest, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult SingleUser(string email)
        {
            var q = (from a in db.Tab_users
                     where a.email.Equals(email)
                     select a).SingleOrDefault();

            Tab_users user = new Tab_users();

            user.access = q.access;
            user.activeCode = q.activeCode;
            user.birth_date = q.birth_date;
            user.description = q.description;
            user.email = q.email;
            user.enable = q.enable;
            user.home_address = q.home_address;
            user.home_phone = q.home_phone;
            user.id = q.id;
            user.image = q.image;
            user.IMEI = q.IMEI;
            user.mob_phone = q.mob_phone;
            user.name = q.name;
            user.Family = q.Family;
            user.ncode = q.ncode;
            user.password = q.password;
            user.sex = q.sex;
            user.SMS_Enable = q.SMS_Enable;


            var jsondata = Json(user, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public int FlowerComment(string text, int star, string phone, int id)
        {

            int result = 0;

            var q = (from a in db.Tab_users
                     where a.mob_phone.Equals(phone)
                     select a).FirstOrDefault();

            Tbl_FlowerComment t = new Tbl_FlowerComment();

            t.IP = "1111";
            t.Name = q.name + " " + q.Family;
            t.Phone = phone;
            t.flowerID = id;
            t.read = false;
            t.Stars = star;
            t.Text = text;
            t.confirm = false;
            t.commentDate = DateTime.Now;
            t.Email = q.email;

            db.Tbl_FlowerComment.Add(t);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                result = 1;
            }
            else
            {
                result = 2;
            }
            return result;
        }
        public JsonResult ShowFlowerComment(int id)
        {
            var q = (from a in db.Tbl_FlowerComment
                     where a.flowerID.Equals(id)
                     select a).ToList();
            List<Tbl_FlowerComment> LstFlower = new List<Tbl_FlowerComment>();

            foreach (var item in q)
            {
                Tbl_FlowerComment t = new Tbl_FlowerComment();
                t.Name = item.Name;
                t.flowerID = item.flowerID;
                t.Phone = item.Phone;
                t.read = item.read;
                t.Stars = item.Stars;
                t.Text = item.Text;
                t.confirm = item.confirm;
                t.commentDate = item.commentDate;
                t.Email = item.Email;
                t.ID = item.ID;
                t.IP = item.IP;
                LstFlower.Add(t);

            }


            var jsondata = Json(LstFlower, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult ShowConfectionaryList()
        {
            var q = (from a in db.Tbl_Confectionary
                     select a).ToList();
            List<Tbl_Confectionary> lstConfectionary = new List<Tbl_Confectionary>();
            foreach (var item in q)
            {
                Tbl_Confectionary tConfectionary = new Tbl_Confectionary();
                tConfectionary.CName = item.CName;
                tConfectionary.CImage = item.CImage;
                tConfectionary.CLatLng = item.CLatLng;
                tConfectionary.CAddress = item.CAddress;
                tConfectionary.CPhone = item.CPhone;
                tConfectionary.userEmail = item.userEmail;
                tConfectionary.ID = item.ID;

                lstConfectionary.Add(tConfectionary);
            }
            var jsondata = Json(lstConfectionary, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult SweetsList(int id)
        {
            var q = (from a in db.Tbl_Sweets
                     where a.ConfectionaryID.Equals(id)
                     select a).ToList();
            List<Tbl_Sweets> lstSweetsList = new List<Tbl_Sweets>();
            foreach (var item in q)
            {
                Tbl_Sweets tSweets = new Tbl_Sweets();
                tSweets.S_Name = item.S_Name;
                tSweets.S_Type = item.S_Type;
                tSweets.S_Price = item.S_Price;
                tSweets.S_Description = item.S_Description;
                tSweets.ConfectionaryID = item.ConfectionaryID;
                tSweets.SImage = item.SImage;
                tSweets.ID = item.ID;

                lstSweetsList.Add(tSweets);
            }
            var jsondata = Json(lstSweetsList, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public int CommentSweetsAPI(int SweetsId, int stars, string phone, string text)
        {
            var q = (from a in db.Tab_users
                     where a.mob_phone.Equals(phone)

                     select a).FirstOrDefault();


            Tbl_SweetsComment comments = new Tbl_SweetsComment();

            comments.SweetsID = SweetsId;
            comments.Name = q.name + " " + q.Family;
            comments.Email = q.email;
            comments.IP = "11111";
            comments.Phone = phone;
            comments.confirm = false;
            comments.CommentDate = DateTime.Now;
            comments.read = false;
            comments.Star = stars;
            comments.Text = text;

            db.Tbl_SweetsComment.Add(comments);
            db.SaveChanges();

            return 1;
        }
        public JsonResult CommentSweetsListAPI(int id)
        {
            var q = (from a in db.Tbl_SweetsComment
                     where a.SweetsID.Equals(id)
                     select a).ToList();

            List<Tbl_SweetsComment> LstCommmt = new List<Tbl_SweetsComment>();
            foreach (var item in q)
            {
                Tbl_SweetsComment tSweetsComment = new Tbl_SweetsComment();

                tSweetsComment.confirm = item.confirm;
                tSweetsComment.CommentDate = item.CommentDate;
                tSweetsComment.Email = item.Email;
                tSweetsComment.ID = item.ID;
                tSweetsComment.IP = item.IP;
                tSweetsComment.Name = item.Name;
                tSweetsComment.Phone = item.Phone;
                tSweetsComment.SweetsID = item.SweetsID;
                tSweetsComment.Star = item.Star;
                tSweetsComment.Text = item.Text;


                LstCommmt.Add(tSweetsComment);
                // db.Tab_comments.Add(t);
            }

            var jsondata = Json(LstCommmt, JsonRequestBehavior.AllowGet);
            return jsondata;

        }
        public JsonResult ShowSweetsDetails(int id)
        {
            var q = (from a in db.Tbl_Sweets
                     where a.ID.Equals(id)
                     select a).SingleOrDefault();

            Tbl_Sweets tSweets = new Tbl_Sweets();
            tSweets.ConfectionaryID = q.ConfectionaryID;
            tSweets.S_Type = q.S_Type;
            tSweets.SImage = q.SImage;
            tSweets.S_Name = q.S_Name;
            tSweets.S_Price = q.S_Price;
            tSweets.S_Description = q.S_Description;
            tSweets.ID = q.ID;

            var jsondata = Json(tSweets, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult OrderFactorListAPI(string userEmail)
        {
            int sum = 0;
            var q = (from a in db.Tbl_OrderFactor
                     where a.UserEmail.Equals(userEmail)
                     select a).ToList();

            List<MyOrder> lstOrder = new List<MyOrder>();
            foreach (var item in q)
            {
                var qResName = (from a in db.Tbl_Restaurant
                                where a.ID.Equals(item.ResID)
                                select a).SingleOrDefault();

                var qFoodList = (from a in db.Tbl_OrderFactorItem
                                 where a.FactorID.Equals(item.ID)
                                 select a).ToList();
                foreach (var item2 in qFoodList)
                {
                    sum += item2.tab_products.cost * item2.FoodCount;
                }
                MyOrder order = new MyOrder();
                order.ID = item.ID;
                order.resImage = qResName.resImage;
                order.Status = item.OrderStatus;
                order.resName = qResName.resName;
                order.OrderPrice = sum;
                order.OrderDate = DateTime.Now.Date.ToPersianDateString();
                order.OrderTime = DateTime.Now.ToString("t");
                lstOrder.Add(order);
            }

            var jsondata = Json(lstOrder, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult OrderItemListAPI(int factorID)
        {
            var q = (from a in db.Tbl_OrderFactorItem
                     where a.FactorID.Equals(factorID)
                     select a).ToList();

            List<MyOrderItem> lstOrder = new List<MyOrderItem>();
            foreach (var item in q)
            {
                var qFoodName = (from a in db.tab_products
                                 where a.id.Equals(item.FoodID)
                                 select a).SingleOrDefault();

                MyOrderItem order = new MyOrderItem();

                order.ID = item.ID;
                order.OrderCount = item.FoodCount;
                order.OrderName = qFoodName.name;
                order.OrderImage = qFoodName.foodImage;

                lstOrder.Add(order);
            }

            var jsondata = Json(lstOrder, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult ResTableListAPI(int resID)
        {
            var q = (from a in db.Tbl_ResTables
                     where a.ResID.Equals(resID)
                     select a).ToList();

            List<Tbl_ResTables> lstTable = new List<Tbl_ResTables>();
            foreach (var item in q)
            {
                Tbl_ResTables resTables = new Tbl_ResTables();
                resTables.ID = item.ID;
                resTables.ResID = item.ResID;
                resTables.TableCapacity = item.TableCapacity;
                resTables.TableNumber = item.TableNumber;
                lstTable.Add(resTables);
            }

            var jsondata = Json(lstTable, JsonRequestBehavior.AllowGet);
            return jsondata;

        }
        //=======================Flower Filter======================================
        public JsonResult FlowerListForSpinnerAPI(int id)
        {
            var q = (from a in db.Tbl_Flower
                     where a.ID.Equals(id)
                     select a).SingleOrDefault();

            var qFlower = (from a in db.Tbl_Flower
                           where a.FlorestID.Equals(q.ID) && a.FlwType.Equals(q.FlwType)
                           select a).ToList();

            List<Tbl_Flower> lstFlower = new List<Tbl_Flower>();

            foreach (var item in qFlower)
            {
                Tbl_Flower f = new Tbl_Flower();
                f.FlorestID = item.FlorestID;
                f.FlwColor = item.FlwColor;
                f.FlwImage = item.FlwImage;
                f.FlwMaintenance = item.FlwMaintenance;
                f.FlwName = item.FlwName;
                f.FlwPrice = item.FlwPrice;
                f.FlwType = item.FlwType;
                f.ID = item.ID;

                lstFlower.Add(f);
            }
            var jsondata = Json(lstFlower, JsonRequestBehavior.AllowGet);
            return jsondata;

        }
        //=======================Res Reservation======================================
        public int ResReservationAPI(int resID, string Date, string Time, int tableID, string userEmail)
        {

            Tbl_ResReservationFactor rrf = new Tbl_ResReservationFactor();

            rrf.Date = Date;
            rrf.ResID = resID;
            rrf.TableID = tableID;
            rrf.Time = Time;
            rrf.UserEmail = userEmail;

            db.Tbl_ResReservationFactor.Add(rrf);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return rrf.ID;
            }
            else
            {
                return 0;
            }
        }
        public void ResReservationListAPI(int foodID, int foodCount, int factorID)
        {
            Tbl_ResReservationFactorItem rrfi = new Tbl_ResReservationFactorItem();
            rrfi.FactorID = factorID;
            rrfi.FoodCount = foodCount;
            rrfi.FoodID = foodID;
            db.Tbl_ResReservationFactorItem.Add(rrfi);
            db.SaveChanges();
        }
        public JsonResult SweetReservedListAPI(string userEmail)
        {
            int sum = 0;
            var q = (from a in db.Tbl_SweetReservationFactor
                     where a.UserEmail.Equals(userEmail)
                     select a).ToList();



            List<MySweetReserved> lstOrder = new List<MySweetReserved>();
            foreach (var item in q)
            {
                var qResName = (from a in db.Tbl_Confectionary
                                where a.ID.Equals(item.ConfectionaryID)
                                select a).SingleOrDefault();

                var qSweetType = (from a in db.Tbl_SweetReservationFactorList
                                  where a.FoodFactorID.Equals(item.ID)
                                  select a).FirstOrDefault();

                MySweetReserved order = new MySweetReserved();
                order.ID = item.ID;
                order.sweetImage = qResName.CImage;
                order.orderType = "fff";
                order.sweetName = qResName.CName;
                //order.totalPrice = Convert.ToInt32(qSweetType.Tbl_SweetReservationFactor.Tbl_Swee.S_Price);
                order.orderDate = item.Date;
                order.orderTime = item.Time;
                order.sweetWeight = item.OrderWeight;
                lstOrder.Add(order);
            }

            var jsondata = Json(lstOrder, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult SweetReservedItemListAPI(int factorID)
        {
            var q = (from a in db.Tbl_SweetReservationFactorList
                     where a.FoodFactorID.Equals(factorID)
                     select a).ToList();

            List<MySweetReservedItem> lstOrder = new List<MySweetReservedItem>();
            foreach (var item in q)
            {
                var qFoodName = (from a in db.Tbl_Sweets
                                 where a.ID.Equals(item.FoodID)
                                 select a).SingleOrDefault();

                MySweetReservedItem order = new MySweetReservedItem();

                order.ID = item.ID;
                order.sweetName = qFoodName.S_Name;
                order.sweetImage = qFoodName.SImage;

                lstOrder.Add(order);
            }

            var jsondata = Json(lstOrder, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public int FoodReservationAPI(int resID, string Date, string Time, string userEmail, string orderType)
        {
            Tbl_FoodReservationFactor rrf = new Tbl_FoodReservationFactor();

            rrf.Date = Date;
            rrf.ResID = resID;
            rrf.Time = Time;
            rrf.UserEmail = userEmail;
            rrf.OrderTyoe = orderType;

            db.Tbl_FoodReservationFactor.Add(rrf);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return rrf.ID;
            }
            else
            {
                return 2;
            }
        }
        public void FoodReservationListAPI(int foodID, int foodCount, int factorID)
        {
            Tbl_FoodReservationFactorList rrfi = new Tbl_FoodReservationFactorList();
            rrfi.FoodFactorID = factorID;
            rrfi.FoodCount = foodCount;
            rrfi.FoodID = foodID;
            db.Tbl_FoodReservationFactorList.Add(rrfi);
            db.SaveChanges();
        }
        public int SweetReservationAPI(int resID, string Date, string Time, string userEmail, string orderType, string orderWeight)
        {
            Tbl_SweetReservationFactor rrf = new Tbl_SweetReservationFactor();

            rrf.Date = Date;
            rrf.ConfectionaryID = resID;
            rrf.Time = Time;
            rrf.UserEmail = userEmail;
            rrf.OrderTyoe = orderType;
            rrf.OrderWeight = orderWeight;

            db.Tbl_SweetReservationFactor.Add(rrf);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return rrf.ID;
            }
            else
            {
                return 2;
            }
        }
        public void SweetReservationListAPI(int foodID, int factorID)
        {
            Tbl_SweetReservationFactorList rrfi = new Tbl_SweetReservationFactorList();
            rrfi.FoodFactorID = factorID;
            rrfi.FoodID = foodID;
            db.Tbl_SweetReservationFactorList.Add(rrfi);
            db.SaveChanges();
        }
        public JsonResult SweetListApi(int id, string type)
        {

            var qSweetList = (from a in db.Tbl_Sweets
                              where a.ConfectionaryID.Equals(id) && a.S_Type.Equals(type)
                              select a).ToList();

            List<Tbl_Sweets> lstSweetsList = new List<Tbl_Sweets>();
            foreach (var item in qSweetList)
            {
                Tbl_Sweets tSweets = new Tbl_Sweets();

                tSweets.ID = item.ID;
                tSweets.SImage = item.SImage;
                tSweets.S_Description = item.S_Description;
                tSweets.S_Name = item.S_Name;
                tSweets.S_Price = item.S_Price;
                tSweets.S_Type = item.S_Type;
                tSweets.ConfectionaryID = item.ConfectionaryID;


                lstSweetsList.Add(tSweets);
            }
            var jsondata = Json(lstSweetsList, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult ResFoodListSpinner(string resName)
        {
            var qFindResID = (from a in db.Tbl_Restaurant
                              where a.resName.Equals(resName)
                              select a).FirstOrDefault();

            var q = (from a in db.tab_products
                     where a.resID.Equals(qFindResID.ID)
                     select a).ToList();
            List<tab_products> lstSweetsList = new List<tab_products>();
            foreach (var item in q)
            {
                tab_products tSweets = new tab_products();
                tSweets.bakingTime = item.bakingTime;
                tSweets.cost = item.cost;
                tSweets.CreateMaterial = item.CreateMaterial;
                tSweets.foodImage = item.foodImage;
                tSweets.menuID = item.menuID;
                tSweets.name = item.name;
                tSweets.Recipe = item.Recipe;
                tSweets.resID = item.resID;
                tSweets.User_Email = item.User_Email;

                lstSweetsList.Add(tSweets);
            }
            var jsondata = Json(lstSweetsList, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult ResFoodListAPI(string resName)
        {
            var qFindResID = (from a in db.Tbl_Restaurant
                              where a.resName.Equals(resName)
                              select a).FirstOrDefault();

            var q = (from a in db.tab_products
                     where a.resID.Equals(qFindResID.ID)
                     select a).ToList();
            List<tab_products> lstSweetsList = new List<tab_products>();
            foreach (var item in q)
            {
                tab_products tSweets = new tab_products();
                tSweets.bakingTime = item.bakingTime;
                tSweets.cost = item.cost;
                tSweets.CreateMaterial = item.CreateMaterial;
                tSweets.foodImage = item.foodImage;
                tSweets.menuID = item.menuID;
                tSweets.name = item.name;
                tSweets.Recipe = item.Recipe;
                tSweets.resID = item.resID;
                tSweets.User_Email = item.User_Email;
                tSweets.id = item.id;

                lstSweetsList.Add(tSweets);
            }
            var jsondata = Json(lstSweetsList, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public int FlowerReservationAPI(int resID, string Date, string Time, string userEmail, string orderType)
        {
            Tbl_FlowerReservationFactor rrf = new Tbl_FlowerReservationFactor();

            rrf.Date = Date;
            rrf.FloristID = resID;
            rrf.Time = Time;
            rrf.UserEmail = userEmail;
            rrf.OrderTyoe = orderType;

            db.Tbl_FlowerReservationFactor.Add(rrf);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return rrf.ID;
            }
            else
            {
                return 2;
            }
        }
        public void FlowerReservationListAPI(int foodID, int foodCount, int factorID)
        {
            Tbl_FlowerReservationFactorList rrfi = new Tbl_FlowerReservationFactorList();
            rrfi.FoodFactorID = factorID;
            rrfi.FoodCount = foodCount;
            rrfi.FoodID = foodID;
            db.Tbl_FlowerReservationFactorList.Add(rrfi);
            db.SaveChanges();
        }
        public JsonResult ShowFlowerListApi(int id)
        {
            var q = (from a in db.Tbl_Flower
                     where a.FlorestID.Equals(id)
                     select a).ToList();
            List<Tbl_Flower> lstPacking = new List<Tbl_Flower>();
            foreach (var item in q)
            {
                Tbl_Flower tFlower = new Tbl_Flower();
                tFlower.FlwColor = item.FlwColor;
                tFlower.FlorestID = item.FlorestID;
                tFlower.FlwMaintenance = item.FlwMaintenance;
                tFlower.FlwName = item.FlwName;
                tFlower.FlwPrice = item.FlwPrice;
                tFlower.FlwType = item.FlwType;
                tFlower.FlwImage = item.FlwImage;
                tFlower.ID = item.ID;

                lstPacking.Add(tFlower);
            }
            var jsondata = Json(lstPacking, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        //=======================Res Reservation==================================
        public JsonResult FloristSearchAPI(string floristName)
        {
            var q = (from a in db.Tbl_Floriest
                     where a.FName.Contains(floristName)
                     select a).ToList();

            List<Tbl_Floriest> LstRestuarant = new List<Tbl_Floriest>();
            foreach (var item in q)
            {
                Tbl_Floriest t = new Tbl_Floriest();
                t.ID = item.ID;
                t.FAddress = item.FAddress;
                t.FImage = item.FImage;
                t.FLatLng = item.FLatLng;
                t.FName = item.FName;
                t.FPhone = item.FPhone;
                t.userEmail = item.userEmail;

                LstRestuarant.Add(t);
            }

            var jsondata = Json(LstRestuarant, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult ConfectionerySearchAPI(string floristName)
        {
            var q = (from a in db.Tbl_Confectionary
                     where a.CName.Contains(floristName)
                     select a).ToList();

            List<Tbl_Confectionary> LstRestuarant = new List<Tbl_Confectionary>();
            foreach (var item in q)
            {
                Tbl_Confectionary t = new Tbl_Confectionary();
                t.ID = item.ID;
                t.CAddress = item.CAddress;
                t.CImage = item.CImage;
                t.CLatLng = item.CLatLng;
                t.CName = item.CName;
                t.CPhone = item.CPhone;
                t.userEmail = item.userEmail;

                LstRestuarant.Add(t);
            }

            var jsondata = Json(LstRestuarant, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminCommentListForResItemAPI(int id)
        {
            var q = (from a in db.Tab_comments
                     where a.product_id.Equals(id)
                     select a).ToList();

            List<ResCommentItemList> lstOrder = new List<ResCommentItemList>();

            foreach (var item in q)
            {
                ResCommentItemList c = new ResCommentItemList();

                c.ID = item.id;
                c.Comment = item.text;
                c.FoodId = item.product_id;
                c.Star = item.Stars;

                lstOrder.Add(c);
            }

            var jsondata = Json(lstOrder, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public void test()
        {

        }
        public int AdminAddNewFoodAPI(Food f)
        {
            tab_products t = new tab_products();

            t.bakingTime = f.BakingTime;
            t.cost = Convert.ToInt32(f.Price);
            t.CreateMaterial = f.CreateMaterial;
            t.foodImage = f.Image;
            t.menuID = f.MenuID;
            t.name = f.Name;
            t.Recipe = f.Recepi;
            t.resID = f.ResID;
            t.User_Email = f.UserEmail;
            t.FoodCount = f.FoodCount;
            t.OrderType = f.ordertype;

            db.tab_products.Add(t);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
        public int AdminEditFoodAPI(Food f)
        {
            var q = (from a in db.tab_products
                     where a.id.Equals(f.ID)
                     select a).SingleOrDefault();

            q.bakingTime = f.BakingTime;
            q.cost = Convert.ToInt32(f.Price);
            q.CreateMaterial = f.CreateMaterial;
            q.foodImage = f.Image;
            q.menuID = f.MenuID;
            q.name = f.Name;
            q.Recipe = f.Recepi;
            q.FoodCount = f.FoodCount;
            q.OrderType = f.ordertype;


            db.tab_products.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public JsonResult AdminSignInAPI(string userPhone, string userPassword)
        {
            var q = (from a in db.Tab_users
                     where a.mob_phone.Equals(userPhone) && a.password.Equals(userPassword)
                     select a).SingleOrDefault();

            Tab_users t = new Tab_users();
            t.access = q.access;
            t.activeCode = q.activeCode;
            t.birth_date = q.birth_date;
            t.description = q.description;
            t.email = q.email;
            t.enable = q.enable;
            t.home_address = q.home_address;
            t.home_phone = q.home_phone;
            t.id = q.id;
            t.image = q.image;
            t.IMEI = q.IMEI;
            t.mob_phone = q.mob_phone;
            t.name = q.name;
            t.Family = q.Family;
            t.ncode = q.ncode;
            t.password = q.password;
            t.sex = q.sex;
            t.SMS_Enable = q.SMS_Enable;

            var jsondata = Json(t, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminResInformationAPI(string userEmail)
        {
            var q = (from a in db.Tab_users
                     where a.email.Equals(userEmail)
                     select a).SingleOrDefault();

            string type = q.access;
            if (type.Equals("رستوران"))
            {
                var qResID = (from a in db.Tbl_Restaurant
                              where a.userEmail.Equals(userEmail)
                              select a).SingleOrDefault();

                Tbl_Restaurant t = new Tbl_Restaurant();

                t.ID = qResID.ID;
                t.resAddress = qResID.resAddress;
                t.resAvgServiceTime = qResID.resAvgServiceTime;
                t.resImage = qResID.resImage;
                t.resLatLng = qResID.resLatLng;
                t.resName = qResID.resName;
                t.resPhone = qResID.resPhone;
                t.resPoints = qResID.resPoints;
                t.StudentRes = qResID.StudentRes;
                t.ResBusinessLicense = qResID.ResBusinessLicense;
                t.resEnable = qResID.resEnable;
                t.resType = qResID.resType;
                t.userEmail = qResID.userEmail;
                t.FirstTime = qResID.FirstTime;
                t.FoodAvragePrice = qResID.FoodAvragePrice;
                t.isGetOrder = qResID.isGetOrder;
                t.SecendTime = qResID.SecendTime;

                var jsondata = Json(t, JsonRequestBehavior.AllowGet);
                return jsondata;
            }
            else if (type.Equals("Florist"))
            {
                var qFloristID = (from a in db.Tbl_Floriest
                                  where a.userEmail.Equals(userEmail)
                                  select a).SingleOrDefault();

                Tbl_Floriest t = new Tbl_Floriest();

                t.FAddress = qFloristID.FAddress;
                t.FImage = qFloristID.FImage;
                t.FLatLng = qFloristID.FLatLng;
                t.FName = qFloristID.FName;
                t.FPhone = qFloristID.FPhone;
                t.ID = qFloristID.ID;
                t.userEmail = qFloristID.userEmail;

                var jsondata = Json(t, JsonRequestBehavior.AllowGet);
                return jsondata;
            }
            else
            {
                var qConfectioneryID = (from a in db.Tbl_Confectionary
                                        where a.userEmail.Equals(userEmail)
                                        select a).SingleOrDefault();

                Tbl_Confectionary t = new Tbl_Confectionary();

                t.CAddress = qConfectioneryID.CAddress;
                t.CImage = qConfectioneryID.CImage;
                t.CLatLng = qConfectioneryID.CLatLng;
                t.CName = qConfectioneryID.CName;
                t.CPhone = qConfectioneryID.CPhone;
                t.ID = qConfectioneryID.ID;
                t.userEmail = qConfectioneryID.userEmail;


                var jsondata = Json(t, JsonRequestBehavior.AllowGet);
                return jsondata;
            }
        }
        public JsonResult AdminFoodListAPI(string userEmail, string type)
        {
            if (type.Equals("رستوران"))
            {
                var qResID = (from a in db.Tbl_Restaurant
                              where a.userEmail.Equals(userEmail)
                              select a).SingleOrDefault();

                var qFoodList = (from a in db.tab_products
                                 where a.resID.Equals(qResID.ID)
                                 select a).ToList();

                List<tab_products> lstFood = new List<tab_products>();

                foreach (var item in qFoodList)
                {
                    tab_products t = new tab_products();

                    t.bakingTime = item.bakingTime;
                    t.cost = item.cost;
                    t.CreateMaterial = item.CreateMaterial;
                    t.foodImage = item.foodImage;
                    t.id = item.id;
                    t.menuID = item.menuID;
                    t.name = item.name;
                    t.Recipe = item.Recipe;
                    t.resID = item.resID;
                    t.User_Email = item.User_Email;
                    t.FoodCount = item.FoodCount;
                    t.OrderType = item.OrderType;

                    lstFood.Add(t);
                }
                var jsondata = Json(lstFood, JsonRequestBehavior.AllowGet);
                return jsondata;
            }
            else if (type.Equals("گل فروشی"))
            {
                var qFloristID = (from a in db.Tbl_Floriest
                                  where a.userEmail.Equals(userEmail)
                                  select a).SingleOrDefault();

                var qFlowerList = (from a in db.Tbl_Flower
                                   where a.FlorestID.Equals(qFloristID.ID)
                                   select a).ToList();

                List<Tbl_Flower> lstFlower = new List<Tbl_Flower>();

                foreach (var item in qFlowerList)
                {
                    Tbl_Flower t = new Tbl_Flower();

                    t.FlorestID = item.FlorestID;
                    t.FlwColor = item.FlwColor;
                    t.FlwImage = item.FlwImage;
                    t.FlwMaintenance = item.FlwMaintenance;
                    t.FlwName = item.FlwName;
                    t.FlwPrice = item.FlwPrice;
                    t.FlwType = item.FlwType;
                    t.ID = item.ID;

                    lstFlower.Add(t);
                }
                var jsondata = Json(lstFlower, JsonRequestBehavior.AllowGet);
                return jsondata;
            }
            else
            {
                var qConfectioneryID = (from a in db.Tbl_Confectionary
                                        where a.userEmail.Equals(userEmail)
                                        select a).SingleOrDefault();

                var qSweetList = (from a in db.Tbl_Sweets
                                  where a.ConfectionaryID.Equals(qConfectioneryID.ID)
                                  select a).ToList();

                List<Tbl_Sweets> lstSweets = new List<Tbl_Sweets>();

                foreach (var item in qSweetList)
                {
                    Tbl_Sweets t = new Tbl_Sweets();

                    t.ConfectionaryID = item.ConfectionaryID;
                    t.ID = item.ID;
                    t.SImage = item.SImage;
                    t.S_Description = item.S_Description;
                    t.S_Name = item.S_Name;
                    t.S_Price = item.S_Price;
                    t.S_Type = item.S_Type;

                    lstSweets.Add(t);
                }
                var jsondata = Json(lstSweets, JsonRequestBehavior.AllowGet);
                return jsondata;
            }
        }
        public JsonResult AdminAddFoodOption(FoodOptionTitle fot)
        {
            Tbl_FoodItemGroup tGroup = new Tbl_FoodItemGroup();

            tGroup.GroupName = fot.GroupName;

            db.Tbl_FoodItemGroup.Add(tGroup);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                int myID = tGroup.ID;
                List<OptionIDs> lstOptionID = new List<OptionIDs>();

                foreach (var item in fot.lstFoodOption)
                {
                    Tbl_CustomizationFood tCustom = new Tbl_CustomizationFood();

                    tCustom.foodItem = item.foodItem;
                    tCustom.itemGroupID = myID;
                    tCustom.itemPrice = item.itemPrice;
                    tCustom.Used = item.Used;

                    db.Tbl_CustomizationFood.Add(tCustom);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        OptionIDs oi = new OptionIDs();

                        oi.OptionID = tCustom.ID;

                        lstOptionID.Add(oi);
                    }
                    else
                    {
                        return null;
                    }

                }
                var jsondata = Json(lstOptionID, JsonRequestBehavior.AllowGet);
                return jsondata;
            }
            else
            {
                return null;
            }

        }
        public int AdminAddFoodAlbumAPI(FoodAlbum fa)
        {
            List<Tbl_FoodAlbume> lstFoodAlbum = new List<Tbl_FoodAlbume>();

            foreach (var item in fa.lstFoodIDs)
            {
                Tbl_FoodAlbume tFoodAlbum = new Tbl_FoodAlbume();

                tFoodAlbum.albumImage = fa.albumImage;
                tFoodAlbum.albumName = fa.albumName;
                tFoodAlbum.foodID = item;

                lstFoodAlbum.Add(tFoodAlbum);
            }
            db.Tbl_FoodAlbume.AddRange(lstFoodAlbum);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int AdminEditFoodAlbumAPI(EditFoodAlbum fa)
        {
            var q = (from a in db.Tbl_FoodAlbume
                     where a.ID.Equals(fa.ID)
                     select a).SingleOrDefault();

            q.albumImage = fa.albumImage;
            q.albumName = fa.albumName;

            db.Tbl_FoodAlbume.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int AdminAddFoodPacketAPI(FoodPacket fp)
        {
            List<Tbl_Packing> lstPacking = new List<Tbl_Packing>();

            foreach (var item in fp.lstFoodIDs)
            {
                Tbl_Packing tFoodPacket = new Tbl_Packing();

                tFoodPacket.packingDescription = fp.packingDescription;
                tFoodPacket.packingImage = fp.packingImage;
                tFoodPacket.foodID = item;

                lstPacking.Add(tFoodPacket);
            }
            db.Tbl_Packing.AddRange(lstPacking);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int AdminEditFoodPacketAPI(EditFoodPacket fa)
        {
            var q = (from a in db.Tbl_Packing
                     where a.ID.Equals(fa.ID)
                     select a).SingleOrDefault();

            q.packingDescription = fa.packingDescription;
            q.packingImage = fa.packingImage;

            db.Tbl_Packing.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public JsonResult AdminShowAllPacketAPI(int resID)
        {
            var q = (from a in db.tab_products
                     where a.resID.Equals(resID)
                     select a).ToList();

            List<ShowPacking> lstPacking = new List<ShowPacking>();

            foreach (var item in q)
            {
                var qPacket = (from a in db.Tbl_Packing
                               where a.foodID.Equals(item.id)
                               select a).ToList();

                foreach (var item1 in qPacket)
                {
                    ShowPacking tPacking = new ShowPacking();
                    var qFoodName = (from a in db.tab_products
                                     where a.id.Equals(item1.foodID)
                                     select a).SingleOrDefault();

                    tPacking.foodName = qFoodName.name;
                    tPacking.ID = item1.ID;
                    tPacking.packingDescription = item1.packingDescription;
                    tPacking.packingImage = item1.packingImage;

                    lstPacking.Add(tPacking);
                }
            }
            var jsondata = Json(lstPacking, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminShowAllAlbumAPI(int resID)
        {
            var q = (from a in db.tab_products
                     where a.resID.Equals(resID)
                     select a).ToList();

            List<ShowAlbum> lstPacking = new List<ShowAlbum>();

            foreach (var item in q)
            {
                var qPacket = (from a in db.Tbl_FoodAlbume
                               where a.foodID.Equals(item.id)
                               select a).ToList();

                foreach (var item1 in qPacket)
                {
                    ShowAlbum tPacking = new ShowAlbum();
                    var qFoodName = (from a in db.tab_products
                                     where a.id.Equals(item1.foodID)
                                     select a).SingleOrDefault();

                    tPacking.foodName = qFoodName.name;
                    tPacking.ID = item1.ID;
                    tPacking.albumName = item1.albumName;
                    tPacking.albumImage = item1.albumImage;

                    lstPacking.Add(tPacking);
                }
            }
            var jsondata = Json(lstPacking, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public int AdminDeleteFoodPacketAPI(int ID)
        {
            var q = (from a in db.Tbl_Packing
                     where a.ID.Equals(ID)
                     select a).SingleOrDefault();

            db.Tbl_Packing.Remove(q);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int AdminDeleteFoodAlbumAPI(int ID)
        {
            var q = (from a in db.Tbl_FoodAlbume
                     where a.ID.Equals(ID)
                     select a).SingleOrDefault();

            db.Tbl_FoodAlbume.Remove(q);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int AdminAddFlowerAPI(AddFlower af)
        {
            Tbl_Flower tFlower = new Tbl_Flower();

            tFlower.FlorestID = af.FlorestID;
            tFlower.FlwColor = "qwe";
            tFlower.FlwImage = af.FlwImage;
            tFlower.FlwMaintenance = af.FlwMaintenance;
            tFlower.FlwName = af.FlwName;
            tFlower.FlwPrice = af.FlwPrice;
            tFlower.FlwType = af.FlwType;

            db.Tbl_Flower.Add(tFlower);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int AdminAddSweetsAPI(AddSweets addSweets)
        {
            Tbl_Sweets tSweets = new Tbl_Sweets();

            tSweets.ConfectionaryID = addSweets.ConfectionaryID;
            tSweets.SImage = addSweets.SImage;
            tSweets.S_Description = addSweets.S_Description;
            tSweets.S_Name = addSweets.S_Name;
            tSweets.S_Price = addSweets.S_Price;
            tSweets.S_Type = addSweets.S_Type;

            db.Tbl_Sweets.Add(tSweets);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int AdminDeleteSweetsAPI(int ID)
        {
            var q = (from a in db.Tbl_Sweets
                     where a.ID.Equals(ID)
                     select a).SingleOrDefault();

            db.Tbl_Sweets.Remove(q);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int AdminDeleteFlowerAPI(int ID)
        {
            var q = (from a in db.Tbl_Flower
                     where a.ID.Equals(ID)
                     select a).SingleOrDefault();

            db.Tbl_Flower.Remove(q);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int AdminDeleteFoodAPI(int ID)
        {
            var q = (from a in db.tab_products
                     where a.id.Equals(ID)
                     select a).SingleOrDefault();

            List<Tbl_FoodOption> qOption = (from a in db.Tbl_FoodOption
                                            where a.FoodID.Equals(q.id)
                                            select a).ToList();

            db.Tbl_FoodOption.RemoveRange(qOption);

            List<Tbl_FoodAlbume> qAlbum = (from a in db.Tbl_FoodAlbume
                                           where a.foodID.Equals(q.id)
                                           select a).ToList();

            db.Tbl_FoodAlbume.RemoveRange(qAlbum);

            List<Tbl_Packing> qPacking = (from a in db.Tbl_Packing
                                          where a.foodID.Equals(q.id)
                                          select a).ToList();

            db.Tbl_Packing.RemoveRange(qPacking);

            db.tab_products.Remove(q);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        //public JsonResult AdminShowFoodReserveParentAPI(int resID)
        //{
        //    int sum = 0;
        //    var q = (from a in db.Tbl_FoodReservationFactor
        //             where a.ResID.Equals(resID)
        //             select a).ToList();

        //    List<FoodReserveParent> lstfrp = new List<FoodReserveParent>();

        //    foreach (var item in q)
        //    {
        //        FoodReserveParent frp = new FoodReserveParent();

        //        frp.Date = item.Date;
        //        frp.ID = item.ID;
        //        var qFoodList = (from a in db.Tbl_FoodReservationFactorList
        //                         where a.FoodFactorID.Equals(item.ID)
        //                         select a).ToList();
        //        foreach (var item1 in qFoodList)
        //        {
        //            sum += item1.tab_products.cost * item1.FoodCount;
        //        }
        //        frp.Price = Convert.ToString(sum);
        //        frp.Time = item.Time;
        //        frp.Type = item.OrderTyoe;

        //        lstfrp.Add(frp);

        //    }

        //    var jsondata = Json(lstfrp, JsonRequestBehavior.AllowGet);
        //    return jsondata;
        //}
        public JsonResult AdminShowFoodReserveChildAPI(int ParentID)
        {
            var q = (from a in db.Tbl_FoodReservationFactorList
                     where a.FoodFactorID.Equals(ParentID)
                     select a).ToList();

            List<FoodReserveChild> lstfrp = new List<FoodReserveChild>();

            foreach (var item in q)
            {
                FoodReserveChild frp = new FoodReserveChild();

                frp.FoodCount = Convert.ToString(item.FoodCount);
                var qFoodImage = (from a in db.tab_products
                                  where a.id.Equals(item.FoodID)
                                  select a).SingleOrDefault();
                frp.FoodImage = qFoodImage.foodImage;
                frp.FoodName = qFoodImage.name;
                frp.ID = item.ID;

                lstfrp.Add(frp);

            }

            var jsondata = Json(lstfrp, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        //public JsonResult AdminShowResReserveParentAPI(int resID)
        //{
        //    int sum = 0;
        //    var q = (from a in db.Tbl_ResReservationFactor
        //             where a.ResID.Equals(resID)
        //             select a).ToList();

        //    List<ResReserveParent> lstfrp = new List<ResReserveParent>();

        //    foreach (var item in q)
        //    {
        //        ResReserveParent frp = new ResReserveParent();

        //        frp.Date = item.Date;
        //        frp.ID = item.ID;
        //        var qFoodList = (from a in db.Tbl_FoodReservationFactorList
        //                         where a.FoodFactorID.Equals(item.ID)
        //                         select a).ToList();
        //        foreach (var item1 in qFoodList)
        //        {
        //            sum += item1.tab_products.cost * item1.FoodCount;
        //        }
        //        frp.Price = Convert.ToString(sum);
        //        frp.Time = item.Time;
        //        frp.TableNumber = Convert.ToString(item.Tbl_ResTables.TableNumber);

        //        lstfrp.Add(frp);

        //    }

        //    var jsondata = Json(lstfrp, JsonRequestBehavior.AllowGet);
        //    return jsondata;
        //}
        public JsonResult AdminShowResReserveChildAPI(int ParentID)
        {
            var q = (from a in db.Tbl_ResReservationFactorItem
                     where a.FactorID.Equals(ParentID)
                     select a).ToList();

            List<ResReserveChild> lstfrp = new List<ResReserveChild>();

            foreach (var item in q)
            {
                ResReserveChild frp = new ResReserveChild();

                frp.FoodCount = Convert.ToString(item.FoodCount);
                var qFoodImage = (from a in db.tab_products
                                  where a.id.Equals(item.FoodID)
                                  select a).SingleOrDefault();
                frp.FoodImage = qFoodImage.foodImage;
                frp.FoodName = qFoodImage.name;
                frp.ID = item.ID;

                lstfrp.Add(frp);

            }

            var jsondata = Json(lstfrp, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminShowFlowerReserveParentAPI(int resID)
        {
            int sum = 0;
            var q = (from a in db.Tbl_FlowerReservationFactor
                     where a.FloristID.Equals(resID)
                     select a).ToList();

            List<FoodReserveParent> lstfrp = new List<FoodReserveParent>();

            foreach (var item in q)
            {
                FoodReserveParent frp = new FoodReserveParent();

                frp.Date = item.Date;
                frp.ID = item.ID;
                var qFoodList = (from a in db.Tbl_FlowerReservationFactorList
                                 where a.FoodFactorID.Equals(item.ID)
                                 select a).ToList();
                foreach (var item1 in qFoodList)
                {
                    sum += Convert.ToInt32(item1.Tbl_Flower.FlwPrice) * item1.FoodCount;
                }
                frp.Price = Convert.ToString(sum);
                frp.Time = item.Time;
                frp.Type = item.OrderTyoe;

                lstfrp.Add(frp);

            }

            var jsondata = Json(lstfrp, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminShowFlowerReserveChildAPI(int ParentID)
        {
            var q = (from a in db.Tbl_FlowerReservationFactorList
                     where a.FoodFactorID.Equals(ParentID)
                     select a).ToList();

            List<FoodReserveChild> lstfrp = new List<FoodReserveChild>();

            foreach (var item in q)
            {
                FoodReserveChild frp = new FoodReserveChild();

                frp.FoodCount = Convert.ToString(item.FoodCount);
                var qFoodImage = (from a in db.Tbl_Flower
                                  where a.ID.Equals(item.FoodID)
                                  select a).SingleOrDefault();
                frp.FoodImage = qFoodImage.FlwImage;
                frp.FoodName = qFoodImage.FlwName;
                frp.ID = item.ID;

                lstfrp.Add(frp);

            }

            var jsondata = Json(lstfrp, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminShowSweetReserveParentAPI(int resID)
        {
            int sum = 0;
            var q = (from a in db.Tbl_SweetReservationFactor
                     where a.ConfectionaryID.Equals(resID)
                     select a).ToList();

            List<SweetsReserveParent> lstfrp = new List<SweetsReserveParent>();

            foreach (var item in q)
            {
                SweetsReserveParent frp = new SweetsReserveParent();

                frp.Date = item.Date;
                frp.ID = item.ID;
                frp.Price = Convert.ToString(sum);
                frp.Time = item.Time;
                frp.Type = item.OrderTyoe;
                frp.Weight = item.OrderWeight;

                lstfrp.Add(frp);

            }

            var jsondata = Json(lstfrp, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminShowSweetReserveChildAPI(int ParentID)
        {
            var q = (from a in db.Tbl_SweetReservationFactorList
                     where a.FoodFactorID.Equals(ParentID)
                     select a).ToList();

            List<SweetReserveChild> lstfrp = new List<SweetReserveChild>();

            foreach (var item in q)
            {
                SweetReserveChild frp = new SweetReserveChild();

                var qFoodImage = (from a in db.Tbl_Sweets
                                  where a.ID.Equals(item.FoodID)
                                  select a).SingleOrDefault();
                frp.FoodImage = qFoodImage.SImage;
                frp.FoodName = qFoodImage.S_Name;
                frp.ID = item.ID;

                lstfrp.Add(frp);

            }

            var jsondata = Json(lstfrp, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminFoodFilter(FoodFilter ff)
        {
            var q = (from a in db.Tbl_OrderFactor
                     where a.ResID.Equals(ff.ResID)
                     select a).ToList();

            List<FoodFilterResult> lstResult = new List<FoodFilterResult>();
            List<FoodFilterResult> lstSecendResult = new List<FoodFilterResult>();

            foreach (var item in q)
            {
                var qFoodList = (from a in db.Tbl_OrderFactorItem
                                 where a.FactorID.Equals(item.ID)
                                 select a).ToList();

                foreach (var item1 in qFoodList)
                {
                    FoodFilterResult ffr = new FoodFilterResult();

                    ffr.Count = Convert.ToString(item1.FoodCount);
                    ffr.Date = item.Date;
                    ffr.FoodName = item1.tab_products.name;
                    ffr.Price = Convert.ToString(qFoodList.Sum(x => (Convert.ToInt32(x.FoodCount) * item1.tab_products.cost)));
                    ffr.Time = item.Time;
                    ffr.Type = item1.tab_products.menuID;

                    lstResult.Add(ffr);
                }

            }

            if (ff.lstType != null)
            {
                foreach (var item2 in lstResult)
                {
                    foreach (var item in ff.lstType)
                    {
                        if (item2.Type == item)
                        {
                            lstSecendResult.Add(item2);
                        }
                    }
                }
            }
            if (ff.FoodName != null)
            {
                foreach (var item in lstSecendResult)
                {
                    if (item.FoodName != ff.FoodName)
                    {
                        lstSecendResult.Remove(item);
                    }
                }
            }
            if (ff.EndCount != null && ff.StartCount != null)
            {
                int startCount = Convert.ToInt32(ff.StartCount);
                int endCount = Convert.ToInt32(ff.EndCount);

                foreach (var item in lstSecendResult)
                {
                    if (Convert.ToInt32(item.Count) < startCount || Convert.ToInt32(item.Count) > endCount)
                    {
                        lstSecendResult.Remove(item);
                    }
                }
            }
            if (ff.EndPrice != null && ff.StartPrice != null)
            {
                int startPrice = Convert.ToInt32(ff.StartPrice);
                int endPrice = Convert.ToInt32(ff.EndTime);

                foreach (var item in lstSecendResult)
                {
                    if (Convert.ToInt32(item.Price) < startPrice || Convert.ToInt32(item.Price) > endPrice)
                    {
                        lstSecendResult.Remove(item);
                    }
                }
            }
            if (ff.EndDate != null && ff.StartDate != null)
            {
                DateTime startDate = DateTime.ParseExact(ff.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(ff.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

                foreach (var item in lstSecendResult)
                {
                    DateTime date = item.Date;

                    if (date < startDate || date > endDate)
                    {
                        lstSecendResult.Remove(item);
                    }
                }
            }
            if (ff.EndDate != null && ff.StartDate != null)
            {
                TimeSpan startTime = TimeSpan.Parse(ff.StartTime);
                TimeSpan endTime = TimeSpan.Parse(ff.EndTime);

                foreach (var item in lstSecendResult)
                {
                    //TimeSpan time = TimeSpan.Parse(item.Time);

                    //if (time < startTime || time > endTime)
                    //{
                    //    lstSecendResult.Remove(item);
                    //}
                }
            }

            var jsondata = Json(lstSecendResult, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public int AdminForgetPassword(string Email, string Phone)
        {
            var q = (from a in db.Tab_users
                     where a.email.Equals(Email) && a.mob_phone.Equals(Phone)
                     select a).SingleOrDefault();

            if (q != null)
            {
                u.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", q.email, ":دریافت رمز از فلان", q.password);
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int AdminEditUser(string name, string address, string email, string family)
        {
            var q = (from a in db.Tab_users
                     where a.email.Equals(email)
                     select a).SingleOrDefault();

            q.name = name;
            q.Family = family;
            q.home_address = address;
            db.Tab_users.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public int AdminNewPasswordUser(string newPassword, string email)
        {
            var q = (from a in db.Tab_users
                     where a.email.Equals(email)
                     select a).SingleOrDefault();

            q.password = newPassword;
            db.Tab_users.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public JsonResult AdminOrderListAPI(string userEmail)
        {
            var qRes = (from a in db.Tbl_Restaurant
                        where a.userEmail.Equals(userEmail)
                        select a).SingleOrDefault();

            int sum = 0;
            var q = (from a in db.Tbl_OrderFactor
                     where a.ResID.Equals(qRes.ID)
                     select a).ToList();

            List<MyOrder> lstOrder = new List<MyOrder>();
            foreach (var item in q)
            {
                var qResName = (from a in db.Tbl_Restaurant
                                where a.ID.Equals(item.ResID)
                                select a).SingleOrDefault();

                var qFoodList = (from a in db.Tbl_OrderFactorItem
                                 where a.FactorID.Equals(item.ID)
                                 select a).ToList();
                foreach (var item2 in qFoodList)
                {
                    sum += item2.tab_products.cost * item2.FoodCount;
                }
                MyOrder order = new MyOrder();
                order.ID = item.ID;
                order.resImage = qResName.resImage;
                order.Status = item.OrderStatus;
                order.resName = qResName.resName;
                order.OrderPrice = sum;
                order.OrderDate = item.Date.ToPersianDateString();
                order.OrderTime = item.Time.ToString("t");
                lstOrder.Add(order);
            }

            var jsondata = Json(lstOrder, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminOrderItemListAPI(int id)
        {
            var q = (from a in db.Tbl_OrderFactorItem
                     where a.FactorID.Equals(id)
                     select a).ToList();

            List<MyOrderItem> lstOrder = new List<MyOrderItem>();
            foreach (var item in q)
            {
                var qFoodName = (from a in db.tab_products
                                 where a.id.Equals(item.FoodID)
                                 select a).SingleOrDefault();

                MyOrderItem order = new MyOrderItem();

                order.ID = item.ID;
                order.OrderCount = item.FoodCount;
                order.OrderName = qFoodName.name;
                order.OrderImage = qFoodName.foodImage;

                lstOrder.Add(order);
            }

            var jsondata = Json(lstOrder, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminResCommentParentAPI(int id)
        {
            var q = (from a in db.Tab_comments
                     select a).ToList();

            List<CommentParent> lstCommentParent = new List<CommentParent>();
            foreach (var item in q)
            {
                CommentParent cp = new CommentParent();

                var qq = (from a in db.tab_products
                          where a.id.Equals(item.product_id)
                          select a).SingleOrDefault();
                bool flag = true;
                if (qq.resID == id)
                {
                    cp.name = qq.name;
                    cp.ID = qq.id;
                    if (lstCommentParent.Count() == 0)
                    {
                        lstCommentParent.Add(cp);
                    }
                    else
                    {
                        foreach (var itemTest in lstCommentParent)
                        {
                            if (itemTest.ID.Equals(cp.ID))
                            {
                                flag = false;
                            }
                        }
                        if (flag)
                            lstCommentParent.Add(cp);
                    }
                }
            }

            var jsondata = Json(lstCommentParent, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public void AdminEditOrderStatus(int id, string status)
        {
            var q = (from a in db.Tbl_OrderFactor
                     where a.ID.Equals(id)
                     select a).SingleOrDefault();

            q.OrderStatus = status;

            db.Tbl_OrderFactor.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }
        public JsonResult AdminResCommentChildAPI(int id)
        {
            var q = (from a in db.Tab_comments
                     where a.product_id.Equals(id)
                     select a).ToList();

            List<CommentChild> lstCommentChild = new List<CommentChild>();
            foreach (var item in q)
            {
                CommentChild cc = new CommentChild();

                cc.Comment = item.text;
                cc.ID = item.id;
                cc.Name = item.name;
                cc.Star = item.Stars;

                lstCommentChild.Add(cc);

            }

            var jsondata = Json(lstCommentChild, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminFlowerCommentParentAPI(int id)
        {
            var q = (from a in db.Tbl_FlowerComment
                     select a).ToList();

            List<CommentParent> lstCommentParent = new List<CommentParent>();
            foreach (var item in q)
            {
                CommentParent cp = new CommentParent();

                var qq = (from a in db.Tbl_Flower
                          where a.ID.Equals(item.flowerID)
                          select a).SingleOrDefault();

                bool flag = true;
                if (qq.FlorestID == id)
                {
                    cp.name = qq.FlwName;
                    cp.ID = qq.ID;
                }
                if (lstCommentParent.Count() == 0)
                {
                    lstCommentParent.Add(cp);
                }
                else
                {
                    foreach (var itemTest in lstCommentParent)
                    {
                        if (itemTest.ID.Equals(cp.ID))
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                        lstCommentParent.Add(cp);
                }

            }

            var jsondata = Json(lstCommentParent, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminFlowerCommentChildAPI(int id)
        {
            var q = (from a in db.Tbl_FlowerComment
                     where a.flowerID.Equals(id)
                     select a).ToList();

            List<CommentChild> lstCommentChild = new List<CommentChild>();
            foreach (var item in q)
            {
                CommentChild cc = new CommentChild();

                cc.Comment = item.Text;
                cc.ID = item.ID;
                cc.Name = item.Name;
                cc.Star = item.Stars;

                lstCommentChild.Add(cc);

            }

            var jsondata = Json(lstCommentChild, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminSweetsCommentParentAPI(int id)
        {
            var q = (from a in db.Tbl_SweetsComment
                     select a).ToList();

            List<CommentParent> lstCommentParent = new List<CommentParent>();
            foreach (var item in q)
            {
                CommentParent cp = new CommentParent();

                var qq = (from a in db.Tbl_Sweets
                          where a.ID.Equals(item.SweetsID)
                          select a).SingleOrDefault();
                bool flag = true;
                if (qq.ConfectionaryID == id)
                {
                    cp.name = qq.S_Name;
                    cp.ID = qq.ID;
                }
                if (lstCommentParent.Count() == 0)
                {
                    lstCommentParent.Add(cp);
                }
                else
                {
                    foreach (var itemTest in lstCommentParent)
                    {
                        if (itemTest.ID.Equals(cp.ID))
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                        lstCommentParent.Add(cp);
                }

            }

            var jsondata = Json(lstCommentParent, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminSweetsCommentChildAPI(int id)
        {
            var q = (from a in db.Tbl_SweetsComment
                     where a.SweetsID.Equals(id)
                     select a).ToList();

            List<CommentChild> lstCommentChild = new List<CommentChild>();
            foreach (var item in q)
            {
                CommentChild cc = new CommentChild();

                cc.Comment = item.Text;
                cc.ID = item.ID;
                cc.Name = item.Name;
                cc.Star = item.Star;

                lstCommentChild.Add(cc);

            }

            var jsondata = Json(lstCommentChild, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminFilterOrderListAPI(string userEmail, string date)
        {
            bool flag = true;
            bool flag2 = true;
            string year = date.Split('/')[0];
            string month = date.Split('/')[1];
            string day = date.Split('/')[2];

            string FinalYear;
            string FinalMonth;
            string FinalDay;

            int monthInt = Convert.ToInt32(month);
            if (monthInt == 1)
                FinalMonth = "01";
            else if (monthInt == 2)
                FinalMonth = "02";
            else if (monthInt == 3)
                FinalMonth = "03";
            else if (monthInt == 4)
                FinalMonth = "04";
            else if (monthInt == 5)
                FinalMonth = "05";
            else if (monthInt == 6)
                FinalMonth = "06";
            else if (monthInt == 7)
                FinalMonth = "07";
            else if (monthInt == 8)
                FinalMonth = "08";
            else if (monthInt == 9)
                FinalMonth = "09";
            else
            {
                flag = false;
                FinalMonth = month;
            }


            int dayInt = Convert.ToInt32(day);
            if (dayInt == 1)
                FinalDay = "01";
            else if (dayInt == 2)
                FinalDay = "02";
            else if (dayInt == 3)
                FinalDay = "03";
            else if (dayInt == 4)
                FinalDay = "04";
            else if (dayInt == 5)
                FinalDay = "05";
            else if (dayInt == 6)
                FinalDay = "06";
            else if (dayInt == 7)
                FinalDay = "07";
            else if (dayInt == 8)
                FinalDay = "08";
            else if (dayInt == 9)
                FinalDay = "09";
            else
            {
                flag2 = false;
                FinalDay = day;
            }

            string newDate = year + "/" + FinalMonth + "/" + FinalDay;
            date = newDate;

            var qRes = (from a in db.Tbl_Restaurant
                        where a.userEmail.Equals(userEmail)
                        select a).SingleOrDefault();

            int sum = 0;
            var q = (from a in db.Tbl_OrderFactor
                     where a.ResID.Equals(qRes.ID)
                     select a).ToList();

            List<MyOrder> lstOrder = new List<MyOrder>();
            foreach (var item in q)
            {
                var qResName = (from a in db.Tbl_Restaurant
                                where a.ID.Equals(item.ResID)
                                select a).SingleOrDefault();

                var qFoodList = (from a in db.Tbl_OrderFactorItem
                                 where a.FactorID.Equals(item.ID)
                                 select a).ToList();
                foreach (var item2 in qFoodList)
                {
                    sum += item2.tab_products.cost * item2.FoodCount;
                }
                MyOrder order = new MyOrder();
                order.ID = item.ID;
                order.resImage = qResName.resImage;
                order.Status = item.OrderStatus;
                order.resName = qResName.resName;
                order.OrderPrice = sum;
                order.OrderDate = item.Date.ToPersianDateString();
                order.OrderTime = item.Time.ToString("t");
                if (date.Equals(item.Date.ToPersianDateString()))
                    lstOrder.Add(order);
            }

            var jsondata = Json(lstOrder, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult AdminFilterOrderItemListAPI(int id)
        {
            var q = (from a in db.Tbl_OrderFactorItem
                     where a.FactorID.Equals(id)
                     select a).ToList();

            List<MyOrderItem> lstOrder = new List<MyOrderItem>();
            foreach (var item in q)
            {
                var qFoodName = (from a in db.tab_products
                                 where a.id.Equals(item.FoodID)
                                 select a).SingleOrDefault();

                MyOrderItem order = new MyOrderItem();

                order.ID = item.ID;
                order.OrderCount = item.FoodCount;
                order.OrderName = qFoodName.name;
                order.OrderImage = qFoodName.foodImage;

                lstOrder.Add(order);
            }

            var jsondata = Json(lstOrder, JsonRequestBehavior.AllowGet);
            return jsondata;
        }

        public int AdminEditSweetsAPI(AddSweets addSweets)
        {
            var q = (from a in db.Tbl_Sweets
                     where a.ID.Equals(addSweets.ID)
                     select a).SingleOrDefault();

            q.ConfectionaryID = addSweets.ConfectionaryID;
            q.SImage = addSweets.SImage;
            q.S_Description = addSweets.S_Description;
            q.S_Name = addSweets.S_Name;
            q.S_Price = addSweets.S_Price;
            q.S_Type = addSweets.S_Type;

            db.Tbl_Sweets.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int AdminEditFlowersAPI(AddFlower addFlower)
        {
            var q = (from a in db.Tbl_Flower
                     where a.ID.Equals(addFlower.ID)
                     select a).SingleOrDefault();

            q.FlorestID = addFlower.FlorestID;
            q.FlwImage = addFlower.FlwImage;
            q.FlwMaintenance = addFlower.FlwMaintenance;
            q.FlwName = addFlower.FlwName;
            q.FlwPrice = addFlower.FlwPrice;
            q.FlwType = addFlower.FlwType;

            db.Tbl_Flower.Attach(q);
            db.Entry(q).State = System.Data.Entity.EntityState.Modified;
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int AddNewCredite(string UserEmail, int price, string code, string receveTime)
        {
            if (price < 0)
            {
                price = 0 - price;
                string dt = DateTime.Now.ToString("HH:mm");
                TimeSpan currentTime = TimeSpan.Parse(dt);
                TimeSpan endTime = TimeSpan.Parse("20:00");
                if (currentTime <= endTime && receveTime.Equals("شام"))
                {
                    var q = (from a in db.Tbl_Credit
                             where a.UserEmail.Equals(UserEmail) && a.RootUser.Equals("U")
                             select a).SingleOrDefault();

                    if (q.Credit <= price)
                    {
                        price = price - q.Credit;
                        db.Tbl_Credit.Remove(q);
                        price = 0 - price;
                        Tbl_Credit tMyCredit = new Tbl_Credit();

                        tMyCredit.Credit = price;
                        tMyCredit.Date = DateTime.Now;
                        tMyCredit.Time = DateTime.Now.ToString("T");
                        tMyCredit.UserEmail = UserEmail;
                        tMyCredit.TransactionCode = code;
                        tMyCredit.RootUser = "M";

                        db.Tbl_Credit.Add(tMyCredit);
                    }
                    else
                    {
                        q.Credit = q.Credit - price;
                        price = 0;
                        db.Tbl_Credit.Attach(q);
                        db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                else
                {
                    price = 0 - price;
                    Tbl_Credit tCredit = new Tbl_Credit();

                    tCredit.Credit = price;
                    tCredit.Date = DateTime.Now;
                    tCredit.Time = DateTime.Now.ToString("T");
                    tCredit.UserEmail = UserEmail;
                    tCredit.TransactionCode = code;
                    tCredit.RootUser = "M";

                    db.Tbl_Credit.Add(tCredit);
                }
            }
            else
            {
                Tbl_Credit tCredit = new Tbl_Credit();

                tCredit.Credit = price;
                tCredit.Date = DateTime.Now;
                tCredit.Time = DateTime.Now.ToString("T");
                tCredit.UserEmail = UserEmail;
                tCredit.TransactionCode = code;
                tCredit.RootUser = "M";

                db.Tbl_Credit.Add(tCredit);
            }

            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public JsonResult ShowCredit(string userEmail)
        {
            string dt = DateTime.Now.ToString("HH:mm");
            TimeSpan currentTime = TimeSpan.Parse(dt);
            TimeSpan endTime = TimeSpan.Parse("20:00");
            if (currentTime <= endTime)
            {
                Tbl_Credit tCredit = new Tbl_Credit();

                var q = (from a in db.Tbl_Credit
                         where a.UserEmail.Equals(userEmail)
                         select a).ToList();

                if (q.Count() != 0)
                {
                    var qLast = q.LastOrDefault();

                    tCredit.Credit = q.Sum(i => i.Credit);
                    tCredit.Date = qLast.Date;
                    tCredit.Time = qLast.Time;
                    tCredit.UserEmail = qLast.UserEmail;
                    tCredit.TransactionCode = qLast.TransactionCode;
                    tCredit.ID = qLast.ID;

                    var jsondata = Json(tCredit, JsonRequestBehavior.AllowGet);
                    return jsondata;
                }
                else
                {
                    tCredit.Credit = 0;
                    tCredit.Date = DateTime.Now;
                    tCredit.Time = "";
                    tCredit.UserEmail = "";
                    tCredit.TransactionCode = "";
                    tCredit.ID = 0;

                    var jsondata = Json(tCredit, JsonRequestBehavior.AllowGet);
                    return jsondata;
                }
            }
            else
            {
                Tbl_Credit tCredit = new Tbl_Credit();

                var q = (from a in db.Tbl_Credit
                         where a.UserEmail.Equals(userEmail) && a.RootUser.Equals("M")
                         select a).ToList();

                if (q.Count() != 0)
                {
                    var qLast = q.LastOrDefault();

                    tCredit.Credit = q.Sum(i => i.Credit);
                    tCredit.Date = qLast.Date;
                    tCredit.Time = qLast.Time;
                    tCredit.UserEmail = qLast.UserEmail;
                    tCredit.TransactionCode = qLast.TransactionCode;
                    tCredit.ID = qLast.ID;

                    var jsondata = Json(tCredit, JsonRequestBehavior.AllowGet);
                    return jsondata;
                }
                else
                {
                    tCredit.Credit = 0;
                    tCredit.Date = DateTime.Now;
                    tCredit.Time = "";
                    tCredit.UserEmail = "";
                    tCredit.TransactionCode = "";
                    tCredit.ID = 0;

                    var jsondata = Json(tCredit, JsonRequestBehavior.AllowGet);
                    return jsondata;
                }
            }
        }
        public JsonResult ShowCreditOnlyM(string userEmail)
        {
            Tbl_Credit tCredit = new Tbl_Credit();

            var q = (from a in db.Tbl_Credit
                     where a.UserEmail.Equals(userEmail) && a.RootUser.Equals("M")
                     select a).ToList();

            if (q.Count() != 0)
            {
                var qLast = q.LastOrDefault();

                tCredit.Credit = q.Sum(i => i.Credit);
                tCredit.Date = qLast.Date;
                tCredit.Time = qLast.Time;
                tCredit.UserEmail = qLast.UserEmail;
                tCredit.TransactionCode = qLast.TransactionCode;
                tCredit.ID = qLast.ID;

                var jsondata = Json(tCredit, JsonRequestBehavior.AllowGet);
                return jsondata;
            }
            else
            {
                tCredit.Credit = 0;
                tCredit.Date = DateTime.Parse(null);
                tCredit.Time = "";
                tCredit.UserEmail = "";
                tCredit.TransactionCode = "";
                tCredit.ID = 0;

                var jsondata = Json(tCredit, JsonRequestBehavior.AllowGet);
                return jsondata;
            }

        }
        public JsonResult ShowCreditOnlyU(string userEmail)
        {
            Tbl_Credit tCredit = new Tbl_Credit();

            var q = (from a in db.Tbl_Credit
                     where a.UserEmail.Equals(userEmail) && a.RootUser.Equals("U")
                     select a).ToList();

            if (q.Count() != 0)
            {
                var qLast = q.LastOrDefault();

                tCredit.Credit = q.Sum(i => i.Credit);
                tCredit.Date = qLast.Date;
                tCredit.Time = qLast.Time;
                tCredit.UserEmail = qLast.UserEmail;
                tCredit.TransactionCode = qLast.TransactionCode;
                tCredit.ID = qLast.ID;

                var jsondata = Json(tCredit, JsonRequestBehavior.AllowGet);
                return jsondata;
            }
            else
            {
                tCredit.Credit = 0;
                tCredit.Date = DateTime.Now;
                tCredit.Time = "";
                tCredit.UserEmail = "";
                tCredit.TransactionCode = "";
                tCredit.ID = 0;

                var jsondata = Json(tCredit, JsonRequestBehavior.AllowGet);
                return jsondata;
            }

        }
        public int CreateOrderFactor(int resID, string userEmail, string Comment, int addressID, bool Credit, bool delivery, string ReceveTime)
        {
            Tbl_OrderFactor t = new Tbl_OrderFactor();

            t.Date = DateTime.Now.Date;
            t.OrderStatus = "در حال بررسی";
            t.ResID = resID;
            t.Time = DateTime.Now.TimeOfDay;
            t.UserEmail = userEmail;
            t.Comment = Comment;
            t.AddressID = addressID;
            t.Credit = Credit;
            t.Delivery = delivery;
            t.CreditAmount = "0";
            t.ReceveTime = ReceveTime;

            db.Tbl_OrderFactor.Add(t);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                var resEmail = (from a in db.Tbl_Restaurant
                                where a.ID.Equals(resID)
                                select a).SingleOrDefault();
                u.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", userEmail, "فاکتور سفارش", "سفارش شما توسط " + resEmail.resName + " دریافت گردید");
                u.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", "zamani.roozbeh.75@gmail.com", "سفارش جدید", "شما یک سفارش جدید دارید");
                u.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", resEmail.userEmail, "سفارش جدید", "شما یک سفارش جدید دارید");
                return t.ID;
            }
            else
            {
                return -1;
            }
        }
        public int UpdateOrderFactor(int id, string credit)
        {
            var qEditFactor = (from a in db.Tbl_OrderFactor
                               where a.ID.Equals(id)
                               select a).SingleOrDefault();

            qEditFactor.CreditAmount = credit;
            db.Tbl_OrderFactor.Attach(qEditFactor);
            db.Entry(qEditFactor).State = System.Data.Entity.EntityState.Modified;
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int CreateOrderFactorItem(FactorItem fi)
        {
            for (int i = 0; i < fi.foodID.Count(); i++)
            {
                Tbl_OrderFactorItem t = new Tbl_OrderFactorItem();

                t.FactorID = fi.factorID;
                t.FoodCount = fi.foodCount.ElementAt(i);
                t.FoodID = fi.foodID.ElementAt(i);

                db.Tbl_OrderFactorItem.Add(t);

                int sFoodID = fi.foodID.ElementAt(i);

                var qFoodForEdit = (from a in db.tab_products
                                    where a.id.Equals(sFoodID)
                                    select a).SingleOrDefault();

                if (qFoodForEdit.FoodCount != -1)
                {
                    qFoodForEdit.FoodCount = qFoodForEdit.FoodCount - fi.foodCount.ElementAt(i);
                    db.tab_products.Attach(qFoodForEdit);
                    db.Entry(qFoodForEdit).State = System.Data.Entity.EntityState.Modified;
                }
            }
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public int CreateOptionFactorItem(List<int> optionID, int factorItemID)
        {
            foreach (var item in optionID)
            {
                Tbl_OptionFactorItem t = new Tbl_OptionFactorItem();

                t.factorItemID = factorItemID;
                t.OptionItemID = item;

                db.Tbl_OptionFactorItem.Add(t);
            }
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public int CreateAlbumFactorItem(int id, int factorID)
        {
            Tbl_AlbumFactorItem t = new Tbl_AlbumFactorItem();

            t.factorItemID = factorID;
            t.AlbumID = id;

            db.Tbl_AlbumFactorItem.Add(t);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public int CreatePackingFactorItem(int id, int factorID)
        {
            Tbl_PacketingFactorItem t = new Tbl_PacketingFactorItem();

            t.factorItemID = factorID;
            t.PacketingID = id;

            db.Tbl_PacketingFactorItem.Add(t);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        public JsonResult AdminPerfecionalFilter(AdminFilterDM af)
        {
            var q = (from a in db.tab_products
                     where a.resID.Equals(af.ResID) && a.name.Equals(af.FoodName)
                     select a).SingleOrDefault();

            var qFactorItem = (from a in db.Tbl_OrderFactorItem
                               where a.FoodID.Equals(q.id)
                               select a).ToList();
            if (af.CountTo != 0 && af.CountFrom != 0)
            {
                var qCount = (from a in qFactorItem
                              where a.FoodCount > af.CountFrom && a.FoodCount < af.CountTo
                              select a).ToList();
            }

            List<Tbl_OrderFactor> lstfactor = new List<Tbl_OrderFactor>();

            foreach (var item in qFactorItem)
            {
                var qFactor = (from a in db.Tbl_OrderFactor
                               where a.ID.Equals(item.FactorID)
                               select a).SingleOrDefault();

                lstfactor.Add(qFactor);
            }

            if (af.DateFrom != null && af.DateTo != null)
            {
                var qDate = (from a in lstfactor
                             where a.Date > af.DateFrom && a.Date < af.DateTo
                             select a).ToList();
            }
            List<Tbl_OrderFactor> lstSecendFactor = new List<Tbl_OrderFactor>();
            foreach (var item1 in lstfactor)
            {
                Tbl_OrderFactor t = new Tbl_OrderFactor();
                t.AddressID = item1.AddressID;
                t.Comment = item1.Comment;
                t.Date = item1.Date;
                t.ID = item1.ID;
                t.OrderStatus = item1.OrderStatus;
                t.ResID = item1.ResID;
                t.Time = item1.Time;
                t.UserEmail = item1.UserEmail;
                lstSecendFactor.Add(t);
            }

            var jsondata = Json(lstSecendFactor, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public JsonResult ShowFoodItemAPI(int foodID, int grupID)
        {
            var q = (from a in db.Tbl_FoodOption
                     where a.FoodID.Equals(foodID)
                     select a).ToList();

            List<FoodItem> lstfoodItem = new List<FoodItem>();
            foreach (var item in q)
            {
                var qOption = (from a in db.Tbl_CustomizationFood
                               where a.ID.Equals(item.OptionID) && a.itemGroupID.Equals(grupID)
                               select a).SingleOrDefault();
                if (qOption != null)
                {
                    FoodItem tFoodItem = new FoodItem();
                    tFoodItem.foodID = item.FoodID;
                    tFoodItem.foodItem = qOption.foodItem;
                    tFoodItem.ID = item.ID;
                    tFoodItem.groupName = qOption.Tbl_FoodItemGroup.GroupName;
                    tFoodItem.itemPrice = qOption.itemPrice;
                    tFoodItem.Used = qOption.Used;
                    lstfoodItem.Add(tFoodItem);
                }
            }
            var jsondata = Json(lstfoodItem, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public void testttttt(int factorID, int count1, int count2, int id1, int id2)
        {
            FactorItem fi = new FactorItem();
            fi.foodCount = new List<int>();
            fi.foodID = new List<int>();

            fi.foodCount.Add(count1);
            fi.foodCount.Add(count2);

            fi.factorID = factorID;

            fi.foodID.Add(id1);
            fi.foodID.Add(id2);

            CreateOrderFactorItem(fi);
        }
        public int ChechFoodCountForOrder(int id)
        {
            var q = (from a in db.tab_products
                     where a.id.Equals(id)
                     select a).SingleOrDefault();

            return q.FoodCount.Value;
        }
        public JsonResult CreditReportList(string userEmail)
        {
            var q = (from a in db.Tbl_Credit
                     where a.UserEmail.Equals(userEmail) && a.RootUser.Equals("M")
                     select a).ToList();

            List<CreditTransaction> lstCredit = new List<CreditTransaction>();

            foreach (var item in q)
            {
                CreditTransaction ct = new CreditTransaction();

                if (item.Credit > 0)
                    ct.CreditType = true;
                else
                    ct.CreditType = false;

                ct.Date = item.Date.ToPersianDateString();
                ct.ID = item.ID;
                if (item.Credit > 0)
                    ct.Price = item.Credit.ToString();
                else
                    ct.Price = (0 - item.Credit).ToString();

                ct.Time = item.Time;
                ct.TransactionCode = item.TransactionCode;

                lstCredit.Add(ct);
            }

            var jsondata = Json(lstCredit, JsonRequestBehavior.AllowGet);
            return jsondata;
        }
        public string TimeToEndUniversityCredit()
        {
            string dt = DateTime.Now.ToString("HH:mm");
            TimeSpan currentTime = TimeSpan.Parse(dt);
            TimeSpan endTime = TimeSpan.Parse("20:00");
            TimeSpan startTime = TimeSpan.Parse("10:00");
            if (currentTime >= startTime && currentTime < endTime)
            {
                TimeSpan tt = endTime - currentTime;
                string ttt = tt.ToString();
                string h = ttt.Split(':')[0];
                string m = ttt.Split(':')[1];
                int hh = Convert.ToInt32(h) * 60;
                int mm = Convert.ToInt32(m);
                int total = hh + mm;
                string totalTime = total.ToString();
                return totalTime;
            }
            else
            {
                string mmmm = "0";
                return mmmm;
            }
        }
        public int TimeToOrder()
        {
            string dt = DateTime.Now.ToString("HH:mm");
            TimeSpan currentTime = TimeSpan.Parse(dt);
            TimeSpan endTime = TimeSpan.Parse("23:00");
            TimeSpan startTime = TimeSpan.Parse("11:00");
            if (currentTime >= startTime && currentTime < endTime)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int TimeToUniCreditOrder()
        {
            string dt = DateTime.Now.ToString("HH:mm");
            TimeSpan currentTime = TimeSpan.Parse(dt);
            TimeSpan endTime = TimeSpan.Parse("20:00");
            TimeSpan startTime = TimeSpan.Parse("08:00");
            if (currentTime >= startTime && currentTime < endTime)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public int LoadingAPI()
        {
            var q = (from a in db.Tbl_Loading
                     select a).ToList();

            return 1;
        }

        public int sendEmailForResManager(string message, string resEmail)
        {
            u.SendEmail("smtp.gmail.com", "zamani.roozbeh.75@gmail.com", "1741034681shm", resEmail, "پیام کاربر - یوفود", message);
            return 1;
        }
        public static async Task<T> CallApi<T>(string apiUrl, object value)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                var w = client.PostAsJsonAsync(apiUrl, value);
                w.Wait();
                HttpResponseMessage response = w.Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<T>();
                    result.Wait();
                    return result.Result;
                }
                return default(T);
            }
        }
        [HttpPost]
        public ActionResult Verify(PurchaseResult result)
        {
            verifyy(result);
            if(result.ResCode == "0")
            {
                AddCredit(result.VerifyResultData.Amount);
            }
            return View(result);
        }

        [HttpPost]
        public ActionResult VerifyRequest(PurchaseResult result)
        {
            try
            {
                var cookie = Request.Cookies["Data"].Value;
                var model = JsonConvert.DeserializeObject<PaymentRequest>(cookie);

                var dataBytes = Encoding.UTF8.GetBytes(result.Token);

                var symmetric = SymmetricAlgorithm.Create("TripleDes");
                symmetric.Mode = CipherMode.ECB;
                symmetric.Padding = PaddingMode.PKCS7;

                var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(model.MerchantKey), new byte[8]);

                var signedData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

                var data = new
                {
                    token = result.Token,
                    SignData = signedData
                };

                var ipgUri = string.Format("{0}/api/v0/Advice/Verify", model.PurchasePage);

                var res = CallApi<VerifyResultData>(ipgUri, data);
                if (res != null && res.Result != null)
                {
                    if (res.Result.ResCode == "0")
                    {
                        result.VerifyResultData = res.Result;
                        res.Result.Succeed = true;
                        ViewBag.Success = res.Result.Description;
                        return View("Verify", result);
                    }
                    ViewBag.Message = res.Result.Description;
                    return View("Verify");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.ToString();
            }

            return View("Verify", result);
        }

        public JsonResult Dargah(string price)
        {
            PaymentRequest request = new PaymentRequest();
            long l1;
            l1 = long.Parse(price);
            request.Amount = l1;
            request.TerminalId = "24000615";
            request.MerchantKey = "kLheA+FS7MLoLlLVESE3v3/FP07uLaRw";
            request.MerchantId = "000000140212149";
            request.PurchasePage = "https://sadad.shaparak.ir/VPG/";

            request.OrderId = new Random().Next(1000, int.MaxValue).ToString();
            var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", request.TerminalId, request.OrderId, request.Amount));

            var symmetric = SymmetricAlgorithm.Create("TripleDes");
            symmetric.Mode = CipherMode.ECB;
            symmetric.Padding = PaddingMode.PKCS7;

            var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(request.MerchantKey), new byte[8]);

            request.SignData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

            if (HttpContext.Request.Url != null)
                request.ReturnUrl = string.Format("{0}://{1}{2}Home/Verify", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            var ipgUri = string.Format("{0}/api/v0/Request/PaymentRequest", request.PurchasePage);


            HttpCookie merchantTerminalKeyCookie = new HttpCookie("Data", JsonConvert.SerializeObject(request));
            Response.Cookies.Add(merchantTerminalKeyCookie);

            var data = new
            {
                request.TerminalId,
                request.MerchantId,
                request.Amount,
                request.SignData,
                request.ReturnUrl,
                LocalDateTime = DateTime.Now,
                request.OrderId,
                //MultiplexingData = request.MultiplexingData
            };

            var res = CallApi<PayResultData>(ipgUri, data);
            res.Wait();

            if (res != null && res.Result != null)
            {
                if (res.Result.ResCode == "0")
                {
                    string s = string.Format("{0}/Purchase/Index?token={1}", request.PurchasePage, res.Result.Token);
                    return Json(s, JsonRequestBehavior.AllowGet);
                    //Response.Redirect(string.Format("{0}/Purchase/Index?token={1}", request.PurchasePage, res.Result.Token));
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public void verifyy(PurchaseResult result)
        {
            try
            {
                var cookie = Request.Cookies["Data"].Value;
                var model = JsonConvert.DeserializeObject<PaymentRequest>(cookie);

                var dataBytes = Encoding.UTF8.GetBytes(result.Token);

                var symmetric = SymmetricAlgorithm.Create("TripleDes");
                symmetric.Mode = CipherMode.ECB;
                symmetric.Padding = PaddingMode.PKCS7;

                var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(model.MerchantKey), new byte[8]);

                var signedData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

                var data = new
                {
                    token = result.Token,
                    SignData = signedData
                };

                var ipgUri = string.Format("{0}/api/v0/Advice/Verify", model.PurchasePage);

                var res = CallApi<VerifyResultData>(ipgUri, data);
                if (res != null && res.Result != null)
                {
                    if (res.Result.ResCode == "0")
                    {
                        result.VerifyResultData = res.Result;
                        res.Result.Succeed = true;
                        ViewBag.Success = res.Result.Description;
                    }
                    ViewBag.Message = res.Result.Description;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.ToString();
            }
            
        }

        [HttpPost]
        public ActionResult OrderVerify(PurchaseResult result)
        {
            string email = Session["email"].ToString();
            var q = (from a in db.Tbl_TempOrder
                     where a.UserEmail.Equals(email)
                     select a).SingleOrDefault();

            OrderVerifyy(result);
            if (result.ResCode == "0")
            {
                SaveShoppingCart(q.InTimeStatus , q.CreditChecked , q.Address , q.Comment);
                db.Tbl_TempOrder.Remove(q);
                db.SaveChanges();
            }
            return View(result);
        }

        [HttpPost]
        public ActionResult OrderVerifyRequest(PurchaseResult result)
        {
            try
            {
                var cookie = Request.Cookies["Data"].Value;
                var model = JsonConvert.DeserializeObject<PaymentRequest>(cookie);

                var dataBytes = Encoding.UTF8.GetBytes(result.Token);

                var symmetric = SymmetricAlgorithm.Create("TripleDes");
                symmetric.Mode = CipherMode.ECB;
                symmetric.Padding = PaddingMode.PKCS7;

                var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(model.MerchantKey), new byte[8]);

                var signedData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

                var data = new
                {
                    token = result.Token,
                    SignData = signedData
                };

                var ipgUri = string.Format("{0}/api/v0/Advice/OrderVerify", model.PurchasePage);

                var res = CallApi<VerifyResultData>(ipgUri, data);
                if (res != null && res.Result != null)
                {
                    if (res.Result.ResCode == "0")
                    {
                        result.VerifyResultData = res.Result;
                        res.Result.Succeed = true;
                        ViewBag.Success = res.Result.Description;
                        return View("OrderVerify", result);
                    }
                    ViewBag.Message = res.Result.Description;
                    return View("OrderVerify");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.ToString();
            }

            return View("OrderVerify", result);
        }

        public JsonResult OrderDargah(string price)
        {
            PaymentRequest request = new PaymentRequest();
            long l1;
            l1 = long.Parse(price);
            request.Amount = l1;
            request.TerminalId = "24000615";
            request.MerchantKey = "kLheA+FS7MLoLlLVESE3v3/FP07uLaRw";
            request.MerchantId = "000000140212149";
            request.PurchasePage = "https://sadad.shaparak.ir/VPG/";

            request.OrderId = new Random().Next(1000, int.MaxValue).ToString();
            var dataBytes = Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", request.TerminalId, request.OrderId, request.Amount));

            var symmetric = SymmetricAlgorithm.Create("TripleDes");
            symmetric.Mode = CipherMode.ECB;
            symmetric.Padding = PaddingMode.PKCS7;

            var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(request.MerchantKey), new byte[8]);

            request.SignData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

            if (HttpContext.Request.Url != null)
                request.ReturnUrl = string.Format("{0}://{1}{2}Home/OrderVerify", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));

            var ipgUri = string.Format("{0}/api/v0/Request/PaymentRequest", request.PurchasePage);


            HttpCookie merchantTerminalKeyCookie = new HttpCookie("Data", JsonConvert.SerializeObject(request));
            Response.Cookies.Add(merchantTerminalKeyCookie);

            var data = new
            {
                request.TerminalId,
                request.MerchantId,
                request.Amount,
                request.SignData,
                request.ReturnUrl,
                LocalDateTime = DateTime.Now,
                request.OrderId,
                //MultiplexingData = request.MultiplexingData
            };

            var res = CallApi<PayResultData>(ipgUri, data);
            res.Wait();

            if (res != null && res.Result != null)
            {
                if (res.Result.ResCode == "0")
                {
                    string s = string.Format("{0}/Purchase/Index?token={1}", request.PurchasePage, res.Result.Token);
                    return Json(s, JsonRequestBehavior.AllowGet);
                    //Response.Redirect(string.Format("{0}/Purchase/Index?token={1}", request.PurchasePage, res.Result.Token));
                }
                else
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        public void OrderVerifyy(PurchaseResult result)
        {
            try
            {
                var cookie = Request.Cookies["Data"].Value;
                var model = JsonConvert.DeserializeObject<PaymentRequest>(cookie);

                var dataBytes = Encoding.UTF8.GetBytes(result.Token);

                var symmetric = SymmetricAlgorithm.Create("TripleDes");
                symmetric.Mode = CipherMode.ECB;
                symmetric.Padding = PaddingMode.PKCS7;

                var encryptor = symmetric.CreateEncryptor(Convert.FromBase64String(model.MerchantKey), new byte[8]);

                var signedData = Convert.ToBase64String(encryptor.TransformFinalBlock(dataBytes, 0, dataBytes.Length));

                var data = new
                {
                    token = result.Token,
                    SignData = signedData
                };

                var ipgUri = string.Format("{0}/api/v0/Advice/OrderVerify", model.PurchasePage);

                var res = CallApi<VerifyResultData>(ipgUri, data);
                if (res != null && res.Result != null)
                {
                    if (res.Result.ResCode == "0")
                    {
                        result.VerifyResultData = res.Result;
                        res.Result.Succeed = true;
                        ViewBag.Success = res.Result.Description;
                    }
                    ViewBag.Message = res.Result.Description;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.ToString();
            }

        }

        public JsonResult PreSaveShoppingCart(string InTimeStatus, string CreditChecked, string Address, string Comment)
        {

            int totalPrice = 0;
            if (CreditChecked == null)
            {
                CreditChecked = "false";
            }
            if (InTimeStatus == null)
            {
                InTimeStatus = "false";
            }
            string email = Session["email"].ToString();

            Tbl_TempOrder tempOrder = new Tbl_TempOrder();

            tempOrder.Address = Address;
            tempOrder.Comment = Comment;
            tempOrder.CreditChecked = CreditChecked;
            tempOrder.InTimeStatus = InTimeStatus;
            tempOrder.UserEmail = email;

            db.Tbl_TempOrder.Add(tempOrder);
            db.SaveChanges();
            var q = (from a in db.Tbl_ShoppingCart
                     where a.UserEmail.Equals(email)
                     select a).FirstOrDefault();
            if (CreditChecked.Equals("true"))
            {
                int cred;
                if (InTimeStatus.Equals("true"))
                {
                    cred = wr.ShowCreditOnlyM(email);
                }
                else
                {
                    cred = wr.ShowCreditWithTime(email);
                }
                if (TimeToUniCreditOrder() == 1)
                {
                    if (InTimeStatus.Equals("true"))
                    {
                        totalPrice = wr.TotalPrice(email) + 3000;
                    }
                    else
                    {
                        totalPrice = wr.TotalPrice(email);
                    }
                }
                else
                {
                    totalPrice = wr.TotalPrice(email) + 3000;
                }
                if (cred >= totalPrice)
                {
                    return Json(0, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int finalPrice = totalPrice - cred;
                    return OrderDargah(finalPrice.ToString());

                }
            }
            else
            {
                if (TimeToUniCreditOrder() == 1)
                {
                    if (InTimeStatus.Equals("true"))
                    {
                        totalPrice = wr.TotalPrice(email) + 3000;
                    }
                    else
                    {
                        totalPrice = wr.TotalPrice(email);
                    }
                }
                else
                {
                    totalPrice = wr.TotalPrice(email) + 3000;
                }
                return OrderDargah(totalPrice.ToString());
            }
        }
    }
}
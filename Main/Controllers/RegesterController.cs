using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Security;

using sql4.Models;
using System.IO;
using Twilio;
using PagedList;
using PagedList.Mvc;
using System.Data;

namespace login.Controllers
{
    public class RegesterController : Controller
    {
        public bool a;


        private BSEntities8 db = new BSEntities8();
        //
        // GET: /Regester/
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
















        [HttpGet]
        public ActionResult Admin(string searchString, string catagory, string Area, int page = 1, int sort = 0)
        {



            if (Session["log"] == null)
            {
                return RedirectToAction("AfterLogin");
            
            
            }



                var Ads = from s in db.ads
                          select s;



                if (!String.IsNullOrWhiteSpace(searchString) && catagory != String.Empty && Area != String.Empty)
                {
                    Ads = Ads.Where(b => b.title.ToUpper().Contains(searchString.ToUpper())
                               && b.catagory == catagory && b.city.cityName == Area);

                }
                else if (!String.IsNullOrWhiteSpace(catagory) && searchString == String.Empty && Area == String.Empty)
                {
                    Ads = Ads.Where(b => b.catagory == catagory);
                }
                else if (String.IsNullOrWhiteSpace(catagory) && searchString != String.Empty && Area == String.Empty)
                {
                    Ads = Ads.Where(b => b.title.ToUpper().Contains(searchString.ToUpper()));
                }
                else if (!String.IsNullOrWhiteSpace(catagory) && searchString == String.Empty && Area != String.Empty)
                {
                    Ads = Ads.Where(b => b.catagory == catagory && b.city.cityName == Area);
                }

                else if (!String.IsNullOrWhiteSpace(catagory) && searchString == String.Empty && Area != String.Empty)
                {
                    Ads = Ads.Where(b => b.catagory == catagory && b.city.cityName == Area);
                }

                if (sort == 1)
                {

                    return View(Ads.OrderBy(v => v.date).ToPagedList(page, 6));


                }
                else if (sort == 2)
                {
                    return View(Ads.OrderBy(v => v.price).ToPagedList(page, 6));

                }


               


                return View(Ads.OrderByDescending(v => v.date).ToPagedList(page, 6));

            
           
        
        
        }
































        [HttpPost]
        public ActionResult Register(user register)
        {
            if (ModelState.IsValid)
            {
                using (BSEntities8 dc = new BSEntities8())
                {

                    var v = dc.users.Where(a => a.e_mail.Equals(register.e_mail)).FirstOrDefault();

                    if (v!= null)
                    {

                        ModelState.AddModelError("", "This Email is already register please try again with another email");

                        Redirect("Regester/Register");
                    }

                    if (v == null)
                    {
                        user Data = new user();


                        Data.name = register.name;
                        Data.e_mail = register.e_mail;
                        Data.password = register.password;
                        Data.cpass = register.cpass;
                        dc.users.Add(Data);

                        dc.SaveChanges();
                        ModelState.Clear();
                        register = null;
                        ViewBag.Message = "Successfully Registration Done";
                    }
                }


            }

            return View(register);
        }


        [HttpGet]

        public ActionResult login()
        {


            return View();
        }
        [HttpPost]

        public ActionResult login(u_login login)
        {



            if (ModelState.IsValid)
            {

                using (BSEntities8 dc = new BSEntities8())
                {
                    var v = dc.users.Where(a => a.e_mail.Equals(login.Email) && a.password.Equals(login.Password)).FirstOrDefault();


                    if (login.Email == "ahmed@gmail.com" && login.Password =="ahmed")
                    {

                        a = true;
                        Session["log"] = "3";
                        return Redirect("/Regester/Admin");

                    }

                    
                    
                    else if (v != null)
                    {
                          
                 


                        Session["LogedUserID"] = v.user_id;
                        Session["LogedUserFullname"] = v.name.ToString();
                        Session["Email"] = login.Email.ToString();

                        TempData["Name"] = Session["LogedUserFullname"];
                        TempData["Email"] = Session["Email"];

                        return RedirectToAction("AfterLogin");
                    }




                    else 
                    {
                        ModelState.AddModelError("", "The user login or password provided is incorrect.If you are not the memeber then please register your self by clicking the create accoun");

                    }

                 

                }

            }

            return View(login);

        }




        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {

                var Ads = from s in db.ads.AsEnumerable()
                          select s;


                Ads = Ads.Where(s => s.user_id.Equals(Session["LogedUserID"]) && s.e_mail.Equals(Session["Email"]));


                return View(Ads.ToList());
            }



            else
            {
                return RedirectToAction("login");
            }




        }



        public ActionResult Logout()
        {


            Session.Clear();
            Session.Abandon();


            return RedirectToAction("login");

        }


     





        [HttpGet]
        public ActionResult Create(int id)
        {
            TempData["user"] = id;


            ViewBag.area_id = new SelectList(db.areas, "area_id", "name");
            ViewBag.user_id = new SelectList(db.users, "user_id", "name");
            ViewBag.cityId = new SelectList(db.cities, "cityId", "cityName");
            return View();
        }

         

        //
        // POST: /ad/Create

        [HttpPost]

        public ActionResult Create(ad ad)
        {


            ///////// Module for not Posting the add again and again////////////////
            /*if (ModelState.IsValid)
            {


                using (BSEntities8 da = new BSEntities8())
                {

                    var ab = da.ads.Where(a => a.catagory.Equals(ad.catagory) && a.description.Equals(ad.description) && a.title.Equals(ad.title)).FirstOrDefault();

                    if (ab != null)
                    {
                       // ModelState.AddModelError("", "The Ad You have entered is already submitted thanks for anticipation");

                        Session["error"] = "The Ad Youhave submitted is already On Paksell thanks for the Anticipation";
                        Redirect("ad/create");



                    }

            
                }

            }


            */


















            var Data = db.ads.Add(ad);
            Data.ad_id = ad.ad_id;
            Data.area_id = ad.area_id;
            Data.catagory = ad.catagory;
            Data.cityId = ad.cityId;
            Data.date = System.DateTime.Now;
            Data.description = ad.description;
            Data.title = ad.title;
            Data.phone = ad.phone;
            Data.e_mail = Session["Email"].ToString();
            Data.user_id = (int)Session["LogedUserID"];
            Data.name = Session["LogedUserFullname"].ToString();


            int i = 0;
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;
                string saveFileName = Path.GetFileName(hpf.FileName);
                string location = Path.Combine(Server.MapPath("~/Images/" + @"\" + saveFileName));
                Request.Files[file].SaveAs(location);

                if (i >= 2)
                    i = 0;
                // Count + 1 each time
                i++;

                if (i == 1)
                {
                    Data.url1 = saveFileName;
                }

                else if (i == 2)
                {
                    Data.url3 = saveFileName;
                }


            }




            if (ModelState.IsValid)
            {

                db.ads.Add(ad);
                db.SaveChanges();


                using (BSEntities8 dc = new BSEntities8())
                {

                    if (dc.Iusers != null)
                    {
                        foreach (var v in dc.Iusers)
                        {
                            var s = dc.Iusers.Where(a => a.Scat.Equals(Data.catagory) && a.Sitem.Equals(Data.title)).FirstOrDefault();
                            {
                                if (s != null)
                                {
                                    string cat = s.Scat;
                                    string item = s.Sitem;
                                    string phone = Data.phone.ToString();
                                    string message = "Your intrested item posted cat is " + cat + "and item is:" + item + "and the cell number is" + phone;

                                    var twilio = new TwilioRestClient("ACfb4f763d08b8e11d5f5937fc052a6464", "c97a3b969fd6fe2bdc8f83fa61f3f465");
                                    var call = twilio.InitiateOutboundCall("+1555456790", "+15551112222", "http://example.com/handleCall");

                                    var msg = twilio.SendMessage("+14843027060", "+923237575485", message);
                                }
                            }

                        }

                    }

                }
                return RedirectToAction("/AfterLogin");

            }
            ViewBag.area_id = new SelectList(db.areas, "area_id", "name", ad.area_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "name", ad.user_id);
            ViewBag.cityId = new SelectList(db.cities, "cityId", "CityName", ad.cityId);
            return View();
        }
    
    //////////////////////////////////////JAson Function////////////


        public JsonResult Citylist(int id)
        {
            var city = from c in db.cities
                       where c.area_id == id
                       select c;
            return Json(new SelectList(city.ToArray(), "cityId", "cityName"), JsonRequestBehavior.AllowGet);
        }





        ////////////////////////DELETE//////////////////////

        public ActionResult Delete(int id = 0)
        {
            ad ad = db.ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        //
        // POST: /ad/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ad ad = db.ads.Find(id);
            db.ads.Remove(ad);
            db.SaveChanges();
            return Redirect("/Regester/AfterLogin");

        }

        ////////////////////////////////Detail/////////

        public ActionResult Details(int id = 0)
        {



            ad ad = db.ads.Find(id);
            Session["Mail"] = ad.e_mail;
            Session["ID"] = id;
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);

        }

/// <summary>
/// //////////////////////////////////////////////////////////////////////////////////USer///////////////////////////////////////
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
/// <param name="searchString"></param>
/// <returns></returns>


        public ActionResult user(string searchString)
        {





            var user = from s in db.users
                      select s;


        //    if (Session["log"] == null)
         //   {
           //     return RedirectToAction("AfterLogin");


//            }



     

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                user = user.Where(b => b.name.ToUpper().Contains(searchString.ToUpper()));
                          

            }
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            return View(user.OrderBy(v => v.user_id));



            
        }
        ///////////////User Ads/////////////////


        public ActionResult u_ad(int id,string name)
    
    {

    

        var uads = from u in db.ads
               
                   select u;
        
        ViewData["pname"] = name;


        uads = uads.Where(b => b.user_id == id);

        return View(uads.OrderBy(v=>v.ad_id));
    }


////////////////////////////////////Reports//////////////

        public ActionResult report()
        {





            adphoto r = new adphoto();

            r.ads = from u in db.ads
                    select u;
            r.reports = from s in db.reports
                        select s;


            var rep = from u in db.reports
                      select u;
        
            

            
            return View(rep);

        }

        /////////////////////////////////////////////////Photo//////////////////////////////////////////////////

        [HttpGet]
        public ActionResult photo()
        {




            return View();
        }



        [HttpPost]
        public ActionResult photo(Photo p)
        {

            var Data1 = db.Photos.Add(p);
            Data1.Id = p.Id;
            Data1.name = p.name;
            



            int i = 0;
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;


                var validImageTypes = new string[]
    {
        "image/gif",
        "image/jpeg",
        "image/pjpeg",
        "image/png"
    };



                if (hpf.ContentLength == 0)
                    continue;

                string saveFileName = Path.GetFileName(hpf.FileName);
                string location = Path.Combine(Server.MapPath("~/Images/" + @"\" + saveFileName));
                Request.Files[file].SaveAs(location);

                if (i >= 2)
                    i = 0;
                // Count + 1 each time
                i++;

                if (i == 1)
                {
                    Data1.path = saveFileName;
                }

                

            }



            if (ModelState.IsValid)
            {

                db.Entry(Data1).State = EntityState.Modified;
         
                db.SaveChanges();




            }
            return View(p);
            
        }




        /// <summary>
        /// /////////////////////////////Admin Delete /////////////////////
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns
        public ActionResult Delete1(int id = 0)
        {
            ad ad = db.ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        //
        // POST: /ad/Delete/5

        [HttpPost, ActionName("Delete1")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed1(int id)
        {
            ad ad = db.ads.Find(id);
            db.ads.Remove(ad);
            db.SaveChanges();
            return Redirect("/Regester/Admin");

        }

        ////////////////////////////////////////////////////////////Report Delete//////////////////////////////



        public ActionResult Delete2(int id = 0)
        {
            ad ad = db.ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }

        //
        // POST: /ad/Delete/5

        [HttpPost, ActionName("Delete2")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int id)
        {
            ad ad = db.ads.Find(id);
            var adb = db.reports.Where(u => u.adId == id);


            foreach (var a in adb)
            {

                db.reports.Remove(a);

            }

            var c = db.views.Where(u => u.ad_id == id);
                foreach (var d in c)
                {
                
                db.views.Remove(d);
                
                }


            db.ads.Remove(ad);
          

            db.SaveChanges();
            return Redirect("/Regester/report");

        }









        ////////////////////////////////////////////////// DElete user Admin//////////////////////

        public ActionResult user_Delete(int id = 0)
        {
            user user = db.users.Find(id);
            
            
            
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("user_Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult user_DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);



            var adb = db.ads.Where(u => u.user_id == id);
              foreach(var a in adb)
              {

                  db.ads.Remove(a);
              
              }
          
           
            
            db.users.Remove(user);

            db.SaveChanges();
            return Redirect("/Regester/user");

        }










    
    
    }



}


//////////////DELETE PAGE/////////////
 
    







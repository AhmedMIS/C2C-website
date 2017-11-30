using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sql4.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;
using Twilio;
using System.Net.Mail;
using System.Web.UI;
using System.Collections;
namespace sql4.Controllers
{
    public class adController : Controller
    {
        
       

        private BSEntities8 db = new BSEntities8();

        //
      
        // GET: /ad

          [OutputCache(NoStore = true, Location = OutputCacheLocation.Client, Duration = 3)]
        public ActionResult ab()
        {
            var model = new sql4.Models.ad();


            return PartialView("__Auctiontile", model);
      
        }


        
        public ActionResult rules()
        {

            return View();
        }
        public ActionResult s_goods()
        {
    
    
    
    return View();
    }
        public ActionResult vehicles()
        {


            return View();
        }

        public ActionResult BrandItem()
        {



            return View();

        }

        public ActionResult SendingAbroad()
        {



            return View();

        }

        public ActionResult Tmoney()
        {



            return View();

        }




        









        public ActionResult Logout()
        {


            Session.Clear();
            Session.Abandon();


            return RedirectToAction("login");

        }

        [HttpGet]
        public ActionResult rep()
        {

           
            return PartialView();
        }




        [HttpPost]
        public ActionResult rep(report rep)

        {

        
            if (ModelState.IsValid)
            {
               
                var Data = db.reports.Add(rep);
                Data.Id = rep.Id;
                Data.complain = rep.complain;
                Data.adId = (int)Session["a"];
                db.reports.Add(Data);
                db.SaveChanges();
                TempData["Msg"] = "Your Complain Have been Submitted Thanks";
                return RedirectToAction("");
            }

      
            return PartialView();
        }




















        [HttpGet]
        public ActionResult Index(string searchString,string catagory,string Area,string pro,int page = 1,int sort=0)

        {
            
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
                else if (String.IsNullOrWhiteSpace(catagory) && searchString != String.Empty && Area== String.Empty)
                {
                    Ads = Ads.Where(b => b.title.ToUpper().Contains(searchString.ToUpper()));
                }
                else if (!String.IsNullOrWhiteSpace(catagory) && searchString == String.Empty && Area != String.Empty)
                {
                    Ads = Ads.Where(b => b.catagory == catagory&&b.city.cityName==Area);
                }
                else if (String.IsNullOrWhiteSpace(catagory) && searchString == String.Empty && Area != String.Empty)
                {
                    Ads = Ads.Where(b=>b.city.cityName == Area);
                }
                else if (String.IsNullOrWhiteSpace(catagory) && searchString == String.Empty && Area == String.Empty && pro!= String.Empty)
                {
                    Ads = Ads.Where(b => b.area.name.ToUpper().Contains(pro.ToUpper()));
                 
                }
           //      if (city != String.Empty)
             //   {
               //     Ads = Ads.Where(b => b.city == city);
                //}
                //if (pro != String.Empty)
               //{

                 //  var area = from t in db.areas
                   //         select t;

                    //area = area.Where(b => b.name == pro);
              // }

                else if (sort==1&&String.IsNullOrWhiteSpace(catagory) && searchString == String.Empty && Area == String.Empty && pro != String.Empty)
                {
                    Ads = Ads.Where(b => b.area.name.ToUpper().Contains(pro.ToUpper()));

                }        

    if(sort==1)
    {

        return View(Ads.OrderByDescending(v => v.date).ToPagedList(page, 6));


    }
else if(sort==2)
    {
        return View(Ads.OrderBy(v => v.price).ToPagedList(page, 6));

    }



    int id = 0;

             Photo add = db.Photos.Find(id);
            

    Session["1"] =add.path ;
    Session["2"] = add.name;

        //    var r =(from u in db.reports
          //    join ad v on u.ad_id equals v.ad_id
            //                     select new{ u,v}).ToList();

    var graph = db.reports.Include("ad").ToList();
            ad ad1=new ad();
            Session["3"] = graph.Where(u => u.adId == ad1.ad_id);
            


            var ads = db.ads.Include(a => a.area).Include(a => a.user);
            return View(Ads.OrderByDescending(v => v.ad_id).ToPagedList(page, 6));
      
        
        }



        

         
      





    
        // GET: /ad/Details/5
        
      public ActionResult Details(int id = 0)
      {

          
          ad ad = db.ads.Find(id);
          Session["a"] = id;
          view add = db.views.Find(id);

          add.org = add.VId+1;
          add.VId = add.org;


//          var userResults = from u in db.views
  //                          where u.ad_id == id
          //
            //                select u.VId;
  
          //foreach (var a in userResults)
          //{
             
              
            //  int ab= (int)add.VId;



                       
          //}



      

         db.Entry(add).State = EntityState.Modified;
             // db.views.Add(addd);
             db.SaveChanges();

         









        
          if (ad == null)
          {
              return HttpNotFound();
          }
          return View(ad);

      }        




   
        //
        // GET: /ad/Create

        public ActionResult Create()
        {
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
            Data.date = DateTime.Now;
            Data.description = ad.description;
            Data.title = ad.title;
            Data.phone = ad.phone;
            Data.e_mail = ad.e_mail;
            Data.name = ad.name;



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
                    Data.url1 = saveFileName;
                }
                
                else if (i == 2)
                {
                    Data.url3 = saveFileName;
                }


            }
            
       
            
            
            if (ModelState.IsValid)
            {


                using (BSEntities8 da = new BSEntities8())
                
                {

                    var ab = da.ads.Where(a => a.catagory.Equals(ad.catagory) && a.description.Equals(ad.description) && a.title.Equals(ad.title)).FirstOrDefault();

                    if (ab != null)
                    {
                        ModelState.AddModelError("", "The Ad You have entered is already submitted thanks for anticipation");

                        Redirect("ad/create");



                    }



                }
                

                db.ads.Add(ad);
              

                view addd = new view();

             
                addd.ad_id = Data.ad_id;



                addd.VId = 0;

                db.views.Add(addd);


                db.SaveChanges();
                using (BSEntities8 dc = new BSEntities8())
                {

                    if (dc.Iusers != null)
                    {
                        foreach (var v in dc.Iusers)
                        {
                            var s = dc.Iusers.Where(a => a.Scat.Equals(Data.catagory)&&a.Sitem.Equals(Data.title)).FirstOrDefault();
                            {
                                if (s != null)
                                {
                                    string cat = s.Scat;
                                    string item = s.Sitem;
                                    string Area = s.area;
                                    string phone = Data.phone;
                                    string message = "Your intrested item posted cat is " + cat + "and item is:" + item + "and the cell number is" + phone+"This is from City"+Area;

                                    var twilio = new TwilioRestClient("ACfb4f763d08b8e11d5f5937fc052a6464", "c97a3b969fd6fe2bdc8f83fa61f3f465");
                                    var call = twilio.InitiateOutboundCall("+1555456790", "+15551112222", "http://example.com/handleCall");

                                    var msg = twilio.SendMessage("+14843027060", "+923237575485", message);
                                }
                            }

                        }

                    }

                }
                
              
                return RedirectToAction("Index");
            }

            ViewBag.area_id = new SelectList(db.areas, "area_id", "name", ad.area_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "name", ad.user_id);
            ViewBag.cityId = new SelectList(db.cities, "cityId", "cityName", ad.cityId);



            return View(ad);
        }



        public JsonResult Citylist(int id)
        {
            var city = from c in db.cities
                       where c.area_id == id
                       select c;
            return Json(new SelectList(city.ToArray(), "cityId", "cityName"), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// ////////////////////Photot////////////////
        
































 
    public ActionResult send(int id,string email)
    {

        Session["i"] = id;
        TempData["m"]= email;


        return View();
 }

        //send controller
        [HttpPost]
        public ActionResult send(send send)
        {


          string mail= (string)TempData["m"];
            int i = (int)Session["i"];
        SmtpClient smtpClient = new SmtpClient();
        System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
            "mustafaahmed25@gmail.com", // From
          mail , // To
            "Message from PakSell", // Subject
            "Name Of the Sender is     "+send.name+"   Email of the sender is :  "+send.e_mail+"      Message:    "+send.description); // Body

        smtpClient.Send(m);




        return RedirectToAction("/Details/", new { id = i });
        }

        //////////////////////////////////////Contact US///////////////////////////////////


        public ActionResult ContactUs()
        {


            return View();
        }

        //send controller
        [HttpPost]
        public ActionResult contactUs(send send)
        {
            if (ModelState.IsValid)
            {

                SmtpClient smtpClient = new SmtpClient();
                System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage(
                    "mustafaahmed25@gmail.com", // From
                  "mustafaahmed25@gmail.com", // To
                    "Message from PakSell", // Subject
                    "Name Of the Sender is     " + send.name + "   Email of the sender is :  " + send.e_mail + "      Message:    " + send.description); // Body

                smtpClient.Send(m);




                return RedirectToAction("/rules");
            }

            return View();
            
        }
















        // GET: /ad/Edit/5

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            ad ad = db.ads.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            ViewBag.area_id = new SelectList(db.areas, "area_id", "name", ad.area_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "name", ad.user_id);
            return View(ad);
        }

        //
        // POST: /ad/Edit/5


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ad ad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ad).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Regester/AfterLogin");
            }
            ViewBag.area_id = new SelectList(db.areas, "area_id", "name", ad.area_id);
            ViewBag.user_id = new SelectList(db.users, "user_id", "name", ad.user_id);
            return View(ad);
        }

        //
        // GET: /ad/Delete/5

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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
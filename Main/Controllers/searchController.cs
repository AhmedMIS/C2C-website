using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sql4.Models;

namespace sql4.Controllers
{
    public class searchController : Controller
    {
        private BSEntities8 db = new BSEntities8();

        //
        // GET: /search/

        public ActionResult Index(int id=0)
        {


            var Ads = from s in db.Iusers.AsEnumerable()
                      select s;


            Ads = Ads.Where(s => s.userId.Equals(id));





          //////////  var iusers = db.Iusers.Include(i => i.user);
            return View(Ads.ToList());
        }

        //
        // GET: /search/Details/

        public ActionResult Details(int id = 0)
        {
            Iuser iuser = db.Iusers.Find(id);
            if (iuser == null)
            {
                return HttpNotFound();
            }
            return View(iuser);
        }

        //
        // GET: /search/Create

        public ActionResult Create(int id)

        {
            Session["ID"] = id;

            ViewBag.userId = new SelectList(db.users, "user_id", "name");
            return View();
        }

        //
        // POST: /search/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Iuser iuser)
        {

            var SData = db.Iusers.Add(iuser);
            SData.Scat = iuser.Scat;
            SData.Sitem = iuser.Sitem;
            SData.area = iuser.area;
            SData.Sphone = iuser.Sphone;
            SData.userId = (int)Session["ID"];
            Session["j"] = SData.userId;

            if (ModelState.IsValid)
            {
                db.Iusers.Add(iuser);
                db.SaveChanges();
                return Redirect("/Regester/AfterLogin");
            }

            ViewBag.userId = new SelectList(db.users, "user_id", "name", iuser.userId);
            return View(iuser);
        }

        //
        // GET: /search/Edit

        public ActionResult Edit(int id = 0)
        {
            Iuser iuser = db.Iusers.Find(id);
            if (iuser == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.users, "user_id", "name", iuser.userId);
            return View(iuser);
        }

        //
        // POST: /search/Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Iuser iuser)
        {

            var SData = db.Iusers.Add(iuser);
        
            SData.userId = (int)Session["j"];
            SData.Sitem = iuser.Sitem;
            SData.Scat = iuser.Scat;
            SData.Sphone = iuser.Sphone;
            if (ModelState.IsValid)
            {
                db.Entry(SData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.users, "user_id", "name", iuser.userId);
            return View(iuser);
        }

        //
        // GET: /search/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Iuser iuser = db.Iusers.Find(id);
            if (iuser == null)
            {
                return HttpNotFound();
            }
            return View(iuser);
        }

        //
        // POST: /search/Delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Iuser iuser = db.Iusers.Find(id);
            db.Iusers.Remove(iuser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
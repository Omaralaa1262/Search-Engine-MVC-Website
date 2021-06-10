using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IR_MVCwebsite;

namespace IR_MVCwebsite.Controllers
{
    public class crawler_dataController : Controller
    {
        private testEntities1 db = new testEntities1();

        // GET: crawler_data
        public ActionResult Index()
        {
            return View(db.crawler_data.ToList());
        }

        // GET: crawler_data/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crawler_data crawler_data = db.crawler_data.Find(id);
            if (crawler_data == null)
            {
                return HttpNotFound();
            }
            return View(crawler_data);
        }

        // GET: crawler_data/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: crawler_data/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,URL,content_data")] crawler_data crawler_data)
        {
            if (ModelState.IsValid)
            {
                db.crawler_data.Add(crawler_data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(crawler_data);
        }

        // GET: crawler_data/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crawler_data crawler_data = db.crawler_data.Find(id);
            if (crawler_data == null)
            {
                return HttpNotFound();
            }
            return View(crawler_data);
        }

        // POST: crawler_data/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,URL,content_data")] crawler_data crawler_data)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crawler_data).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(crawler_data);
        }

        // GET: crawler_data/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            crawler_data crawler_data = db.crawler_data.Find(id);
            if (crawler_data == null)
            {
                return HttpNotFound();
            }
            return View(crawler_data);
        }

        // POST: crawler_data/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            crawler_data crawler_data = db.crawler_data.Find(id);
            db.crawler_data.Remove(crawler_data);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

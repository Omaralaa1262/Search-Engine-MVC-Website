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
    public class tockenzsController : Controller
    {
        private testEntities1 db = new testEntities1();

        // GET: tockenzs
        public ActionResult Index()
        {
            var tockenzs = db.tockenzs.Include(t => t.crawler_data);
            return View(tockenzs.ToList());
        }

        // GET: tockenzs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tockenz tockenz = db.tockenzs.Find(id);
            if (tockenz == null)
            {
                return HttpNotFound();
            }
            return View(tockenz);
        }

        // GET: tockenzs/Create
        public ActionResult Create()
        {
            ViewBag.doc_no = new SelectList(db.crawler_data, "id", "URL");
            return View();
        }

        // POST: tockenzs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,sring,doc_no,position,frequency")] tockenz tockenz)
        {
            if (ModelState.IsValid)
            {
                db.tockenzs.Add(tockenz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.doc_no = new SelectList(db.crawler_data, "id", "URL", tockenz.doc_no);
            return View(tockenz);
        }

        // GET: tockenzs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tockenz tockenz = db.tockenzs.Find(id);
            if (tockenz == null)
            {
                return HttpNotFound();
            }
            ViewBag.doc_no = new SelectList(db.crawler_data, "id", "URL", tockenz.doc_no);
            return View(tockenz);
        }

        // POST: tockenzs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,sring,doc_no,position,frequency")] tockenz tockenz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tockenz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.doc_no = new SelectList(db.crawler_data, "id", "URL", tockenz.doc_no);
            return View(tockenz);
        }

        // GET: tockenzs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tockenz tockenz = db.tockenzs.Find(id);
            if (tockenz == null)
            {
                return HttpNotFound();
            }
            return View(tockenz);
        }

        // POST: tockenzs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tockenz tockenz = db.tockenzs.Find(id);
            db.tockenzs.Remove(tockenz);
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

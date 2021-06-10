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
    public class before_stemmerController : Controller
    {
        private testEntities1 db = new testEntities1();

        // GET: before_stemmer
        public ActionResult Index()
        {
            return View(db.before_stemmer.ToList());
        }

        // GET: before_stemmer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            before_stemmer before_stemmer = db.before_stemmer.Find(id);
            if (before_stemmer == null)
            {
                return HttpNotFound();
            }
            return View(before_stemmer);
        }

        // GET: before_stemmer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: before_stemmer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "doc_id,term,id")] before_stemmer before_stemmer)
        {
            if (ModelState.IsValid)
            {
                db.before_stemmer.Add(before_stemmer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(before_stemmer);
        }

        // GET: before_stemmer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            before_stemmer before_stemmer = db.before_stemmer.Find(id);
            if (before_stemmer == null)
            {
                return HttpNotFound();
            }
            return View(before_stemmer);
        }

        // POST: before_stemmer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "doc_id,term,id")] before_stemmer before_stemmer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(before_stemmer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(before_stemmer);
        }

        // GET: before_stemmer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            before_stemmer before_stemmer = db.before_stemmer.Find(id);
            if (before_stemmer == null)
            {
                return HttpNotFound();
            }
            return View(before_stemmer);
        }

        // POST: before_stemmer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            before_stemmer before_stemmer = db.before_stemmer.Find(id);
            db.before_stemmer.Remove(before_stemmer);
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

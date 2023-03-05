using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class BookWritingsController : Controller
    {
        private KhachHang db = new KhachHang();

        // GET: BookWritings
        public ActionResult Index()
        {
            var bookWritings = db.BookWritings.Include(b => b.author).Include(b => b.book);
            return View(bookWritings.ToList());
        }

        // GET: BookWritings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookWriting bookWriting = db.BookWritings.Find(id);
            if (bookWriting == null)
            {
                return HttpNotFound();
            }
            return View(bookWriting);
        }

        // GET: BookWritings/Create
        public ActionResult Create()
        {
            ViewBag.authorID = new SelectList(db.Authors, "authorID", "name");
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title");
            return View();
        }

        // POST: BookWritings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "index,authorID,bookID,role")] BookWriting bookWriting)
        {
            if (ModelState.IsValid)
            {
                db.BookWritings.Add(bookWriting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.authorID = new SelectList(db.Authors, "authorID", "name", bookWriting.authorID);
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", bookWriting.bookID);
            return View(bookWriting);
        }

        // GET: BookWritings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookWriting bookWriting = db.BookWritings.Find(id);
            if (bookWriting == null)
            {
                return HttpNotFound();
            }
            ViewBag.authorID = new SelectList(db.Authors, "authorID", "name", bookWriting.authorID);
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", bookWriting.bookID);
            return View(bookWriting);
        }

        // POST: BookWritings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "index,authorID,bookID,role")] BookWriting bookWriting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookWriting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.authorID = new SelectList(db.Authors, "authorID", "name", bookWriting.authorID);
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", bookWriting.bookID);
            return View(bookWriting);
        }

        // GET: BookWritings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookWriting bookWriting = db.BookWritings.Find(id);
            if (bookWriting == null)
            {
                return HttpNotFound();
            }
            return View(bookWriting);
        }

        // POST: BookWritings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookWriting bookWriting = db.BookWritings.Find(id);
            db.BookWritings.Remove(bookWriting);
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

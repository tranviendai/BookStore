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
    public class DetailsOrdersController : Controller
    {
        private KhachHang db = new KhachHang();

        // GET: DetailsOrders
        public ActionResult Index()
        {
            var detailsOrders = db.DetailsOrders.Include(d => d.Bill).Include(d => d.Book);
            return View(detailsOrders.ToList());
        }

        // GET: DetailsOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsOrder detailsOrder = db.DetailsOrders.Find(id);
            if (detailsOrder == null)
            {
                return HttpNotFound();
            }
            return View(detailsOrder);
        }

        // GET: DetailsOrders/Create
        public ActionResult Create()
        {
            ViewBag.billID = new SelectList(db.Bills, "billID", "nameCustomer");
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title");
            return View();
        }

        // POST: DetailsOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "index,billID,bookID,quantity,price,totalPrice")] DetailsOrder detailsOrder)
        {
            if (ModelState.IsValid)
            {
                db.DetailsOrders.Add(detailsOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.billID = new SelectList(db.Bills, "billID", "nameCustomer", detailsOrder.billID);
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", detailsOrder.bookID);
            return View(detailsOrder);
        }

        // GET: DetailsOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsOrder detailsOrder = db.DetailsOrders.Find(id);
            if (detailsOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.billID = new SelectList(db.Bills, "billID", "nameCustomer", detailsOrder.billID);
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", detailsOrder.bookID);
            return View(detailsOrder);
        }

        // POST: DetailsOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "index,billID,bookID,quantity,price,totalPrice")] DetailsOrder detailsOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailsOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.billID = new SelectList(db.Bills, "billID", "nameCustomer", detailsOrder.billID);
            ViewBag.bookID = new SelectList(db.Books, "bookID", "title", detailsOrder.bookID);
            return View(detailsOrder);
        }

        // GET: DetailsOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsOrder detailsOrder = db.DetailsOrders.Find(id);
            if (detailsOrder == null)
            {
                return HttpNotFound();
            }
            return View(detailsOrder);
        }

        // POST: DetailsOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetailsOrder detailsOrder = db.DetailsOrders.Find(id);
            db.DetailsOrders.Remove(detailsOrder);
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

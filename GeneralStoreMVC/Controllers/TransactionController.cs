using GeneralStoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GeneralStoreMVC.Controllers
{
    public class TransactionController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Transaction
        public ActionResult Index()
        {
            return View(_db.Transaction.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(_db.Products.ToList(), "ProductID", "Name");
            ViewBag.CustomerID = new SelectList(_db.Customers.ToList(), "CustomerID", "FullName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Transactions transactions)
        {
            if (ModelState.IsValid)
            {
                _db.Transaction.Add(transactions);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(_db.Products.ToList(), "ProductID", "Name");
            ViewBag.CustomerID = new SelectList(_db.Customers.ToList(), "CustomerID", "FullName");
            return View(transactions);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transaction = _db.Transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }
        //POST: Transactions/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Transactions transaction = _db.Transaction.Find(id);
            _db.Transaction.Remove(transaction);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transaction = _db.Transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(_db.Products.ToList(), "ProductID", "Name");
            ViewBag.CustomerID = new SelectList(_db.Customers.ToList(), "CustomerID", "FullName");
            return View(transaction);
        }
        //POST: Transaction/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Transactions transaction)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(transaction).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(_db.Products.ToList(), "ProductID", "Name");
            ViewBag.CustomerID = new SelectList(_db.Customers.ToList(), "CustomerID", "FullName");
            return View(transaction);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transaction = _db.Transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }
    }
}
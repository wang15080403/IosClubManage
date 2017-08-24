using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IosClubManage.MVC.Models;

namespace IosClubManage.MVC.Controllers
{
    [UserAuthorize]
    public class SuppliesController : Controller
    {
        private IosClubDbContext db = new IosClubDbContext();

        // GET: Supplies
        public ActionResult Index()
        {
            var supplies = db.Supplies.Include(s => s.SuppliesCategory);
            return View(supplies.ToList());
        }

        // GET: Supplies/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplies supplies = db.Supplies.Find(id);
            if (supplies == null)
            {
                return HttpNotFound();
            }
            return View(supplies);
        }

        // GET: Supplies/Create
        public ActionResult Create()
        {
            ViewBag.SuppliesCategoryId = new SelectList(db.SuppliesCategories, "Id", "SuppliesCategoryName");
            return View();
        }

        // POST: Supplies/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SuppliesName,SuppliesCategoryId,PurchaseDate,Price,Remarks,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] Supplies supplies)
        {
            if (ModelState.IsValid)
            {
                supplies.Id = Guid.NewGuid();
                db.Supplies.Add(supplies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SuppliesCategoryId = new SelectList(db.SuppliesCategories, "Id", "SuppliesCategoryName", supplies.SuppliesCategoryId);
            return View(supplies);
        }

        // GET: Supplies/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplies supplies = db.Supplies.Find(id);
            if (supplies == null)
            {
                return HttpNotFound();
            }
            ViewBag.SuppliesCategoryId = new SelectList(db.SuppliesCategories, "Id", "SuppliesCategoryName", supplies.SuppliesCategoryId);
            return View(supplies);
        }

        // POST: Supplies/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SuppliesName,SuppliesCategoryId,PurchaseDate,Price,Remarks,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] Supplies supplies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SuppliesCategoryId = new SelectList(db.SuppliesCategories, "Id", "SuppliesCategoryName", supplies.SuppliesCategoryId);
            return View(supplies);
        }

        // GET: Supplies/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplies supplies = db.Supplies.Find(id);
            if (supplies == null)
            {
                return HttpNotFound();
            }
            return View(supplies);
        }

        // POST: Supplies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Supplies supplies = db.Supplies.Find(id);
            db.Supplies.Remove(supplies);
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

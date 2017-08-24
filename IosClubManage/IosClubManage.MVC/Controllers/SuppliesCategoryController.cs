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
    public class SuppliesCategoryController : Controller
    {
        private IosClubDbContext db = new IosClubDbContext();

        // GET: SuppliesCategory
        public ActionResult Index()
        {
            return View(db.SuppliesCategories.ToList());
        }

        // GET: SuppliesCategory/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuppliesCategory suppliesCategory = db.SuppliesCategories.Find(id);
            if (suppliesCategory == null)
            {
                return HttpNotFound();
            }
            return View(suppliesCategory);
        }

        // GET: SuppliesCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SuppliesCategory/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SuppliesCategoryName,Remarks,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] SuppliesCategory suppliesCategory)
        {
            if (ModelState.IsValid)
            {
                suppliesCategory.Id = Guid.NewGuid();
                db.SuppliesCategories.Add(suppliesCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(suppliesCategory);
        }

        // GET: SuppliesCategory/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuppliesCategory suppliesCategory = db.SuppliesCategories.Find(id);
            if (suppliesCategory == null)
            {
                return HttpNotFound();
            }
            return View(suppliesCategory);
        }

        // POST: SuppliesCategory/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SuppliesCategoryName,Remarks,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] SuppliesCategory suppliesCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suppliesCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(suppliesCategory);
        }

        // GET: SuppliesCategory/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuppliesCategory suppliesCategory = db.SuppliesCategories.Find(id);
            if (suppliesCategory == null)
            {
                return HttpNotFound();
            }
            return View(suppliesCategory);
        }

        // POST: SuppliesCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SuppliesCategory suppliesCategory = db.SuppliesCategories.Find(id);
            db.SuppliesCategories.Remove(suppliesCategory);
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

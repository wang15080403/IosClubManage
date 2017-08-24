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
    public class CapitalController : Controller
    {
        private IosClubDbContext db = new IosClubDbContext();

        // GET: Capital
        public ActionResult Index()
        {
            return View(db.Capitals.ToList());
        }

        // GET: Capital/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capital capital = db.Capitals.Find(id);
            if (capital == null)
            {
                return HttpNotFound();
            }
            return View(capital);
        }

        // GET: Capital/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Capital/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Subject,Remarks,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] Capital capital)
        {
            if (ModelState.IsValid)
            {
                capital.Id = Guid.NewGuid();
                db.Capitals.Add(capital);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(capital);
        }

        // GET: Capital/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capital capital = db.Capitals.Find(id);
            if (capital == null)
            {
                return HttpNotFound();
            }
            return View(capital);
        }

        // POST: Capital/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Subject,Remarks,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] Capital capital)
        {
            if (ModelState.IsValid)
            {
                db.Entry(capital).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(capital);
        }

        // GET: Capital/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capital capital = db.Capitals.Find(id);
            if (capital == null)
            {
                return HttpNotFound();
            }
            return View(capital);
        }

        // POST: Capital/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Capital capital = db.Capitals.Find(id);
            db.Capitals.Remove(capital);
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

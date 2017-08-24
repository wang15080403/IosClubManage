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
    public class CapitalflowController : Controller
    {
        private IosClubDbContext db = new IosClubDbContext();

        // GET: Capitalflow
        public ActionResult Index()
        {
            var capitalflows = db.Capitalflows.Include(c => c.Capital).Include(c => c.User);
            return View(capitalflows.ToList());
        }

        // GET: Capitalflow/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capitalflow capitalflow = db.Capitalflows.Find(id);
            if (capitalflow == null)
            {
                return HttpNotFound();
            }
            return View(capitalflow);
        }

        // GET: Capitalflow/Create
        public ActionResult Create()
        {
            ViewBag.CapitalId = new SelectList(db.Capitals, "Id", "Subject");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Capitalflow/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CapitalId,Abstract,Income,Expenditure,Date,Remarks,UserId,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] Capitalflow capitalflow)
        {
            if (ModelState.IsValid)
            {
                capitalflow.Id = Guid.NewGuid();
                db.Capitalflows.Add(capitalflow);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CapitalId = new SelectList(db.Capitals, "Id", "Subject", capitalflow.CapitalId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", capitalflow.UserId);
            return View(capitalflow);
        }

        // GET: Capitalflow/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capitalflow capitalflow = db.Capitalflows.Find(id);
            if (capitalflow == null)
            {
                return HttpNotFound();
            }
            ViewBag.CapitalId = new SelectList(db.Capitals, "Id", "Subject", capitalflow.CapitalId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", capitalflow.UserId);
            return View(capitalflow);
        }

        // POST: Capitalflow/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CapitalId,Abstract,Income,Expenditure,Date,Remarks,UserId,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] Capitalflow capitalflow)
        {
            if (ModelState.IsValid)
            {
                db.Entry(capitalflow).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CapitalId = new SelectList(db.Capitals, "Id", "Subject", capitalflow.CapitalId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", capitalflow.UserId);
            return View(capitalflow);
        }

        // GET: Capitalflow/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capitalflow capitalflow = db.Capitalflows.Find(id);
            if (capitalflow == null)
            {
                return HttpNotFound();
            }
            return View(capitalflow);
        }

        // POST: Capitalflow/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Capitalflow capitalflow = db.Capitalflows.Find(id);
            db.Capitalflows.Remove(capitalflow);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IosClubManage.MVC.Models;
using PagedList;

namespace IosClubManage.MVC.Controllers
{
    [UserAuthorize]
    public class SuppliesRecordController : Controller
    {
        private IosClubDbContext db = new IosClubDbContext();

        // GET: SuppliesRecord
        public ActionResult Index(string keywords, int? pageIndex, Guid? Name)
        {
            var suppliesRecords = db.SuppliesRecords.Include(s => s.Supplies).Include(s => s.User);
            suppliesRecords = suppliesRecords.OrderBy(p => p.CreatedOn);
            int pageSize = 8;
            int pageNumber = (pageIndex ?? 1);
            if (Name != null)
            {
                suppliesRecords = suppliesRecords.Where(p => p.UserId == Name);
            }
            ViewBag.Name = new SelectList(db.Users, "Id", "Name", Name);

            return View(suppliesRecords.ToPagedList(pageNumber, pageSize));
        }

        // GET: SuppliesRecord/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuppliesRecord suppliesRecord = db.SuppliesRecords.Find(id);
            if (suppliesRecord == null)
            {
                return HttpNotFound();
            }
            return View(suppliesRecord);
        }

        // GET: SuppliesRecord/Create
        public ActionResult Create()
        {
            ViewBag.SuppliesId = new SelectList(db.Supplies, "Id", "SuppliesName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: SuppliesRecord/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RecordId,SuppliesId,UseNum,NumUnit,UserId,UseDate,Remarks,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] SuppliesRecord suppliesRecord)
        {
            if (ModelState.IsValid)
            {
                suppliesRecord.Id = Guid.NewGuid();
                db.SuppliesRecords.Add(suppliesRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SuppliesId = new SelectList(db.Supplies, "Id", "SuppliesName", suppliesRecord.SuppliesId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", suppliesRecord.UserId);
            return View(suppliesRecord);
        }

        // GET: SuppliesRecord/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuppliesRecord suppliesRecord = db.SuppliesRecords.Find(id);
            if (suppliesRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.SuppliesId = new SelectList(db.Supplies, "Id", "SuppliesName", suppliesRecord.SuppliesId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", suppliesRecord.UserId);
            return View(suppliesRecord);
        }

        // POST: SuppliesRecord/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RecordId,SuppliesId,UseNum,NumUnit,UserId,UseDate,Remarks,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] SuppliesRecord suppliesRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suppliesRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SuppliesId = new SelectList(db.Supplies, "Id", "SuppliesName", suppliesRecord.SuppliesId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", suppliesRecord.UserId);
            return View(suppliesRecord);
        }

        // GET: SuppliesRecord/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SuppliesRecord suppliesRecord = db.SuppliesRecords.Find(id);
            if (suppliesRecord == null)
            {
                return HttpNotFound();
            }
            return View(suppliesRecord);
        }

        // POST: SuppliesRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SuppliesRecord suppliesRecord = db.SuppliesRecords.Find(id);
            db.SuppliesRecords.Remove(suppliesRecord);
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

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
    public class EquipmentRecordController : Controller
    {
        private IosClubDbContext db = new IosClubDbContext();

        // GET: EquipmentRecord
        public ActionResult Index(string keywords, int? pageIndex, Guid? Name)
        {
            var equipmentRecords = db.EquipmentRecords.Include(e => e.Equipment).Include(e => e.User);
            equipmentRecords = equipmentRecords.OrderBy(p => p.CreatedOn);
            int pageSize = 8;
            int pageNumber = (pageIndex ?? 1);
            if (Name != null)
            {
                equipmentRecords = equipmentRecords.Where(p => p.UserId == Name);
            }
            ViewBag.Name = new SelectList(db.Users, "Id", "Name", Name);
           
            return View(equipmentRecords.ToPagedList(pageNumber, pageSize));
        }

        // GET: EquipmentRecord/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentRecord equipmentRecord = db.EquipmentRecords.Find(id);
            if (equipmentRecord == null)
            {
                return HttpNotFound();
            }
            return View(equipmentRecord);
        }

        // GET: EquipmentRecord/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "EquipmentName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: EquipmentRecord/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RecordId,EquipmentId,UserId,UseDate,Status,Remarks,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] EquipmentRecord equipmentRecord)
        {
            if (ModelState.IsValid)
            {
                equipmentRecord.Id = Guid.NewGuid();
                db.EquipmentRecords.Add(equipmentRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "EquipmentName", equipmentRecord.EquipmentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", equipmentRecord.UserId);
            return View(equipmentRecord);
        }

        // GET: EquipmentRecord/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentRecord equipmentRecord = db.EquipmentRecords.Find(id);
            if (equipmentRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "EquipmentName", equipmentRecord.EquipmentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", equipmentRecord.UserId);
            return View(equipmentRecord);
        }

        // POST: EquipmentRecord/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RecordId,EquipmentId,UserId,UseDate,Status,Remarks,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] EquipmentRecord equipmentRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipmentRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentId = new SelectList(db.Equipments, "Id", "EquipmentName", equipmentRecord.EquipmentId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", equipmentRecord.UserId);
            return View(equipmentRecord);
        }

        // GET: EquipmentRecord/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentRecord equipmentRecord = db.EquipmentRecords.Find(id);
            if (equipmentRecord == null)
            {
                return HttpNotFound();
            }
            return View(equipmentRecord);
        }

        // POST: EquipmentRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            EquipmentRecord equipmentRecord = db.EquipmentRecords.Find(id);
            db.EquipmentRecords.Remove(equipmentRecord);
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


        public ActionResult Charts()
        {
            return View();
        }
        public JsonResult GetCharts()
        {
            var list = IosClubManage.BLL.DrugManager.GetDrugChart();
            return Json(new { total = list.Total, data = list }, JsonRequestBehavior.AllowGet);
        }
    }
}

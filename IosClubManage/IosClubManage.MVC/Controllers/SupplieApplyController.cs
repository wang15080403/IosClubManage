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
    public class SupplieApplyController : Controller
    {
        private IosClubDbContext db = new IosClubDbContext();

        // GET: SupplieApply
        public ActionResult Index(string keywords, int? pageIndex, Guid? Name)
        {
            var supplieApplies = db.SupplieApplies.Include(s => s.Supplies).Include(s => s.User);
            supplieApplies = supplieApplies.OrderBy(p => p.CreatedOn);
            int pageSize = 8;
            int pageNumber = (pageIndex ?? 1);
            if (Name != null)
            {
                supplieApplies = supplieApplies.Where(p => p.UserId == Name);
            }
            ViewBag.Name = new SelectList(db.Users, "Id", "Name", Name);
            return View(supplieApplies.ToPagedList(pageNumber, pageSize));
        }

        // GET: SupplieApply/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplieApply supplieApply = db.SupplieApplies.Find(id);
            if (supplieApply == null)
            {
                return HttpNotFound();
            }
            return View(supplieApply);
        }

        // GET: SupplieApply/Create
        public ActionResult Create()
        {
            ViewBag.SuppliesId = new SelectList(db.Supplies, "Id", "SuppliesName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: SupplieApply/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SuppliesId,ApplyNum,NumUnit,ApplyDate,ApplyDepart,Departhead,Remarks,UserId,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] SupplieApply supplieApply)
        {
            if (ModelState.IsValid)
            {
                supplieApply.Id = Guid.NewGuid();
                db.SupplieApplies.Add(supplieApply);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SuppliesId = new SelectList(db.Supplies, "Id", "SuppliesName", supplieApply.SuppliesId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", supplieApply.UserId);
            return View(supplieApply);
        }

        // GET: SupplieApply/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplieApply supplieApply = db.SupplieApplies.Find(id);
            if (supplieApply == null)
            {
                return HttpNotFound();
            }
            ViewBag.SuppliesId = new SelectList(db.Supplies, "Id", "SuppliesName", supplieApply.SuppliesId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", supplieApply.UserId);
            return View(supplieApply);
        }

        // POST: SupplieApply/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SuppliesId,ApplyNum,NumUnit,ApplyDate,ApplyDepart,Departhead,Remarks,UserId,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] SupplieApply supplieApply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplieApply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SuppliesId = new SelectList(db.Supplies, "Id", "SuppliesName", supplieApply.SuppliesId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", supplieApply.UserId);
            return View(supplieApply);
        }

        // GET: SupplieApply/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupplieApply supplieApply = db.SupplieApplies.Find(id);
            if (supplieApply == null)
            {
                return HttpNotFound();
            }
            return View(supplieApply);
        }

        // POST: SupplieApply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SupplieApply supplieApply = db.SupplieApplies.Find(id);
            db.SupplieApplies.Remove(supplieApply);
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

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
    public class RoleController : Controller
    {
        private IosClubDbContext db = new IosClubDbContext();

        // GET: Role
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Role/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoleName,RoleCode,IsSuperRole,IsDefauletRole,Remark,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] Role role)
        {
            if (ModelState.IsValid)
            {
                role.Id = Guid.NewGuid();
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Role/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Role/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoleName,RoleCode,IsSuperRole,IsDefauletRole,Remark,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Role/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
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


        #region 授权用户

        // GET: SystemRole/Approve/5
        public ActionResult Approve(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            //便于View中按部门排序展开所有用户
            ViewBag.Departments = db.Departments.Include(p => p.Users).ToList();
            //ViewBag.Users = db.Users.Where(p => p.IsDeleted == false && p.IsActive == true).Include(p => p.Department).ToList(); 
            return View(role);
        }
        #endregion

        // POST: SystemRole/Approve/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve([Bind(Include = "Id,RoleName,IsSuperRole,IsDefaultRole,Remarks,DisplayOrder,IsActive")] Role roles, string[] roleUsers)
        {
            var role = db.Roles.Where(p => p.Id == roles.Id).Include(p => p.Users).FirstOrDefault();
            var allUsers = db.Users.ToList();

            if (roleUsers != null)
            {
                var selectedRoleUsers = new HashSet<string>(roleUsers);

                foreach (var item in allUsers)
                {
                    if (selectedRoleUsers.Contains(item.Id.ToString()))
                    {
                        if (role.Users.Where(p => p.Id == item.Id).Count() == 0)
                        {
                            role.Users.Add(item);
                        }
                    }
                    else
                    {
                        if (role.Users.Where(p => p.Id == item.Id).Count() > 0)
                        {
                            role.Users.Remove(item);
                        }
                    }
                }
            }
            else
            {
                foreach (var item in allUsers)
                {
                    role.Users.Remove(item);
                }
            }
            db.Entry(role).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}

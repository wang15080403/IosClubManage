using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IosClubManage.MVC.Models;
using IosClubManage.MVC.ViewModels;
using IosClubManage.MVC.Services;
using PagedList;



namespace IosClubManage.MVC.Controllers
{
    [UserAuthorize]
    public class UserController : Controller
    {
        private IosClubDbContext db = new IosClubDbContext();

        // GET: User
        public ActionResult Index(string keywords, int? pageIndex, Guid? UserCode)
        {
            var users = db.Users.Include(u => u.Department);
            users = users.OrderBy(p => p.CreatedOn);
            int pageSize = 8;
            int pageNumber = (pageIndex ?? 1);
            if (UserCode != null)
            {
                users = users.Where(p => p.Id == UserCode);
            }
            ViewBag.UserCode = new SelectList(db.Users, "Id", "UserCode", UserCode);
            if (!String.IsNullOrEmpty(keywords))
            {
                ViewBag.keywords = keywords;
                users = users.Where(p => p.Name.Contains(keywords));

            }
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // GET: User/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartName");
            return View();
        }

        // POST: User/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserCode,UserPwd,Name,DepartmentId,Birthday,Email,Phone,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartName", user.DepartmentId);
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartName", user.DepartmentId);
            return View(user);
        }

        // POST: User/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserCode,UserPwd,Name,DepartmentId,Birthday,Email,Phone,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartName", user.DepartmentId);
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
        [AllowAnonymous]

        // GET: /User/Login
        public ActionResult Login()
        {
            //var vmuser = new VMUserLogin();
            return View();
        }

        // POST: /User/login
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login([Bind(Include = "LoginId,Password,IsRemberMe")]VMUserLogin vmuser)
        {
            var thisUser = UserService.Login(vmuser.LoginId, vmuser.Password);
            if (!ModelState.IsValid)
            {
                return View(vmuser);
            }
            if (thisUser == null)
            {
                ModelState.AddModelError("", "账号、密码错误，或该账号未启用！");
                return View(vmuser);
            }
            
            UserService.SetAuthTicketCookie(thisUser, vmuser.IsRemberMe);
            return RedirectToAction("Index", "Home");
        }


        // GET: /User/ModifPassword
        public ActionResult ModifyPwd()
        {

            var loginUser = UserService.GetUserFromCookie();
            if (loginUser == null)
            {
                return HttpNotFound();
            }
            var user = new VModifyPwd();
            user.LoginId = loginUser.UserCode;
            //VModifyPwd vmuser = new VModifyPwd();
            //HttpCookie cookie = Request.Cookies["UserLogin"];
            //if (cookie == null)
            //{
            //    return RedirectToAction("Login", "User", new { });
            //}
            //else
            //{
            //    vmuser.LoginId = cookie.Values["loginId"];
            //}
            return View(user);
        }

        // POST: /User/ModifPassword
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyPwd(VModifyPwd vmuser)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Where(p => p.UserCode == vmuser.LoginId && p.UserPwd == vmuser.Pwd).FirstOrDefault();
                if (user != null)
                {
                    user.UserPwd = vmuser.NewPwd;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "User", new { });
                }
                ModelState.AddModelError("", "用户名或原密码错误！修改失败！");
            }
            return View(vmuser);
        }
        // GET: /User/LoginPartial  登陆部件（菜单）
        public ActionResult LoginPartial()
        {
            var user = UserService.GetUserFromCookie();
            return View(user);
        }
        // GET: /User/LoginOut  注销
        public ActionResult LoginOut()
        {
            UserService.LoginOut();
            return RedirectToAction("Login", "User");
        }
    }
}

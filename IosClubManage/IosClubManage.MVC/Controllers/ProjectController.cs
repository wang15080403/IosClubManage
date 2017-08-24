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
using IosClubManage.MVC.Services;
using IosClubManage.MVC.ViewModels;

namespace IosClubManage.MVC.Controllers
{
    [UserAuthorize]
    public class ProjectController : Controller
    {
        private IosClubDbContext db = new IosClubDbContext();

        // GET: Project
        public ActionResult Index(string keywords, int? pageIndex, Guid? cateId, int? projectstatus)
        {
            var projects = db.Projects.Include(p => p.Category).Include(p => p.Department).Include(p => p.Source);
            projects = projects.OrderBy(p => p.CreatedOn);
            int pageSize = 8;
            int pageNumber = (pageIndex ?? 1);
            if (!String.IsNullOrEmpty(keywords))
            {
                ViewBag.keywords = keywords;
                ViewBag.Cate = cateId;
                projects = projects.Where(p => p.ProjectName.Contains(keywords));
            }
            if (cateId != null)
            {
                projects = projects.Where(p => p.CategoryId == cateId);

            }
            ViewBag.cateId = new SelectList(db.Categories, "Id", "CategoryName", cateId);
            switch (projectstatus)
            {
                case 0:
                    projects = projects.Where(p => p.IsApproval == false);//未立项
                    break;
                case 1:
                    projects = projects.Where(p => p.IsApproval && p.IsKnot == false);//已立项未结项
                    break;
                case 2:
                    projects = projects.Where(p => p.IsApproval && p.IsKnot);//已立项结项
                    break;
                case 3:
                    projects = projects.Where(p => p.IsStop == false);//未终止
                    break;
                case 4:
                    projects = projects.Where(p => p.IsStop);//已终止
                    break;
            }
            ViewBag.ProjectStatus = new List<SelectListItem>() {
                new SelectListItem(){Value="0",Text="未立项"},
                new SelectListItem(){Value="1",Text="已立项未结项"},
                new SelectListItem(){Value="2",Text="已立项且结项"},
                new SelectListItem(){Value="3",Text="未终止"},
                new SelectListItem(){Value="4",Text="已终止"}
            };
            return View(projects.ToPagedList(pageNumber, pageSize));
            //return View(projects.ToList());
        }

        // GET: Project/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartName");
            ViewBag.SourceId = new SelectList(db.Sources, "Id", "Name");
            return View();
        }

        // POST: Project/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectCode,ProjectName,CategoryId,DepartmentId,UserName,ProjectAmmount,Remarks,SourceId,IsApproval,ApprovalTime,PlanKnotTime,IsKnot,KnotTime,IsStop,StopTime,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.Id = Guid.NewGuid();
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", project.CategoryId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartName", project.DepartmentId);
            ViewBag.SourceId = new SelectList(db.Sources, "Id", "Name", project.SourceId);
            return View(project);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", project.CategoryId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartName", project.DepartmentId);
            ViewBag.SourceId = new SelectList(db.Sources, "Id", "Name", project.SourceId);
            return View(project);
        }

        // POST: Project/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectCode,ProjectName,CategoryId,DepartmentId,UserName,ProjectAmmount,Remarks,SourceId,IsApproval,ApprovalTime,PlanKnotTime,IsKnot,KnotTime,IsStop,StopTime,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", project.CategoryId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartName", project.DepartmentId);
            ViewBag.SourceId = new SelectList(db.Sources, "Id", "Name", project.SourceId);
            return View(project);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
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


        #region 统计报表
       // GET: Report
        public ActionResult Report(string condition)
        {
            var projects = db.Projects.Where(p => p.IsDelete == false)
                .Include(p => p.Category)
                .Include(p => p.Source)
                .Include(p => p.Department)
                .ToList();
            var report = new List<VMReport>();

            switch (condition)
            {
                case "Category":
                    ViewBag.ConditionName = "项目类别";
                    report = projects.GroupBy(p => p.Category.CategoryName)
                        .Select(p => new VMReport
                        {
                            Condition = p.Key,
                            ProjectCount = p.Count(),
                            ProjectsIsApproved = p.Count(t => t.IsApproval),
                            ProjectsIsKnot = p.Count(t => t.IsKnot),
                            ProjectsIsStop = p.Count(t => t.IsStop)
                        }).ToList();
                    break;
                case "Department":
                    ViewBag.ConditionName = "承担部门";
                    report = projects.GroupBy(p => p.Department.DepartName)
                        .Select(p => new VMReport
                        {
                            Condition = p.Key,
                            ProjectCount = p.Count(),
                            ProjectsIsApproved = p.Count(t => t.IsApproval),
                            ProjectsIsKnot = p.Count(t => t.IsKnot),
                            ProjectsIsStop = p.Count(t => t.IsStop)
                        }).ToList();
                    break;
                //case "Manager":
                //    ViewBag.ConditionName = "项目负责人";
                //    report = projects.GroupBy(p => p.ProjectManager.DisplayName)
                //        .Select(p => new VMReport
                //        {
                //            Condition = p.Key,
                //            ProjectCount = p.Count(),
                //            ProjectsIsApproved = p.Count(t => t.IsApproved),
                //            ProjectsIsFinancialAudited = p.Count(t => t.IsFinancialAudited),
                //            ProjectsIsFinished = p.Count(t => t.IsFinished),
                //            ProjectsIsTerminated = p.Count(t => t.IsTerminated)
                //        }).ToList();
                //    break;

                default:
                    ViewBag.ConditionName = "项目来源";
                    report = projects.GroupBy(p => p.Source.Name)
                        .Select(p => new VMReport
                        {
                            Condition = p.Key,
                            ProjectCount = p.Count(),
                            ProjectsIsApproved = p.Count(t => t.IsApproval),
                            ProjectsIsKnot = p.Count(t => t.IsKnot),
                            ProjectsIsStop = p.Count(t => t.IsStop)
                        }).ToList();
                    break;

            }


            var lists = new List<SelectListItem>();
            lists.Add(new SelectListItem { Text = "项目来源", Value = "" });
            lists.Add(new SelectListItem { Text = "项目类别", Value = "Category" });
            lists.Add(new SelectListItem { Text = "承担部门", Value = "Department" });
            //lists.Add(new SelectListItem { Text = "项目负责人", Value = "Manager" });

            ViewBag.Condition = new SelectList(lists, "Value", "Text", condition);

            return View(report);
        }
        #endregion
        /*---------------------------下拉框两级联动--------------------------------*/
        public ActionResult test(Project project)
        {
            ViewBag.Department = new SelectList(db.Departments, "Id", "DepartName", project.DepartmentId);
            ViewBag.User = new SelectList(db.Users, "Id", "Name");
            return View(project);
        }

        public JsonResult GetProvincelist()
        {
            var list = db.Departments.ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCitylist(Guid departId)
        {
            var user = db.Users.Where(m => m.DepartmentId == departId).ToList();

            List<SelectListItem> item = new List<SelectListItem>();

            foreach (var User in user)
            {
                item.Add(new SelectListItem { Text = User.Name, Value = User.Id.ToString() });
            }
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        /*----------------------------------------------------------------------------*/


        #region 我的项目
        // GET: MyProjectsIndex
        public ActionResult MyProjectsIndex(int? projectStatus)
        {
            var user = UserService.GetUserFromCookie();

            var projects = db.Projects.Where(p => p.UserName == user.Name).Include(p => p.Category);
            switch (projectStatus)
            {
                case 0:
                    projects = projects.Where(p => p.IsApproval == false);
                    break;
                case 1:
                    projects = projects.Where(p => p.IsApproval);
                    break;
                case 2:
                    projects = projects.Where(p => p.IsApproval && p.IsKnot == false);
                    break;
                case 3:
                    projects = projects.Where(p => p.IsApproval && p.IsKnot);
                    break;
                //case 4:
                //    projects = projects.Where(p => p.IsApproved && p.IsFinished == false);
                //    break;
                //case 5:
                //    projects = projects.Where(p => p.IsApproved && p.IsFinished);
                //    break;
                //case 6:
                //    projects = projects.Where(p => p.IsApproved && p.IsTerminated);
                //    break;
                default:
                    break;

            }

            var lists = new List<SelectListItem>();
            lists.Add(new SelectListItem { Text = "立项申请中…", Value = "0" });
            lists.Add(new SelectListItem { Text = "已立项", Value = "1" });
            lists.Add(new SelectListItem { Text = "财务审核中…", Value = "2" });
            lists.Add(new SelectListItem { Text = "财务已审核", Value = "3" });
            lists.Add(new SelectListItem { Text = "未结项", Value = "4" });
            lists.Add(new SelectListItem { Text = "已结项", Value = "5" });
            lists.Add(new SelectListItem { Text = "已终止", Value = "6" });

            ViewBag.ProjectStatus = new SelectList(lists, "Value", "Text", projectStatus);


            return View(projects.ToList());
        }
        //return View(projects.ToList());

        #endregion
    }
}

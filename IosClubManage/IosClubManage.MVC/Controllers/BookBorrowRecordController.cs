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
    public class BookBorrowRecordController : Controller
    {
        private IosClubDbContext db = new IosClubDbContext();

        // GET: BookBorrowRecord
        public ActionResult Index(string keywords, int? pageIndex, Guid? Name)
        {
            var bookBorrowRecords = db.BookBorrowRecords.Include(b => b.Book).Include(b => b.User);
            bookBorrowRecords = bookBorrowRecords.OrderBy(p => p.CreatedOn);
            int pageSize = 8;
            int pageNumber = (pageIndex ?? 1);
            if (Name != null)
            {
                bookBorrowRecords = bookBorrowRecords.Where(p => p.UserId == Name);
            }
            ViewBag.Name = new SelectList(db.Users, "Id", "Name", Name);

            return View(bookBorrowRecords.ToPagedList(pageNumber, pageSize));
        }

        // GET: BookBorrowRecord/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookBorrowRecord bookBorrowRecord = db.BookBorrowRecords.Find(id);
            if (bookBorrowRecord == null)
            {
                return HttpNotFound();
            }
            return View(bookBorrowRecord);
        }

        // GET: BookBorrowRecord/Create
        public ActionResult Create()
        {
            ViewBag.BookId = new SelectList(db.Books, "Id", "BookName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: BookBorrowRecord/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookCode,BookId,BorrowDate,ReturnDate,UserId,Librarian,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] BookBorrowRecord bookBorrowRecord)
        {
            if (ModelState.IsValid)
            {
                bookBorrowRecord.Id = Guid.NewGuid();
                db.BookBorrowRecords.Add(bookBorrowRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookId = new SelectList(db.Books, "Id", "BookName", bookBorrowRecord.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", bookBorrowRecord.UserId);
            return View(bookBorrowRecord);
        }

        // GET: BookBorrowRecord/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookBorrowRecord bookBorrowRecord = db.BookBorrowRecords.Find(id);
            if (bookBorrowRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Books, "Id", "BookName", bookBorrowRecord.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", bookBorrowRecord.UserId);
            return View(bookBorrowRecord);
        }

        // POST: BookBorrowRecord/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookCode,BookId,BorrowDate,ReturnDate,UserId,Librarian,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] BookBorrowRecord bookBorrowRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookBorrowRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Books, "Id", "BookName", bookBorrowRecord.BookId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", bookBorrowRecord.UserId);
            return View(bookBorrowRecord);
        }

        // GET: BookBorrowRecord/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookBorrowRecord bookBorrowRecord = db.BookBorrowRecords.Find(id);
            if (bookBorrowRecord == null)
            {
                return HttpNotFound();
            }
            return View(bookBorrowRecord);
        }

        // POST: BookBorrowRecord/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            BookBorrowRecord bookBorrowRecord = db.BookBorrowRecords.Find(id);
            db.BookBorrowRecords.Remove(bookBorrowRecord);
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

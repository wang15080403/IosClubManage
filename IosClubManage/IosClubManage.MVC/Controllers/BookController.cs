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
    public class BookController : Controller
    {
        private IosClubDbContext db = new IosClubDbContext();

        // GET: Book
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.BookCategory);
            return View(books.ToList());
        }

        // GET: Book/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            ViewBag.BookCategoryId = new SelectList(db.BookCategories, "Id", "BookCategoryName");
            return View();
        }

        // POST: Book/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookCode,BookName,BookCategoryId,Author,Publisher,PublisherDate,Price,Num,Remarks,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = Guid.NewGuid();
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookCategoryId = new SelectList(db.BookCategories, "Id", "BookCategoryName", book.BookCategoryId);
            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookCategoryId = new SelectList(db.BookCategories, "Id", "BookCategoryName", book.BookCategoryId);
            return View(book);
        }

        // POST: Book/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookCode,BookName,BookCategoryId,Author,Publisher,PublisherDate,Price,Num,Remarks,IsActive,IsDelete,CreatedOn,CreatedBy,UpdateOdn,UpdatedBy")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookCategoryId = new SelectList(db.BookCategories, "Id", "BookCategoryName", book.BookCategoryId);
            return View(book);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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

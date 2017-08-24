using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IosClubManage.MVC.Models
{
    public class Book : EntityBase
    {
        public Book()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Display(Name = "图书编号")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string BookCode { get; set; }

        [Display(Name = "图书名称")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string BookName { get; set; }

        [Display(Name = "图书类别")]
        public virtual Guid BookCategoryId { get; set; }
        public virtual BookCategory BookCategory { get; set; }

        [Display(Name = "作者")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string Author { get; set; }

        [Display(Name = "出版社")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string Publisher { get; set; }

        [Display(Name = "出版时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "{0}必须是日期类型")]
        public DateTime? PublisherDate { get; set; }

        [Display(Name = "价格")]
        [Required(ErrorMessage = "{0}是必需的")]
        public decimal Price { get; set; }

        [Display(Name = "数量")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string Num { get; set; }

        [Display(Name = "备注")]
        public string Remarks { get; set; }


        public virtual ICollection<BookBorrowRecord> BookBorrowRecords { get; set; }
    }
}
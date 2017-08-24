using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IosClubManage.MVC.Models
{
    public class BookBorrowRecord : EntityBase
    {
        public BookBorrowRecord()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Display(Name = "图书编号")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string BookCode { get; set; }

        [Display(Name = "图书名称")]
        public virtual Guid BookId { get; set; }
        public virtual Book Book { get; set; }

        [Display(Name = "借阅日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "{0}必须是日期类型")]
        public DateTime? BorrowDate { get; set; }

        [Display(Name = "归还日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "{0}必须是日期类型")]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "借阅人")]
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }

        [Display(Name = "图书管理员")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string Librarian { get; set; }
    }
}
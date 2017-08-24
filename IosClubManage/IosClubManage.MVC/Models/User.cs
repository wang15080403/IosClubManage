using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IosClubManage.MVC.Models
{
    public class User : EntityBase
    {
        public User()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        [Display(Name = "员工号")]
        public string UserCode { get; set; }
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        public string UserPwd { get; set; }
        [Display(Name = "真实姓名")]
        public string Name { get; set; }
        [Display(Name = "部门名称")]
        public virtual Guid DepartmentId { get; set; }
        [Display(Name = "部门名称")]
        public virtual Department Department { get; set; }
        [Display(Name = "出生日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? Birthday { get; set; }
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
        [Display(Name = "电话")]
        public string Phone { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        public virtual ICollection<Warehouse> Warehouses { get; set; }
        public virtual ICollection<EquipmentRecord> EquipmentRecords { get; set; }
        public virtual ICollection<SuppliesRecord> SuppliesRecords { get; set; }
        public virtual ICollection<SupplieApply> SupplieApplys { get; set; }
        public virtual ICollection<Capitalflow> Capitalflows { get; set; }
        public virtual ICollection<BookBorrowRecord> BookBorrowRecords { get; set; }
    }
}
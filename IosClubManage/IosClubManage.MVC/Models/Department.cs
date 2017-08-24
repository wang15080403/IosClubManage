using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IosClubManage.MVC.Models
{
    public class Department : EntityBase
    {
        public Department()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        [Display(Name = "部门编码")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string DepartCode { get; set; }
        [Display(Name = "部门名称")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string DepartName { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
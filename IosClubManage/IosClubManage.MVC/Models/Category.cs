using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IosClubManage.MVC.Models
{
    public class Category : EntityBase
    {
        public Category()
        {
            Id = Guid.NewGuid();
            NodeLevel = 0;
        }
        public Guid Id { get; set; }
        [Display(Name = "类别名称")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string CategoryName { get; set; }

        [Display(Name = "备注")]
        public string Remarks { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

        [Display(Name = "分类层级")]
        public int NodeLevel { get; set; }

        [Display(Name = "上级分类")]
        public Guid? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> ChildCates { get; set; }
    }
}
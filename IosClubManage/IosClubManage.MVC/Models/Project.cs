using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IosClubManage.MVC.Models
{
    public class Project : EntityBase
    {
        public Project()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Display(Name = "项目编码")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string ProjectCode { get; set; }
        [Display(Name = "项目名称")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string ProjectName { get; set; }

        [Display(Name = "分类名称")]
        public virtual Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Display(Name = "部门名称")]
        public virtual Guid DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        [Display(Name = "项目负责人")]
        public string UserName { get; set; }
        [Display(Name = "项目总经费")]
        public decimal ProjectAmmount { get; set; }
        [Display(Name = "备注")]
        public string Remarks { get; set; }
        [Display(Name = "项目来源")]
        public virtual Guid SourceId { get; set; }
        public virtual Source Source { get; set; }


        [Display(Name = "是否立项")]
        public bool IsApproval { get; set; }
        [Display(Name = "立项日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? ApprovalTime { get; set; }
        [Display(Name = "计划结项日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? PlanKnotTime { get; set; }

        [Display(Name = "是否结项")]
        public bool IsKnot { get; set; }
        [Display(Name = "结项日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? KnotTime { get; set; }
        [Display(Name = "是否终止")]
        public bool IsStop { get; set; }
        [Display(Name = "终止日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? StopTime { get; set; }
    }
}
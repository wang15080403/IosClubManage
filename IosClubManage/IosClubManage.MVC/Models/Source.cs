using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IosClubManage.MVC.Models
{
    public class Source : EntityBase
    {
        public Source()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        [Display(Name = "项目来源名称")]
        public string Name { get; set; }
        [Display(Name = "备注")]
        public string Remarks { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IosClubManage.MVC.ViewModels
{
    public class VMReport
    {
        public string Condition { get; set; }
        [Display(Name = "总项目数")]
        public int ProjectCount { get; set; }
        [Display(Name = "已立项")]
        public int ProjectsIsApproved { get; set; }
        [Display(Name = "已结项")]
        public int ProjectsIsKnot { get; set; }
        [Display(Name = "已终止")]
        public int ProjectsIsStop { get; set; }
    }
}
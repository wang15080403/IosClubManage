using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IosClubManage.MVC.ViewModels
{
    public class VModifyPwd
    {
        [Display(Name = "用户名")]
        public string LoginId { get; set; }

        [Display(Name = "原密码")]
        [DataType(DataType.Password)]
        public string Pwd { get; set; }

        [Display(Name = "新密码")]
        [DataType(DataType.Password)]
        public string NewPwd { get; set; }

        [Display(Name = "重复密码")]
        [DataType(DataType.Password)]
        [Compare("NewPwd")]
        public string RePwd { get; set; }
    }
}
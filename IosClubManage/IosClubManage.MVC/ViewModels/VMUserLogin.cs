using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IosClubManage.MVC.ViewModels
{
    public class VMUserLogin
    {
        [Display(Name = "用户账号")]
        [Required]
        public string LoginId { get; set; }
        [Display(Name = "密码")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "记住我")]
        [Required]
        public bool IsRemberMe { get; set; }
    }
}
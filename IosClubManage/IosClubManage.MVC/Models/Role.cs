using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IosClubManage.MVC.Models
{
    public class Role : EntityBase
    {
        public Role()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }
        [Display(Name = "角色编码")]
        public string RoleCode { get; set; }
        [Display(Name = "是否超级管理员角色")]
        public bool IsSuperRole { get; set; }
        [Display(Name = "是否默认角色")]
        public bool IsDefauletRole { get; set; }
        [Display(Name = "备注")]
        public string Remark { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
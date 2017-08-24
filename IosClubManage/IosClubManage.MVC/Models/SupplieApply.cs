using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IosClubManage.MVC.Models
{
    public class SupplieApply : EntityBase
    {
        public SupplieApply()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Display(Name = "耗材名称")]
        public virtual Guid SuppliesId { get; set; }
        public virtual Supplies Supplies { get; set; }

        [Display(Name = "申请数量")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string ApplyNum { get; set; }

        [Display(Name = "数量单位")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string NumUnit { get; set; }

        [Display(Name = "申请日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "{0}必须是日期类型")]
        public DateTime? ApplyDate { get; set; }

        [Display(Name = "申领部门")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string ApplyDepart { get; set; }

        [Display(Name = "部门负责人")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string Departhead { get; set; }

        [Display(Name = "备注")]
        public string Remarks { get; set; }

        [Display(Name = "领用申请人")]
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
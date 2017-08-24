using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IosClubManage.MVC.Models
{
    public class SuppliesRecord : EntityBase
    {
        public SuppliesRecord()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Display(Name = "记录编号")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string RecordId { get; set; }

        [Display(Name = "耗材名称")]
        public virtual Guid SuppliesId { get; set; }
        public virtual Supplies Supplies { get; set; }

        [Display(Name = "领用数量")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string UseNum { get; set; }

        [Display(Name = "数量单位")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string NumUnit { get; set; }

        [Display(Name = "领用人")]
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }

        [Display(Name = "领用日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "{0}必须是日期类型")]
        public DateTime? UseDate { get; set; }

        [Display(Name = "备注")]
        public string Remarks { get; set; }
    }
}
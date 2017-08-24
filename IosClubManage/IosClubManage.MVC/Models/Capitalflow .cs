using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IosClubManage.MVC.Models
{
    public class Capitalflow : EntityBase
    {
        public Capitalflow()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Display(Name = "资金科目")]
        public virtual Guid CapitalId { get; set; }
        public virtual Capital Capital { get; set; }

        [Display(Name = "摘要")]
        public string Abstract { get; set; }

        [Display(Name = "收入")]
        public decimal Income { get; set; }

        [Display(Name = "支出")]
        public decimal Expenditure { get; set; }

        [Display(Name = "日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "{0}必须是日期类型")]
        public DateTime? Date { get; set; }

        [Display(Name = "备注")]
        public string Remarks { get; set; }

        [Display(Name = "资金管理员")]
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
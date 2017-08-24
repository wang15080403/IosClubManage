using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IosClubManage.MVC.Models
{
    public class Supplies : EntityBase
    {
        public Supplies()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Display(Name = "耗材名称")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string SuppliesName { get; set; }


        [Display(Name = "耗材类别")]
        public virtual Guid SuppliesCategoryId { get; set; }
        public virtual SuppliesCategory SuppliesCategory { get; set; }


        [Display(Name = "购买日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "{0}必须是日期类型")]
        public DateTime? PurchaseDate { get; set; }


        [Display(Name = "价格")]
        [Required(ErrorMessage = "{0}是必需的")]
        public decimal Price { get; set; }


        [Display(Name = "备注")]
        public string Remarks { get; set; }

        public virtual ICollection<SupplieApply> SupplieApplys { get; set; }

        public virtual ICollection<SuppliesRecord> SuppliesRecords { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IosClubManage.MVC.Models
{
    public class Warehouse : EntityBase
    {
        public Warehouse()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Display(Name = "仓库编码")]
        [Required(ErrorMessage = "{0}是必须的。")]
        [StringLength(17, MinimumLength = 3)]
        public string WarehouseCode { get; set; }

        [Display(Name = "仓库名称")]
        [Required(ErrorMessage = "{0}是必须的。")]
        public string WarehouseName { get; set; }

        [Display(Name = "库存产品")]
        [Required(ErrorMessage = "{0}是必须的。")]
        public string WarehProduct { get; set; }

        [Display(Name = "库存数量")]
        [Required(ErrorMessage = "{0}是必须的。")]
        public string Invequantity { get; set; }

        [Display(Name = "存放地点")]
        [Required(ErrorMessage = "{0}是必须的。")]
        public string Storagelocation { get; set; }

        [Display(Name = "存放时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "{0}必须是日期类型")]
        public DateTime? Storagetime { get; set; }

        [Display(Name = "备注")]
        public string Remarks { get; set; }


        [Display(Name = "负责人")]
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
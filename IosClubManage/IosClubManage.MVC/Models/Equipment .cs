using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IosClubManage.MVC.Models
{
    public class Equipment : EntityBase
    {
        public Equipment()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Display(Name = "设备型号")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string EquipModel { get; set; }

        [Display(Name = "设备编号")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string EquipCode { get; set; }

        [Display(Name = "设备名称")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string EquipmentName { get; set; }

        [Display(Name = "生产日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "{0}必须是日期类型")]
        public DateTime? ProductDate { get; set; }

        [Display(Name = "购买日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "{0}必须是日期类型")]
        public DateTime? PurchaseDate { get; set; }

        [Display(Name = "报废日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "{0}必须是日期类型")]
        public DateTime? ScrapDate { get; set; }

        [Display(Name = "使用年限")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string Servicelife { get; set; }

        [Display(Name = "折现率")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string Discountrate { get; set; }

        [Display(Name = "价格")]
        [Required(ErrorMessage = "{0}是必需的")]
        public decimal Price { get; set; }


        [Display(Name = "备注")]
        public string Remarks { get; set; }

        public virtual ICollection<EquipmentRecord> EquipmentRecords { get; set; }
    }
}
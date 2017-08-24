using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IosClubManage.MVC.Models
{
    public class EquipmentRecord : EntityBase
    {
        public EquipmentRecord()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Display(Name = "记录编号")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string RecordId { get; set; }

        [Display(Name = "设备名称")]
        public virtual Guid EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set; }

        [Display(Name = "使用人")]
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }

        [Display(Name = "使用日期")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "{0}必须是日期类型")]
        public DateTime? UseDate { get; set; }

        [Display(Name = "设备状态")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string Status { get; set; }

        [Display(Name = "备注")]
        public string Remarks { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IosClubManage.MVC.Models
{
    public class EntityBase
    {
        [Display(Name = "是否启用")]
        public bool IsActive { get; set; }
        [Display(Name = "是否删除")]
        public bool IsDelete { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdateOdn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
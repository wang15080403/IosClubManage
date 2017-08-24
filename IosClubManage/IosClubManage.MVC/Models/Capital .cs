using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IosClubManage.MVC.Models
{
    public class Capital : EntityBase
    {
        public Capital()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Display(Name = "资金科目")]
        [Required(ErrorMessage = "{0}是必需的")]
        public string Subject { get; set; }

        [Display(Name = "备注")]
        public string Remarks { get; set; }


        public virtual ICollection<Capitalflow> Capitalflows { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace IosClubManage.MVC.Models
{
    public partial class IosClubDbContext : DbContext
    {
        static IosClubDbContext()
        {
            Database.SetInitializer<IosClubDbContext>(null);
        }
        public IosClubDbContext()
            : base("IosClubDbContext")
        { }
        public DbSet<Category> Categories { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.Department> Departments { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.Source> Sources { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.Role> Roles { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.Project> Projects { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.Book> Books { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.BookCategory> BookCategories { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.BookBorrowRecord> BookBorrowRecords { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.Capital> Capitals { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.Capitalflow> Capitalflows { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.Equipment> Equipments { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.EquipmentRecord> EquipmentRecords { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.Supplies> Supplies { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.SuppliesCategory> SuppliesCategories { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.SuppliesRecord> SuppliesRecords { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.SupplieApply> SupplieApplies { get; set; }

        public System.Data.Entity.DbSet<IosClubManage.MVC.Models.Warehouse> Warehouses { get; set; }
    }
}
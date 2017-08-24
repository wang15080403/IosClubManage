namespace IosClubManage.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateDB1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BookCategoryName = c.String(nullable: false),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdateOdn = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BookCode = c.String(nullable: false),
                        BookName = c.String(nullable: false),
                        BookCategoryId = c.Guid(nullable: false),
                        Author = c.String(nullable: false),
                        Publisher = c.String(nullable: false),
                        PublisherDate = c.DateTime(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Num = c.String(nullable: false),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdateOdn = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookCategories", t => t.BookCategoryId, cascadeDelete: true)
                .Index(t => t.BookCategoryId);
            
            CreateTable(
                "dbo.BookBorrowRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BookCode = c.String(nullable: false),
                        BookId = c.Guid(nullable: false),
                        BorrowDate = c.DateTime(),
                        ReturnDate = c.DateTime(),
                        UserId = c.Guid(nullable: false),
                        Librarian = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Capitalflows",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CapitalId = c.Guid(nullable: false),
                        Abstract = c.String(),
                        Income = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Expenditure = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(),
                        Remarks = c.String(),
                        UserId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdateOdn = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Capitals", t => t.CapitalId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CapitalId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Capitals",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Subject = c.String(nullable: false),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdateOdn = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EquipmentRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RecordId = c.String(nullable: false),
                        EquipmentId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        UseDate = c.DateTime(),
                        Status = c.String(nullable: false),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdateOdn = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipments", t => t.EquipmentId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.EquipmentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        EquipModel = c.String(nullable: false),
                        EquipCode = c.String(nullable: false),
                        EquipmentName = c.String(nullable: false),
                        ProductDate = c.DateTime(),
                        PurchaseDate = c.DateTime(),
                        ScrapDate = c.DateTime(),
                        Servicelife = c.String(nullable: false),
                        Discountrate = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdateOdn = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SupplieApplies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SuppliesId = c.Guid(nullable: false),
                        ApplyNum = c.String(nullable: false),
                        NumUnit = c.String(nullable: false),
                        ApplyDate = c.DateTime(),
                        ApplyDepart = c.String(nullable: false),
                        Departhead = c.String(nullable: false),
                        Remarks = c.String(),
                        UserId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdateOdn = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Supplies", t => t.SuppliesId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.SuppliesId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Supplies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SuppliesName = c.String(nullable: false),
                        SuppliesCategoryId = c.Guid(nullable: false),
                        PurchaseDate = c.DateTime(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdateOdn = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SuppliesCategories", t => t.SuppliesCategoryId, cascadeDelete: true)
                .Index(t => t.SuppliesCategoryId);
            
            CreateTable(
                "dbo.SuppliesCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SuppliesCategoryName = c.String(nullable: false),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdateOdn = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SuppliesRecords",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RecordId = c.String(nullable: false),
                        SuppliesId = c.Guid(nullable: false),
                        UseNum = c.String(nullable: false),
                        NumUnit = c.String(nullable: false),
                        UserId = c.Guid(nullable: false),
                        UseDate = c.DateTime(),
                        Remarks = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdateOdn = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Supplies", t => t.SuppliesId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.SuppliesId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        WarehouseCode = c.String(nullable: false, maxLength: 17),
                        WarehouseName = c.String(nullable: false),
                        WarehProduct = c.String(nullable: false),
                        Invequantity = c.String(nullable: false),
                        Storagelocation = c.String(nullable: false),
                        Storagetime = c.DateTime(),
                        Remarks = c.String(),
                        UserId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdateOdn = c.DateTime(),
                        UpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "BookCategoryId", "dbo.BookCategories");
            DropForeignKey("dbo.Warehouses", "UserId", "dbo.Users");
            DropForeignKey("dbo.SupplieApplies", "UserId", "dbo.Users");
            DropForeignKey("dbo.SuppliesRecords", "UserId", "dbo.Users");
            DropForeignKey("dbo.SuppliesRecords", "SuppliesId", "dbo.Supplies");
            DropForeignKey("dbo.Supplies", "SuppliesCategoryId", "dbo.SuppliesCategories");
            DropForeignKey("dbo.SupplieApplies", "SuppliesId", "dbo.Supplies");
            DropForeignKey("dbo.EquipmentRecords", "UserId", "dbo.Users");
            DropForeignKey("dbo.EquipmentRecords", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.Capitalflows", "UserId", "dbo.Users");
            DropForeignKey("dbo.Capitalflows", "CapitalId", "dbo.Capitals");
            DropForeignKey("dbo.BookBorrowRecords", "UserId", "dbo.Users");
            DropForeignKey("dbo.BookBorrowRecords", "BookId", "dbo.Books");
            DropIndex("dbo.Warehouses", new[] { "UserId" });
            DropIndex("dbo.SuppliesRecords", new[] { "UserId" });
            DropIndex("dbo.SuppliesRecords", new[] { "SuppliesId" });
            DropIndex("dbo.Supplies", new[] { "SuppliesCategoryId" });
            DropIndex("dbo.SupplieApplies", new[] { "UserId" });
            DropIndex("dbo.SupplieApplies", new[] { "SuppliesId" });
            DropIndex("dbo.EquipmentRecords", new[] { "UserId" });
            DropIndex("dbo.EquipmentRecords", new[] { "EquipmentId" });
            DropIndex("dbo.Capitalflows", new[] { "UserId" });
            DropIndex("dbo.Capitalflows", new[] { "CapitalId" });
            DropIndex("dbo.BookBorrowRecords", new[] { "UserId" });
            DropIndex("dbo.BookBorrowRecords", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "BookCategoryId" });
            DropTable("dbo.Warehouses");
            DropTable("dbo.SuppliesRecords");
            DropTable("dbo.SuppliesCategories");
            DropTable("dbo.Supplies");
            DropTable("dbo.SupplieApplies");
            DropTable("dbo.Equipments");
            DropTable("dbo.EquipmentRecords");
            DropTable("dbo.Capitals");
            DropTable("dbo.Capitalflows");
            DropTable("dbo.BookBorrowRecords");
            DropTable("dbo.Books");
            DropTable("dbo.BookCategories");
        }
    }
}

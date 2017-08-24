namespace IosClubManage.MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateDB2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookBorrowRecords", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.BookBorrowRecords", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.BookBorrowRecords", "CreatedOn", c => c.DateTime());
            AddColumn("dbo.BookBorrowRecords", "CreatedBy", c => c.String());
            AddColumn("dbo.BookBorrowRecords", "UpdateOdn", c => c.DateTime());
            AddColumn("dbo.BookBorrowRecords", "UpdatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookBorrowRecords", "UpdatedBy");
            DropColumn("dbo.BookBorrowRecords", "UpdateOdn");
            DropColumn("dbo.BookBorrowRecords", "CreatedBy");
            DropColumn("dbo.BookBorrowRecords", "CreatedOn");
            DropColumn("dbo.BookBorrowRecords", "IsDelete");
            DropColumn("dbo.BookBorrowRecords", "IsActive");
        }
    }
}

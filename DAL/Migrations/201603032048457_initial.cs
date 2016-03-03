namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consumptions",
                c => new
                    {
                        ConsumptionId = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        Name = c.String(unicode: false),
                        Available = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ConsumptionId);
            
            CreateTable(
                "dbo.FoodOrders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, precision: 0),
                        TotalAmount = c.Double(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.KitchenOrders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, precision: 0),
                        TotalAmount = c.Double(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        OrderLineId = c.Int(nullable: false, identity: true),
                        NumberOfItems = c.Int(nullable: false),
                        PriceAmount = c.Double(nullable: false),
                        OrderId = c.Int(nullable: false),
                        ConsumptionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderLineId)
                .ForeignKey("dbo.Consumptions", t => t.ConsumptionId, cascadeDelete: true)
                .ForeignKey("dbo.KitchenOrders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ConsumptionId);
            
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserName = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Avatar = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        FirstName = c.String(unicode: false),
                        PostalCode = c.String(unicode: false),
                        DateOfBirth = c.DateTime(precision: 0),
                        Origin = c.String(unicode: false),
                        Steam = c.String(unicode: false),
                        Nickname = c.String(unicode: false),
                        BatlleNet = c.String(unicode: false),
                        Wargaming = c.String(unicode: false),
                        NewsletterSubscription = c.Boolean(nullable: false),
                        TeamId = c.Int(nullable: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(unicode: false),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                        ApplicationUser_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        LoginProvider = c.String(unicode: false),
                        ProviderKey = c.String(unicode: false),
                        ApplicationUser_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ApplicationUser_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                        IdentityRole_Id = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        WalletId = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        ApplicationUserid = c.String(maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.WalletId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUserid)
                .Index(t => t.ApplicationUserid);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        Type = c.Int(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        OrderID = c.Int(nullable: false),
                        WalletId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.KitchenOrders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Wallets", t => t.WalletId, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.WalletId);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Payments", "WalletId", "dbo.Wallets");
            DropForeignKey("dbo.Payments", "OrderID", "dbo.KitchenOrders");
            DropForeignKey("dbo.Wallets", "ApplicationUserid", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.KitchenOrders", "ApplicationUserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.FoodOrders", "ApplicationUserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.OrderLines", "OrderId", "dbo.KitchenOrders");
            DropForeignKey("dbo.OrderLines", "ConsumptionId", "dbo.Consumptions");
            DropIndex("dbo.Payments", new[] { "WalletId" });
            DropIndex("dbo.Payments", new[] { "OrderID" });
            DropIndex("dbo.Wallets", new[] { "ApplicationUserid" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.OrderLines", new[] { "ConsumptionId" });
            DropIndex("dbo.OrderLines", new[] { "OrderId" });
            DropIndex("dbo.KitchenOrders", new[] { "ApplicationUserId" });
            DropIndex("dbo.FoodOrders", new[] { "ApplicationUserId" });
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Payments");
            DropTable("dbo.Wallets");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
            DropTable("dbo.OrderLines");
            DropTable("dbo.KitchenOrders");
            DropTable("dbo.FoodOrders");
            DropTable("dbo.Consumptions");
        }
    }
}

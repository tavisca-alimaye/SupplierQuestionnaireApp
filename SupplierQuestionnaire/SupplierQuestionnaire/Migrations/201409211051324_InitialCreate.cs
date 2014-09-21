namespace SupplierQuestionnaire.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(),
                        Keyword = c.String(),
                        ProductId = c.Int(nullable: false),
                        VerbId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Verbs", t => t.VerbId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.VerbId);
            
            CreateTable(
                "dbo.Verbs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuestionAnswerMappings",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.SupplierId })
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.SupplierId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.QuestionAnswerMappings", new[] { "SupplierId" });
            DropIndex("dbo.QuestionAnswerMappings", new[] { "QuestionId" });
            DropIndex("dbo.Questions", new[] { "VerbId" });
            DropIndex("dbo.Questions", new[] { "ProductId" });
            DropForeignKey("dbo.QuestionAnswerMappings", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.QuestionAnswerMappings", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Questions", "VerbId", "dbo.Verbs");
            DropForeignKey("dbo.Questions", "ProductId", "dbo.Products");
            DropTable("dbo.QuestionAnswerMappings");
            DropTable("dbo.Verbs");
            DropTable("dbo.Questions");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Products");
        }
    }
}

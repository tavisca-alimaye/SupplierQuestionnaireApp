namespace SupplierQuestionnaire.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionAnswerMappings", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionAnswerMappings", "SupplierId", "dbo.Suppliers");
            DropIndex("dbo.QuestionAnswerMappings", new[] { "QuestionId" });
            DropIndex("dbo.QuestionAnswerMappings", new[] { "SupplierId" });
            CreateTable(
                "dbo.QuestionAnswerMappers",
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
            
            DropTable("dbo.QuestionAnswerMappings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QuestionAnswerMappings",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.SupplierId });
            
            DropIndex("dbo.QuestionAnswerMappers", new[] { "SupplierId" });
            DropIndex("dbo.QuestionAnswerMappers", new[] { "QuestionId" });
            DropForeignKey("dbo.QuestionAnswerMappers", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.QuestionAnswerMappers", "QuestionId", "dbo.Questions");
            DropTable("dbo.QuestionAnswerMappers");
            CreateIndex("dbo.QuestionAnswerMappings", "SupplierId");
            CreateIndex("dbo.QuestionAnswerMappings", "QuestionId");
            AddForeignKey("dbo.QuestionAnswerMappings", "SupplierId", "dbo.Suppliers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.QuestionAnswerMappings", "QuestionId", "dbo.Questions", "Id", cascadeDelete: true);
        }
    }
}

namespace PersistenceComparision.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OneModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        One = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ManyModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Many = c.String(unicode: false),
                        OneModelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OneModel", t => t.OneModelId, cascadeDelete: true)
                .Index(t => t.OneModelId);
            
            CreateTable(
                "dbo.TinyModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ManyModel", "OneModelId", "dbo.OneModel");
            DropIndex("dbo.ManyModel", new[] { "OneModelId" });
            DropTable("dbo.TinyModel");
            DropTable("dbo.ManyModel");
            DropTable("dbo.OneModel");
        }
    }
}

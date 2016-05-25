namespace PersistenceComparision.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TinyModelTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TinyModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TinyModels");
        }
    }
}

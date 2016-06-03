namespace PersistenceComparision.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LargeModelTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LargeModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Large = c.String(unicode: false),
                        LargeDescription2 = c.String(unicode: false),
                        LargeDescription3 = c.String(unicode: false),
                        LargeDescription4 = c.String(unicode: false),
                        LargeDescription5 = c.String(unicode: false),
                        LargeDescription6 = c.String(unicode: false),
                        LargeDescription7 = c.String(unicode: false),
                        LargeDescription8 = c.String(unicode: false),
                        LargeDescription9 = c.String(unicode: false),
                        LargeDescription10 = c.String(unicode: false),
                        LargeDescription11 = c.String(unicode: false),
                        LargeDescription12 = c.String(unicode: false),
                        LargeDescription13 = c.String(unicode: false),
                        LargeDescription14 = c.String(unicode: false),
                        LargeDescription15 = c.String(unicode: false),
                        LargeDescription16 = c.String(unicode: false),
                        LargeDescription17 = c.String(unicode: false),
                        LargeDescription18 = c.String(unicode: false),
                        LargeDescription19 = c.String(unicode: false),
                        LargeDescription20 = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LargeModel");
        }
    }
}

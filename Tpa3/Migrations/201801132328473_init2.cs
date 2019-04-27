namespace Tpa3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NamespaceMetas", "ParentID", c => c.Int(nullable: false));
            AddColumn("dbo.NamespaceMetas", "ParentType", c => c.String());
            AddColumn("dbo.ParameterMetadatas", "ParentID", c => c.Int(nullable: false));
            AddColumn("dbo.ParameterMetadatas", "ParentType", c => c.String());
            AddColumn("dbo.PropertyMetadatas", "ParentID", c => c.Int(nullable: false));
            AddColumn("dbo.PropertyMetadatas", "ParentType", c => c.String());
            AddColumn("dbo.TypeMetadatas", "ParentID", c => c.Int(nullable: false));
            AddColumn("dbo.TypeMetadatas", "ParentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TypeMetadatas", "ParentType");
            DropColumn("dbo.TypeMetadatas", "ParentID");
            DropColumn("dbo.PropertyMetadatas", "ParentType");
            DropColumn("dbo.PropertyMetadatas", "ParentID");
            DropColumn("dbo.ParameterMetadatas", "ParentType");
            DropColumn("dbo.ParameterMetadatas", "ParentID");
            DropColumn("dbo.NamespaceMetas", "ParentType");
            DropColumn("dbo.NamespaceMetas", "ParentID");
        }
    }
}

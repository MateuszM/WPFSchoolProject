namespace Tpa3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TypeMetadatas", "DeclaringType_TypeName", "dbo.TypeMetadatas");
            DropIndex("dbo.TypeMetadatas", new[] { "DeclaringType_TypeName" });
            RenameColumn(table: "dbo.TypeMetadatas", name: "DeclaringType_TypeName", newName: "DeclaringType_ID");
            DropPrimaryKey("dbo.AssemblyMetadatas");
            DropPrimaryKey("dbo.NamespaceMetas");
            DropPrimaryKey("dbo.ParameterMetadatas");
            DropPrimaryKey("dbo.PropertyMetadatas");
            DropPrimaryKey("dbo.TypeMetadatas");
            AddColumn("dbo.AssemblyMetadatas", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.NamespaceMetas", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.ParameterMetadatas", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.PropertyMetadatas", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.TypeMetadatas", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.AssemblyMetadatas", "MName", c => c.String());
            AlterColumn("dbo.NamespaceMetas", "MNamespaceName", c => c.String());
            AlterColumn("dbo.ParameterMetadatas", "Name", c => c.String());
            AlterColumn("dbo.PropertyMetadatas", "Name", c => c.String());
            AlterColumn("dbo.TypeMetadatas", "TypeName", c => c.String());
            AlterColumn("dbo.TypeMetadatas", "DeclaringType_ID", c => c.Int());
            AddPrimaryKey("dbo.AssemblyMetadatas", "ID");
            AddPrimaryKey("dbo.NamespaceMetas", "ID");
            AddPrimaryKey("dbo.ParameterMetadatas", "ID");
            AddPrimaryKey("dbo.PropertyMetadatas", "ID");
            AddPrimaryKey("dbo.TypeMetadatas", "ID");
            CreateIndex("dbo.TypeMetadatas", "DeclaringType_ID");
            AddForeignKey("dbo.TypeMetadatas", "DeclaringType_ID", "dbo.TypeMetadatas", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeMetadatas", "DeclaringType_ID", "dbo.TypeMetadatas");
            DropIndex("dbo.TypeMetadatas", new[] { "DeclaringType_ID" });
            DropPrimaryKey("dbo.TypeMetadatas");
            DropPrimaryKey("dbo.PropertyMetadatas");
            DropPrimaryKey("dbo.ParameterMetadatas");
            DropPrimaryKey("dbo.NamespaceMetas");
            DropPrimaryKey("dbo.AssemblyMetadatas");
            AlterColumn("dbo.TypeMetadatas", "DeclaringType_ID", c => c.String(maxLength: 128));
            AlterColumn("dbo.TypeMetadatas", "TypeName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.PropertyMetadatas", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ParameterMetadatas", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.NamespaceMetas", "MNamespaceName", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AssemblyMetadatas", "MName", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.TypeMetadatas", "ID");
            DropColumn("dbo.PropertyMetadatas", "ID");
            DropColumn("dbo.ParameterMetadatas", "ID");
            DropColumn("dbo.NamespaceMetas", "ID");
            DropColumn("dbo.AssemblyMetadatas", "ID");
            AddPrimaryKey("dbo.TypeMetadatas", "TypeName");
            AddPrimaryKey("dbo.PropertyMetadatas", "Name");
            AddPrimaryKey("dbo.ParameterMetadatas", "Name");
            AddPrimaryKey("dbo.NamespaceMetas", "MNamespaceName");
            AddPrimaryKey("dbo.AssemblyMetadatas", "MName");
            RenameColumn(table: "dbo.TypeMetadatas", name: "DeclaringType_ID", newName: "DeclaringType_TypeName");
            CreateIndex("dbo.TypeMetadatas", "DeclaringType_TypeName");
            AddForeignKey("dbo.TypeMetadatas", "DeclaringType_TypeName", "dbo.TypeMetadatas", "TypeName");
        }
    }
}

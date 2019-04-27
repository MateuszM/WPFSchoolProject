namespace Tpa3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssemblyMetadatas",
                c => new
                    {
                        MName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MName);
            
            CreateTable(
                "dbo.NamespaceMetas",
                c => new
                    {
                        MNamespaceName = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MNamespaceName);
            
            CreateTable(
                "dbo.ParameterMetadatas",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.PropertyMetadatas",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Name);
            
            CreateTable(
                "dbo.TypeMetadatas",
                c => new
                    {
                        TypeName = c.String(nullable: false, maxLength: 128),
                        NamespaceName = c.String(),
                        TypeKinds = c.Int(nullable: false),
                        DeclaringType_TypeName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TypeName)
                .ForeignKey("dbo.TypeMetadatas", t => t.DeclaringType_TypeName)
                .Index(t => t.DeclaringType_TypeName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeMetadatas", "DeclaringType_TypeName", "dbo.TypeMetadatas");
            DropIndex("dbo.TypeMetadatas", new[] { "DeclaringType_TypeName" });
            DropTable("dbo.TypeMetadatas");
            DropTable("dbo.PropertyMetadatas");
            DropTable("dbo.ParameterMetadatas");
            DropTable("dbo.NamespaceMetas");
            DropTable("dbo.AssemblyMetadatas");
        }
    }
}

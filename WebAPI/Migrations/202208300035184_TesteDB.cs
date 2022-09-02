namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TesteDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CPF = c.String(nullable: false),
                        RG = c.String(),
                        DataExpedicao = c.DateTime(),
                        OrgaoExpedicao = c.String(),
                        UF = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                        Sexo = c.String(nullable: false),
                        EstadoCivil = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Enderecoes",
                c => new
                    {
                        EnderecoId = c.Int(nullable: false),
                        Cep = c.String(nullable: false),
                        Logradouro = c.String(nullable: false),
                        Numero = c.Int(nullable: false),
                        Complemento = c.String(),
                        Bairro = c.String(nullable: false),
                        Cidade = c.String(nullable: false),
                        UF = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EnderecoId)
                .ForeignKey("dbo.Clientes", t => t.EnderecoId)
                .Index(t => t.EnderecoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enderecoes", "EnderecoId", "dbo.Clientes");
            DropIndex("dbo.Enderecoes", new[] { "EnderecoId" });
            DropTable("dbo.Enderecoes");
            DropTable("dbo.Clientes");
        }
    }
}

using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;

namespace WebAPI.Models
{
    public class MyContext : DbContext
    {

        public MyContext()
            : base("name=MyContext")
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasOptional(e => e.Endereco)
                .WithRequired(c => c.Cliente);
        }

    }


}
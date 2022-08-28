using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseContexts
{
    public partial class NTTContext:DbContext
    {
        public NTTContext()
        {

        }
        public NTTContext([NotNullAttribute] DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Persona> Persona { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Movimientos> Movimientos { get; set; }
        public DbSet<Cuenta> Cuenta { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Cliente>().ToTable("Cliente");

            builder.Entity<Cuenta>()
                .HasIndex(u => u.NumeroCuenta)
                .IsUnique();

            builder.Entity<Cliente>().Property(c => c.Estado).HasDefaultValue(true);
            builder.Entity<Persona>().HasIndex(c => c.Identificacion).IsUnique();


            base.OnModelCreating(builder);
        }
    }
}

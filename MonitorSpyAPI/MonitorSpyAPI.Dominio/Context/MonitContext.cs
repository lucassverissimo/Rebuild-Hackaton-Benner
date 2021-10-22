using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorSpyAPI.Dominio.Context {
    public class MonitContext : DbContext {
        public MonitContext() : base() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Monitoramento>()
                .HasNoKey();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                
            }
        }

        public DbSet<Monitoramento> Monitoramentos { get; set; }
    }
}

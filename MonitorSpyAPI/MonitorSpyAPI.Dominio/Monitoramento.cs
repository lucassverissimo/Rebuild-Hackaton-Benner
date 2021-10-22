using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MonitorSpyAPI.Dominio {
    public class Monitoramento {
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<BlogPostsCount>()
                .HasNoKey();
        }

        public string Token { get; set; }
        public string Perfil { get; set; }
        public string Usuario { get; set; }
        public DateTime DataHora { get; set; }
        public string Aplicacao { get; set; }
        public string Acao { get; set; }
        public string Modulo { get; set; }
        public string Funcionalidade { get; set; }
        public string Cliente { get; set; }
    }
}

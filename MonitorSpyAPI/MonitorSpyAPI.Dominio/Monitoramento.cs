using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MonitorSpyAPI.Dominio {
    public class Monitoramento {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
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

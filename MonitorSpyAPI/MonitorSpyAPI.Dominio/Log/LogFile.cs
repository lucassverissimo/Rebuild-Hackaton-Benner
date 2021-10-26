using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorSpyAPI.Dominio.Log {
    public class LogFile {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Mensagem { get; set; }
        public string Classe { get; set; }
        public string Projeto { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorSpyAPI.Dominio {
    public class MonitoramentoClick : Monitoramento {
        public string Campo { get; set; }
        public string TipoCampo { get; set; }
    }
}

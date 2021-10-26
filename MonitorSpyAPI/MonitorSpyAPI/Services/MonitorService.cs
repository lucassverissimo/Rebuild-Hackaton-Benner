using MongoDB.Driver;
using MonitorSpyAPI.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorSpyAPI.Services {
    public class MonitorService {
        private readonly IMongoCollection<Monitoramento> _monitoramentos;
        public MonitorService(IMonitorStoreDatabaseSettings settings) {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _monitoramentos = database.GetCollection<Monitoramento>("Monitoramento");
        }

        public List<Monitoramento> Get() => _monitoramentos.Find(monit => true).ToList();

        public Monitoramento Get(string id) => _monitoramentos.Find(monit => monit.Id == id).FirstOrDefault();

        public Monitoramento Insert(Monitoramento monitoramento) {
            _monitoramentos.InsertOne(monitoramento);
            return monitoramento;
        }
        public void Update(string id, Monitoramento monitoramentoIn) =>
            _monitoramentos.ReplaceOne(monit => monit.Id == id, monitoramentoIn);

        public void Remove(Monitoramento monitoramentoIn) =>
            _monitoramentos.DeleteOne(monit => monit.Id == monitoramentoIn.Id);

        public void Remove(string id) =>
            _monitoramentos.DeleteOne(monit => monit.Id == id);


    }
}

using MongoDB.Driver;
using MonitorSpyAPI.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorSpyAPI.Services {
    public class MonitorClickService {
        private readonly IMongoCollection<MonitoramentoClick> _monitoramentos;
        public MonitorClickService(IMonitorStoreDatabaseSettings settings) {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _monitoramentos = database.GetCollection<MonitoramentoClick>("MonitoramentoClick");
        }

        public List<MonitoramentoClick> Get() => _monitoramentos.Find(monit => true).ToList();

        public MonitoramentoClick Get(string id) => _monitoramentos.Find(monit => monit.Id == id).FirstOrDefault();

        public MonitoramentoClick Insert(MonitoramentoClick monitoramentoClick) {
            _monitoramentos.InsertOne(monitoramentoClick);
            return monitoramentoClick;
        }
        public void Update(string id, MonitoramentoClick monitoramentoClickIn) =>
            _monitoramentos.ReplaceOne(monit => monit.Id == id, monitoramentoClickIn);

        public void Remove(MonitoramentoClick monitoramentoClickIn) =>
            _monitoramentos.DeleteOne(monit => monit.Id == monitoramentoClickIn.Id);

        public void Remove(string id) =>
            _monitoramentos.DeleteOne(monit => monit.Id == id);
    }
}

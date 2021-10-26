using MongoDB.Driver;
using MonitorSpyAPI.Dominio;
using MonitorSpyAPI.Dominio.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitorSpyAPI.Services {
    public class LogErroService {
        private readonly IMongoCollection<LogFile> _LogFiles;
        public LogErroService(IMonitorStoreDatabaseSettings settings) {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _LogFiles = database.GetCollection<LogFile>("LogFile");
        }

        public List<LogFile> Get() => _LogFiles.Find(log => true).ToList();

        public LogFile Get(string id) => _LogFiles.Find(log => log.Id == id).FirstOrDefault();

        public LogFile Insert(LogFile LogFile) {
            _LogFiles.InsertOne(LogFile);
            return LogFile;
        }
        public void Update(string id, LogFile LogFileIn) =>
            _LogFiles.ReplaceOne(log => log.Id == id, LogFileIn);

        public void Remove(LogFile LogFileIn) =>
            _LogFiles.DeleteOne(log => log.Id == LogFileIn.Id);

        public void Remove(string id) =>
            _LogFiles.DeleteOne(log => log.Id == id);
    }
}

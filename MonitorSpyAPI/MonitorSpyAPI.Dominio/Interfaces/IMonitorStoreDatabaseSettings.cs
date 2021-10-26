using System;
using System.Collections.Generic;
using System.Text;

namespace MonitorSpyAPI.Dominio {
    public interface IMonitorStoreDatabaseSettings {
        public string MonitorCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

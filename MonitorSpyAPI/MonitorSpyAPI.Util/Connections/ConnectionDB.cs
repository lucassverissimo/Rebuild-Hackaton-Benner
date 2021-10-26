using MonitorSpyAPI.Util.Helpers;

namespace MonitorSpyAPI.Util {
    public static class ConnectionDB {
        
        public static string GetConnectionString(string connectionString, string user, string password) {
            return string.Format(connectionString, user.Decrypt(), password.Decrypt());
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonitorSpyAPI.Util.Helpers;

namespace MonitorSpyAPITest {
    [TestClass]
    public class UnitTest {
        [TestMethod]
        public void GeneralTest() {
            var usuario = "admin";

            var enc = usuario.Encrypt();
        }
    }
}

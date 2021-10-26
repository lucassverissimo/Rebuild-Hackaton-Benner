using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace MonitorSpyAPI.Util {
    public static class ExtensionHelper {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentFullMethod() {
            return GetProjectName() + "." +
                   GetCurrentClass() + "." +
                   GetCurrentMethod();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod() {
            var st = new StackTrace();
            var methodName = string.Empty;
            try {
                var sf = st.GetFrame(1);
                methodName = sf.GetMethod().Name;
            } catch (Exception) {
                var sf = st.GetFrame(0);
                methodName = sf.GetMethod().Name;
            }
            return methodName;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentClass() {
            var st = new StackTrace();
            var className = string.Empty;
            try {
                var sf = st.GetFrame(1);
                className = sf.GetMethod().DeclaringType.Name;
            } catch (Exception) {
                var sf = st.GetFrame(0);
                className = sf.GetMethod().DeclaringType.Name;
            }
            return className;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetProjectName() {
            return Assembly.GetCallingAssembly().GetName().Name;
        }
    }
}

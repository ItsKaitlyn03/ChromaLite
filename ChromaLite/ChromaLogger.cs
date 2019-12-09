using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromaLite {

    public static class ChromaLogger {

        public static void Log(Exception e) {
            Log(e.ToString());
        }

        public static void Log(Object obj) {
            Log(obj.ToString());
        }

        public static void Log(string message) {
            Console.WriteLine("[ChromaLite] " + message);
        }

    }

}

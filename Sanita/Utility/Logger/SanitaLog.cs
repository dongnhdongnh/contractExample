using Newtonsoft.Json;
using System;
using System.IO;

namespace Sanita.Utility.Logger
{
    public class SanitaLog
    {
        public const string FILEPATH = "log.txt";
        private static object lockObj = new Object();

        public static void Log(string text, object logMessage)
        {
            logMessage = (logMessage ?? String.Empty).ToString();
            lock (lockObj)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    w.WriteLine("{0} {1} - {2}", DateTime.Now.ToString("hh:mm:ss tt"), DateTime.Now.ToString("yyyy/MM/dd"), text + ": " + logMessage.ToString());
                    w.WriteLine("{0} {1} - {2}", DateTime.Now.ToString("hh:mm:ss tt"), DateTime.Now.ToString("yyyy/MM/dd"), "------------------------------------------------------------------------------");
                }
            }
        }

        public static void Error(params object[] values)
        {
            lock (lockObj)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    w.WriteLine("{0} {1} - {2}", DateTime.Now.ToString("hh:mm:ss tt"), DateTime.Now.ToString("yyyy/MM/dd"), "ERROR: " + string.Join(",", values));
                    w.WriteLine("{0} {1} - {2}", DateTime.Now.ToString("hh:mm:ss tt"), DateTime.Now.ToString("yyyy/MM/dd"), "------------------------------------------------------------------------------");
                }
            }
        }

        public static void Success(string logMessage)
        {
            lock (lockObj)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    w.WriteLine("{0} {1} - {2}", DateTime.Now.ToString("hh:mm:ss tt"), DateTime.Now.ToString("yyyy/MM/dd"), "SUCCESS: " + logMessage);
                    w.WriteLine("{0} {1} - {2}", DateTime.Now.ToString("hh:mm:ss tt"), DateTime.Now.ToString("yyyy/MM/dd"), "------------------------------------------------------------------------------");
                }
            }
        }

        public static void Exception(Exception e)
        {
            lock (lockObj)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    w.WriteLine("{0} {1} - {2}", DateTime.Now.ToString("hh:mm:ss tt"), DateTime.Now.ToString("yyyy/MM/dd"), "EXCEPTION: " + e.ToString());
                    w.WriteLine("{0} {1} - {2}", DateTime.Now.ToString("hh:mm:ss tt"), DateTime.Now.ToString("yyyy/MM/dd"), "------------------------------------------------------------------------------");
                }
            }
        }

        public static void Method(string methodName, string className)
        {
            lock (lockObj)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    w.WriteLine("{0} {1} - {2}", DateTime.Now.ToString("hh:mm:ss tt"), DateTime.Now.ToString("yyyy/MM/dd"), "Method " + methodName + " in class " + className);
                    w.WriteLine("{0} {1} - {2}", DateTime.Now.ToString("hh:mm:ss tt"), DateTime.Now.ToString("yyyy/MM/dd"), "------------------------------------------------------------------------------");
                }
            }
        }

        public static void LogObject(string text, Object objMessage)
        {
            lock (lockObj)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    w.WriteLine("{0} {1} - {2}", DateTime.Now.ToString("hh:mm:ss tt"), DateTime.Now.ToString("yyyy/MM/dd"), text + ": " + JsonConvert.SerializeObject(objMessage));
                    w.WriteLine("{0} {1} - {2}", DateTime.Now.ToString("hh:mm:ss tt"), DateTime.Now.ToString("yyyy/MM/dd"), "------------------------------------------------------------------------------");
                }
            }
        }

        public static void DumpLog()
        {
            using (StreamReader r = File.OpenText("log.txt"))
            {
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}

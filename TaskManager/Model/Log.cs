using System.IO;
using System;
using System.Threading;

namespace TaskManager.Model
{
    public class Log
    {
        private static Log instance;
        private static object syncLock = new object();
        private Log()
        {

        }
        public static Log GetLog()
        {
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                        instance = new Log();
                }
            }
            return instance;
        }
        public void logThis(string msg)
        {            
            File.AppendAllText("Log.txt", $"{DateTime.Now.ToString()}:{msg}{Environment.NewLine}");
        }
    }
}

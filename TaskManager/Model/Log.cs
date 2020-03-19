using System.IO;
using System;
using System.Threading;

namespace TaskManager.Model
{
    public interface ILog
    {
        void logThis(string msg);
    }
    public class Log : ILog
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
            File.AppendAllText("Log.txt", msg + Environment.NewLine );
        }
    }
}

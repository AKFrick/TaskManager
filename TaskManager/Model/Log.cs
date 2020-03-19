using System.IO;
using System;

namespace TaskManager.Model
{
    public interface ILog
    {
        void logThis(string msg);
    }
    public class Log : ILog
    {
        public void logThis(string msg)
        {            
            File.AppendAllText("Log.txt", msg + Environment.NewLine );
        }
    }
}

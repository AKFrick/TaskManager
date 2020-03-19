using TaskManager.TaskProxy;

namespace TaskManager.Model
{
    public class PlcProxy
    {
        private static PlcProxy instance;
        private static object syncLock = new object();
        private PlcProxy() { }
        public static PlcProxy Instance()
        {
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                        instance = new PlcProxy();
                }
            }
            return instance;
        }
        public void SendTaskToPlc(Task task)
        {            
            Log.GetLog().logThis($"Испытание отправлено в ПЛК: {task.PrintToString()}");            
        }
    }
}

using TaskManager.TaskProxy;

namespace TaskManager.Model
{
    public interface IPlcProxy
    {
        void SendTaskToPlc(Task task);
    }
    public class PlcProxy : IPlcProxy
    {
        public void SendTaskToPlc(Task task)
        {
            ILog log = Log.GetLog();
            log.logThis(task.PrintToString());            
        }
    }
}

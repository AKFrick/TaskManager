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
            Log.GetLog().logThis($"Испытание отправлено в ПЛК: {task.PrintToString()}");            
        }
    }
}

using TaskManager.TaskProxy;

namespace TaskManager.Model
{
    public static class TaskHelper
    {
        public static string PrintToString(this Task task)
        {
            return $"ID:{task.ID}, Number:{task.Number}, Item:{task.Item}, TargetCount:{task.TargetCount}";
        }
    }
}

using TaskManager.TaskProxy;

namespace TaskManager.Model
{
    public static class TaskHelper
    {
        public static string PrintToString(this Task task)
        {
            return $"ID:{task.ID}, Number:{task.Number.Trim()}, Item:{task.Item.Trim()}, TargetCount:{task.TargetCount}";
        }
    }
}

﻿using TaskManager.TaskProxy;

namespace TaskManager.Model
{
    public interface IPlcProxy
    {
        bool SendTaskToPlc(Task task);
    }
    public class PlcProxy : IPlcProxy
    {
        public bool SendTaskToPlc(Task task)
        {
            ILog log = Log.GetLog();
            log.logThis(task.PrintToString());
            return true;
        }
    }
}
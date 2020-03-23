using TaskManager.TaskProxy;
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;
using System;
using System.Threading.Tasks;

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
        public void SendTaskToPlc(TaskProxy.Task task)
        {
            OpcUa opc = new OpcUa();
            // opc.SendTask(task).Wait();
            try
            {
                opc.UaClient().Wait();
            }
            catch (Exception ex)
            {
                Log.GetLog().logThis(ex.Message);
            }

            Log.GetLog().logThis($"Испытание отправлено в ПЛК: {task.PrintToString()}");            
        }
       
    }
}

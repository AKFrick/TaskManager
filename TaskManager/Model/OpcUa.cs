using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Opc.UaFx.Client;
using TaskManager.TaskProxy;


namespace TaskManager.Model
{
    public class OpcUa
    {
        OpcClient client;

        public void SendTask(TaskProxy.Task task)
        {
            using (client = new OpcClient("opc.tcp://192.168.10.2:4840"))
            {
                client.Connect();

                object[] result = client.CallMethod(
                                        "ns=3;s=\"SendNewTask\"",
                                        "ns=3;s=\"SendNewTask\".Method",
                                        (Int16)task.ID,
                                        (String)task.Number.Trim(),
                                        (String)task.Item.Trim(),
                                        (String)task.StartSerial.Trim(),
                                        (Int16)task.TargetCount);
                Log.GetLog().logThis(task.PrintToString());
            }
        }
        
        
    }
}

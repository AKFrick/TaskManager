using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.ServiceModel;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using System.Linq;

namespace TaskWatcherConsole
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
    public class TaskTicker : ITaskTicker, IDisposable
    {
        private readonly List<ITaskCallback> callbackList = new List<ITaskCallback>();
        private readonly string connectionString;
        private readonly SqlTableDependency<Task> sqlTableDependency;
        public TaskTicker()
        {
            connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            sqlTableDependency = new SqlTableDependency<Task>(connectionString, "Tasks");

            sqlTableDependency.OnChanged += TableDependency_Changed;
            sqlTableDependency.OnError += (sender, args) => Console.WriteLine($"Error: {args.Message}");
            sqlTableDependency.Start();
        }
        private void TableDependency_Changed(object sender, RecordChangedEventArgs<Task> e)
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine($"DML: {e.ChangeType}");
            Console.WriteLine($"ID: {e.Entity.ID}");
            Console.WriteLine($"Item: {e.Entity.Item}");              
        }        

        public void Subscribe()
        {
            var registeredUser = OperationContext.Current.GetCallbackChannel<ITaskCallback>();
            if (!callbackList.Contains(registeredUser))
            {
                callbackList.Add(registeredUser);
            }
        }

        public void Unsubscribe()
        {
            var registeredUser = OperationContext.Current.GetCallbackChannel<ITaskCallback>();
            if (callbackList.Contains(registeredUser))
            {
                callbackList.Remove(registeredUser);
            }
        }

        public void PublishTaskChanged(RecordChangedEventArgs<Task> task)
        {            
            foreach(ITaskCallback client in callbackList)
            {
                if (task.ChangeType == TableDependency.SqlClient.Base.Enums.ChangeType.Insert)
                    client.TaskInserted((Task)task.Entity);
                else if (task.ChangeType == TableDependency.SqlClient.Base.Enums.ChangeType.Update)
                    client.TaskChanged((Task)task.Entity);
                else if (task.ChangeType == TableDependency.SqlClient.Base.Enums.ChangeType.Delete)
                    client.TaskDeleted((Task)task.Entity);
            }            
        }
        public IList<Task> GetTasks()
        {
            List<Task> tasks = new List<Task>();
            using (var db = new IMS189Entities())
            {
                var query = from b in db.Tasks select b;                
                foreach (Task task in query)
                {
                    tasks.Add(task);
                }                                
            }
            return tasks;
        }

        public void Dispose()
        {
            sqlTableDependency.Stop();
        }

    }
}

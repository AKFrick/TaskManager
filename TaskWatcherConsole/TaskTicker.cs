using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.ServiceModel;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

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
           // Console.WriteLine(sqlTableDependency.Status);
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



        public void Dispose()
        {
            sqlTableDependency.Stop();
        }

    }
}

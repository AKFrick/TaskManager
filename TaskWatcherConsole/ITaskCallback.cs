using System.ServiceModel;
using System.Collections.Generic;
using TableDependency.SqlClient.Base.EventArgs;

namespace TaskWatcherConsole
{
    public interface ITaskCallback
    {
        [OperationContract]
        void TaskInserted(Task task);
        [OperationContract]
        void TaskDeleted(Task task);
        [OperationContract]
        void TaskChanged(Task task);

    }
}

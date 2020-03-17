using System.ServiceModel;

namespace TaskWatcherConsole
{
    public interface ITaskCallback
    {
        [OperationContract]
        void TaskInserted(int ID);
        [OperationContract]
        void TaskDeleted(int ID);
    }
}

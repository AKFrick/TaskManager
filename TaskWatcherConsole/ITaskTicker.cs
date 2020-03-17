using System.Collections.Generic;
using System.ServiceModel;

namespace TaskWatcherConsole
{
    [ServiceContract(CallbackContract = typeof(ITaskCallback))]
    public interface ITaskTicker
    {
        [OperationContract]
        void Subscribe();

        [OperationContract]
        void Unsubscribe();
    }
}

using Prism.Mvvm;
using Prism.Commands;
using TaskManager.Model;
using TaskManager.TaskProxy;

namespace TaskManager.ModelView
{
    public class NewTaskVM : BindableBase
    {
        public Task task { get; }
        public DelegateCommand Create { get; }
        public NewTaskVM()
        {
            Create = new DelegateCommand(() => CurrentTasks.Instance().InsertNewTask(task));
        }
    }
}

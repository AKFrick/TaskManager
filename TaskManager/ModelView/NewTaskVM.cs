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
        CurrentTasks currentTasks;
        public NewTaskVM(CurrentTasks currentTasks)
        {
            this.currentTasks = currentTasks;
            Create = new DelegateCommand(() => currentTasks.InsertNewTask(task));
        }
    }
}

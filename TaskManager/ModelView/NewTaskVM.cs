using Prism.Mvvm;
using Prism.Commands;
using TaskManager.Model;
using TaskManager.TaskProxy;
using System;

namespace TaskManager.ModelView
{
    public class NewTaskVM : BindableBase
    {
        public Task task { get; } = new Task();
        public DelegateCommand Create { get; }
        public event Action TaskCreated;
        public NewTaskVM()
        {
            Create = new DelegateCommand(() =>
            {
                CurrentTasks.Instance().InsertNewTask(task);
                TaskCreated?.Invoke();
            });
        }


    }
}

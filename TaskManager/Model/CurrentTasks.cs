using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using Prism.Mvvm;
using TaskManager.TaskProxy;

namespace TaskManager.Model
{
    public class CurrentTasks : BindableBase, ITaskTickerCallback
    {        
        public CurrentTasks()
        {            
            proxy = new TaskTickerClient(new InstanceContext(this));
            proxy.Subscribe();
            tasks = new ObservableCollection<Task>(proxy.GetTasks());
            Tasks = new ReadOnlyObservableCollection<Task>(tasks);
        }
        private readonly TaskTickerClient proxy;
        private ObservableCollection<Task> tasks;
        public ReadOnlyObservableCollection<Task> Tasks { get; private set; }

        public void TaskInserted(Task task)
        {
            tasks.Add(task);
        }

        public void TaskDeleted(Task task)
        {
            //tasks.Remove(task);
            tasks.Remove(tasks.Where(item => item.ID == task.ID).Single());
        }

        public void TaskChanged(Task task)
        {
            throw new System.NotImplementedException();
        }
        public void InsertNewTask(Task task)
        {
            proxy.InsertTask(task);
        }
    }
}

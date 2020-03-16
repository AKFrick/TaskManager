using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;

namespace TaskManager.Model
{
    public class CurrentTasks : BindableBase
    {
        public CurrentTasks()
        {
            using (var db = new IMS189Database())
            {
                var query = from b in db.Tasks select b;
                foreach(Task task in query)
                {
                    tasks.Add(task);
                }
                Tasks = new ReadOnlyObservableCollection<Task>(tasks);
            }   
        }
        private ObservableCollection<Task> tasks = new ObservableCollection<Task>();
        public ReadOnlyObservableCollection<Task> Tasks { get; }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Mvvm;
using TaskManager.TaskProxy;

namespace TaskManager.Model
{
    public class CurrentTasks : BindableBase
    {        
        public CurrentTasks()
        {
                
        }
        private readonly TaskTickerClient taskTicker;
        private readonly IList<Task> tasks;

        //private ObservableCollection<Task> tasks = new ObservableCollection<Task>();
        public ReadOnlyObservableCollection<Task> Tasks { get; private set; }
    }
}

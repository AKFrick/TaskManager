using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Timers;

namespace TaskManager.Model
{
    public class CurrentTasks : BindableBase
    {
        public CurrentTasks()
        {
            RefreshTasks();
            timer = new Timer(5000);
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RefreshTasks();
        }

        private void RefreshTasks()
        {
            using (var db = new IMS189Database())
            {
                var query = from b in db.Tasks select b;
                foreach (Task task in query)
                {
                    tasks.Add(task);
                }
                Tasks = new ReadOnlyObservableCollection<Task>(tasks);
                CallCount++;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<Task> tasks = new ObservableCollection<Task>();
        public ReadOnlyObservableCollection<Task> Tasks { get; private set; }
        private Timer timer;
        public int CallCount { get; set; } = 0;
    }
}

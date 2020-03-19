using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using System.Linq;
using System.Collections.Specialized;
using Prism.Commands;
using System.Windows;
using System;
using TaskManager.View.NewTask;
using TaskManager.Model;
using TaskManager.TaskProxy;

namespace TaskManager.ModelView
{
    class MainVM : BindableBase
    {
        public MainVM()
        {
            currentTasks = new CurrentTasks();
            CreateNewTask = new DelegateCommand(() =>
            {
                NewTaskVM newTaskVM = new NewTaskVM(currentTasks);
                NewTaskWindow newTaskWindow = new NewTaskWindow(newTaskVM);                                
                newTaskWindow.ShowDialog();
            });
            StartTask = new DelegateCommand(() =>
            {
                IPlcProxy plcProxy = new PlcProxy();
                plcProxy.SendTaskToPlc(SelectedTask);
            });
            TaskList = new ObservableCollection<Task>(currentTasks.Tasks);
            ((INotifyCollectionChanged)currentTasks.Tasks).CollectionChanged += (s, a) =>
            {
                if (a.NewItems?.Count == 1)
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        TaskList.Add(a.NewItems[0] as Task)));
                if (a.OldItems?.Count == 1)
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        TaskList.Remove(a.OldItems[0] as Task)));
            };
        }
        public DelegateCommand AddTask { get; }
        public DelegateCommand CreateNewTask { get; }
        public DelegateCommand StartTask { get; }
        public ObservableCollection<Task> TaskList { get; private set; }
        public Task SelectedTask { get; set; }
        private CurrentTasks currentTasks;               
        
    }
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

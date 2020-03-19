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
            TaskList = new ObservableCollection<Task>(currentTasks.Tasks);
            CreateNewTask = new DelegateCommand(createNewTask);
            StartTask = new DelegateCommand(startTask);

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
        private void createNewTask()
        {
            NewTaskVM newTaskVM = new NewTaskVM(currentTasks);
            NewTaskWindow newTaskWindow = new NewTaskWindow(newTaskVM);
            newTaskWindow.ShowDialog();
        }
        public DelegateCommand StartTask { get; }
        private void startTask()
        {
            IPlcProxy plcProxy = new PlcProxy();
            plcProxy.SendTaskToPlc(SelectedTask);
        }
        public ObservableCollection<Task> TaskList { get; private set; }
        public Task SelectedTask { get; set; }
        private CurrentTasks currentTasks;               
        
    }
}

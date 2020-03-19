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
            TaskList = new ObservableCollection<Task>(CurrentTasks.Instance().Tasks);
            CreateNewTask = new DelegateCommand(createNewTask);
            StartTask = new DelegateCommand(startTask);

        ((INotifyCollectionChanged)CurrentTasks.Instance().Tasks).CollectionChanged += (s, a) =>
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
            NewTaskWindow newTaskWindow = new NewTaskWindow();
            newTaskWindow.ShowDialog();
        }
        public DelegateCommand StartTask { get; }
        private void startTask()
        {            
            if (SelectedTask != null) PlcProxy.Instance().SendTaskToPlc(SelectedTask);
        }
        public ObservableCollection<Task> TaskList { get; private set; }
        public Task SelectedTask { get; set; }                   
    }
}

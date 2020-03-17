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

namespace TaskManager.ModelView
{
    class MainVM : BindableBase
    {
        public MainVM()
        {
            CreateNewTask = new DelegateCommand(() =>
            {
                NewTaskWindow newTaskWindow = new NewTaskWindow();
                newTaskWindow.ShowDialog();
            });
            currentTasks = new CurrentTasks();
            TaskList = new ObservableCollection<Task>(currentTasks.Tasks);
            currentTasks.PropertyChanged += (s, a) => RaisePropertyChanged(nameof(CallCount));
            currentTasks.PropertyChanged += (s, a) =>
            {
                TaskList = new ObservableCollection<Task>(currentTasks.Tasks);
            };
            
        }

        public DelegateCommand AddTask { get; }
        public DelegateCommand CreateNewTask { get; }
        public ObservableCollection<Task> TaskList { get; private set; }
        private CurrentTasks currentTasks;
        public int CallCount => currentTasks.CallCount;
    }
}

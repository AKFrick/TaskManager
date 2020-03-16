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
            //TaskList = new ObservableCollection<Task>()
        }

        public DelegateCommand AddTask { get; }
        public DelegateCommand CreateNewTask { get; }
        public ObservableCollection<Task> TaskList { get; }        
    }
}

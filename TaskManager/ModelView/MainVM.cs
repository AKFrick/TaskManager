using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using System.Linq;
using System.Collections.Specialized;
using Prism.Commands;
using System.Windows;
using System;

namespace TaskManager.ModelView
{
    class MainVM : BindableBase
    {
        public MainVM()
        {
            NewID = 2;
            AddTask = new DelegateCommand(() =>
            {
                using (var db = new IMS189Database())
                {
                    var task = new Task { Number = "New 123", Item = "item 32", TargetCount = 5 };
                    db.Tasks.Add(task);
                    db.SaveChanges();
                    NewID = task.ID;
                    RaisePropertyChanged(nameof(NewID));
                }
            }
            );
        }

        public DelegateCommand AddTask { get; }
        public int NewID { get; set; }
    }
}

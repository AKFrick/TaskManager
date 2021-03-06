﻿using System;
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
        private static CurrentTasks instance;
        private static object syncLock = new object();
        private CurrentTasks()
        {            
            proxy = new TaskTickerClient(new InstanceContext(this));
            
            try
            {
                proxy.Subscribe();
                tasks = new ObservableCollection<Task>(proxy.GetTasks());
            }
            catch
            {
                tasks = new ObservableCollection<Task>();
            }            
            Tasks = new ReadOnlyObservableCollection<Task>(tasks);
        }
        public static CurrentTasks Instance()
        {
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                        instance = new CurrentTasks();
                }
            }
            return instance;
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

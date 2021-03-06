﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskManager.ModelView;

namespace TaskManager.View.NewTask
{
    /// <summary>
    /// Interaction logic for CreateNewTask.xaml
    /// </summary>
    public partial class NewTaskWindow : Window
    {
        public NewTaskWindow(NewTaskVM newTaskVM)
        {
            DataContext = newTaskVM;
            newTaskVM.TaskCreated += closeWindow;
            InitializeComponent();
        }
        private void closeWindow()
        {
            Close();
        }
    }
}

﻿using System.ComponentModel;
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
using Workstation.ServiceModel.Ua;
using Workstation.ServiceModel.Ua.Channels;

namespace TaskManager.ModelView
{
    class MainVM : BindableBase
    {
        public MainVM()
        {
            //TaskList = new ObservableCollection<Task>(CurrentTasks.Instance().Tasks);
            OpenNewTaskWindow = new DelegateCommand(openNewTaskWindow);
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
        public DelegateCommand OpenNewTaskWindow { get; }
        NewTaskWindow newTaskWindow;
        private void openNewTaskWindow()
        {
            newTaskWindow = new NewTaskWindow(new NewTaskVM());            
            newTaskWindow.ShowDialog();
        }      
        public DelegateCommand StartTask { get; }
        private void startTask()
        {
            //if (SelectedTask != null) PlcProxy.Instance().SendTaskToPlc(SelectedTask);
            try
            {
                UaClient().Wait();
            }
            catch (Exception ex)
            {
                Log.GetLog().logThis(ex.Message);
            }
        }
        public ObservableCollection<Task> TaskList { get; private set; }
        public Task SelectedTask { get; set; }
        public async System.Threading.Tasks.Task UaClient()
        {
            // describe this client application.
            var clientDescription = new ApplicationDescription
            {
                ApplicationName = "Workstation.UaClient.FeatureTests",
                ApplicationUri = $"urn:{System.Net.Dns.GetHostName()}:Workstation.UaClient.FeatureTests",
                ApplicationType = ApplicationType.Client
            };

            // create a 'UaTcpSessionChannel', a client-side channel that opens a 'session' with the server.
            var channel = new UaTcpSessionChannel(
                clientDescription,
                null, // no x509 certificates
                new AnonymousIdentity(), // no user identity
                "opc.tcp://192.168.10.2:4840", // the public endpoint of a server at opcua.rocks.
                SecurityPolicyUris.None); // no encryption
            try
            {
                // try opening a session and reading a few nodes.
                await channel.OpenAsync();
                Log.GetLog().logThis($"Opened session with endpoint '{channel.RemoteEndpoint.EndpointUrl}'.");
                Log.GetLog().logThis($"SecurityPolicy: '{channel.RemoteEndpoint.SecurityPolicyUri}'.");
                Log.GetLog().logThis($"SecurityMode: '{channel.RemoteEndpoint.SecurityMode}'.");
                Log.GetLog().logThis($"UserIdentityToken: '{channel.UserIdentity}'.");
                var readRequest = new ReadRequest
                {
                    // set the NodesToRead to an array of ReadValueIds.
                    NodesToRead = new[] {
                    // construct a ReadValueId from a NodeId and AttributeId.
                    new ReadValueId {
                        // you can parse the nodeId from a string.
                        // e.g. NodeId.Parse("ns=2;s=Demo.Static.Scalar.Double")
                        NodeId = NodeId.Parse(VariableIds.Server_ServerStatus),
                        // variable class nodes have a Value attribute.
                        AttributeId = AttributeIds.Value
                    },
                    new ReadValueId
                    {
                        NodeId = new NodeId("\"OPCUA\".\"MyInt\"", 3),
                        AttributeId = AttributeIds.Value
                    }
                }
                };
                var readResult = await channel.ReadAsync(readRequest);
                var MyInt = readResult.Results[1];
                Log.GetLog().logThis($" OPCUA.MyInt: { MyInt.GetValueOrDefault<Int16>()}");
                await channel.CloseAsync();
            }
            catch (Exception ex)
            {
                await channel.AbortAsync();
                Log.GetLog().logThis(ex.Message);
            }
        }
    }
}

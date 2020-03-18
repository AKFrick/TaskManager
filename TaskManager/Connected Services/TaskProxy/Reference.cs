﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManager.TaskProxy {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Task", Namespace="http://schemas.datacontract.org/2004/07/TaskWatcherConsole")]
    [System.SerializableAttribute()]
    public partial class Task : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ItemField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StartSerialField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TargetCountField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Item {
            get {
                return this.ItemField;
            }
            set {
                if ((object.ReferenceEquals(this.ItemField, value) != true)) {
                    this.ItemField = value;
                    this.RaisePropertyChanged("Item");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Number {
            get {
                return this.NumberField;
            }
            set {
                if ((object.ReferenceEquals(this.NumberField, value) != true)) {
                    this.NumberField = value;
                    this.RaisePropertyChanged("Number");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StartSerial {
            get {
                return this.StartSerialField;
            }
            set {
                if ((object.ReferenceEquals(this.StartSerialField, value) != true)) {
                    this.StartSerialField = value;
                    this.RaisePropertyChanged("StartSerial");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TargetCount {
            get {
                return this.TargetCountField;
            }
            set {
                if ((this.TargetCountField.Equals(value) != true)) {
                    this.TargetCountField = value;
                    this.RaisePropertyChanged("TargetCount");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TaskProxy.ITaskTicker", CallbackContract=typeof(TaskManager.TaskProxy.ITaskTickerCallback))]
    public interface ITaskTicker {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskTicker/Subscribe", ReplyAction="http://tempuri.org/ITaskTicker/SubscribeResponse")]
        void Subscribe();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskTicker/Subscribe", ReplyAction="http://tempuri.org/ITaskTicker/SubscribeResponse")]
        System.Threading.Tasks.Task SubscribeAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskTicker/Unsubscribe", ReplyAction="http://tempuri.org/ITaskTicker/UnsubscribeResponse")]
        void Unsubscribe();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskTicker/Unsubscribe", ReplyAction="http://tempuri.org/ITaskTicker/UnsubscribeResponse")]
        System.Threading.Tasks.Task UnsubscribeAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskTicker/GetTasks", ReplyAction="http://tempuri.org/ITaskTicker/GetTasksResponse")]
        TaskManager.TaskProxy.Task[] GetTasks();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskTicker/GetTasks", ReplyAction="http://tempuri.org/ITaskTicker/GetTasksResponse")]
        System.Threading.Tasks.Task<TaskManager.TaskProxy.Task[]> GetTasksAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskTicker/InsertTask", ReplyAction="http://tempuri.org/ITaskTicker/InsertTaskResponse")]
        TaskManager.TaskProxy.Task InsertTask(TaskManager.TaskProxy.Task task);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskTicker/InsertTask", ReplyAction="http://tempuri.org/ITaskTicker/InsertTaskResponse")]
        System.Threading.Tasks.Task<TaskManager.TaskProxy.Task> InsertTaskAsync(TaskManager.TaskProxy.Task task);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITaskTickerCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskTicker/TaskInserted", ReplyAction="http://tempuri.org/ITaskTicker/TaskInsertedResponse")]
        void TaskInserted(TaskManager.TaskProxy.Task task);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskTicker/TaskDeleted", ReplyAction="http://tempuri.org/ITaskTicker/TaskDeletedResponse")]
        void TaskDeleted(TaskManager.TaskProxy.Task task);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITaskTicker/TaskChanged", ReplyAction="http://tempuri.org/ITaskTicker/TaskChangedResponse")]
        void TaskChanged(TaskManager.TaskProxy.Task task);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITaskTickerChannel : TaskManager.TaskProxy.ITaskTicker, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TaskTickerClient : System.ServiceModel.DuplexClientBase<TaskManager.TaskProxy.ITaskTicker>, TaskManager.TaskProxy.ITaskTicker {
        
        public TaskTickerClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public TaskTickerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public TaskTickerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public TaskTickerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public TaskTickerClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Subscribe() {
            base.Channel.Subscribe();
        }
        
        public System.Threading.Tasks.Task SubscribeAsync() {
            return base.Channel.SubscribeAsync();
        }
        
        public void Unsubscribe() {
            base.Channel.Unsubscribe();
        }
        
        public System.Threading.Tasks.Task UnsubscribeAsync() {
            return base.Channel.UnsubscribeAsync();
        }
        
        public TaskManager.TaskProxy.Task[] GetTasks() {
            return base.Channel.GetTasks();
        }
        
        public System.Threading.Tasks.Task<TaskManager.TaskProxy.Task[]> GetTasksAsync() {
            return base.Channel.GetTasksAsync();
        }
        
        public TaskManager.TaskProxy.Task InsertTask(TaskManager.TaskProxy.Task task) {
            return base.Channel.InsertTask(task);
        }
        
        public System.Threading.Tasks.Task<TaskManager.TaskProxy.Task> InsertTaskAsync(TaskManager.TaskProxy.Task task) {
            return base.Channel.InsertTaskAsync(task);
        }
    }
}

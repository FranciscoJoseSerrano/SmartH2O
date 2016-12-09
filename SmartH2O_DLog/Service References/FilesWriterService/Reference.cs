﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartH2O_DLog.FilesWriterService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FilesWriterService.FilesWriter")]
    public interface FilesWriter {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/FilesWriter/sendWaterParameter", ReplyAction="http://tempuri.org/FilesWriter/sendWaterParameterResponse")]
        void sendWaterParameter(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/FilesWriter/sendWaterParameter", ReplyAction="http://tempuri.org/FilesWriter/sendWaterParameterResponse")]
        System.Threading.Tasks.Task sendWaterParameterAsync(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/FilesWriter/sendWaterAlarm", ReplyAction="http://tempuri.org/FilesWriter/sendWaterAlarmResponse")]
        void sendWaterAlarm(string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/FilesWriter/sendWaterAlarm", ReplyAction="http://tempuri.org/FilesWriter/sendWaterAlarmResponse")]
        System.Threading.Tasks.Task sendWaterAlarmAsync(string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface FilesWriterChannel : SmartH2O_DLog.FilesWriterService.FilesWriter, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FilesWriterClient : System.ServiceModel.ClientBase<SmartH2O_DLog.FilesWriterService.FilesWriter>, SmartH2O_DLog.FilesWriterService.FilesWriter {
        
        public FilesWriterClient() {
        }
        
        public FilesWriterClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FilesWriterClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FilesWriterClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FilesWriterClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void sendWaterParameter(string message) {
            base.Channel.sendWaterParameter(message);
        }
        
        public System.Threading.Tasks.Task sendWaterParameterAsync(string message) {
            return base.Channel.sendWaterParameterAsync(message);
        }
        
        public void sendWaterAlarm(string message) {
            base.Channel.sendWaterAlarm(message);
        }
        
        public System.Threading.Tasks.Task sendWaterAlarmAsync(string message) {
            return base.Channel.sendWaterAlarmAsync(message);
        }
    }
}

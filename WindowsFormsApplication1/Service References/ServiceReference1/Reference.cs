﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApplication1.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.SmartH20Service")]
    public interface SmartH20Service {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SmartH20Service/GetHourlyInSpecificDay", ReplyAction="http://tempuri.org/SmartH20Service/GetHourlyInSpecificDayResponse")]
        string GetHourlyInSpecificDay(string year, string month, string day, string parameter);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SmartH20Service/GetHourlyInSpecificDay", ReplyAction="http://tempuri.org/SmartH20Service/GetHourlyInSpecificDayResponse")]
        System.Threading.Tasks.Task<string> GetHourlyInSpecificDayAsync(string year, string month, string day, string parameter);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SmartH20Service/GetDailyInThreshold", ReplyAction="http://tempuri.org/SmartH20Service/GetDailyInThresholdResponse")]
        string GetDailyInThreshold(string firstYear, string firstMonth, string firstDay, string secondYear, string secondMonth, string secondDay, string parameter);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SmartH20Service/GetDailyInThreshold", ReplyAction="http://tempuri.org/SmartH20Service/GetDailyInThresholdResponse")]
        System.Threading.Tasks.Task<string> GetDailyInThresholdAsync(string firstYear, string firstMonth, string firstDay, string secondYear, string secondMonth, string secondDay, string parameter);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SmartH20ServiceChannel : WindowsFormsApplication1.ServiceReference1.SmartH20Service, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SmartH20ServiceClient : System.ServiceModel.ClientBase<WindowsFormsApplication1.ServiceReference1.SmartH20Service>, WindowsFormsApplication1.ServiceReference1.SmartH20Service {
        
        public SmartH20ServiceClient() {
        }
        
        public SmartH20ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SmartH20ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SmartH20ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SmartH20ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetHourlyInSpecificDay(string year, string month, string day, string parameter) {
            return base.Channel.GetHourlyInSpecificDay(year, month, day, parameter);
        }
        
        public System.Threading.Tasks.Task<string> GetHourlyInSpecificDayAsync(string year, string month, string day, string parameter) {
            return base.Channel.GetHourlyInSpecificDayAsync(year, month, day, parameter);
        }
        
        public string GetDailyInThreshold(string firstYear, string firstMonth, string firstDay, string secondYear, string secondMonth, string secondDay, string parameter) {
            return base.Channel.GetDailyInThreshold(firstYear, firstMonth, firstDay, secondYear, secondMonth, secondDay, parameter);
        }
        
        public System.Threading.Tasks.Task<string> GetDailyInThresholdAsync(string firstYear, string firstMonth, string firstDay, string secondYear, string secondMonth, string secondDay, string parameter) {
            return base.Channel.GetDailyInThresholdAsync(firstYear, firstMonth, firstDay, secondYear, secondMonth, secondDay, parameter);
        }
    }
}
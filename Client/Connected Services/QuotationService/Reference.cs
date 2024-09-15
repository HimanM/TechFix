﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.QuotationService {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="QuotationService.QuotationServiceSoap")]
    public interface QuotationServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string HelloWorld();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<string> HelloWorldAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddQuotation", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void AddQuotation(int supplierId, int productId, int quantityRequested, decimal priceOffered);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/AddQuotation", ReplyAction="*")]
        System.Threading.Tasks.Task AddQuotationAsync(int supplierId, int productId, int quantityRequested, decimal priceOffered);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UpdateQuotationStatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        void UpdateQuotationStatus(int quotationId, string status);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/UpdateQuotationStatus", ReplyAction="*")]
        System.Threading.Tasks.Task UpdateQuotationStatusAsync(int quotationId, string status);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetQuotationById", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GetQuotationById(int quotationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetQuotationById", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetQuotationByIdAsync(int quotationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllQuotations", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GetAllQuotations();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAllQuotations", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetAllQuotationsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetQuotationsByStatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataTable GetQuotationsByStatus(string status);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetQuotationsByStatus", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetQuotationsByStatusAsync(string status);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface QuotationServiceSoapChannel : Client.QuotationService.QuotationServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class QuotationServiceSoapClient : System.ServiceModel.ClientBase<Client.QuotationService.QuotationServiceSoap>, Client.QuotationService.QuotationServiceSoap {
        
        public QuotationServiceSoapClient() {
        }
        
        public QuotationServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public QuotationServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QuotationServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public QuotationServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string HelloWorld() {
            return base.Channel.HelloWorld();
        }
        
        public System.Threading.Tasks.Task<string> HelloWorldAsync() {
            return base.Channel.HelloWorldAsync();
        }
        
        public void AddQuotation(int supplierId, int productId, int quantityRequested, decimal priceOffered) {
            base.Channel.AddQuotation(supplierId, productId, quantityRequested, priceOffered);
        }
        
        public System.Threading.Tasks.Task AddQuotationAsync(int supplierId, int productId, int quantityRequested, decimal priceOffered) {
            return base.Channel.AddQuotationAsync(supplierId, productId, quantityRequested, priceOffered);
        }
        
        public void UpdateQuotationStatus(int quotationId, string status) {
            base.Channel.UpdateQuotationStatus(quotationId, status);
        }
        
        public System.Threading.Tasks.Task UpdateQuotationStatusAsync(int quotationId, string status) {
            return base.Channel.UpdateQuotationStatusAsync(quotationId, status);
        }
        
        public System.Data.DataTable GetQuotationById(int quotationId) {
            return base.Channel.GetQuotationById(quotationId);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetQuotationByIdAsync(int quotationId) {
            return base.Channel.GetQuotationByIdAsync(quotationId);
        }
        
        public System.Data.DataTable GetAllQuotations() {
            return base.Channel.GetAllQuotations();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetAllQuotationsAsync() {
            return base.Channel.GetAllQuotationsAsync();
        }
        
        public System.Data.DataTable GetQuotationsByStatus(string status) {
            return base.Channel.GetQuotationsByStatus(status);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetQuotationsByStatusAsync(string status) {
            return base.Channel.GetQuotationsByStatusAsync(status);
        }
    }
}

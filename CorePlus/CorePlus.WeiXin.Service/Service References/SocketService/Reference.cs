﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34014
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CorePlus.WeiXin.Service.SocketService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseSocketMessageEntity", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(CorePlus.WeiXin.Service.SocketService.SocketP2PMessageEntity))]
    public partial class BaseSocketMessageEntity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SenderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReceiverField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Sender {
            get {
                return this.SenderField;
            }
            set {
                if ((object.ReferenceEquals(this.SenderField, value) != true)) {
                    this.SenderField = value;
                    this.RaisePropertyChanged("Sender");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string Receiver {
            get {
                return this.ReceiverField;
            }
            set {
                if ((object.ReferenceEquals(this.ReceiverField, value) != true)) {
                    this.ReceiverField = value;
                    this.RaisePropertyChanged("Receiver");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SocketP2PMessageEntity", Namespace="http://tempuri.org/")]
    [System.SerializableAttribute()]
    public partial class SocketP2PMessageEntity : CorePlus.WeiXin.Service.SocketService.BaseSocketMessageEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdentityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MsgTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DialogIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OpenIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WeiXinNoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OwnerField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string Identity {
            get {
                return this.IdentityField;
            }
            set {
                if ((object.ReferenceEquals(this.IdentityField, value) != true)) {
                    this.IdentityField = value;
                    this.RaisePropertyChanged("Identity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string MsgType {
            get {
                return this.MsgTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.MsgTypeField, value) != true)) {
                    this.MsgTypeField = value;
                    this.RaisePropertyChanged("MsgType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string DialogId {
            get {
                return this.DialogIdField;
            }
            set {
                if ((object.ReferenceEquals(this.DialogIdField, value) != true)) {
                    this.DialogIdField = value;
                    this.RaisePropertyChanged("DialogId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string OpenId {
            get {
                return this.OpenIdField;
            }
            set {
                if ((object.ReferenceEquals(this.OpenIdField, value) != true)) {
                    this.OpenIdField = value;
                    this.RaisePropertyChanged("OpenId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string WeiXinNo {
            get {
                return this.WeiXinNoField;
            }
            set {
                if ((object.ReferenceEquals(this.WeiXinNoField, value) != true)) {
                    this.WeiXinNoField = value;
                    this.RaisePropertyChanged("WeiXinNo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string Owner {
            get {
                return this.OwnerField;
            }
            set {
                if ((object.ReferenceEquals(this.OwnerField, value) != true)) {
                    this.OwnerField = value;
                    this.RaisePropertyChanged("Owner");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SocketService.SocketServiceSoap")]
    public interface SocketServiceSoap {
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 entity 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendMessageToP2PServer", ReplyAction="*")]
        CorePlus.WeiXin.Service.SocketService.SendMessageToP2PServerResponse SendMessageToP2PServer(CorePlus.WeiXin.Service.SocketService.SendMessageToP2PServerRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendMessageToP2PServerRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendMessageToP2PServer", Namespace="http://tempuri.org/", Order=0)]
        public CorePlus.WeiXin.Service.SocketService.SendMessageToP2PServerRequestBody Body;
        
        public SendMessageToP2PServerRequest() {
        }
        
        public SendMessageToP2PServerRequest(CorePlus.WeiXin.Service.SocketService.SendMessageToP2PServerRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SendMessageToP2PServerRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public CorePlus.WeiXin.Service.SocketService.SocketP2PMessageEntity entity;
        
        public SendMessageToP2PServerRequestBody() {
        }
        
        public SendMessageToP2PServerRequestBody(CorePlus.WeiXin.Service.SocketService.SocketP2PMessageEntity entity) {
            this.entity = entity;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendMessageToP2PServerResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendMessageToP2PServerResponse", Namespace="http://tempuri.org/", Order=0)]
        public CorePlus.WeiXin.Service.SocketService.SendMessageToP2PServerResponseBody Body;
        
        public SendMessageToP2PServerResponse() {
        }
        
        public SendMessageToP2PServerResponse(CorePlus.WeiXin.Service.SocketService.SendMessageToP2PServerResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class SendMessageToP2PServerResponseBody {
        
        public SendMessageToP2PServerResponseBody() {
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SocketServiceSoapChannel : CorePlus.WeiXin.Service.SocketService.SocketServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SocketServiceSoapClient : System.ServiceModel.ClientBase<CorePlus.WeiXin.Service.SocketService.SocketServiceSoap>, CorePlus.WeiXin.Service.SocketService.SocketServiceSoap {
        
        public SocketServiceSoapClient() {
        }
        
        public SocketServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SocketServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SocketServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SocketServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        CorePlus.WeiXin.Service.SocketService.SendMessageToP2PServerResponse CorePlus.WeiXin.Service.SocketService.SocketServiceSoap.SendMessageToP2PServer(CorePlus.WeiXin.Service.SocketService.SendMessageToP2PServerRequest request) {
            return base.Channel.SendMessageToP2PServer(request);
        }
        
        public void SendMessageToP2PServer(CorePlus.WeiXin.Service.SocketService.SocketP2PMessageEntity entity) {
            CorePlus.WeiXin.Service.SocketService.SendMessageToP2PServerRequest inValue = new CorePlus.WeiXin.Service.SocketService.SendMessageToP2PServerRequest();
            inValue.Body = new CorePlus.WeiXin.Service.SocketService.SendMessageToP2PServerRequestBody();
            inValue.Body.entity = entity;
            CorePlus.WeiXin.Service.SocketService.SendMessageToP2PServerResponse retVal = ((CorePlus.WeiXin.Service.SocketService.SocketServiceSoap)(this)).SendMessageToP2PServer(inValue);
        }
    }
}

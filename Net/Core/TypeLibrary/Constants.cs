using System;
using System.Runtime.InteropServices;
using Primavera.Platform.CloudServices900.Context;

namespace Primavera.Platform.CloudServices900
{
    /// <summary>
    /// Exposes constants, types and enumerations to COM.
    /// The static class itself is not exposed.
    /// </summary>
    public static class Constants
    {
        #region Public Constants, Types and Enums

        /// <summary>
        /// Proxy state.
        /// </summary>
        [ComVisible(true)]
        [Guid(Guids.ProxyState)]
        public enum ProxyState
        {
            /// <summary>
            /// Proxy initialized.
            /// </summary>
            Inited = 0,

            /// <summary>
            /// Proxy opened.
            /// </summary>
            Opened = 1,

            /// <summary>
            /// Proxy closed.
            /// </summary>
            Closed = 2
        }

        /// <summary>
        /// Enumeration of ERP product (from <c>InstanceVersion</c> enumeration of <c>Plataforma.Contexto.TipoProduto</c>). 
        /// Related with branding features.
        /// </summary>
        [ComVisible(true)]
        [Guid(Guids.ErpProduct)]
        public enum ErpProduct
        {
            /// <summary>
            /// None or undefined product.
            /// </summary>
            None = 0,

            /// <summary>
            /// Professional product.
            /// </summary>
            Professional = 1,

            /// <summary>
            /// Executive product.
            /// </summary>
            Executive = 2,

            /// <summary>
            /// <c>SaaS</c> product.
            /// </summary>
            SaaS = 3,

            /// <summary>
            /// Starter product.
            /// </summary>
            Starter = 4,

            /// <summary>
            /// Starter Plus product.
            /// </summary>
            StarterPlus = 5,
        }

        /// <summary>
        /// Enumeration of ERP environment (from <c>EnumTipoAmbiente</c> enumeration of <c>Plataforma.Contexto.TipoAmbiente</c>).
        /// Related with functional features.
        /// </summary>
        [ComVisible(true)]
        [Guid(Guids.ErpEnvironment)]
        public enum ErpEnvironment
        {
            /// <summary>
            /// None or undefined.
            /// </summary>
            None = -1,

            /// <summary>
            /// Normal environment.
            /// </summary>
            Normal = 0,

            /// <summary>
            /// <c>SaaS</c> environment.
            /// </summary>
            SaaS = 1
        }

        /// <summary>
        /// Enumeration of ERP platform (from <c>EnumTipoPlataforma</c> enumeration of <c>Plataforma.Contexto.TipoPlataforma</c>).
        /// Related with functional features.
        /// </summary>
        [ComVisible(true)]
        [Guid(Guids.ErpPlatform)]
        public enum ErpPlatform
        {
            /// <summary>
            /// None or undefined.
            /// </summary>
            None = -1,

            /// <summary>
            /// Executive platform.
            /// </summary>
            Executive = 0,

            /// <summary>
            /// Professional platform.
            /// </summary>
            Professional = 1,

            /// <summary>
            /// First platform.
            /// </summary>
            First = 2
        }

        /// <summary>
        /// Enumeration of ERP connector status (from <c>EnumEstadoConnector</c> enumeration).
        /// Related with information features.
        /// </summary>
        [ComVisible(true)]
        [Guid(Guids.ErpConnectorStatus)]
        public enum ErpConnectorStatus
        {
            /// <summary>
            /// Connector connected.
            /// </summary>
            Connected = 0,

            /// <summary>
            /// No instance registered in connector.
            /// </summary>
            NoInstanceRegistered = 1,

            /// <summary>
            /// No communication with connector.
            /// </summary>
            NoCommunication = 2,

            /// <summary>
            /// Connector not installed.
            /// </summary>
            NotInstalled = 3
        }

        /// <summary>
        /// Enumeration of http encodings.
        /// Related with http features.
        /// </summary>
        [ComVisible(true)]
        [Guid(Guids.HttpEncoding)]
        public enum HttpEncoding
        {
            /// <summary>
            /// Use ASCII for plain text data.
            /// </summary>
            Ascii = 0,

            /// <summary>
            /// Use UTF8 for xml data.
            /// </summary>
            Utf8 = 1
        }

        /// <summary>
        /// Enumeration of browser behaviors.
        /// Related with UI features.
        /// </summary>
        [ComVisible(true)]
        [Guid(Guids.BrowserBehavior)]
        public enum BrowserBehavior
        {
            /// <summary>
            /// Standard behavior.
            /// </summary>
            Standard = 0,

            /// <summary>
            /// Enhanced behavior.
            /// </summary>
            Enhanced = 1,

            /// <summary>
            /// Debug behavior.
            /// </summary>
            Debug = 2
        }

        /// <summary>
        /// Enumeration of hosting behaviors.
        /// Related with the integration of managed/unmanaged UI.
        /// </summary>
        [ComVisible(true)]
        [Guid(Guids.HostBehavior)]
        public enum HostBehavior
        {
            /// <summary>
            /// Host in a self managed window.
            /// </summary>
            SelfWindow = 0,

            /// <summary>
            /// Host in a parent unmanaged window.
            /// </summary>
            ParentWindow = 1,

            /// <summary>
            /// Host in a parent unmanaged window MDI.
            /// </summary>
            ParentWindowMdi = 2
        }

        /// <summary>
        /// Enumeration of HTTP Status Codes.
        /// Related with navigating errors.
        /// </summary>
        [ComVisible(true)]
        [Guid(Guids.HttpStatusCode)]
        public enum HttpStatusCode
        {
            /// <summary>
            /// None or undefined.
            /// </summary>
            None = 0,

            /// <summary>
            /// HttpStatus BadGateway.
            /// </summary>
            HttpStatusBadGateway = 502,

            /// <summary>
            /// HttpStatus Bad Method.
            /// </summary>
            HttpStatusBadMethod = 405,

            /// <summary>
            /// HttpStatus Bad Request.
            /// </summary>
            HttpStatusBadRequest = 400,

            /// <summary>
            /// HttpStatus Conflict.
            /// </summary>
            HttpStatusConflict = 409,

            /// <summary>
            /// HttpStatus Denied.
            /// </summary>
            HttpStatusDenied = 401,

            /// <summary>
            /// HttpStatus Forbidden.
            /// </summary>
            HttpStatusForbidden = 403,

            /// <summary>
            /// HttpStatus Gateway Timeout.
            /// </summary>
            HttpStatusGatewayTimeout = 504,

            /// <summary>
            /// HttpStatus Gone.
            /// </summary>
            HttpStatusGone = 410,

            /// <summary>
            /// HttpStatus Length Required.
            /// </summary>
            HttpStatusLengthRequired = 411,

            /// <summary>
            /// HttpStatus None Acceptable.
            /// </summary>
            HttpStatusNoneAcceptable = 406,

            /// <summary>
            /// HttpStatus Not Found.
            /// </summary>
            HttpStatusNotFound = 404,

            /// <summary>
            /// HttpStatus Not Supported.
            /// </summary>
            HttpStatusNotSupported = 501,

            /// <summary>
            /// HttpStatus Payment Required.
            /// </summary>
            HttpStatusPaymentReq = 402,

            /// <summary>
            /// HttpStatus Precondition Failed.
            /// </summary>
            HttpStatusPrecondFailed = 412,

            /// <summary>
            /// HttpStatus Proxy Auth Required.
            /// </summary>
            HttpStatusProxyAuthReq = 407,

            /// <summary>
            /// HttpStatus Request Timeout.
            /// </summary>
            HttpStatusRequestTimeout = 408,

            /// <summary>
            /// HttpStatus Request Too Large.
            /// </summary>
            HttpStatusRequestTooLarge = 413,

            /// <summary>
            /// HttpStatus Retry With.
            /// </summary>
            HttpStatusRetryWith = 449,

            /// <summary>
            /// HttpStatus Server Error.
            /// </summary>
            HttpStatusServerError = 500,

            /// <summary>
            /// HttpStatus Service Unavailable.
            /// </summary>
            HttpStatusServiceUnavail = 503,

            /// <summary>
            /// HttpStatus Unsupported Media.
            /// </summary>
            HttpStatusUnsupportedMedia = 415,

            /// <summary>
            /// HttpStatus URI Too Long.
            /// </summary>
            HttpStatusUriTooLong = 414,

            /// <summary>
            /// HttpStatus Version Not Sup.
            /// </summary>
            HttpStatusVersionNotSup = 505
        }

        /// <summary>
        /// Enumeration of regular keys used in ContextDictionary.
        /// Use the <c>ContextDictionary.KeyParse</c> method to get the enumeration string value.
        /// </summary>
        [ComVisible(true)]
        [Guid(Guids.ContextKey)]
        public enum ContextKey
        {
            /// <summary>
            /// Notification key.
            /// </summary>
            [StringValue("Notification")]
            Notification,

            /// <summary>
            /// Default key.
            /// </summary>
            [StringValue("Default")]
            Default,

            /// <summary>
            /// Success key.
            /// </summary>
            [StringValue("Success")]
            Success,

            /// <summary>
            /// Culture key.
            /// </summary>
            [StringValueAttribute("Culture")]
            Culture,

            /// <summary>
            /// Instance key.
            /// </summary>
            [StringValue("Instance")]
            Instance,

            /// <summary>
            /// Company key.
            /// </summary>
            [StringValue("Company")]
            Company,

            /// <summary>
            /// User key.
            /// </summary>
            [StringValue("User")]
            User,

            /// <summary>
            /// <c>ErpProduct</c>  key.
            /// </summary>
            [StringValueAttribute("ErpProduct")]
            ErpProduct,

            /// <summary>
            /// <c>ErpPlatform</c> key.
            /// </summary>
            [StringValueAttribute("ErpPlatform")]
            ErpPlatform,

            /// <summary>
            /// <c>ErpEnvironment</c> key.
            /// </summary>
            [StringValueAttribute("ErpEnvironment")]
            ErpEnvironment,

            /// <summary>
            /// <c>ErpConfigPath</c> key.
            /// </summary>
            [StringValueAttribute("ErpConfigPath")]
            ErpConfigPath,

            /// <summary>
            /// Browser key.
            /// </summary>
            [StringValueAttribute("Browser")]
            Browser,

            /// <summary>
            /// BusinessEntity key.
            /// </summary>
            [StringValueAttribute("BusinessEntity")]
            BusinessEntity,

            /// <summary>
            /// ServicesProxy key.
            /// </summary>
            [StringValueAttribute("ServicesProxy")]
            ServicesProxy,

            /// <summary>
            /// ConfigProxy key.
            /// </summary>
            [StringValueAttribute("ConfigProxy")]
            ConfigProxy,

            /// <summary>
            /// CloudConnectorRoutingAddress key.
            /// </summary>
            [StringValueAttribute("CloudConnectorRoutingAddress")]
            CloudConnectorRoutingAddress,

            /// <summary>
            /// CloudPortalBaseAddress key.
            /// </summary>
            /// <remarks>
            /// WARNING, This string value identifies a key name in the ServiceAddress.config file that is located on the CloudConnector hosting directory.
            /// DO NOT CHANGE THE VALUE WITHOUT CHEKING THE CORRESPONDING KEY NAME IN THE FILE.
            /// </remarks>
            [StringValueAttribute("CloudPortal.BaseAddress")]
            CloudPortalBaseAddress,

            /// <summary>
            /// CloudServicesBaseAddress key.
            /// </summary>
            /// <remarks>
            /// WARNING, This string value identifies a key name in the ServiceAddress.config file that is located on the CloudConnector hosting directory.
            /// DO NOT CHANGE THE VALUE WITHOUT CHEKING THE CORRESPONDING KEY NAME IN THE FILE.
            /// </remarks>
            [StringValueAttribute("CloudServices.BaseAddress")]
            CloudServicesBaseAddress,

            /// <summary>
            /// ObjectProxy key.
            /// </summary>
            [StringValueAttribute("ObjectProxy")]
            ObjectProxy,

            /// <summary>
            /// Url key.
            /// </summary>
            [StringValueAttribute("Url")]
            Url,

            /// <summary>
            /// Service key.
            /// </summary>
            [StringValueAttribute("Service")]
            Service,

            /// <summary>
            /// Operation key.
            /// </summary>
            [StringValueAttribute("Operation")]
            Operation,

            /// <summary>
            /// Data key.
            /// </summary>
            [StringValueAttribute("Data")]
            Data,

            /// <summary>
            /// <c>CloudServicesConfigPath</c> key.
            /// </summary>
            [StringValueAttribute("CloudServicesConfigPath")]
            CloudServicesConfigPath,

            /// <summary>
            /// <c>Customer identifier key.</c> key.
            /// </summary>
            [StringValueAttribute("CustomerKey")]
            CustomerKey,

            /// <summary>
            /// Context identifier key.
            /// </summary>
            [StringValue("ContextID")]
            ContextID,

            /// <summary>
            /// Event identifier key.
            /// </summary>
            [StringValue("EventID")]
            EventID,

            /// <summary>
            /// Request identifier key.
            /// </summary>
            [StringValue("RequestID")]
            RequestID,

            /// <summary>
            /// Response identifier key.
            /// </summary>
            [StringValue("ResponseID")]
            ResponseID,

            /// <summary>
            /// Value identifier key.
            /// </summary>
            [StringValue("Value")]
            Value,

            /// <summary>
            /// Signed In Customer identifier key.
            /// </summary>
            [StringValueAttribute("SignedInCustomer")]
            SignedInCustomer,

            /// <summary>
            /// Exception key.
            /// </summary>
            [StringValue("Exception")]
            Exception,

            /// <summary>
            /// Login email key.
            /// </summary>
            [StringValueAttribute("UserLogin")]
            UserLogin,

            /// <summary>
            /// Login remember key.
            /// </summary>
            [StringValueAttribute("UserPassword")]
            UserPassword,
            
            /// <summary>
            /// Signin remember key.
            /// </summary>
            [StringValueAttribute("SignInRemember")]
            SignInRemember
        }

        #endregion
    }
}

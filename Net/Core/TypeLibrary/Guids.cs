namespace Primavera.Platform.CloudServices900
{
    /// <summary>
    /// Provides the identity for exposed COM classes and interfaces.
    /// If you change them, existing clients will no longer be able to access the class.
    /// Also be aware that COM instantiation uses Namespace for late binding and AssemblyName for direct reference binding.
    /// Therefore changing the AssemblyName or Namespace requires the generation of new IDs to avoid conflicts.
    /// </summary>
    internal static class Guids
    {
        // Typelib IDs when this assembly is exposed to COM

        #region Assembly
        
        /// <summary>
        /// Assembly identity for COM.
        /// </summary>
        public const string Assembly = "811EDBB7-B721-4EAE-BCFA-29F510A1FF26";

        #endregion

        #region Proxy

        /// <summary>
        /// ProxyState identity for COM.
        /// </summary>
        public const string ProxyState = "A2703A0E-7A4D-4EF9-9071-127377198837";

        #endregion

        #region Erp
        
        /// <summary>
        /// <c>ErpPlatform</c> identity for COM.
        /// </summary>
        public const string ErpPlatform = "5C516BA6-84F2-484F-8159-975484128709";

        /// <summary>
        /// <c>ErpProduct</c> identity for COM.
        /// </summary>
        public const string ErpProduct = "6C19A974-1FEB-4BD2-9DF1-15231380D424";

        /// <summary>
        /// <c>ErpEnvironment</c> identity for COM.
        /// </summary>
        public const string ErpEnvironment = "BE93846C-8966-473A-9228-06153E809C3B";

        /// <summary>
        /// <c>ErpConnectorStatus</c> identity for COM.
        /// </summary>
        public const string ErpConnectorStatus = "37B17A55-01F5-4F36-A8BB-D83C8A1F9887";

        #endregion

        #region ContextDictionary

        /// <summary>
        /// IContextDictionary identity for COM.
        /// </summary>
        public const string IContextDictionary = "6259E13F-2CB4-4073-B56F-5065168FBA64";

        /// <summary>
        /// ContextDictionary identity for COM.
        /// </summary>
        public const string ContextDictionary = "D2D87F32-7F09-4527-902F-4E668A756689";

        /// <summary>
        /// ContextKey identity for COM.
        /// </summary>
        public const string ContextKey = "FB214D23-DB45-4059-8982-44B17B4BE258";

        #endregion

        #region HttpProxy

        /// <summary>
        /// IHttpProxy identity for COM.
        /// </summary>
        public const string IHttpProxy = "BE5CC23C-4FBB-4E60-AC68-685625E487F0";

        /// <summary>
        /// HttpProxy identity for COM.
        /// </summary>
        public const string HttpProxy = "4A8ADADC-6957-4227-9F1A-F35C9B1736A6";

        /// <summary>
        /// HttpEncoding identity for COM.
        /// </summary>
        public const string HttpEncoding = "FA27D05E-7471-41F2-B10F-788584A89A21";

        #endregion

        #region WcfProxy

        /// <summary>
        /// <c>IWcfProxy</c> identity for COM.
        /// </summary>
        public const string IWcfProxy = "CC9981F1-BB82-4FBF-BEB2-972FFA4EFDD2";

        /// <summary>
        /// <c>WcfProxy</c> identity for COM.
        /// </summary>
        public const string WcfProxy = "78D3A5DB-D07E-41DB-9639-997EAD4DF475";

        #endregion

        #region WebBrowserProxy

        /// <summary>
        /// IWebBrowserProxy identity for COM.
        /// </summary>
        public const string IWebBrowserProxy = "9B7E861E-274D-4B50-96E5-C1E3CE744735";

        /// <summary>
        /// IWebBrowserEvents identity for COM.
        /// </summary>
        public const string IWebBrowserEvents = "EF682E0C-E0D2-4793-9D9A-32365AC5B59A";

        /// <summary>
        /// WebBrowserProxy identity for COM.
        /// </summary>
        public const string WebBrowserProxy = "F5CCB878-A4A6-47BE-9D82-089DFF736F19";

        /// <summary>
        /// BrowserBehavior identity for COM.
        /// </summary>
        public const string BrowserBehavior = "3B1B6BB3-2344-4B33-8A89-6656B1F45539";

        /// <summary>
        /// HostBehavior identity for COM.
        /// </summary>
        public const string HostBehavior = "8DD93056-EFEC-45C5-B64E-076BAC374531";

        /// <summary>
        /// HttpStatusCode identity for COM.
        /// </summary>
        public const string HttpStatusCode = "47EB3DB3-FD51-4E5E-B27D-E5D995B67C31";

        #endregion

        #region WebBrowserProxy (Events)

        /// <summary>
        /// INavigatingEventArgs identity for COM.
        /// </summary>
        public const string INavigatingEventArgs = "FC20FEA9-5291-4961-8079-59DB5CC4C1DC";

        /// <summary>
        /// NavigatingEventArgs identity for COM.
        /// </summary>
        public const string NavigatingEventArgs = "D034ADFE-6A56-467B-8698-B40FDD3B55CA";

        /// <summary>
        /// INavigatedEventArgs identity for COM.
        /// </summary>
        public const string INavigatedEventArgs = "D609DAA4-F8A7-45F0-885C-4F04453198E1";

        /// <summary>
        /// NavigatedEventArgs identity for COM.
        /// </summary>
        public const string NavigatedEventArgs = "2C5C7774-5D80-471B-B432-1379E7EC1A92";

        /// <summary>
        /// IDocumentCompletedEventArgs identity for COM.
        /// </summary>
        public const string IDocumentCompletedEventArgs = "A321CD40-8F27-4EF5-B7E0-85A1555FD38A";

        /// <summary>
        /// DocumentCompletedEventArgs identity for COM.
        /// </summary>
        public const string DocumentCompletedEventArgs = "B3ACFA2D-C100-4B45-AB2E-14A33360B4B4";

        /// <summary>
        /// INavigateErrorEventArgs identity for COM.
        /// </summary>
        public const string INavigateErrorEventArgs = "01A66FDD-74E1-42EB-BA8C-6317A04EF262";

        /// <summary>
        /// NavigateErrorEventArgs identity for COM.
        /// </summary>
        public const string NavigateErrorEventArgs = "E36AC9BF-DF03-41DD-B5CA-15EF4F93C6BD";

        /// <summary>
        /// IFormClosedEventArgs identity for COM.
        /// </summary>
        public const string IClosedEventArgs = "A3FD8914-D3EF-41D7-934F-CA8D2D8F1ED8";

        /// <summary>
        /// FormClosedEventArgs identity for COM.
        /// </summary>
        public const string ClosedEventArgs = "96156EFC-7F6D-41EC-BD74-D42BFC4520D2";

        #endregion

        #region ConfigProxy

        /// <summary>
        /// IConfigProxy identity for COM.
        /// </summary>
        public const string IConfigProxy = "CA03276F-5694-4A97-B5F2-723CE7465F15";

        /// <summary>
        /// ConfigProxy identity for COM.
        /// </summary>
        public const string ConfigProxy = "33844FFA-A0C5-48F4-AEB2-5764D82E84BE";

        #endregion

        #region TestingProxy

        /// <summary>
        /// ITestingProxy identity for COM.
        /// </summary>
        public const string ITestingProxy = "062DF12A-96C8-42BC-9DBE-043A1D1C765D";

        /// <summary>
        /// TestingProxy identity for COM.
        /// </summary>
        public const string TestingProxy = "D2FFCC9B-842C-45EE-9A77-1270B32665A7";

        #endregion

        #region ServicesProxy

        /// <summary>
        /// IServicesProxy identity for COM.
        /// </summary>
        public const string IServicesProxy = "92E9EB20-18A9-447F-AB13-19BAC9F957DD";

        /// <summary>
        /// IServicesEvents identity for COM.
        /// </summary>
        public const string IServicesEvents = "BF2F3FB4-A1F4-4953-B129-06F642CC5D7C";

        /// <summary>
        /// ServicesProxy identity for COM.
        /// </summary>
        public const string ServicesProxy = "B2919907-E864-488A-BF1B-86662EFC15F8";

        #endregion

        #region Notification (Event)

        /// <summary>
        /// INotificationEventArgs identity for COM.
        /// </summary>
        public const string INotificationEventArgs = "99F6E51B-295D-4E8B-8BD9-AB78F3BA1518";

        /// <summary>
        /// NotificationEventArgs identity for COM.
        /// </summary>
        public const string NotificationEventArgs = "1B5D75EE-86B8-4D1C-9373-5DE5E9AD2A8D";

        #endregion
    }
}

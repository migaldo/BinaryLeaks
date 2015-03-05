using System;
using System.Runtime.InteropServices;
using ComTypes = System.Runtime.InteropServices.ComTypes;

namespace Primavera.Platform.CloudServices900.Helpers
{
    /// <summary>
    /// Provides a COM helper.
    /// </summary>
    public static class ComHelperNativeMethods
    {
        /// <summary> 
        /// Returns a string value representing the type name of the specified COM object. 
        /// </summary> 
        /// <param name="comObject">A COM object the type name of which to return.</param> 
        /// <returns>A string containing the type name.</returns> 
        public static string GetTypeName(object comObject)
        {
            if (comObject == null)
            {
                return string.Empty;
            }

            if (!Marshal.IsComObject(comObject))
            {
                // The specified object is not a COM object 
                return string.Empty;
            }

            IDispatch dispatch = comObject as IDispatch;

            if (dispatch == null)
            {
                // The specified COM object doesn't support getting type information 
                return string.Empty;
            }

            ComTypes.ITypeInfo typeInfo = null;

            try
            {
                try
                {
                    // Obtain the ITypeInfo interface from the object 
                    var errorCode = dispatch.GetTypeInfo(0, 0, out typeInfo);

                    if (errorCode != 0)
                    {
                        // Cannot get the ITypeInfo interface for the specified COM object 
                        return string.Empty;
                    }
                }
                catch (Exception)
                {
                    // Cannot get the ITypeInfo interface for the specified COM object 
                    return string.Empty;
                }

                string typeName = string.Empty;
                string documentation, helpFile;
                int helpContext = -1;

                try
                {
                    // Retrieves the documentation string for the specified type description 
                    typeInfo.GetDocumentation(-1, out typeName, out documentation, out helpContext, out helpFile);
                }
                catch (Exception)
                {
                    // Cannot extract ITypeInfo information 
                    return string.Empty;
                }

                return typeName;
            }
            catch (Exception)
            {
                // Unexpected error 
                return string.Empty;
            }
            finally
            {
                if (typeInfo != null)
                {
                    Marshal.ReleaseComObject(typeInfo);
                }
            }
        }

        /// <summary> 
        /// Exposes objects, methods and properties to programming tools and other 
        /// applications that support Automation. 
        /// </summary> 
        [ComImport]
        [Guid("00020400-0000-0000-C000-000000000046")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IDispatch
        {
            /// <summary>
            /// Gets the type info count.
            /// </summary>
            /// <param name="count">The count.</param>
            /// <returns>The count result.</returns>
            [PreserveSig]
            int GetTypeInfoCount(out int count);

            /// <summary>
            /// Gets the type info.
            /// </summary>
            /// <param name="iTInfo">The <c>iTInfo</c> parameter.</param>
            /// <param name="lcid">The <c>lcid</c> parameter.</param>
            /// <param name="typeInfo">The <c>typeInfo</c> parameter.</param>
            /// <returns>The <c>typeInfo</c> result.</returns>
            [PreserveSig]
            int GetTypeInfo([MarshalAs(UnmanagedType.U4)] int iTInfo, [MarshalAs(UnmanagedType.U4)] int lcid, out ComTypes.ITypeInfo typeInfo);

            /// <summary>
            /// Gets the IDs of names.
            /// </summary>
            /// <param name="riid">The <c>riid</c> parameter.</param>
            /// <param name="rgsNames">The <c>rgsNames</c> parameter.</param>
            /// <param name="cNames">The <c>cNames</c> parameter.</param>
            /// <param name="lcid">The <c>lcid</c> parameter.</param>
            /// <param name="rgDispId">The <c>rgDispId</c> parameter.</param>
            /// <returns>The IDs of names result.</returns>
            [PreserveSig]
            int GetIDsOfNames(ref Guid riid, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr)] string[] rgsNames, int cNames, int lcid, [MarshalAs(UnmanagedType.LPArray)] int[] rgDispId);

            /// <summary>
            /// Invokes the specified <c>dispIdMember</c>.
            /// </summary>
            /// <param name="dispIdMember">The <c>dispIdMember</c> parameter.</param>
            /// <param name="riid">The <c>riid</c> parameter.</param>
            /// <param name="lcid">The <c>lcid</c> parameter.</param>
            /// <param name="wFlags">The <c>wFlags</c> parameter.</param>
            /// <param name="pDispParams">The <c>pDispParams</c> parameter.</param>
            /// <param name="pVarResult">The <c>pVarResult</c> parameter.</param>
            /// <param name="pExcepInfo">The <c>pExcepInfo</c> parameter.</param>
            /// <param name="pArgErr">The <c>pArgErr</c> parameter.</param>
            /// <returns>The code result.</returns>
            [PreserveSig]
            int Invoke(int dispIdMember, ref Guid riid, uint lcid, ushort wFlags, ref ComTypes.DISPPARAMS pDispParams, out object pVarResult, ref ComTypes.EXCEPINFO pExcepInfo, IntPtr[] pArgErr);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace BinaryLeaks.Core.Extensions
{
  /// <summary>
  /// Provides Windows Data Protection API (DPAPI) extension methods
  /// that deal with string and <see cref="SecureString"/>
  /// encryption, decryption and encapsulation.
  /// </summary>
  /// <remarks>
  /// The Windows Data Protection API (DPAPI) is a great technology to securely encrypt
  /// user or machine specific data without having to worry about an encryption key.
  /// Since .NET 2.0, DPAPI is part of the .NET framework.
  /// Encrypting data using the DataProtectionScope.CurrentUser grants that result
  /// can only be decrypted by the same current user. That's why this technique is highly
  /// recommended to encrypting user settings.
  /// </remarks>
  public static class SecurityExtensions
  {
    /// <summary>
    /// Specifies the data protection scope of the DPAPI.
    /// </summary>
    private const DataProtectionScope Scope = DataProtectionScope.CurrentUser;

    /// <summary>
    /// Encrypts the specified plain text.
    /// </summary>
    /// <param name="plainText">An unencrypted string that needs to be secured.</param>
    /// <returns>
    /// A base64 encoded string that represents the encrypted binary data.
    /// </returns>
    /// <remarks>
    /// This solution is not really secure as we are keeping strings in memory.
    /// If runtime protection is essential,<see cref="SecureString"/> should be used.</remarks>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="plainText"/> is a null reference.
    /// </exception>
    public static string Encrypt(this string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
        {
           throw new ArgumentNullException("plainText");
        }

        try
        {
            // encrypt data

            var data = Encoding.Unicode.GetBytes(plainText);
            byte[] encrypted = ProtectedData.Protect(data, null, Scope);

            // return as base64 string

            return Convert.ToBase64String(encrypted);
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Decrypts the specified cipher.
    /// </summary>
    /// <param name="cipher">The cipher.</param>
    /// <returns>The decrypted cipher.</returns>
    public static string Decrypt(this string cipher)
    {
        return Decrypt(cipher, true);
    }

    /// <summary>
    /// Decrypts the specified cipher.
    /// </summary>
    /// <param name="cipher">A base64 encoded string that was created
    /// through the <see cref="Encrypt(string)"/> or
    /// <see cref="Encrypt(SecureString)"/> extension methods.</param>
    /// <param name="throwException">If set to <c>true</c> [throw exception].</param>
    /// <returns>The decrypted string.</returns>
    /// <remarks>Keep in mind that the decrypted string remains in memory
    /// and makes your application vulnerable per se. If runtime protection
    /// is essential, <see cref="SecureString"/> should be used.</remarks>
    /// <exception cref="ArgumentNullException">If <paramref name="cipher"/>
    /// is a null reference.</exception>
    public static string Decrypt(this string cipher, bool throwException)
    {
        if (string.IsNullOrEmpty(cipher))
        {
            if (throwException)
            {
                throw new ArgumentNullException("cipher");
            }
        }
        
        try
        {
            // parse base64 string
            byte[] data = Convert.FromBase64String(cipher);

            // decrypt data
            byte[] decrypted = ProtectedData.Unprotect(data, null, Scope);
            return Encoding.Unicode.GetString(decrypted);
        }
        catch (Exception)
        {
            if (throwException)
            {
                throw;
            }
            else
            {
                return default(string);
            }
        }
    }

    /// <summary>
    /// Encrypts the contents of a secure string.
    /// </summary>
    /// <param name="value">An unencrypted string that needs
    /// to be secured.</param>
    /// <returns>A base64 encoded string that represents the encrypted
    /// binary data.
    /// </returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/>
    /// is a null reference.</exception>
    public static string Encrypt(this SecureString value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }

        IntPtr ptr = Marshal.SecureStringToCoTaskMemUnicode(value);
        try
        {
            char[] buffer = new char[value.Length];
            Marshal.Copy(ptr, buffer, 0, value.Length);

            byte[] data = Encoding.Unicode.GetBytes(buffer);
            byte[] encrypted = ProtectedData.Protect(data, null, Scope);

            // return as base64 string

            return Convert.ToBase64String(encrypted);
        }
        finally
        {
        Marshal.ZeroFreeCoTaskMemUnicode(ptr);
        }
    }

    /// <summary>
    /// Decrypts a base64 encrypted string and returns the decrypted data
    /// wrapped into a <see cref="SecureString"/> instance.
    /// </summary>
    /// <param name="cipher">A base64 encoded string that was created
    /// through the <see cref="Encrypt(string)"/> or
    /// <see cref="Encrypt(SecureString)"/> extension methods.</param>
    /// <returns>The decrypted string, wrapped into a
    /// <see cref="SecureString"/> instance.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="cipher"/>
    /// is a null reference.</exception>
    public static SecureString DecryptSecure(this string cipher)
    {
        if (cipher == null)
        {
            throw new ArgumentNullException("cipher");
        }

        // parse base64 string

        byte[] data = Convert.FromBase64String(cipher);

        // decrypt data

        byte[] decrypted = ProtectedData.Unprotect(data, null, Scope);

        SecureString ss = new SecureString();

        // parse characters one by one - doesn't change the fact that
        // we have them in memory however...

        int count = Encoding.Unicode.GetCharCount(decrypted);
        int bc = decrypted.Length / count;
        for (int i = 0; i < count; i++)
        {
            ss.AppendChar(Encoding.Unicode.GetChars(decrypted, i * bc, bc)[0]);
        }

        // mark as read-only

        ss.MakeReadOnly();
        return ss;
    }

    /// <summary>
    /// Wraps a managed string into a <see cref="SecureString"/> 
    /// instance.
    /// </summary>
    /// <param name="value">A string or char sequence that 
    /// should be encapsulated.</param>
    /// <returns>A <see cref="SecureString"/> that encapsulates the
    /// submitted value.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/>
    /// is a null reference.</exception>
    public static SecureString ToSecureString(this IEnumerable<char> value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }

        var secured = new SecureString();

        var charArray = value.ToArray();
        for (int i = 0; i < charArray.Length; i++)
        {
            secured.AppendChar(charArray[i]);
        }

        secured.MakeReadOnly();
        return secured;
    }

    /// <summary>
    /// Unwraps the contents of a secured string and
    /// returns the contained value.
    /// </summary>
    /// <param name="value">The secure string.</param>
    /// <returns>The unwrapped string.</returns>
    /// <remarks>Be aware that the unwrapped managed string can be
    /// extracted from memory.</remarks>
    /// <exception cref="ArgumentNullException">If <paramref name="value"/>
    /// is a null reference.</exception>
    public static string Unwrap(this SecureString value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }

        IntPtr ptr = Marshal.SecureStringToCoTaskMemUnicode(value);
        try
        {
            return Marshal.PtrToStringUni(ptr);
        }
        finally
        {
            Marshal.ZeroFreeCoTaskMemUnicode(ptr);
        }
    }

    /// <summary>
    /// Checks whether a <see cref="SecureString"/> is either
    /// null or has a <see cref="SecureString.Length"/> of 0.
    /// </summary>
    /// <param name="value">The secure string to be inspected.</param>
    /// <returns>True if the string is either null or empty.</returns>
    public static bool IsNullOrEmpty(this SecureString value)
    {
        return value == null || value.Length == 0;
    }

    /// <summary>
    /// Performs <c>bytewise</c> comparison of two secure strings.
    /// </summary>
    /// <param name="first">The first secure string.</param>
    /// <param name="second">The second secure string.</param>
    /// <returns>True if the strings are equal.</returns>
    public static bool Matches(this SecureString first, SecureString second)
    {
        if (first == null && second == null)
        {
            return true;
        }

        if (first == null || second == null)
        {
            return false;
        }

        if (first.Length != second.Length)
        {
            return false;
        }

        if (first.Length == 0 && second.Length == 0)
        {
            return true;
        }

        IntPtr ptrA = Marshal.SecureStringToCoTaskMemUnicode(first);
        IntPtr ptrB = Marshal.SecureStringToCoTaskMemUnicode(second);
        try
        {
            // parse characters one by one, 
            // doesn't change the fact that we have them in memory however

            byte byteA = 1;
            byte byteB = 1;

            int index = 0;
            while (((char)byteA) != '\0' && ((char)byteB) != '\0')
            {
                byteA = Marshal.ReadByte(ptrA, index);
                byteB = Marshal.ReadByte(ptrB, index);

                if (byteA != byteB)
                { 
                    return false; 
                }

                index += 2;
            }

            return true;
        }
        finally
        {
            Marshal.ZeroFreeCoTaskMemUnicode(ptrA);
            Marshal.ZeroFreeCoTaskMemUnicode(ptrB);
        }
    }
  }
}

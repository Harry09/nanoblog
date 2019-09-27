using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Nanoblog.AppCore.Extensions
{
    public static class SecureStringExtensions
    {
        public static string Unsecure(this SecureString secureString)
        {
            IntPtr valuePtr = IntPtr.Zero;

            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}

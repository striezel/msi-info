/*
    This file is part of msi-info.
    Copyright (C) 2022, 2024  Dirk Stolle

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace msi_info
{
    /// <summary>
    /// Utility class that wraps calls to msi.dll functions to get MSI property data.
    /// </summary>
    internal static class MsiUtils
    {
        [DllImport("msi.dll", CharSet = CharSet.Unicode, PreserveSig = true, SetLastError = true, ExactSpelling = true)]
        private static extern uint MsiOpenPackageW(string szPackagePath, out IntPtr hProduct);

        [DllImport("msi.dll", CharSet = CharSet.Unicode, PreserveSig = true, SetLastError = true, ExactSpelling = true)]
        private static extern uint MsiCloseHandle(IntPtr hAny);
        
        [DllImport("msi.dll", CharSet = CharSet.Unicode, PreserveSig = true, SetLastError = true, ExactSpelling = true)]
        private static extern uint MsiGetPropertyW(IntPtr hAny, string name, StringBuilder buffer, ref int bufferLength);

        /// <summary>
        /// Gets a property from an MSI file.
        /// </summary>
        /// <param name="msiPath">path to the MSI file</param>
        /// <param name="property">name of the property (case-sensitive)</param>
        /// <returns>Returns the value of the property in case of success.
        /// Returns null, if an error occurred.</returns>
        private static string GetProperty(string msiPath, string property)
        {
            IntPtr MsiHandle = IntPtr.Zero;
            try
            {
                var res = MsiOpenPackageW(msiPath, out MsiHandle);
                if (res != 0)
                {
                    return null;
                }
                int length = 512;
                var buffer = new StringBuilder(length);
                res = MsiGetPropertyW(MsiHandle, property, buffer, ref length);
                return buffer.ToString();
            }
            finally
            {
                if (MsiHandle != IntPtr.Zero)
                {
                    _ = MsiCloseHandle(MsiHandle);
                }
            }
        }

        /// <summary>
        /// Gets the product code of the MSI.
        /// The product code is an GUID in the form of {12345678-9ABC-DEF0-1234-56789ABCDEF0}.
        /// </summary>
        /// <param name="msiPath">path to the MSI file</param>
        /// <returns>Returns the product code in case of success.
        /// Returns null, if an error occurred.</returns>
        public static string GetProductCode(string msiPath)
        {
            return GetProperty(msiPath, "ProductCode");
        }

        /// <summary>
        /// Gets the product name of the MSI.
        /// </summary>
        /// <param name="msiPath">path to the MSI file</param>
        /// <returns>Returns the product name in case of success.
        /// Returns null, if an error occurred.</returns>
        public static string GetProductName(string msiPath)
        {
            return GetProperty(msiPath, "ProductName");
        }

        /// <summary>
        /// Gets the product version of the MSI.
        /// </summary>
        /// <param name="msiPath">path to the MSI file</param>
        /// <returns>Returns the product version in case of success.
        /// Returns null, if an error occurred.</returns>
        public static string GetProductVersion(string msiPath)
        {
            return GetProperty(msiPath, "ProductVersion");
        }
    }
}

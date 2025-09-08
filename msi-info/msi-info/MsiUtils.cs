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
    /// Holds values of MSI properties.
    /// </summary>
    internal class MsiProperties
    {
        /// <summary>
        /// value of ProductName property, usually the name of the software to install
        /// </summary>
        public string name;

        /// <summary>
        /// Contains value of ProductCode property, i.e. a GUID in the form
        /// of {12345678-9ABC-DEF0-1234-56789ABCDEF0}.
        /// </summary>
        public string code;

        /// <summary>
        /// value of ProductVersion property
        /// </summary>
        public string version;

        /// <summary>
        /// value of ProductLanguage property, a numerical language ID
        /// </summary>
        public string language;

        /// <summary>
        /// value of the Manufacturer property
        /// </summary>
        public string manufacturer;

        public MsiProperties()
        {
            name = null;
            code = null;
            version = null;
            language = null;
            manufacturer = null;
        }
    }


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

        const uint ERROR_MORE_DATA = 234;


        /// <summary>
        /// Gets some properties from an MSI file.
        /// </summary>
        /// <param name="msiPath">path to the MSI file</param>
        /// <returns>Returns the values of the property in case of success.
        /// Returns null, if an error occurred.</returns>
        /// <remarks>Note that some of the individual properties could be null,
        /// even if the function itself was successful.</remarks>
        public static MsiProperties GetProperties(string msiPath)
        {
            IntPtr MsiHandle = IntPtr.Zero;
            try
            {
                var res = MsiOpenPackageW(msiPath, out MsiHandle);
                if (res != 0)
                {
                    return null;
                }
                return new MsiProperties()
                {
                    name = GetProperty(MsiHandle, "ProductName"),
                    code = GetProperty(MsiHandle, "ProductCode"),
                    version = GetProperty(MsiHandle, "ProductVersion"),
                    language = GetProperty(MsiHandle, "ProductLanguage"),
                    manufacturer = GetProperty(MsiHandle, "Manufacturer")
                };
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
        /// Gets a property value from an MSI file.
        /// </summary>
        /// <param name="msiHandle">handle to an open MSI file, opened with the
        /// MsiOpenPackageW() function</param>
        /// <param name="property">name of the property (case-sensitive)</param>
        /// <returns>Returns the value of the property in case of success.
        /// Returns null, if an error occurred.</returns>
        private static string GetProperty(IntPtr msiHandle, string property)
        {
            int length = 512;
            var buffer = new StringBuilder(length);
            var res = MsiGetPropertyW(msiHandle, property, buffer, ref length);
            if (res == ERROR_MORE_DATA)
            {
                buffer = new StringBuilder(length);
                res = MsiGetPropertyW(msiHandle, property, buffer, ref length);
            }
            return res == 0 ? buffer.ToString() : null;
        }
    }
}

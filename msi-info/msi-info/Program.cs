/*
    This file is part of msi-info.
    Copyright (C) 2022, 2024, 2025  Dirk Stolle

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
using System.IO;
using System.Runtime.InteropServices;

namespace msi_info
{
    class Program
    {
        /// <summary>
        /// Shows the version of the program on standard output.
        /// </summary>
        static void ShowVersion()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var ver = asm.GetName().Version;
            Console.WriteLine(asm.GetName().Name + ", version " + ver.ToString(3)
                + ", running on " + RuntimeInformation.FrameworkDescription);
            Console.WriteLine();
            Console.WriteLine("Copyright (C) 2022 - 2025  Dirk Stolle");
            Console.WriteLine("License GPLv3+: GNU GPL version 3 or later <https://gnu.org/licenses/gpl.html>");
            Console.WriteLine("This is free software: you are free to change and redistribute it under the");
            Console.WriteLine("terms of the GNU General Public License version 3 or any later version.");
            Console.WriteLine("There is NO WARRANTY, to the extent permitted by law.");
        }

        /// <summary>
        /// Shows a basic help text on standard output.
        /// </summary>
        static void ShowHelp()
        {
            Console.WriteLine("msi-info.exe [options] MSI_PATH");
            Console.WriteLine();
            Console.WriteLine("options:");
            Console.WriteLine("  -? | --help     - Shows this help message.");
            Console.WriteLine("  -v | --version  - Shows the version of the program.");
            Console.WriteLine("  MSI_PATH        - Path to the MSI file for which the information is to be");
            Console.WriteLine("                    displayed.");
        }

        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Error: You have to specify a paramater"
                    + " for the program to run properly, usually the path to an"
                    + " MSI file!");
                Console.Error.WriteLine();
                ShowHelp();
                return 1;
            }

            if ((args[0] == "--version") || (args[0] == "-v"))
            {
                ShowVersion();
                return 0;
            }

            if ((args[0] == "--help") || (args[0] == "-?") || (args[0] == "/?"))
            {
                ShowHelp();
                return 0;
            }

            string msiPath = args[0];
            if (!File.Exists(msiPath))
            {
                Console.Error.WriteLine("Error: The file " + msiPath + " does not exist.");
                return 2;
            }

            var properties = MsiUtils.GetProperties(msiPath);
            if (properties == null)
            {
                Console.Error.WriteLine("Error: Could not get MSI properties from file!");
                return 3;
            }
            if (string.IsNullOrEmpty(properties.name))
            {
                Console.Error.WriteLine("Error: Could not get MSI property from file!");
                return 3;
            }
            if (string.IsNullOrEmpty(properties.code))
            {
                Console.Error.WriteLine("Error: Could not get MSI product code from file!");
                return 3;
            }
            if (string.IsNullOrEmpty(properties.version))
            {
                Console.WriteLine("Warning: Could not get MSI product version from file!");
            }
            if (string.IsNullOrEmpty(properties.language))
            {
                Console.WriteLine("Warning: Could not get MSI language from file!");
            }
            if (string.IsNullOrEmpty(properties.manufacturer))
            {
                Console.WriteLine("Warning: Could not get MSI manufacturer from file!");
            }

            Console.WriteLine("Product name:     " + properties.name);
            Console.WriteLine("Product code:     " + properties.code);
            Console.WriteLine("Product version:  " + (properties.version ?? "unknown"));
            Console.WriteLine("Product language: " + (properties.language ?? "unknown"));
            Console.WriteLine("Manufacturer:     " + (properties.manufacturer ?? "unknown"));
            
            return 0;
        }
    }
}

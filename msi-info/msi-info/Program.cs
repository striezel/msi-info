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
using System.IO;

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
            Console.WriteLine(asm.GetName().Name + ", version " + ver.ToString(3));
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

            var name = MsiUtils.GetProductName(msiPath);
            if (string.IsNullOrEmpty(name))
            {
                Console.Error.WriteLine("Error: Could not get MSI property from file!");
                return 3;
            }
            var code = MsiUtils.GetProductCode(msiPath);
            if (string.IsNullOrEmpty(code))
            {
                Console.Error.WriteLine("Error: Could not get MSI product code from file!");
                return 3;
            }

            Console.WriteLine("Product name: " + name);
            Console.WriteLine("Product code: " + code);
            return 0;
        }
    }
}

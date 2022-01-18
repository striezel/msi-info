/*
    This file is part of msi-info.
    Copyright (C) 2022  Dirk Stolle

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
        static int Main(string[] args)
        {
            Console.WriteLine("msi-info");

            if (args.Length == 0)
            {
                Console.Error.WriteLine("Error: You have to specify the path to an MSI file as first parameter!");
                return 1;
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

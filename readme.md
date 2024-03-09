# msi-info

`msi-info` is a command line application that can display the `ProductName` and
`ProductCode` properties of any given MSI package.

## Build status

[![GitHub CI](https://github.com/striezel/msi-info/workflows/Build%20with%20.NET%20on%20Ubuntu/badge.svg)](https://github.com/striezel/msi-info/actions)
[![GitHub CI](https://github.com/striezel/msi-info/workflows/MSBuild%20on%20Windows/badge.svg)](https://github.com/striezel/msi-info/actions)
[![GitLab pipeline status](https://gitlab.com/striezel/msi-info/badges/master/pipeline.svg)](https://gitlab.com/striezel/msi-info/-/pipelines)

## Prerequisites

To run the `msi-info` program you need the .NET 6 runtime.
The current .NET 6 runtime can be downloaded from
<https://dotnet.microsoft.com/en-us/download/dotnet/6.0/runtime>.

## Usage

Basic invocation via command line is as follows:

    msi-info.exe C:\User\ExAmple\Downloads\Path\To\your.msi

Complete list of command line options:

```
msi-info.exe [options] MSI_PATH

options:
  -? | --help     - Shows this help message.
  -v | --version  - Shows the version of the program.
  MSI_PATH        - Path to the MSI file for which the information is to be
                    displayed.
```

## Getting the source code and building the application

Get the source directly from GitHub by cloning the Git repository (e.g. in Git
Bash) and change to the directory after the repository is completely cloned:

    git clone https://github.com/striezel/msi-info.git msi-info
    cd msi-info

That's it, you should now have the current source code of msi-info on your
machine.

After that, open Visual Studio (2019 Community Edition or later recommended)
and just build the solution **msi-info/msi-info.sln** from the checked out
sources.

## Copyright and Licensing

Copyright 2022, 2024  Dirk Stolle

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

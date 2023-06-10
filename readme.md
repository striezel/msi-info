# msi-info

`msi-info` _(working title, name may change)_ is a command line application
that can display the `ProductName` and `ProductCode` properties of any given MSI
package.

## Build status

[![GitHub CI](https://github.com/striezel/msi-info/workflows/Build%20with%20.NET%20on%20Ubuntu/badge.svg)](https://github.com/striezel/msi-info/actions)
[![GitHub CI](https://github.com/striezel/msi-info/workflows/MSBuild%20on%20Windows/badge.svg)](https://github.com/striezel/msi-info/actions)
[![GitLab pipeline status](https://gitlab.com/striezel/msi-info/badges/master/pipeline.svg)](https://gitlab.com/striezel/msi-info/-/pipelines)

## Usage

Basic invocation via command line is as follows:

    msi-info.exe C:\\User\\ExAmple\\Downloads\\Path\\To\\your.msi

## Getting the source code and building the application

Get the source directly from GitHub by cloning the Git repository (e.g. in Git
Bash) and change to the directory after the repository is completely cloned:

    git clone https://github.com/striezel/msi-info.git msi-info
    cd msi-info

That's it, you should now have the current source code of msi-info on your
machine.

After that, open Visual Studio (2017 Community Edition or later recommended)
and just build the solution **msi-info/msi-info.sln** from the checked out
sources.

## Copyright and Licensing

Copyright 2022  Dirk Stolle

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

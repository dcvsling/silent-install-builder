﻿#region License
// Copyright 2017 OSIsoft, LLC
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// <http://www.apache.org/licenses/LICENSE-2.0>
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using PropertyChanged;

namespace SilentPackagesBuilder.Core
{
    [Serializable]
    [XmlType("Executable")]
    [ImplementPropertyChanged]
    public class Executable : BasePackage, IHasInstallFile
    {
        public Executable()
        {
            this.Type=PackageType.Executable;
        }

        public PackageFileInfo FileInfo { get; set; }

        public string Arguments { get; set; } = string.Empty;
    }
}

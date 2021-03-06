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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SilentPackagesBuilder.Core;
using SilentPackagesBuilderGUI;

namespace SilentPackagesBuilder
{
    public partial class frmNewPackage : Form
    {
        private InstallModel _model = null;
        PackageFileInfo _currentFile;

        public frmNewPackage()
        {
            InitializeComponent();
            ConfigureEvents();

        }

 

        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if(packageSearchControl1.DataGridView.SelectedRows.Count>0)
                _currentFile = (PackageFileInfo)this.packageSearchControl1.DataGridView.SelectedRows[0].DataBoundItem;
        }

        public frmNewPackage(InstallModel model)
        {
            _model = model;
            InitializeComponent();
            ConfigureEvents();
        }

        private void ConfigureEvents()
        {
            this.packageSearchControl1.DataGridView.SelectionChanged += DataGridView_SelectionChanged;
            this.packageSearchControl1.DataGridView.CellDoubleClick += packageSearchcontrol_DoubleClick;
        }

        private void packageSearchcontrol_DoubleClick(object sender, EventArgs e)
        {
            if (_currentFile != null)
                btnOk_Click(sender, e);
        }

        private void frmNewPackage_Load(object sender, EventArgs e)
        {

            var values = Enum.GetValues(typeof(PackageType)).Cast<PackageType>()
            .Where(act => act != PackageType.PowershellCodeBlock)
            .ToList();

            this.cmbPackagetypes.DataSource = values;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (_currentFile == null)
            {
                MessageBox.Show("Can't proceed because no file is selected in the grid.");
                return;
            }


            if (cmbPackagetypes.SelectedItem == null)
            {
                MessageBox.Show("Can't proceed because no package type is selected in the combo box.");
            }


            var packageType = (PackageType)cmbPackagetypes.SelectedItem;
            IInstallationStep newInstallationStep = null;
            switch (packageType)
            {
                case PackageType.Executable:
                    newInstallationStep = new Executable() {FileInfo = _currentFile};
                    break;
                case PackageType.PIDataArchive:
                    newInstallationStep = new PIDataArchive() { FileInfo = _currentFile };
                    break;
                
                case PackageType.OSIAutoExtractSetupPackage:
                    newInstallationStep = new OSIAutoExtractSetupPackage() { FileInfo = _currentFile };
                    break;

            }

            newInstallationStep.DisplayName = _currentFile.FileName;
            
            _model.Items.Add(newInstallationStep);

            Program.ShowPackageEditor(newInstallationStep);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

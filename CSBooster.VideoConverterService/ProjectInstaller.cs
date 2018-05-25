using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace _4screen.CSB.VideoConverterService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
﻿using System.Diagnostics;
using System.Reflection;
using System.Windows.Input;

namespace MissileSharp.Launcher.ViewModels
{
    public class AboutWindowViewModel : BaseViewModel
    {
        private FileVersionInfo info;

        public ICommand LinkCommand
        {
            get { return new RelayCommand(this.FollowLink); }
        }

        public AboutWindowViewModel()
        {
            this.info = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
        }

        public string VersionNumber
        {
            get { return this.info.ProductName + " " + this.info.FileVersion; }
        }

        public string CopyRight
        {
            get { return this.info.LegalCopyright; }
        }

        public string SiteUrl
        {
            get { return this.info.CompanyName + "#launcher"; }
        }

        public string LicenseUrl
        {
            get { return "https://bitbucket.org/christianspecht/missilesharp/raw/tip/License.txt"; }
        }

        public void FollowLink(object url)
        {
            Process.Start(new ProcessStartInfo(url.ToString()));
        }
    }
}

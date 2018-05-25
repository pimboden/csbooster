// ******************************************************************************
// Company:   4 screen AG, CH-6005 Lucerne, http://www.4screen.com
// System:    sieme.net
// ******************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using _4screen.CSB.Common;

namespace _4screen.CSB.WebUI.Admin
{
    public partial class ResourcesCompile : System.Web.UI.Page
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool LogonUser(string principal, string authority, string password, LogonSessionType logonType, LogonProvider logonProvider, out IntPtr token);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr handle);
        enum LogonSessionType : uint
        {
            Interactive = 2,
            Network,
            Batch,
            Service,
            NetworkCleartext = 8,
            NewCredentials
        }
        enum LogonProvider : uint
        {
            Default = 0,
            WinNT35,
            WinNT40,
            WinNT50
        }

        private Dictionary<string, List<FileInfo>> resourceCultures = new Dictionary<string, List<FileInfo>>();
        private string resourcesFolder;
        private string binariesFolder;
        private string resourcesNamespace = "_4screen.CSB.Localization";
        private string neutralLibraryName = "CSBooster.Localization.dll";
        private string satelliteLibraryName = "CSBooster.Localization.resources.dll";

        protected void Page_Load(object sender, EventArgs e)
        {
            //((MasterPages_SiteAdmin)this.Master).SetNavigationItem("ResourcesCompile");

            _4screen.CSB.Extensions.Business.TrackingManager.TrackEventPage(null, null, IsPostBack, LogSitePageType.SiteAdmin);

            resourcesFolder = string.Format("{0}App_Resources", WebRootPath.Instance.ToString());
            binariesFolder = string.Format("{0}bin", WebRootPath.Instance.ToString());
            string[] resxFiles = Directory.GetFiles(resourcesFolder, "*.resx", SearchOption.TopDirectoryOnly);

            // Group resources by culture
            foreach (string resxFile in resxFiles)
            {
                FileInfo resxFileInfo = new FileInfo(resxFile);
                string resourceName = resxFileInfo.Name.Substring(0, resxFileInfo.Name.LastIndexOf('.'));

                string culture = "neutral";
                Match cultureMatch = Regex.Match(resourceName, @".*\.(([a-z]{2}-[A-Z]{2})|([a-z]{2}))");
                if (cultureMatch.Success)
                {
                    culture = cultureMatch.Groups[1].Value;
                }
                if (!resourceCultures.ContainsKey(culture))
                    resourceCultures.Add(culture, new List<FileInfo>());
                resourceCultures[culture].Add(resxFileInfo);
            }

            // Show available resources
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            foreach (var resourceCulture in resourceCultures)
            {
                sb.AppendLine(string.Format("<tr><td colspan=\"2\" class=\"CSB_admin_title3\">Culture {0}</td></tr>", resourceCulture.Key));
                foreach (FileInfo resxFileInfo in resourceCulture.Value.OrderBy(x => x.LastWriteTime).Reverse())
                {
                    sb.AppendLine(string.Format("<tr><td style=\"padding-right:10px;\">{0}</td><td>{1}</td></tr>", resxFileInfo.Name, resxFileInfo.LastWriteTime));
                }
            }
            sb.AppendLine("</table>");
            this.PhResxFiles.Controls.Add(new LiteralControl(sb.ToString()));

            // Get service status
            IntPtr token = IntPtr.Zero;
            WindowsImpersonationContext impersonatedUser = null;

            try
            {
                bool result = LogonUser("", "", "", LogonSessionType.Service, LogonProvider.Default, out token);

                if (result)
                {
                    WindowsIdentity id = new WindowsIdentity(token);
                    impersonatedUser = id.Impersonate();

                    ServiceController service = new ServiceController("CSBooster DataAccess Background Service", "CSBMEDIA");

                    switch (service.Status)
                    {
                        case ServiceControllerStatus.Stopped:
                            LitServiceStatus.Text = "Data Access Service läuft nicht";
                            break;
                        case ServiceControllerStatus.Running:
                            LitServiceStatus.Text = "Data Access Service läuft";
                            break;
                        default:
                            LitServiceStatus.Text = "Data Access Service Status: " + service.Status;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.PnlMsg2.CssClass = "CSB_admin_error";
                LitMsg2.Text += ex.Message + Environment.NewLine;
            }
            finally
            {
                if (impersonatedUser != null)
                    impersonatedUser.Undo();
                if (token != IntPtr.Zero)
                    CloseHandle(token);
            }
        }

        protected void OnStartServiceClick(object sender, EventArgs e)
        {
            IntPtr token = IntPtr.Zero;
            WindowsImpersonationContext impersonatedUser = null;

            try
            {
                bool result = LogonUser("", "", "", LogonSessionType.Service, LogonProvider.Default, out token);

                if (result)
                {
                    WindowsIdentity id = new WindowsIdentity(token);
                    impersonatedUser = id.Impersonate();

                    ServiceController service = new ServiceController("CSBooster DataAccess Background Service", "CSBMEDIA");

                    if (service.Status == ServiceControllerStatus.Stopped)
                    {
                        service.Start();
                        LitMsg2.Text += "Service wurde gestartet!" + Environment.NewLine;
                    }
                    else
                    {
                        this.PnlMsg2.CssClass = "CSB_admin_error";
                        LitMsg2.Text += "Service ist bereits gestoppt!" + Environment.NewLine;
                    }
                }
            }
            catch (Exception ex)
            {
                this.PnlMsg2.CssClass = "CSB_admin_error";
                LitMsg2.Text += ex.Message + Environment.NewLine;
            }
            finally
            {
                if (impersonatedUser != null)
                    impersonatedUser.Undo();
                if (token != IntPtr.Zero)
                    CloseHandle(token);
            }
        }

        protected void OnStopServiceClick(object sender, EventArgs e)
        {
            IntPtr token = IntPtr.Zero;
            WindowsImpersonationContext impersonatedUser = null;

            try
            {
                bool result = LogonUser("", "", "", LogonSessionType.Service, LogonProvider.Default, out token);

                if (result)
                {
                    WindowsIdentity id = new WindowsIdentity(token);
                    impersonatedUser = id.Impersonate();

                    ServiceController service = new ServiceController("CSBooster DataAccess Background Service", "CSBMEDIA");

                    if (service.Status == ServiceControllerStatus.Running)
                    {
                        service.Stop();
                        try
                        {
                            service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 10));
                        }
                        catch (Exception)
                        {
                            throw new Exception("Service konnte nicht gestoppet werden!");
                        }

                        // Copy dll to data access service folder
                        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DataAccessServiceLocation"]))
                        {
                            foreach (var resourceCulture in resourceCultures)
                            {
                                string cultureDirectory = CheckBinaryFolders(binariesFolder, resourceCulture.Key);

                                string dataAccessServiceFolder = CheckBinaryFolders(ConfigurationManager.AppSettings["DataAccessServiceLocation"], resourceCulture.Key);
                                if (resourceCulture.Key == "neutral")
                                    File.Copy(string.Format("{0}\\{1}", cultureDirectory, neutralLibraryName), string.Format("{0}\\{1}", dataAccessServiceFolder, neutralLibraryName), true);
                                else
                                    File.Copy(string.Format("{0}\\{1}", cultureDirectory, satelliteLibraryName), string.Format("{0}\\{1}", dataAccessServiceFolder, satelliteLibraryName), true);
                            }
                        }

                        LitMsg2.Text += "Service wurde gestoppet und aktualisiert!" + Environment.NewLine;
                    }
                    else
                    {
                        this.PnlMsg2.CssClass = "CSB_admin_error";
                        LitMsg2.Text += "Service läuft bereits!" + Environment.NewLine;
                    }
                }
            }
            catch (Exception ex)
            {
                this.PnlMsg2.CssClass = "CSB_admin_error";
                LitMsg2.Text += ex.Message + Environment.NewLine;
            }
            finally
            {
                if (impersonatedUser != null)
                    impersonatedUser.Undo();
                if (token != IntPtr.Zero)
                    CloseHandle(token);
            }
        }

        protected void OnCompileClick(object sender, EventArgs e)
        {
            bool compilationError = false;

            // Convert resx to resources and generate resource classes
            foreach (var resourceCulture in resourceCultures)
            {
                foreach (FileInfo resxFileInfo in resourceCulture.Value)
                {
                    ProcessStartInfo proc = new ProcessStartInfo();
                    proc.FileName = ConfigurationManager.AppSettings["ResGenLocation"];
                    proc.Arguments = string.Format("\"{0}\\{1}.resx\" \"{0}\\{2}.{1}.resources\" /str:c#,{2},{1},{1}.cs /publicClass", resourcesFolder, resxFileInfo.GetNameWithoutExtension(), resourcesNamespace);
                    proc.RedirectStandardInput = false;
                    proc.RedirectStandardOutput = true;
                    proc.CreateNoWindow = true;
                    proc.UseShellExecute = false;
                    proc.WorkingDirectory = resourcesFolder;
                    Process p = Process.Start(proc);
                    string output = p.StandardOutput.ReadToEnd();
                    p.WaitForExit();
                    this.TxtOutput.Text += output + Environment.NewLine;
                    if (output.Contains("error"))
                        compilationError = true;
                }
            }

            // Compile resource classes and embed resources
            foreach (var resourceCulture in resourceCultures)
            {
                string resourcesEmbedResString = "";
                string resourcesSourcesString = "";
                foreach (FileInfo resxFileInfo in resourceCulture.Value)
                {
                    resourcesEmbedResString += string.Format("/res:\"{0}\\{1}.{2}.resources\" ", resourcesFolder, resourcesNamespace, resxFileInfo.GetNameWithoutExtension());
                    resourcesSourcesString += string.Format("\"{0}\\{1}.cs\" ", resourcesFolder, resxFileInfo.GetNameWithoutExtension());
                }

                string cultureDirectory = CheckBinaryFolders(binariesFolder, resourceCulture.Key);

                ProcessStartInfo proc = new ProcessStartInfo();
                proc.FileName = ConfigurationManager.AppSettings["CSCLocation"];
                if (resourceCulture.Key == "neutral")
                    proc.Arguments = string.Format("{0} /out:\"{1}\\{2}\" /target:library {3}", resourcesEmbedResString, cultureDirectory, neutralLibraryName, resourcesSourcesString);
                else
                    proc.Arguments = string.Format("{0} /out:\"{1}\\{2}\" /target:library", resourcesEmbedResString, cultureDirectory, satelliteLibraryName);
                proc.RedirectStandardInput = false;
                proc.RedirectStandardOutput = true;
                proc.CreateNoWindow = true;
                proc.UseShellExecute = false;
                proc.WorkingDirectory = resourcesFolder;
                Process p = Process.Start(proc);
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                this.TxtOutput.Text += output + Environment.NewLine;
                if (output.Contains("error"))
                    compilationError = true;
            }

            if (compilationError)
            {
                this.PnlMsg.CssClass = "CSB_admin_error";
                this.LitMsg.Text = "Bei der Kompilierung ist ein Fehler aufgetreten!";
            }
            else
            {
                this.LitMsg.Text = "Die Kompilierung wurde erfolgreich durchgeführt!";
            }
        }

        private string CheckBinaryFolders(string cultureDirectory, string culture)
        {
            if (culture != "neutral")
            {
                cultureDirectory += "\\" + culture;
                if (!Directory.Exists(cultureDirectory))
                    Directory.CreateDirectory(cultureDirectory);
            }
            return cultureDirectory;
        }
    }
}

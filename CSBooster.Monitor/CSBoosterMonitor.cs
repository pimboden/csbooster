//*****************************************************************************************
//	Company:		4 screen AG, CH-6005 Lucerne, http://www.4screen.ch
//	Project:		CSBooster.Monitor
//
//  History
//  ---------------------------------------------------------------------------------------
//  2007.07.24  1.0.0.0  AW  Initial release
//  2007.11.06  1.0.0.3  AW  Deserialization changed because of a new monitor
//  2007.11.20  1.0.0.4  AW  Deserialization changed because of a new monitor
//*****************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Web.Services.Protocols;
using System.Reflection;
using System.Diagnostics;

namespace _4screen.CSB.Monitor
{
   public partial class CSBoosterMonitor : DevExpress.XtraEditors.XtraForm
   {
      private static ServiceForm serviceForm;
      private static AuthenticationHeader authHeader;
      private static List<Type> serializableTypeList;
      private CSBoosterMonitorProperties properties;
      private string clientVersion;
      private string serverVersion;

      public CSBoosterMonitor(CSBoosterMonitorProperties properties)
      {
         InitializeComponent();
         this.mainMenuBar.LinksPersistInfo.Clear();

         this.properties = properties;
      }

      public static ServiceForm GetServiceForm()
      {
         if (serviceForm == null)
            serviceForm = new ServiceForm();
         return serviceForm;
      }

      public static AuthenticationHeader GetAuthHeader()
      {
         if (authHeader == null)
            authHeader = new AuthenticationHeader();
         return authHeader;
      }

      public static void AddSerializableType(Type type)
      {
         if (serializableTypeList == null)
            serializableTypeList = new List<Type>();

         if (!serializableTypeList.Contains(type))
            serializableTypeList.Add(type);
      }

      public bool Login(string username, string password)
      {
         try
         {
            GetAuthHeader().Username = username;
            GetAuthHeader().Password = password;
            Authorization authorization = ServiceHelper.GetService(this.properties.DatabaseServiceUrl).Authenticate();
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string[] serviceVersion = authorization.Version.Split(new char[] { '.' });
            this.clientVersion = version.ToString();
            this.serverVersion = authorization.Version;
            if (version.Major != int.Parse(serviceVersion[0]) || version.Minor != int.Parse(serviceVersion[1]))
            {
               MessageBox.Show("Ihre CSBooster Monitor Version wird nicht mehr unterstützt!\nCSBooster Monitor: " + version.ToString() + "\nCSBooster Monitor Service: " + authorization.Version, "Version nicht unterstützt", MessageBoxButtons.OK, MessageBoxIcon.Error);
               return false;
            }
            if (authorization.IsAuthorized)
            {
               Initialize(authorization.Roles);
               return true;
            }
            else
            {
               return false;
            }
         }
         catch (Exception)
         {
            MessageBox.Show("Verbindung mit dem Webservice [" + this.properties.DatabaseServiceUrl + "] konnte nicht erstellt werden!", "Server nicht gefunden", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
         return false;
      }

      private void Initialize(Role[] roles)
      {
         this.addMonitorSubMenu.ClearLinks();
         List<Role> roleList = new List<Role>(roles);
         if (roleList.Contains(Role.StatisticsViewer))
         {
            this.addMonitorSubMenu.AddItem(this.newStatisticsMonitorItem);
         }
         if (roleList.Contains(Role.UserManager))
         {
            this.addMonitorSubMenu.AddItem(this.newUserMonitorItem);
            this.addMonitorSubMenu.AddItem(this.newContentEditorItem);
            this.addMonitorSubMenu.AddItem(this.newFeaturedContentEditorItem);
            this.addMonitorSubMenu.AddItem(this.newAdCampaignEditorItem);
         }

         this.mainMenuBar.ClearLinks();
         this.mainMenuBar.AddItem(this.fileMenu);
         this.mainMenuBar.AddItem(this.viewMenu);
         this.mainMenuBar.AddItem(this.settingsMenu);
         this.mainMenuBar.AddItem(this.helpMenu);
         this.settingsMenu.ClearLinks();
         this.settingsMenu.AddItem(this.monitorSettingsItem);
         this.settingsMenu.AddItem(this.loadSettingsItem);
         this.settingsMenu.AddItem(this.saveSettingsItem);

         // Add mdi form
         ServiceForm serviceForm = CSBoosterMonitor.GetServiceForm();
         serviceForm.MdiParent = this;
         serviceForm.Show();

         // Restore settings
         OnLoadSettingsItemClick(null, null);
      }

      private void OpenLoginForm()
      {
         this.mainMenuBar.ClearLinks();
         this.mainMenuBar.AddItem(this.fileMenu);
         this.mainMenuBar.AddItem(this.settingsMenu);
         this.mainMenuBar.AddItem(this.helpMenu);
         this.settingsMenu.ClearLinks();
         this.settingsMenu.AddItem(this.monitorSettingsItem);

         LoginForm loginForm = new LoginForm(this);
         loginForm.Show();
         loginForm.SetDesktopLocation(this.DesktopLocation.X + 30, this.DesktopLocation.Y + 70);
      }

      private void AddPanel(Control control, bool isDocked)
      {
         DevExpress.XtraBars.Docking.DockPanel dockPanel;
         if (isDocked)
            dockPanel = this.dockManager1.AddPanel(DevExpress.XtraBars.Docking.DockingStyle.Bottom);
         else
            dockPanel = this.dockManager1.AddPanel(new Point(200, 200));
         dockPanel.Text = control.Text;
         dockPanel.Size = new Size(0, this.Size.Height / 4);
         control.Dock = DockStyle.Fill;
         dockPanel.FloatSize = new Size(350, 350);
         dockPanel.Controls.Add(control);
      }

      ////////////////////////////////////////////////////////////////////////////////////////////
      // UI Event Handlers
      ////////////////////////////////////////////////////////////////////////////////////////////

      private void OnNewStatisticsMonitorItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         MonitorControlStatisticsProperties monitorControlStatisticsProperties = new MonitorControlStatisticsProperties();
         monitorControlStatisticsProperties.ServiceLocation = "Datenbank Web Service";
         monitorControlStatisticsProperties.ServiceName = "Youser - Email Provider";
         AddPanel(monitorControlStatisticsProperties.GetControl(), false);
      }

      private void OnNewUserMonitorItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         MonitorControlUsersProperties monitorControlUsersProperties = new MonitorControlUsersProperties();
         monitorControlUsersProperties.ServiceLocation = "Datenbank Web Service";
         AddPanel(monitorControlUsersProperties.GetControl(), false);
      }


      private void OnNewContentEditorItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         MonitorControlContentEditProperties monitorControlTableEditProperties = new MonitorControlContentEditProperties();
         monitorControlTableEditProperties.ServiceLocation = "Datenbank Web Service";
         AddPanel(monitorControlTableEditProperties.GetControl(), false);
      }

      private void OnNewFeaturedContentEditorItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         MonitorControlFeaturedContentEditProperties monitorControlFeaturedContentEditProperties = new MonitorControlFeaturedContentEditProperties();
         monitorControlFeaturedContentEditProperties.ServiceLocation = "Datenbank Web Service";
         AddPanel(monitorControlFeaturedContentEditProperties.GetControl(), false);
      }


      private void OnNewAdCampaignEditorItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         MonitorControlAdCampaignsProperties monitorControlAdCampaignsProperties = new MonitorControlAdCampaignsProperties();
         monitorControlAdCampaignsProperties.ServiceLocation = "Datenbank Web Service";
         AddPanel(monitorControlAdCampaignsProperties.GetControl(), false);
      }

      private void OnLoadSettingsItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         try
         {
            dockManager1.RestoreFromXml(Application.StartupPath + @"\layout.config");

            XmlSerializer serializer = new XmlSerializer(typeof(List<PersistentPanel>),
                                                         new Type[] { typeof(MonitorControlStatisticsProperties),
                                                                      typeof(MonitorControlUsersProperties),
                                                                      typeof(MonitorControlContentEditProperties),
                                                                      typeof(MonitorControlFeaturedContentEditProperties),
                                                                      typeof(MonitorControlAdCampaignsProperties) });
            StreamReader streamReader = new StreamReader(Application.StartupPath + @"\monitors.config", false);
            List<PersistentPanel> panels = (List<PersistentPanel>)serializer.Deserialize(streamReader);
            streamReader.Close();

            foreach (PersistentPanel panel in panels)
            {
               Control control = (Control)panel.MonitorControlProperties.GetControl();
               control.Dock = DockStyle.Fill;
               dockManager1.Panels[panel.PanelId].Controls[0].Controls.Clear();
               dockManager1.Panels[panel.PanelId].Controls.Add(control);
            }
         }
         catch (Exception ex)
         {
            MessageBox.Show(this, "Couldn't load settings: " + ex.Message, "Load error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            dockManager1.Clear();
         }
      }

      private void OnSaveSettingsItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         try
         {
            dockManager1.SaveToXml(Application.StartupPath + @"\layout.config");

            List<PersistentPanel> panels = new List<PersistentPanel>();
            foreach (DevExpress.XtraBars.Docking.DockPanel dockPanel in dockManager1.Panels)
            {
               if (dockPanel.Controls[0].Controls[0] is MonitorControl)
               {
                  MonitorControl monitorControl = (MonitorControl)dockPanel.Controls[0].Controls[0];
                  PersistentPanel persistentPanel = new PersistentPanel(dockPanel.ID, monitorControl.GetProperties());
                  panels.Add(persistentPanel);
               }
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<PersistentPanel>), (Type[])serializableTypeList.ToArray());
            serializer.Serialize(new StreamWriter(Application.StartupPath + @"\monitors.config", false), panels);

            serializer = new XmlSerializer(typeof(CSBoosterMonitorProperties));
            serializer.Serialize(new StreamWriter(Application.StartupPath + @"\settings.config", false), this.properties);
         }
         catch (Exception ex)
         {
            MessageBox.Show(this, "Couldn't save settings: " + ex.Message, "Save error", MessageBoxButtons.OK, MessageBoxIcon.Error);
         }
      }

      private void OnMonitorSettingsItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         PropertyForm propertyForm = PropertyForm.GetInstance();
         propertyForm.SetSelectedObject(this.properties);
         propertyForm.Show();
      }

      private void OnShown(object sender, EventArgs e)
      {
         this.OpenLoginForm();
      }

      private void OnClosedPanel(object sender, DevExpress.XtraBars.Docking.DockPanelEventArgs e)
      {
         PropertyForm.GetInstance().Close();
         this.dockManager1.RemovePanel(e.Panel);
      }

      private void OnExitItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         this.Close();
      }

      private void OnAboutItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
      {
         AboutForm aboutForm = new AboutForm();
         aboutForm.SetInfo("CSBooster Monitor Version:          " + this.clientVersion + "\r\n" +
                           "CSBooster Monitor Service Version:  " + this.serverVersion + "\r\n\r\n" +
                           "Copyright © 2007 4 screen AG\r\n\r\n" +
                           "Programm Icons:\r\nhttp://www.famfamfam.com/lab/icons/silk/");
         aboutForm.ShowDialog();
      }
   }
}

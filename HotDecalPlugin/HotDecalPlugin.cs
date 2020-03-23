using Decal.Adapter;
using Decal.Adapter.Wrappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using VirindiViewService;
using VirindiViewService.Controls;
using VirindiViewService.XMLParsers;

namespace HotDecalPluginTemplate {
    public class HotDecalPlugin {
        private HudView view;
        private ViewProperties properties;
        private ControlGroup controls;

        private HudButton EchoButton;
        private HudTextBox EchoText;

        /// <summary>
        /// Assembly directory (contains both loader and plugin dlls)
        /// </summary>
        public static string PluginAssemblyDirectory {
            get {
                string fullPath = System.Reflection.Assembly.GetAssembly(typeof(HotDecalPlugin)).Location;
                return System.IO.Path.GetDirectoryName(fullPath);
            }
        }

        #region Startup / Shutdown
        /// <summary>
        /// Called onces when the plugin is loaded
        /// </summary>
        public void Startup(NetServiceHost host, CoreManager core, string accountName, string characterName, string serverName) {
            WriteLog($"Plugin.Startup");

            CreateView();
        }

        /// <summary>
        /// Called when the plugin is shutting down
        /// </summary>
        public void Shutdown() {
            WriteLog($"Plugin.Shutdown");
            view.Visible = false;
            view.Dispose();
        }
        #endregion

        #region VVS Views
        private void CreateView() {
            new Decal3XMLParser().ParseFromResource("HotDecalPluginLoaderTemplate.Views.MainView.xml", out properties, out controls);

            view = new VirindiViewService.HudView(properties, controls);

            EchoButton = (HudButton)view["EchoButton"];
            EchoText = (HudTextBox)view["EchoText"];

            EchoButton.Hit += EchoButton_Hit;
        }

        private void EchoButton_Hit(object sender, EventArgs e) {
            CoreManager.Current.Actions.AddChatText($"You hit the button! Text was: {EchoText.Text}", 5);
        }
        #endregion

        #region Logging
        public static void WriteLog(string message) {
            try {
                using (StreamWriter writer = new StreamWriter(System.IO.Path.Combine(PluginAssemblyDirectory, "exceptions.txt"), true)) {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
            catch { }
        }
        #endregion
    }
}

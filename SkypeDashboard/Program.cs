using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using System.Data.Entity;
using System.Text;
using DevExpress.DashboardCommon.Server;

namespace SkypeDashboard {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {        

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);        
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            //DashboardSession.ForceUseNewEngineOnly = true;
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
            Application.Run(new ViewerForm());
        }
    }
}
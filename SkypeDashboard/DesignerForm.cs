using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;

namespace SkypeDashboard {
    public partial class DesignerForm : DevExpress.XtraEditors.XtraForm {
        
        public DesignerForm() {
            InitializeComponent();
        }

        void DesignerForm_Load(object sender, EventArgs e) {
            dashboardDesigner1.LoadDashboard(Properties.Settings.Default.Dashboard_file);
        }

        void dashboardDesigner1_ConfigureDataConnection(object sender, DashboardConfigureDataConnectionEventArgs e) {
            SQLiteConnectionParameters connectionParameters = e.ConnectionParameters as SQLiteConnectionParameters;
            if(connectionParameters != null)                
                connectionParameters.FileName = string.Format("{0}\\skype\\{1}\\main.db",Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Properties.Settings.Default.Skype_name);
        }

    }
}

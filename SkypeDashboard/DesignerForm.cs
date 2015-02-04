using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ConnectionParameters;

namespace SkypeDashboard {
    public partial class DesignerForm : DevExpress.XtraEditors.XtraForm {
        const string skypeName = "Write_you_skype_login_here";
        public DesignerForm() {
            InitializeComponent();
        }

        void DesignerForm_Load(object sender, EventArgs e) {
            dashboardDesigner1.LoadDashboard("..\\..\\Dashboard\\Skype.xml");
        }

        void dashboardDesigner1_ConfigureDataConnection(object sender, ConfigureDataConnectionEventArgs e) {
            SQLiteConnectionParameters connectionParameters = e.ConnectionParameters as SQLiteConnectionParameters;
            if(connectionParameters != null)                
                connectionParameters.FileName = string.Format("{0}\\skype\\{1}\\main.db",Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), skypeName);
        }

    }
}

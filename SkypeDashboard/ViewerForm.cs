using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.DashboardCommon;
using DevExpress.Data;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.XtraGrid;

namespace SkypeDashboard {
    public partial class ViewerForm : Form {        
        static IList<long> chatIds;

        public static IList<long> ChatIds { get { return chatIds; } }

        public ViewerForm() {
            InitializeComponent();
        }

        void barButtonItemDesigner_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            using(DesignerForm designer = new DesignerForm()) {
                designer.ShowDialog();
                ReloadDashboard();
            }
        }
        void ReloadDashboard() {
            dashboardViewer1.LoadDashboard(Properties.Settings.Default.Dashboard_file);
        }
        void dashboardViewer1_ConfigureDataConnection(object sender, DashboardConfigureDataConnectionEventArgs e) {
            SQLiteConnectionParameters connectionParameters = e.ConnectionParameters as SQLiteConnectionParameters;
            if(connectionParameters != null)
                connectionParameters.FileName = string.Format("{0}\\skype\\{1}\\main.db", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Properties.Settings.Default.Skype_name);
        }

        void ViewerForm_Load(object sender, EventArgs e) {
            ReloadDashboard();
        }
     

        void dashboardViewer1_CustomParameters(object sender, CustomParametersEventArgs e) {
            IParameter param = e.Parameters.Where(p => p.Name == "Chat").FirstOrDefault();
            if(param != null) {
                IEnumerable chats = param.Value as IEnumerable;
                ViewerForm.chatIds = new List<long>();
                foreach(object value in chats) {
                    try {
                        chatIds.Add(Convert.ToInt64(value));
                    } catch {

                    }


                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkypeDashboard {
    public partial class DesignerForm : DevExpress.XtraEditors.XtraForm {
        public DesignerForm() {
            InitializeComponent();
        }

        void DesignerForm_Load(object sender, EventArgs e) {
            dashboardDesigner1.LoadDashboard("..\\..\\Dashboard\\Skype.xml");
        }

    }
}

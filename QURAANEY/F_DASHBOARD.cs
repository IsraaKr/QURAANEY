using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QURAANEY.SOURA
{
    public partial class F_DASHBOARD : F_INHERATENZ
    {
        public F_DASHBOARD()
        {
            InitializeComponent();
        }

        private void F_DASHBOARD_Load(object sender, EventArgs e)
        {
            view_inheretanz_butomes(false, false, false, false, false, false, true);
            dashboardViewer1.DashboardSource = @"G:\برنامج قرآني\QURAANEY\QURAANEY\Resources\dash_quranney.xml";
        }
    }
}

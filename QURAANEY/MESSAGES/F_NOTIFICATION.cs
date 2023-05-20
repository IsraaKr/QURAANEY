using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QURAANEY.MESSAGES
{
    public partial class F_NOTIFICATION : Form
    {
        public F_NOTIFICATION()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private void lbl_note_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace QURAANEY.SETTING
{
    public partial class F_CONN_SETTING : F_INHERATENZ
    {
        private readonly bool first_Start =false;

        public F_CONN_SETTING()
        {
            InitializeComponent();
            get_server_name();
         
        }
        public F_CONN_SETTING( bool first_start)
        {
            InitializeComponent();
            get_server_name();
            first_Start = first_start;
        }
        public override void save()
        {
            var server = txt_server.Text;
            var database = "MY_QURAAN";
            var time = numericUpDown1.Value;
            var user_name = txt_user_name.Text;
            var pass = txt_pass_word.Text;
            if (rd_local.Checked)
            {
                set_local_conn(server,database) ;
            }
            else if (rd_net.Checked)
            {
                set_net_conn(server, database,time,user_name,pass);
            }
            base.save();

        }

        private void set_net_conn(string server, string database, decimal time, string user_name, string pass)
        {
            throw new NotImplementedException();
        }

        private void set_local_conn(string server, string database)
        {
            var conn_string = @"Data Source = " + server + "; Initial Catalog = " + database + "; Integrated Security = true; ";
            Properties.Settings.Default.conn_string = conn_string;
            Properties.Settings.Default.Save();
            MessageBox.Show(""+conn_string);
            
        }

        public string get_server_name()
        {
            string server_name = "";
            var registaryviewarray = new[] { RegistryView.Registry32, RegistryView.Registry64 };
            foreach (var registryview in registaryviewarray)
            {
                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryview))
                using (var key = hklm.OpenSubKey(@"software\microsoft\microsoft sql server"))
                {
                    var instances = (string[])key?.GetValue("InstalledInstances");
                    txt_user_name.Text = server_name;
                    if (instances != null)
                        foreach (var element in instances)
                            if (element == "MSSQLSERVER")
                                server_name = System.Environment.MachineName;
                            else
                                server_name = System.Environment.MachineName + @"\" + element;
                    txt_server.Text = server_name;
                }
            }
            return server_name;

        }

        private void rd_local_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_local.Checked)
            {
                txt_pass_word.Enabled = false;
                txt_user_name.Enabled = false;
                numericUpDown1.Enabled = false;
            }
        }

        private void rd_net_CheckedChanged(object sender, EventArgs e)
        {
            if (rd_net.Checked)
            {
                txt_pass_word.Enabled = true;
                txt_user_name.Enabled = true;
                numericUpDown1.Enabled = true;
            }
        }
    }
}

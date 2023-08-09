using QURAANEY.USERES;
using QURAANEY.SETTING;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QURAANEY
{
    public partial class F_START : Form
    {
        public F_START()
        {
            InitializeComponent();
            create_db();
        }
        string server_nam = "";
        string db_nam = "MY_QURAAN";
        string sql;
        DataTable dt;

        //انشاء قاعدة البيانات
        private void create_db()
        {  // أول استدعاء من اجل انشاء قاعدة البيانات و الجداول
            try//جلب اسم السيرفر و  الاتصال بالسيرفر
            {
                server_nam = c_db.get_server_name();
                //  MessageBox.Show("تم جلب اسم السيرفر : " + server_nam);
            }
            catch (Exception)
            {
                //  MessageBox.Show("Error in ServerName part");
            }
            c_db.server_connection(server_nam);
      
            //     MessageBox.Show ("تم الاتصال بالسيرف " + server_nam);

            // ******************************************
            string sql = "select name from sys.databases"; //تجلب اسماء قواعد البيانات التي عندي
            DataTable dt = c_db.select(sql);

            try//إنشاء قاعدة  البيانات و الاتصال بها
            {
                c_db.create_DB(db_nam);
                //  MessageBox.Show("تم إنشاء قاعدة البيانات : " + db_nam);
            }
            catch (Exception)
            {
                //  MessageBox.Show("Error in data base part");
            }
            c_db.db_conection(server_nam, db_nam);
            //  MessageBox.Show ("تم الاتصال بقاعدة البيانات " + db_nam);
           
        }

        private void F_START_Load(object sender, EventArgs e)
        {
           // check_conn();
        }

        private void check_conn()
        {
            try
            {
                //var chec=  c_db.db_conection(server_nam, db_nam ,"");
                var chec=  true;

                if (chec == true)
                {
                    lbl_state.Text = "جاري الاتصال بقاعدة البيانات";
                    dt = c_db.select(@"select * from T_USERS ");
                    if (dt.Rows.Count > 0)
                    {
                        F_LOGIN f = new F_LOGIN();                     
                        f.Show();
                        this.Hide();

                    }
                    else
                    {
                        F_ADD_USER f = new F_ADD_USER(true);
                        f.Show();
                        this.Hide();
                    }
                }
                else if (chec == false)
                {
                    var res = MessageBox.Show("خطاء في الاتصال بقاعدة البيانات !!! اختر نعم لضبط نص الاتصال أو لا للخروج من البرنامج", "تأكيد",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        F_CONN_SETTING f = new F_CONN_SETTING( true);
                        f.Show();
                    }
                    else 
                    {
                        Application.Exit();
                    }
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(""+ex);
            }
        }
    }
}

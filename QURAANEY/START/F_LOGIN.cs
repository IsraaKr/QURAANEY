using QURAANEY.USERES;
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
    public partial class F_LOGIN : Form
    {
        public F_LOGIN()
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

            //************************************************
            //try//إنشاء الجداول
            //{
            //    //c_db.Create_Tables();
           
            //     MessageBox.Show("تم إنشاء كل الجداول  ");
            //}
            //catch (Exception)
            //{
            //     MessageBox.Show("Error in tables part"); 
            //}

        }
       
        private void btn_login_Click(object sender, EventArgs e)
        {
            if (txt_username.Text == string.Empty)
            {
                txt_username.ErrorText = "الرجاء اختيار اسم المستخدم";
                txt_username.Focus();
                return;
            }
            if (txt_password.Text == string.Empty)
            {
                txt_password.ErrorText = "الرجاء إدخال كلمة المرور";
                txt_password.Focus();
                return;
            }
            sql = @"SELECT     user_name, pass_word
            FROM         T_USERS
             WHERE     (user_name ='" + txt_username.Text + "') AND" +
             " (pass_word = '" + txt_password.Text + "')";
    
            dt = c_db.select(sql);
            if (dt.Rows.Count > 0)
            {
                F_MAIN f = new F_MAIN();
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show(" يرجى التأكد من اسم المستخدم أو كلمة المرور", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txt_username.Focus();
                return;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lkp_username_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                txt_password.Focus();
            }
        }

        private void txt_password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btn_login.PerformClick();
            }
        }

        private void btn_new_user_Click(object sender, EventArgs e)
        {
            if (txt_username.Text =="admin" && txt_password.Text=="admin" )
            {
                F_ADD_USER f = new F_ADD_USER(false);
                f.Show();
                
            }
            else
                MessageBox.Show(" الرجاء تسجيل الدخول كمدير", "info", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txt_password.Properties.PasswordChar = (txt_password.Properties.PasswordChar == '*') ? '\0' : '*';

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
    
}

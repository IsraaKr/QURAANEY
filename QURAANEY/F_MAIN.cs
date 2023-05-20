using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Reflection;
using DevExpress.XtraEditors;
using QURAANEY.SOURA;

namespace QURAANEY
{
    public partial class F_MAIN : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public F_MAIN()
        {
            InitializeComponent();
            create_db();
            F_SOURA_GRID f = new F_SOURA_GRID();
            nav(f,pan_nav);
        }
        string server_nam = "";
        string db_nam = "MY_QURAAN";
      


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
            //    c_db.Create_Tables();
            //    //  MessageBox.Show("تم إنشاء كل الجداول  ");
            //}
            //catch (Exception)
            //{
            //    //  MessageBox.Show("Error in tables part"); 
            //}

        }
        //نستدعيه عند كل فتحة فورم جديد 
        public void nav(Form f, PanelControl p)
        {
            f.TopLevel = false;
            f.Size = p.Size;
            f.Dock = DockStyle.Fill;
            p.Controls.Clear();
            p.Controls.Add(f);
            f.Show();
        }
        //فتح الفورم عن طريق اسمه باستعمال الاسمبلي
        public  void open_form_byname(string name)
        {  //الاسمبلي الذي نحنا نعمل فيه .الانواع .الاأول
            var ins = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == name);
            if (ins != null)
            {   //انشاء انستانس من التايب و ارجاعه على شكل فورم
                var frm = Activator.CreateInstance(ins) as Form;
                if (Application.OpenForms[frm.Name] != null)//التأكد إذا الفورم كان مفتوح
                {
                    //frm = Application.OpenForms[frm.Name];
                    nav(frm , pan_nav);
                }
                else
                {
                    // frm.Show();
                    nav(frm, pan_nav);
                }
                frm.BringToFront();
            }
        }
        //حدث يتم تطبيقه عند الضغط على أي المنت في الاكورديون كونترول
       private void accordionControl1_ElementClick_1(object sender, DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        {
            //نضع التاغ من الديزايننر نوع سترينغ و القيمة اسم الفورم الذي اريد فتحه
            var tag = e.Element.Tag as string;
            if (tag != string.Empty && tag != null)
            {
                open_form_byname(tag);
            }
        }

        private void ribbon_ItemClick(object sender, ItemClickEventArgs e)
        {
            //نضع التاغ من الديزايننر نوع سترينغ و القيمة اسم الفورم الذي اريد فتحه
            var tag = e.Item.Tag as string;
            if (tag != string.Empty && tag != null)
            {
                open_form_byname(tag);
            }
        }

 
    }
}
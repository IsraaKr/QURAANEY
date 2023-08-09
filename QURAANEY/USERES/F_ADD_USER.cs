using QURAANEY.MESSAGES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QURAANEY.USERES
{
    public partial class F_ADD_USER : F_INHERATENZ
    {
        public F_ADD_USER(bool first_time)
        {
            InitializeComponent();
            load_data("");
            view_inheretanz_butomes();
        }
        public F_ADD_USER()
        {
            InitializeComponent();
            load_data("");
            view_inheretanz_butomes();
        }
        string sqll;
        DataTable dt;
        int done;
        private string maxid;
        private void view_inheretanz_butomes()
        {
            btn_clear.Visible = true;
            btn_delete.Visible = true;
            btn_exite.Visible = true;
            btn_new.Visible = true;
            btn_print.Visible = true;
            btn_save.Visible = true;
            btn_show.Visible = false;

        }
        public override void print()
        {
            C_MASTER.print_header("المستخدمين  ", gc);
            base.print();
        }
        public override void neew()
        {
            clear(this.Controls);
            de_date.EditValue = DateTime.Today.ToShortDateString();
            set_auto_id_person();
            base.neew();
        }
        public override void load_data(string status_mess)
        {
            de_date.EditValue = DateTime.Today.ToShortDateString();
            set_auto_id_person();
            string sql = @"SELECT dbo.T_USERS.id AS التسلسل, dbo.T_USERS.name AS الاسم, dbo.T_USERS.user_name AS [اسم المستخدم], dbo.T_USERS.pass_word AS [كلمة المرور],
                         dbo.T_USERS.is_active AS [فعال ], dbo.T_USERS.in_date AS [تاريخ الإضافة], dbo.T_USERS_TYPES.name AS [نوع المستخدم]
                          FROM            dbo.T_USERS INNER JOIN
                          dbo.T_USERS_TYPES ON dbo.T_USERS.user_type_id = dbo.T_USERS_TYPES.id";
            dt = c_db.select(sql);
            gc.DataSource = dt;
          //  gv.Columns["التسلسل"].Visible = false;

            dt = c_db.select(@"SELECT        id AS التسلسل, name AS [نوع المستخدم]
                                 FROM            dbo.T_USERS_TYPES");
            lkp_user_type.lkp_iniatalize_data(dt, "نوع المستخدم", "التسلسل");
            lkp_user_type.Properties.PopulateColumns();
           // lkp_user_type.Properties.Columns[1].Visible = false;              
            base.load_data(status_mess);
        }
        //عمل اي دي تلقائي للاشخاص
        private void set_auto_id_person()
        {
            maxid = c_db.max("SELECT    id  FROM     dbo.T_USERS");
            int x = 0;
            if (maxid == null || maxid == "")
                x = 0;
            else
                x = int.Parse(maxid);
            x++;
            txt_id.Text = x.ToString();
        }
        public override void save()
        {
            if (! vallidate_data() )
                return;
            if (txt_pass_word.Text == txt_confirm.Text)
            {
                sqll = @"SELECT    id  FROM     dbo.T_USERS
                      where id=" + int.Parse(txt_id.Text) + "";
                dt = c_db.select(sqll);
                if (dt.Rows.Count <= 0)
                    add_new_persone();
                else
                    update_persone();
                base.save();
                clear(this.Controls);
            }
            else
                txt_confirm.ErrorText = "الكلمتان غير متطابقاتان";     
        
        }
        public override bool vallidate_data()
        {
            int number_of_errores = 0;
            number_of_errores += lkp_user_type.is_editevalue_valid() ? 0 : 1;
            number_of_errores += txt_name.is_text_valid() ? 0 : 1;
            number_of_errores += txt_user_name.is_text_valid() ? 0 : 1;
            number_of_errores += txt_pass_word.is_text_valid() ? 0 : 1;
            return (number_of_errores == 0);
        }
        public override void delete()
        {
            sqll = @"DELETE FROM dbo.T_USERS
                       WHERE        (id = "+ int.Parse(txt_id.Text)+")";
            done = c_db.insert_upadte_delete(sqll);
           
            if (done != 0)
                C_MESSAGE_COLLECTION.show_delete_note();
            base.delete();
        }

        private void update_persone()
        {
            //تعديل الأشخاص
            sqll = @"UPDATE       dbo.T_USERS
                        SET     name =N'" + txt_name.Text + "'," +
                        " is_active =N'" + che_is_active.Checked + "'," +
                        " user_name =N'" + txt_user_name.Text + "'," +
                        " pass_word =N'" + txt_pass_word.Text + "'," +
                        " in_date = N'"+ de_date.DateTime.ToShortDateString() +"' ," +
                        " user_type_id =" + Convert.ToInt32(lkp_user_type.EditValue) + "  " +
                        " WHERE        (id = " + int.Parse(txt_id.Text) + ")";
            done = c_db.insert_upadte_delete(sqll);

            C_MESSAGE_COLLECTION.show_update_note();
        }
        private void add_new_persone()
        {
            //ادخال لجدول الأشخاص
            sqll = @"INSERT INTO dbo.T_USERS
                         (name, is_active, user_name, pass_word,in_date, user_type_id)
                     VALUES        (N'" + txt_name.Text + "'," +
                     "N'" + che_is_active.Checked + "'," +
               "N'" + txt_user_name.Text + "'," +
               "N'" + txt_pass_word.Text + "'," +
               " N'" + de_date.DateTime.ToShortDateString() + "' ," +
               " " + Convert.ToInt32(lkp_user_type.EditValue) + ")";
                  done = c_db.insert_upadte_delete(sqll);

            C_MESSAGE_COLLECTION.show_add_note();
        }

        private void btn_permisions_Click(object sender, EventArgs e)
        {
            F_PERMISIONS f = new F_PERMISIONS();
            f.Show();
        }

        private void gv_DoubleClick(object sender, EventArgs e)
        {
            txt_id.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[0]).ToString();
            txt_name.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[1]).ToString();
            txt_user_name.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[2]).ToString();
            txt_pass_word.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[3]).ToString();
            che_is_active.Checked = Convert.ToBoolean(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[4]));
            de_date.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[5]).ToString();
            lkp_user_type.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[6]).ToString();

        }

        private void checkButton1_CheckedChanged(object sender, EventArgs e)
        {
            txt_pass_word.Properties.PasswordChar = (txt_pass_word.Properties.PasswordChar == '*') ? '\0' : '*';

        }

        private void checkButton2_CheckedChanged(object sender, EventArgs e)
        {
            txt_confirm.Properties.PasswordChar = (txt_confirm.Properties.PasswordChar == '*') ? '\0' : '*';

        }
    }


}

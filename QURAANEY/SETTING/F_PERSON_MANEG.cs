using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QURAANEY.DB;
using QURAANEY.MESSAGES;

namespace QURAANEY.SETTING
{
    public partial class F_PERSON_MANEG : F_INHERATENZ
    {
        public F_PERSON_MANEG()
        {
            InitializeComponent();
            load_data("");
        }
        public F_PERSON_MANEG(string name)
        {
            pers_name = name;
            InitializeComponent();
            load_data("");
        }

        string sqll;
        string pers_name = "";
        int pers_id = 0;
        DataTable dt;
        int done;
        private string maxid;
        int state_change = 0;
        int state_id = 0;
        int rate_change = 0;
        int type_change = 0;
        int id_to_delet = 0;
        public static bool is_double_click = false;


        private void F_PERSON_MANEG_Load(object sender, EventArgs e)
        {
            load_data("");

            neew();
            txt_name.Text = pers_name;
        }
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
        private void set_date_edite()
        {
            dtp_in_date.Text = DateTime.Today.ToShortDateString();
            dtp_rate_change.Text = DateTime.Today.ToShortDateString();
            dtp_state_change.Text = DateTime.Today.ToShortDateString();
            dtp_type_change.Text = DateTime.Today.ToShortDateString();

        }


        #region توابع الوراثة
        public override void load_data(string status_mess)
        {

            set_date_edite();

            sqll = @"SELECT    T_PERSONE.id AS التسلسل , T_PERSONE.name AS الاسم, T_PERSONE.phone AS الهاتف, T_PERSONE.adress AS العنوان, T_PERSONE.email AS الايميل, T_PERSONE.studey AS الدراسة, 
                      T_PERSONE.woke AS العمل, T_PERSONE.in_date AS [تاريخ الالتحاق], T_PERSONE.is_active AS فعال, T_PERSONE.inviting_pers AS الداعي, T_PERS_TYPE.name AS الأدوار, 
                      T_PERS_TYPE_CHANGE.change_date AS [تاريخ الدور], T_PERS_STATE.name AS الحالة, T_PERS_STATE_CHANGE.change_date AS [تاريخ الحالة], 
                      T_PERS_STATE_CHANGE.pers_id_deside AS [مغير الحالة], T_PERS_RATE_KEEP.name AS [معدل الحفظ], T_PERS_RATE_KEEP_CHANGE.change_date AS [تاريخ المعدل]
FROM        T_PERSONE left JOIN
                      T_PERS_RATE_KEEP_CHANGE ON T_PERSONE.id = T_PERS_RATE_KEEP_CHANGE.pers_id left JOIN
                      T_PERS_RATE_KEEP ON T_PERS_RATE_KEEP_CHANGE.rate_id = T_PERS_RATE_KEEP.id left JOIN
                      T_PERS_STATE_CHANGE ON T_PERSONE.id = T_PERS_STATE_CHANGE.pers_id left JOIN
                      T_PERS_STATE ON T_PERS_STATE_CHANGE.state_id = T_PERS_STATE.id left JOIN
                      T_PERS_TYPE_CHANGE ON T_PERSONE.id = T_PERS_TYPE_CHANGE.pers_id left JOIN
                      T_PERS_TYPE ON T_PERS_TYPE_CHANGE.type_id = T_PERS_TYPE.id  ";
            dt = c_db.select(sqll);
            gc.DataSource = dt;

            dt = c_db.select(@"select id , name  from T_PERS_STATE");
            lkp_state.lkp_iniatalize_data(dt, "name", "id");

            dt = c_db.select(@"select id , name  from T_PERS_TYPE");
            chbl_type.chbl_iniatalize_data(dt, "name", "id");

            dt = c_db.select(@"select id , name  from T_PERS_RATE_KEEP");
            lkp_keep_rate.lkp_iniatalize_data(dt, "name", "id");

            string sql = @"select id , name  from T_PERSONE ";
            dt = c_db.select(sql);
            lkp_pers_state_change.lkp_iniatalize_data(dt, "name", "id");
            lkp_inviting_pers.lkp_iniatalize_data(dt, "name", "id");

            //القيم الافتراضية
            dt = c_db.select(@"  SELECT name, value, value_id FROM dbo.T_DEFULT_THWABET");
            lkp_state.EditValue = int.Parse(dt.Rows[0][2].ToString());

            int index = int.Parse(dt.Rows[1][2].ToString());
            chbl_type.SetItemChecked(index - 1, true);
            lkp_keep_rate.EditValue = int.Parse(dt.Rows[2][2].ToString());

            base.load_data("");
            view_inheretanz_butomes();

        }
        public override void neew()
        {
            clear(this.Controls);
            set_auto_id_person();
            set_date_edite();
            base.neew();
        }
        public override void clear(Control.ControlCollection s_controls)
        {
            base.clear(s_controls);
            set_auto_id_person();
        }
        public override void print()
        {
            C_MASTER.print_header("تقرير الأشخاص", gc);
            base.print();
        }
        public override void save()
        {
            if (vallidate_data())
            {
                dt = c_db.select(@"select id from T_PERSONE 
                      where id=" + int.Parse(txt_id.Text) + "");
                if (dt.Rows.Count <= 0)
                {
                    add_all();
                }
                else if (dt.Rows.Count > 0)
                {
                    update_all();
                }

                clear(this.Controls);
                is_double_click = false;

            }
        }
        public override void delete()
        {
            int delete_mess = 0;
            if (is_double_click == false)
            {
                MessageBox.Show("يجب اختيار سجل لحذفه", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (MessageBox.Show("هل انت متاكد انك تريد حذف السجل", "تأكيد",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    delete_mess += delete_type();
                    delete_mess += delete_state();
                    delete_mess += delete_rate();
                    delete_mess += delete_pers();
                    if (delete_mess == 4)
                    {
                        load_data("d");
                        delete_mess = 0;
                    }
                }
                else
                    return;
            }
            catch (Exception)
            {
                DialogResult res = MessageBox.Show("هل تريد حذف المعلومات بشكل نهائي  ",
                    "لايمكن حذف السجل المحدد بسبب ارتباطه بأماكن أخرى !!!!",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {


                    delete_mess += delete_pers();
                    if (delete_mess == 5)
                    {
                        load_data("d");
                        delete_mess = 0;
                    }
                }
                else if (res == DialogResult.No)
                {
                    DialogResult res2 = MessageBox.Show("هل تريد اختيار غير فعال لهذا الشخص   ",
                    " غير فعال",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res2 == DialogResult.Yes)
                    {
                        c_db.insert_upadte_delete(@"UPDATE       dbo.T_PERSONE
                          SET   is_active =N'" + false + "'," +
                         " WHERE        (id = " + int.Parse(txt_id.Text) + ")");
                    }
                    else
                        return;
                }
            }
            base.delete();
            is_double_click = false;
        }
        public override bool vallidate_data()
        {
            int number_of_errores = 0;

            number_of_errores += lkp_state.is_editevalue_valid() ? 0 : 1;
            number_of_errores += lkp_inviting_pers.is_editevalue_valid() ? 0 : 1;
            number_of_errores += lkp_pers_state_change.is_editevalue_valid() ? 0 : 1;
            number_of_errores += lkp_keep_rate.is_editevalue_valid() ? 0 : 1;
            number_of_errores += txt_name.is_text_valid() ? 0 : 1;
            number_of_errores += txt_phone.is_text_valid() ? 0 : 1;

            return (number_of_errores == 0);

        }
        #endregion
        private void set_auto_id_person()
        {
            maxid = c_db.max("SELECT   T_PERSONE.id from T_PERSONE");
            int x = 0;
            if (maxid == null || maxid == "")
                x = 0;
            else
                x = int.Parse(maxid);
            x++;
            txt_id.Text = x.ToString();
        }

        #region deleate توابع الحذف
        private int delete_pers()
        {
            try
            {
                sqll = @"DELETE FROM dbo.T_PERSONE
                    WHERE        (id = " + int.Parse(txt_id.Text) + ")";
                done = c_db.insert_upadte_delete(sqll);
                return 1;
            }
            catch (Exception)
            {
                //DialogResult res = MessageBox.Show("هل تريد حذف المعلومات بشكل نهائي  ",
                //     "لايمكن حذف السجل المحدد بسبب ارتباطه بأماكن أخرى !!!!",
                //  MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                //if (res == DialogResult.Yes)
                //{
                //    delete_mess += delete_type();
                //    delete_mess += delete_state();
                //    delete_mess += delete_rate();
                //    delete_mess += delete_keep();
                //    delete_mess += delete_pers();
                //    if (delete_mess == 5)
                //    {
                //        load_data("d");
                //        delete_mess = 0;
                //    }
                //}
                //else if (res == DialogResult.No)
                //{
                //    DialogResult res2 = MessageBox.Show("هل تريد اختيار غير فعال لهذا الشخص   ",
                //    " غير فعال",
                //MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                //    if (res2 == DialogResult.Yes)
                //    {
                //        c_db.insert_upadte_delete(@"UPDATE       dbo.T_PERSONE
                //          SET   is_active =N'" + false + "'," +
                //         " WHERE        (id = " + int.Parse(txt_id.Text) + ")");
                //    }
                //    else
                //        return;
                return 0;
            }
        
    
        }

        private int delete_rate()
        {
            try
            {
                sqll = @"DELETE FROM dbo.T_PERS_RATE_KEEP_CHANGE
                       WHERE        (pers_id = " + int.Parse(txt_id.Text) + ")";
                done = c_db.insert_upadte_delete(sqll);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                return 0;
            }

        }

        private int delete_state()
        {
            try
            {
                sqll = @"DELETE FROM dbo.T_PERS_STATE_CHANGE
                     WHERE        (pers_id = " + int.Parse(txt_id.Text) + ")";
                done = c_db.insert_upadte_delete(sqll);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                return 0;
            }

        }

        private int delete_type()
        {
            try
            {
                sqll = @"DELETE FROM dbo.T_PERS_TYPE_CHANGE
                       WHERE        (pers_id = " + int.Parse(txt_id.Text) + ")";
                done = c_db.insert_upadte_delete(sqll);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                return 0;
            }

        }
        private int delete_keep()
        {
            try
            {
                c_db.insert_upadte_delete(@"   DELETE FROM dbo.T_SOURA_KEEP
                                  WHERE(pers_hafez_id  = " + int.Parse(txt_id.Text) + " )");
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                return 0;
            }

        }
    
        #endregion

        #region update توابع التعديل
        private void update_all()
        {
            update_pers();
            insert_type();
            insert_state();
            insert_keep_rate();
            //if (type_change == 1)           
            //    delete_type();
            //    insert_type();
            //if (state_change == 1)
            //    insert_state();
            //if (rate_change == 1)
            //    insert_keep_rate();
            C_MESSAGE_COLLECTION.show_update_note();
        }
        private void update_pers()
        {
            try
            {
                //تعديل الأشخاص
                sqll = @"UPDATE       dbo.T_PERSONE
                          SET          
             name =N'" + txt_name.Text + "' , " +
                 " phone =N'" + txt_phone.Text + "'," +
                 " adress =N'" + txt_adress.Text + "', " +
                 " email =N'" + txt_email.Text + "'," +
                 "studey =N'" + txt_studey.Text + "'," +
                 " woke =N'" + txt_work.Text + "'," +
                 "in_date =N'" + dtp_in_date.Text + "'," +
                 "is_active =N'" + ch_is_active.Checked + "'," +
                 " inviting_pers =N'" + lkp_inviting_pers.Text + "'  " +
                 " WHERE        (id = " + int.Parse(txt_id.Text) + ")";

                done = c_db.insert_upadte_delete(sqll);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "");
            }

        }
        private void update_keep_rate()
        {
            try
            {
                //تعديل معدل الحفظ 
                sqll = @" UPDATE dbo.T_PERS_RATE_KEEP_CHANGE
            SET                pers_id =" + int.Parse(txt_id.Text) + "," +
                " rate_id = " + Convert.ToInt32(lkp_keep_rate.EditValue) + "," +
                " change_date = N'" + dtp_rate_change.Text + "' " +
                " WHERE   (pers_id = " + int.Parse(txt_id.Text) + ")";
                done = c_db.insert_upadte_delete(sqll);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }

        }
        private void update_state()
        {
            try
            {
                if (state_change == 1)
                {
                    state_id = Convert.ToInt32(lkp_state.EditValue);
                    //ادخال لجدول تغير الحالات
                    sqll = @"INSERT INTO dbo.T_PERS_STATE_CHANGE
                         (state_id, pers_id, change_date,pers_id_deside)
                             VALUES        (" + state_id + "," +
                                     "" + int.Parse(txt_id.Text) + "," +
                                     "N'" + dtp_state_change.Text+ "'," +
                                     "N'" + lkp_inviting_pers.Text + "')";
                    done = c_db.insert_upadte_delete(sqll);
                    state_change = 0;
                    state_id = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }

        }
    
           // //تعديل تغير الحاللات
           // sqll = @"UPDATE       dbo.T_PERS_STATE_CHANGE
           //SET                state_id=" + Convert.ToInt32(lkp_state.EditValue) + "," +
           //" pers_id = " + int.Parse(txt_id.Text) + "," +
           //" change_date = N'" + de_state_date.DateTime.ToShortDateString() + "' ," +
           //" pers_id_deside = N'" + lkp_pers_state_change.Text + "' " +
           //" WHERE        (pers_id = " + int.Parse(txt_id.Text) + ")";

           // done = c_db.insert_upadte_delete(sqll);
        
        private void update_type()
        {
            try
            {
                //تعديل تغير الأدوار
                sqll = @"UPDATE       dbo.T_PERS_TYPE_CHANGE
            SET               
                pers_id = " + int.Parse(txt_id.Text) + "," +
                   " type_id = " + 1 + "," +
                   " change_date = '" + dtp_type_change.Text + "' " +
                    " WHERE        ( pers_id = " + int.Parse(txt_id.Text) + ")";

                done = c_db.insert_upadte_delete(sqll);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }
            type_change = 0;
        }

        #endregion

        #region insert توابع الإدخال

        private void add_all()
        {
                int num_done = 0;
            num_done += insert_pers();
            num_done +=  insert_state();
            num_done += insert_type();
            num_done += insert_keep_rate();
            if (num_done == 4)
            {
                load_data("i");
                 //   save();
            }
       

        }
        private int insert_pers()
        {
            try
            {
                //ادخال لجدول الأشخاص
                sqll = @"INSERT INTO dbo.T_PERSONE
                         (name, phone, adress, email, studey, woke, in_date, is_active,inviting_pers)
                         VALUES        (N'" + txt_name.Text + "'," +
                             "N'" + txt_phone.Text + "'," +
                             "N'" + txt_adress.Text + "'," +
                             "N'" + txt_email.Text + "'," +
                             "N'" + txt_studey.Text + "'," +
                             "N'" + txt_work.Text + "'," +
                             "N'" + dtp_in_date.Text + "'," +
                             "N'" + ch_is_active.Checked + "'," +
                             "N'" + lkp_inviting_pers.Text + "')";
                done = c_db.insert_upadte_delete(sqll);

               dt = c_db.select("SELECT   T_PERSONE.id from T_PERSONE where name = N'"+txt_name.Text+"'");
                pers_id =int.Parse( dt.Rows[0][0].ToString());
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                return 0;
            }
           
        }
        private int insert_type()
        {
            try
            {

                    List<int> checked_type_id = new List<int>();
            foreach (var item_check in chbl_type.CheckedIndices)
              {
                        checked_type_id.Add(Convert.ToInt32(chbl_type.GetItemValue(item_check)));
              }
                foreach (var item in checked_type_id)
                {
                    sqll = @"INSERT INTO dbo.T_PERS_TYPE_CHANGE
                             (pers_id, type_id, change_date)
                              VALUES        (" + pers_id + "," +
                                  "" + item + "," +
                                  "N'" + dtp_type_change.Text + "')";
                    done = c_db.insert_upadte_delete(sqll);                       
                }
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                return 0;
            }           
            type_change = 0;
        }
        private int  insert_state()
        {
            try
            {              
                state_id = Convert.ToInt32(lkp_state.EditValue);
                //ادخال لجدول تغير الحالات
                sqll = @"INSERT INTO dbo.T_PERS_STATE_CHANGE
                         (state_id, pers_id, change_date,pers_id_deside)
                             VALUES        (" + state_id + "," +
                                 " " + pers_id + "," +
                                 " N'" + dtp_state_change.Text + "'," +
                                 " N'" + lkp_inviting_pers.Text + "') ";
                done = c_db.insert_upadte_delete(sqll);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                return 0;
            }            
            state_change = 0;           
        }
        private int  insert_keep_rate()
        {
            try
            {
               int rate_id = Convert.ToInt32(lkp_keep_rate.EditValue);
                //ادخال لجدول تغير معدل الحفظ
                sqll = @"INSERT INTO dbo.T_PERS_RATE_KEEP_CHANGE
                         (pers_id, rate_id, change_date)
                             VALUES        (" + pers_id + "," +
                                 " " + rate_id + "," +
                                 " N'" + dtp_rate_change.Text + "')";
                done = c_db.insert_upadte_delete(sqll);
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                return 0;
            }
            
            rate_change = 0;
        }
        #endregion
        private void gv_DoubleClick(object sender, EventArgs e)
        {
            is_double_click = true;
         txt_id.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[0]).ToString();
         txt_name.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[1]).ToString();
         txt_phone.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[2]).ToString();
         txt_adress.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[3]).ToString();
         txt_email.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[4]).ToString();
         txt_studey.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[5]).ToString();
         txt_work.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[6]).ToString();    
         dtp_in_date.Text= gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[7]).ToString();
            ch_is_active.Checked =Convert.ToBoolean( gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[8]).ToString());
         lkp_inviting_pers.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[9]).ToString();
        // chbl_type.che = Convert.ToBoolean(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[10]);
        dtp_type_change.Text= gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[11]).ToString();
            lkp_state.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[12]).ToString();
           dtp_state_change.Text= gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[13]).ToString();
            lkp_pers_state_change.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[14]).ToString();
            lkp_keep_rate.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[15]).ToString();
           dtp_rate_change.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[16]).ToString();

        }
        private void lkp_state_Enter(object sender, EventArgs e)
        {
            state_change = 1;
        }
        private void lkp_keep_rate_Enter(object sender, EventArgs e)
        {
            rate_change = 1;
        }
        private void chbl_type_Enter(object sender, EventArgs e)
        {
            type_change = 1;
        }

        private void btn_show_state_Click(object sender, EventArgs e)
        {
            MessageBox.Show("");
            if (is_double_click)
            {
                F_STATE_PERS f = new F_STATE_PERS(int.Parse(txt_id.Text));
                f.WindowState = FormWindowState.Maximized;
                f.show();
            }
            is_double_click = false;

        }

        private void menu_delete_Click(object sender, EventArgs e)
        {
            txt_id.Text = id_to_delet.ToString();
            is_double_click = true;
                delete();
            is_double_click = false;
        }

        private void gv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                gv.SelectRow(e.RowHandle);
              id_to_delet = int.Parse(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[0]).ToString());

            }
        }
    }   
}


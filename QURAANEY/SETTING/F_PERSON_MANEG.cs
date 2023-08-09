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
using QURAANEY.CLASS_TABLES;

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
            view_inheretanz_butomes(true ,true, true, true, true, false, true);
            load_data("");
            neew();
            txt_name.Text = pers_name;
        }


        private void set_date_edite()
        {
            dtp_in_date.Text = DateTime.Today.ToShortDateString();
            dtp_rate_change.Text = DateTime.Today.ToShortDateString();
            dtp_state_change.Text = DateTime.Today.ToShortDateString();
        }
        #region توابع الوراثة
        public override void load_data(string status_mess)
        {
            clear(this.Controls);
            set_auto_id_person();
            set_date_edite();


            dt = C_PERSON_sql.get_all_pers();
            gc.DataSource = dt;

            dt = C_PERS_STATE_sql.get_all_state();
            lkp_state.lkp_iniatalize_data(dt, "name", "id");

            dt = C_PERS_TYPE_sql.get_all_types();
            chbl_type.chbl_iniatalize_data(dt, "name", "id");

            dt = C_PERS_RATE_KEEP_sql.get_all_rate_keep();
            lkp_keep_rate.lkp_iniatalize_data(dt, "name", "id");

            dt = C_PERSON_sql.get_pers_id_name(); 
            lkp_pers_state_change.lkp_iniatalize_data(dt, "name", "id");
            lkp_inviting_pers.lkp_iniatalize_data(dt, "name", "id");

            //القيم الافتراضية
            dt = C_DEFULTES_sql.get_all_defult();
            lkp_state.EditValue = int.Parse(dt.Rows[0][2].ToString());

            int index = int.Parse(dt.Rows[1][2].ToString());
            chbl_type.SetItemChecked(index - 1, true);
            lkp_keep_rate.EditValue = int.Parse(dt.Rows[2][2].ToString());

            base.load_data(status_mess);
       

        }

        private void all_states()
        {
            dt = C_PERS_STATE_sql.get_last_state_by_id_with_alias(pers_id);
            glkp_all_state.Properties.DataSource = dt;

        }
        private void all_rates()
        {
            dt = C_PERS_RATE_KEEP_sql.get_all_rates_by_id_from_view(pers_id);
            glkp_all_keep_rate.Properties.DataSource = dt;

        }
        public override void neew()
        {
            clear(this.Controls);
            set_auto_id_person();
            set_date_edite();
            dt = C_PERSON_sql.get_all_pers();
            if (dt.Rows.Count > 1)
            {
                lkp_inviting_pers.Text = "admin";
                lkp_pers_state_change.Text = "admin";
            }
            base.neew();
        }
        public override void show_rep()
        {

            base.set_data();
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
                dt = C_PERSON_sql.get_pers_id_name_by_id(int.Parse( txt_id.Text));

                if (dt.Rows.Count <= 0)
                {
                    add_all();
                }
                else if (dt.Rows.Count > 0)
                {
                    update_all();
                }
                //  load_data("i");
                neew();
                
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
            DialogResult res = MessageBox.Show("   هل انت متاكد انك تريد حذف المعلومات بشكل نهائي  من كافة الأماكن المرتبطة بهذا الاسم؟؟؟", "تأكيد",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            try
            {
                if (res == DialogResult.Yes)
                {
                    delete_mess += delete_type_true_false();
                    delete_mess += delete_state();
                    delete_mess += delete_rate();
                    delete_mess += delete_keep();
                    delete_mess += delete_fail();
                    delete_mess += delete_Nashat_keep();
                    delete_mess += delete_pers();
                    if (delete_mess == 7)
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
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex);
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

        // private int delete_type()
        // {
        //     try
        //     {
        //         sqll = @"DELETE FROM dbo.T_PERS_TYPE_CHANGE
        //                WHERE        (pers_id = " + int.Parse(txt_id.Text) + ")";
        //         done = c_db.insert_upadte_delete(sqll);
        //         return 1;
        //     }
        //     catch (Exception ex)
        //     {
        //         MessageBox.Show(ex + "");
        //         return 0;
        //     }

        // }
        private int delete_type_true_false()
        {
            try
            {
                sqll = @"DELETE FROM dbo.T_PERS_TYPES_TRUE_FALSE
                       WHERE        (pers_id = " + pers_id + ")";
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
                                  WHERE(pers_hafez_id  = " + pers_id + " )");
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                return 0;
            }

        }
        private int delete_fail()
        {
            try
            {
                c_db.insert_upadte_delete(@"   DELETE FROM dbo.T_PERS_FAIL
                                  WHERE(pers_id  = " + int.Parse(txt_id.Text) + " )");
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                return 0;
            }

        }
        private int delete_Nashat_keep()
        {
            try
            {
                c_db.insert_upadte_delete(@"   DELETE FROM dbo.T_NASHAT_KEEP
                              WHERE        (pers_id = "+pers_id+" ) ");
                               
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
            update_type_true_false();
            insert_state();
            insert_keep_rate();

            load_data("i");
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
                 " WHERE        (id = " + pers_id + ")";

                done = c_db.insert_upadte_delete(sqll);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "");
            }

        }
        private void update_type_true_false()
        {
            dt = c_db.select(@"select id , name  from T_PERS_TYPE");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var state = Convert.ToBoolean(chbl_type.GetItemCheckState(i));
                string x = dt.Rows[i][1].ToString();
                c_db.insert_upadte_delete(@" update T_PERS_TYPES_TRUE_FALSE set  " + x + " = '" + state + "'" +
                    " where pers_id= " + pers_id + " ");
            }
        }

        //private void update_keep_rate()
        //{
        //    try
        //    {
        //        //تعديل معدل الحفظ 
        //        sqll = @" UPDATE dbo.T_PERS_RATE_KEEP_CHANGE
        //    SET                pers_id =" + int.Parse(txt_id.Text) + "," +
        //        " rate_id = " + Convert.ToInt32(lkp_keep_rate.EditValue) + "," +
        //        " change_date = N'" + dtp_rate_change.Text + "' " +
        //        " WHERE   (pers_id = " + int.Parse(txt_id.Text) + ")";
        //        done = c_db.insert_upadte_delete(sqll);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex + "");
        //    }

        //}
        //private void update_state()
        //{
        //    try
        //    {
        //        if (state_change == 1)
        //        {
        //            state_id = Convert.ToInt32(lkp_state.EditValue);
        //            //ادخال لجدول تغير الحالات
        //            sqll = @"INSERT INTO dbo.T_PERS_STATE_CHANGE
        //                 (state_id, pers_id, change_date,pers_id_deside)
        //                     VALUES        (" + state_id + "," +
        //                             "" + int.Parse(txt_id.Text) + "," +
        //                             "N'" + dtp_state_change.Text+ "'," +
        //                             "N'" + lkp_inviting_pers.Text + "')";
        //            done = c_db.insert_upadte_delete(sqll);
        //            state_change = 0;
        //            state_id = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex + "");
        //    }

        //}

        //private void update_type()
        //{
        //    try
        //    {
        //        //تعديل تغير الأدوار
        //        sqll = @"UPDATE       dbo.T_PERS_TYPE_CHANGE
        //    SET               
        //        pers_id = " + int.Parse(txt_id.Text) + "," +
        //           " type_id = " + 1 + "," +
        //           " change_date = '" + dtp_type_change.Text + "' " +
        //            " WHERE        ( pers_id = " + int.Parse(txt_id.Text) + ")";

        //        done = c_db.insert_upadte_delete(sqll);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex + "");
        //    }
        //    type_change = 0;
        //}

        #endregion

        #region insert توابع الإدخال

        private void add_all()
        {
            state_change = 1;
            rate_change = 1;
            int num_done = 0;
            num_done += insert_pers();
            num_done += insert_state();
            num_done += insert_type_true_false();
            update_type_true_false();
            num_done += insert_keep_rate();

            load_data("i");

        }
        private int insert_pers()
        {
           
            try
            {
                if (int.Parse(txt_id.Text) ==1)
                {

                }
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

                dt = c_db.select("SELECT   T_PERSONE.id from T_PERSONE where name = N'" + txt_name.Text + "'");
                pers_id = int.Parse(dt.Rows[0][0].ToString());
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                return 0;
            }

        }
        //private int insert_type()
        //{
        //    try
        //    {
        //        List<int> checked_type_id = new List<int>();
        //        foreach (var item_check in chbl_type.CheckedIndices)
        //        {
        //            checked_type_id.Add(Convert.ToInt32(chbl_type.GetItemValue(item_check)));
        //        }
        //        foreach (var item in checked_type_id)
        //        {
        //            sqll = @"INSERT INTO dbo.T_PERS_TYPE_CHANGE
        //                         (pers_id, type_id, change_date)
        //                          VALUES        (" + pers_id + "," +
        //                          "" + item + "," +
        //                          "N'" + dtp_type_change.Text + "')";
        //            done = c_db.insert_upadte_delete(sqll);
        //        }
        //        return 1;





        //        return 1;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex + "");
        //        return 0;
        //    }           
        //    type_change = 0;
        //}      
        private int insert_type_true_false()
        {
            try
            {
                sqll = @"INSERT INTO  dbo.T_PERS_TYPES_TRUE_FALSE
                                 (pers_id )
                                  VALUES        (" + pers_id + " ) ";
                done = c_db.insert_upadte_delete(sqll);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        private int insert_state()
        {
            if (state_change != 0)
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
            }
            state_change = 0;
            return 0;
        }

        private int insert_keep_rate()
        {
            if (rate_change != 0)
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

            }

            rate_change = 0;
            return 0;
        }
        #endregion
        private void gv_DoubleClick(object sender, EventArgs e)
        {
            is_double_click = true;
            txt_id.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[0]).ToString();
            pers_id = int.Parse(txt_id.Text);
            txt_name.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[1]).ToString();
            txt_phone.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[2]).ToString();
            txt_adress.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[3]).ToString();
            txt_email.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[4]).ToString();
            txt_studey.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[5]).ToString();
            txt_work.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[6]).ToString();
            dtp_in_date.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[7]).ToString();
            ch_is_active.Checked = Convert.ToBoolean(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[8]).ToString());
            lkp_inviting_pers.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[9]).ToString();

            dt = c_db.select(@"  SELECT        pers_id, pers_name, ratr_id, rate_name, num, rate_in_days, change_date
                                  FROM            dbo.V_RATE_MAX_DATE
                            WHERE        (pers_id = " + pers_id + ")");
            lkp_keep_rate.Text = dt.Rows[0][3].ToString();
            dtp_rate_change.Text = dt.Rows[0][6].ToString();


            dt  = C_PERS_STATE_sql.get_last_state_by_id ( pers_id);
            lkp_state.Text = dt.Rows[0][3].ToString();
            dtp_state_change.Text = dt.Rows[0][4].ToString();
            lkp_pers_state_change.Text = dt.Rows[0][5].ToString();


            dt = C_PERS_TYPE_sql.get_all_types_true_false_by_id(pers_id);
            int count = chbl_type.ItemCount;
            int chl_index = 0;
            foreach (DataColumn item in dt.Columns)
            {
                int index = dt.Columns.IndexOf(item);
                if (index > 1)
                {
                    if (count >= 0)
                    {

                        Boolean state = Convert.ToBoolean(dt.Rows[0][index].ToString());
                        //  MessageBox.Show("" + dt.Columns.IndexOf(item) + state);
                        chbl_type.SetItemChecked(chl_index, state);
                        chl_index++;
                        count--;
                    }

                }

            }


            all_states();
            all_rates();



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
                f.show_rep();
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


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

namespace QURAANEY.SETTING
{
    public partial class F_THWABET : F_INHERATENZ
    {
        DataTable dt;
        string maxid;

        public F_THWABET()
        {
            InitializeComponent();
            view_inheretanz_butomes();
            load_data("");
        }
        private void view_inheretanz_butomes()
        {
            btn_clear.Visible = false;
            btn_delete.Visible = false;
            btn_exite.Visible = true;
            btn_new.Visible = false;
            btn_print.Visible = false;
            btn_save.Visible = false;
            btn_show.Visible = false;

        }
        private string set_auto_id_person(string qurey)
        {
            maxid = c_db.max(qurey);
            int x = 0;
            if (maxid == null || maxid == "")
                x = 0;
            else
                x = int.Parse(maxid);
            x++;
            return x.ToString();
        }
        public override void load_data(string status_mess)
        {
            load_state();
            load_user_type();
            load_rate();
            load_evaluation();
            load_types();
            load_keep_types();

            dt = c_db.select(@"  SELECT        name AS [اسم الافتراضي], value AS القيمة, value_id AS المعرف
FROM            dbo.T_DEFULT_THWABET");
            lkp_evaluation_def.EditValue = int.Parse(dt.Rows[4][2].ToString());
            //lkp_evaluation_def.Properties.Columns["id"].Visible = false;
            lkp_state_def.EditValue = int.Parse(dt.Rows[0][2].ToString());
            lkp_user_type_def.EditValue = int.Parse(dt.Rows[3][2].ToString());
            lkp_keep_rate_def.EditValue = int.Parse(dt.Rows[2][2].ToString());
            lkp_pers_type_def.EditValue = int.Parse(dt.Rows[1][2].ToString());
            lkp_keep_type_def.EditValue = int.Parse(dt.Rows[5][2].ToString());
            base.load_data(status_mess);
        }
        public override void save()
        {
            base.save();
        }
        public override void delete()
        {
            base.delete();
        }

        #region def_page
        private void btn_save_def_Click(object sender, EventArgs e)
        {
            c_db.insert_upadte_delete(@"UPDATE     dbo.T_DEFULT_THWABET
                      SET           value_id = " + lkp_state_def.EditValue + ", " +
                              " value  = N'" + lkp_state_def.Text + "' " +
                              " WHERE        (id = 1)");
            c_db.insert_upadte_delete(@"UPDATE     dbo.T_DEFULT_THWABET
                      SET           value_id = " + lkp_pers_type_def.EditValue + ", " +
                             " value  =N'" + lkp_pers_type_def.Text + "' " +
                             " WHERE        (id = 2)");
            c_db.insert_upadte_delete(@"UPDATE     dbo.T_DEFULT_THWABET
                      SET           value_id = " + lkp_keep_rate_def.EditValue + ", " +
                            " value  =N'" + lkp_keep_rate_def.Text + "' " +
                            " WHERE        (id = 3)");
            c_db.insert_upadte_delete(@"UPDATE     dbo.T_DEFULT_THWABET
                      SET           value_id = " + lkp_user_type_def.EditValue + ", " +
                            " value  =N'" + lkp_user_type_def.Text + "' " +
                            " WHERE        (id = 4)");
            c_db.insert_upadte_delete(@"UPDATE     dbo.T_DEFULT_THWABET
                      SET           value_id = " + lkp_evaluation_def.EditValue + ", " +
                          " value  =N'" + lkp_evaluation_def.Text + "' " +
                          " WHERE        (id = 5)");
            c_db.insert_upadte_delete(@"UPDATE     dbo.T_DEFULT_THWABET
                      SET           value_id = " + lkp_keep_type_def.EditValue + ", " +
                          " value  =N'" + lkp_keep_type_def.Text + "' " +
                          " WHERE        (id = 6)");
            load_data("i");

            save();

        }


        #endregion


        #region satet_page
        private void load_state()
        {
            clear_state();
            //الحالات 
            txt_id.Text = set_auto_id_person("SELECT    id  FROM     dbo.T_PERS_STATE");
            dt = c_db.select(@" SELECT        id AS المعرف, name AS [اسم الحالة]
FROM            dbo.T_PERS_STATE");
            lbc_state.lbc_iniatalize_data(dt, "اسم الحالة", "المعرف");
            lkp_state_def.lkp_iniatalize_data(dt, "اسم الحالة", "المعرف");

            if (dt.Rows.Count == 1)
            {
                lbc_state.Enabled = false;
                btn_delete_state.Enabled = false; c_db.insert_upadte_delete(@"UPDATE     dbo.T_DEFULT_THWABET
                                 SET           value_id = " + int.Parse(dt.Rows[0][0].ToString()) + ", " +
              " value  =N'" + dt.Rows[0][1].ToString() + "' " +
                              " WHERE        (id = 1)");
            }
            else
            {
                lbc_state.Enabled = true;
                btn_delete_state.Enabled = true;
            }


            //  lkp_state_def.Properties.Columns["id"].Visible = false;

            dt = c_db.select(@"SELECT dbo.T_PERSONE.name AS[اسم الحافظ], dbo.T_PERS_STATE.name AS الحالة
                         FROM            dbo.T_PERSONE LEFT OUTER JOIN
                         dbo.T_PERS_STATE_CHANGE ON dbo.T_PERSONE.id = dbo.T_PERS_STATE_CHANGE.pers_id LEFT OUTER JOIN
                         dbo.T_PERS_STATE ON dbo.T_PERS_STATE_CHANGE.state_id = dbo.T_PERS_STATE.id");
            gc.DataSource = dt;



        }
        private void btn_save_state_Click(object sender, EventArgs e)
        {
            if (txt_state.Text == string.Empty)
            {
                txt_state.ErrorText = "حقل مطلوب";
                return;
            }
            else
            {
                string sql = @" INSERT INTO dbo.T_PERS_STATE
                         (name)
                          VALUES(N'" + txt_state.Text + "')";
                int done = c_db.insert_upadte_delete(sql);
                load_data("i");
                save();
            }
            load_state();

        }
        private void btn_delete_state_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_state.Text == string.Empty)
                {
                    MessageBox.Show("الرجاء اختيار عنصر من القائمة لحذفه");
                    return;
                }
                else
                {
                    string sql = @" DELETE FROM dbo.T_PERS_STATE
                          WHERE(id = " + int.Parse(txt_id.Text) + ")";
                    int done = c_db.insert_upadte_delete(sql);
                    load_data("d");
                    delete();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("لايمكن حذف العنصر المختار بسبب استخدامه في أماكن أخرى");
            }

            load_state();
        }
        private void lbc_state_DoubleClick(object sender, EventArgs e)
        {
            txt_id.Text = lbc_state.GetItemValue(lbc_state.SelectedIndex).ToString();
            txt_state.Text = lbc_state.GetItemText(lbc_state.SelectedIndex).ToString();

        }
        private void btn_clear_state_Click(object sender, EventArgs e)
        {
            clear_state();
        }
        private void clear_state()
        {
            txt_id.Text = "";
            txt_state.Text = "";
        }
        #endregion

        #region user_type_page
        private void load_user_type()
        {
            clear_state();
            //أنواع المستخدمين
            txt_id_user.Text = set_auto_id_person("SELECT    id  FROM    dbo.T_USERS_TYPES");
            dt = c_db.select(@" SELECT        id AS المعرف, name AS [نوع المستخدم]
FROM            dbo.T_USERS_TYPES");
            lbc_user_type.lbc_iniatalize_data(dt, "نوع المستخدم", "المعرف");
            lkp_user_type_def.lkp_iniatalize_data(dt, "نوع المستخدم", "المعرف");

            if (dt.Rows.Count == 1)
            {
                lbc_user_type.Enabled = false;
                btn_delete_user.Enabled = false;
                c_db.insert_upadte_delete(@"UPDATE     dbo.T_DEFULT_THWABET
                                                     SET           value_id = " + int.Parse(dt.Rows[0][0].ToString()) + ", " +
              " value  =N'" + dt.Rows[0][1].ToString() + "' " +
                " WHERE        (id = 4)");

            }

            else
            {
                lbc_user_type.Enabled = true;
                btn_delete_user.Enabled = true;
            }



            dt = c_db.select(@" SELECT dbo.T_USERS.name AS[اسم المستخدم], dbo.T_USERS_TYPES.name AS[نوع المستخدم]
                            FROM dbo.T_USERS LEFT OUTER JOIN
                         dbo.T_USERS_TYPES ON dbo.T_USERS.user_type_id = dbo.T_USERS_TYPES.id");
            gc_user.DataSource = dt;



        }
        private void btn_save_user_Click(object sender, EventArgs e)
        {
            if (txt_user_type.Text == string.Empty)
            {
                txt_user_type.ErrorText = "حقل مطلوب";
                return;
            }
            else
            {
                string sql = @" INSERT INTO dbo.T_USERS_TYPES
                         (name)
                          VALUES(N'" + txt_user_type.Text + "')";
                int done = c_db.insert_upadte_delete(sql);
                load_data("i");
                save();
            }
            load_user_type();
        }
        private void btn_delete_user_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_user_type.Text == string.Empty)
                {
                    MessageBox.Show("الرجاء اختيار عنصر من القائمة لحذفه");
                    return;
                }
                else
                {
                    string sql = @" DELETE FROM dbo.T_USERS_TYPES
                                    WHERE      (id = " + int.Parse(txt_id_user.Text) + " )";
                    int done = c_db.insert_upadte_delete(sql);
                    load_data("d");

                    delete();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("لايمكن حذف العنصر المختار بسبب استخدامه في أماكن أخرى");
            }

            load_user_type();
        }
        private void btn_clear_user_Click(object sender, EventArgs e)
        {
            clear_user();
        }
        private void clear_user()
        {
            txt_id_user.Text = "";
            txt_user_type.Text = "";
        }
        private void lbc_user_type_DoubleClick(object sender, EventArgs e)
        {
            txt_id_user.Text = lbc_user_type.GetItemValue(lbc_user_type.SelectedIndex).ToString();
            txt_user_type.Text = lbc_user_type.GetItemText(lbc_user_type.SelectedIndex).ToString();
        }
        #endregion


        #region rate
        private void load_rate()
        {
            clear_rate();
            //معدل الحفظ
            txt_id_rate.Text = set_auto_id_person("SELECT    id  FROM    dbo.T_PERS_RATE_KEEP");
            dt = c_db.select(@"  SELECT        id AS المعرف, name AS [معدل الحفظ], num AS رقما, rate_in_days AS [عدد الأيام]
                      FROM            dbo.T_PERS_RATE_KEEP");
            lkp_keep_rate_def.lkp_iniatalize_data(dt, "معدل الحفظ", "المعرف");

            if (dt.Rows.Count == 1)
            {
                btn_del_rate.Enabled = false;
                c_db.insert_upadte_delete(@"UPDATE     dbo.T_DEFULT_THWABET
                       SET           value_id = " + int.Parse(dt.Rows[0][0].ToString()) + ", " +
              " value  =N'" + dt.Rows[0][1].ToString() + "' " +
                    " WHERE        (id = 3)");
            }
            else
                btn_del_rate.Enabled = true;

            gc_ratee.DataSource = dt;
            //gv_ratee.Columns["id"].Caption = "تسلسل";
            //gv_ratee.Columns["name"].Caption = "النص";
            //gv_ratee.Columns["num"].Caption = "عدد الصفحات";
            //gv_ratee.Columns["rate_in_days"].Caption = "عدد الأيام";



            dt = c_db.select(@"  SELECT dbo.T_PERSONE.name AS[اسم الحافظ], dbo.T_PERS_RATE_KEEP.name AS[معدل الحفظ], dbo.T_PERS_RATE_KEEP.num AS[المعدل رقما],
                dbo.T_PERS_RATE_KEEP.rate_in_days AS بالأيام
                         FROM            dbo.T_PERSONE LEFT OUTER JOIN
                         dbo.T_PERS_RATE_KEEP_CHANGE ON dbo.T_PERSONE.id = dbo.T_PERS_RATE_KEEP_CHANGE.pers_id LEFT OUTER JOIN
                         dbo.T_PERS_RATE_KEEP ON dbo.T_PERS_RATE_KEEP_CHANGE.rate_id = dbo.T_PERS_RATE_KEEP.id");
            gc_rate.DataSource = dt;


        }
        private void btn_save_rate_Click(object sender, EventArgs e)
        {
            if (txt_rate.Text == string.Empty || txt_rate_dayes.Text == string.Empty ||
                txt_rate_pages.Text == string.Empty)
            {
                txt_rate.ErrorText = "حقل مطلوب";
                txt_rate_dayes.ErrorText = "حقل مطلوب";
                txt_rate_pages.ErrorText = "حقل مطلوب";
                return;
            }
            else
            {
                string sql = @" INSERT INTO dbo.T_PERS_RATE_KEEP
                         (name, num, rate_in_days)
                          VALUES(N'" + txt_rate.Text + "'," +
                          "" + int.Parse(txt_rate_pages.Text) + "," +
                          "" + int.Parse(txt_rate_dayes.Text) + ")";
                int done = c_db.insert_upadte_delete(sql);
                load_data("i");
                save();
            }
            load_rate();
        }
        private void btn_del_rate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_rate.Text == string.Empty)
                {
                    MessageBox.Show("الرجاء اختيار عنصر من القائمة لحذفه");
                    return;
                }
                else
                {
                    string sql = @" DELETE FROM dbo.T_PERS_RATE_KEEP
                 WHERE      (id = " + int.Parse(txt_id_rate.Text) + " )";
                    int done = c_db.insert_upadte_delete(sql);
                    load_data("d");

                    delete();

                }
            }
            catch (Exception)
            {
                MessageBox.Show("لايمكن حذف العنصر المختار بسبب استخدامه في أماكن أخرى");
            }
            load_rate();

        }
        private void btn_clear_rate_Click(object sender, EventArgs e)
        {
            clear_rate();
        }
        private void clear_rate()
        {
            txt_id_rate.Text = "";
            txt_rate.Text = "";
            txt_rate_dayes.Text = "";
            txt_rate_pages.Text = "";
        }
        private void gv_ratee_DoubleClick(object sender, EventArgs e)
        {
            txt_id_rate.Text = gv_ratee.GetRowCellValue(gv_ratee.FocusedRowHandle, gv_ratee.Columns[0]).ToString();
            txt_rate.Text = gv_ratee.GetRowCellValue(gv_ratee.FocusedRowHandle, gv_ratee.Columns[1]).ToString();
            txt_rate_dayes.Text = gv_ratee.GetRowCellValue(gv_ratee.FocusedRowHandle, gv_ratee.Columns[3]).ToString();
            txt_rate_pages.Text = gv_ratee.GetRowCellValue(gv_ratee.FocusedRowHandle, gv_ratee.Columns[2]).ToString();
        }
        #endregion


        #region evaluation _page
        private void load_evaluation()
        {
            clear_evaluatin();
            // التقيم
            txt_id_evaluation.Text = set_auto_id_person("SELECT   id  FROM     dbo.T_SOURA_EVALUATION");

            dt = c_db.select(@" SELECT        id AS المعرف, name AS التقيم
FROM            dbo.T_SOURA_EVALUATION");
            lkp_evaluation_def.lkp_iniatalize_data(dt, "التقيم", "المعرف");
            lbc_evaluation.lbc_iniatalize_data(dt, "التقيم", "المعرف");

            if (dt.Rows.Count == 1)
            {
                lbc_evaluation.Enabled = false;
                btn_del_evaluation.Enabled = false;
                c_db.insert_upadte_delete(@"UPDATE     dbo.T_DEFULT_THWABET
                      SET           value_id = " + int.Parse(dt.Rows[0][0].ToString()) + ", " +
              " value  =N'" + dt.Rows[0][1].ToString() + "' " +
              " WHERE        (id = 5)");

            }
            else
            {
                lbc_evaluation.Enabled = true;
                btn_del_evaluation.Enabled = true;
            }

            //lkp_evaluation_def.Properties.Columns["id"].Visible = false;
        }
        private void btn_save_evaluation_Click(object sender, EventArgs e)
        {
            if (txt_evaluation.Text == string.Empty)
            {
                txt_evaluation.ErrorText = "حقل مطلوب";
                return;
            }
            else
            {
                string sql = @"  INSERT INTO dbo.T_SOURA_EVALUATION
                         (name)
                            VALUES(N'" + txt_evaluation.Text + "') ";
                int done = c_db.insert_upadte_delete(sql);
                load_data("i");
                save();
            }
            load_evaluation();
        }
        private void btn_clear_evaluation_Click(object sender, EventArgs e)
        {
            clear_evaluatin();
        }
        private void clear_evaluatin()
        {
            txt_id_evaluation.Text = "";
            txt_evaluation.Text = "";

        }
        private void btn_del_evaluation_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_evaluation.Text == string.Empty)
                {
                    MessageBox.Show("الرجاء اختيار عنصر من القائمة لحذفه");
                    return;
                }
                else
                {
                    string sql = @" DELETE FROM dbo.T_SOURA_EVALUATION
            WHERE      (id = " + int.Parse(txt_id_evaluation.Text) + " )";
                    int done = c_db.insert_upadte_delete(sql);
                    load_data("d");

                    delete();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("لايمكن حذف العنصر المختار بسبب استخدامه في أماكن أخرى");
            }
            load_evaluation();
        }
        private void lbc_evaluation_DoubleClick(object sender, EventArgs e)
        {
            txt_id_evaluation.Text = lbc_evaluation.GetItemValue(lbc_evaluation.SelectedIndex).ToString();
            txt_evaluation.Text = lbc_evaluation.GetItemText(lbc_evaluation.SelectedIndex).ToString();

        }
        #endregion

        #region pers_type_page

        private void load_types()
        {
            clear_pers_type();
            //الأدوار
            txt_id_pers_type.Text = set_auto_id_person("SELECT    id  FROM     dbo.T_PERS_TYPE");
            dt = c_db.select(@"  SELECT        id AS المعرف, name AS الدور
FROM            dbo.T_PERS_TYPE");
            lbc_pers_type.lbc_iniatalize_data(dt, "الدور", "المعرف");
            lkp_pers_type_def.lkp_iniatalize_data(dt, "الدور", "المعرف");

            if (dt.Rows.Count <= 4)
            {
                lbc_pers_type.Enabled = false;
                btn_del_pers_type.Enabled = false;
                c_db.insert_upadte_delete(@"UPDATE     dbo.T_DEFULT_THWABET
                      SET           value_id = " + int.Parse(dt.Rows[0][0].ToString()) + ", " +
              " value  =N'" + dt.Rows[0][1].ToString() + "' " +
                           " WHERE        (id = 2)");
            }
            else
            {
                lbc_pers_type.Enabled = true;
                btn_del_pers_type.Enabled = true;
            }

            show_graid_pers_type();

        }

        private void show_graid_pers_type()
        {
            dt = c_db.select(@" SELECT dbo.T_PERS_TYPES_TRUE_FALSE.*
                                   FROM            dbo.T_PERS_TYPES_TRUE_FALSE ");

            int count = 0;
            DataTable dt_show_count = new DataTable();
            dt_show_count.Columns.Add("الدور");
            dt_show_count.Columns.Add("عدد الاشخاص");
            foreach (DataColumn col in dt.Columns)
            {
                int index = dt.Columns.IndexOf(col);
                if (index > 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][index].ToString() != null && dt.Rows[i][index].ToString() != string.Empty)
                        {
                            Boolean state = Convert.ToBoolean(dt.Rows[i][index].ToString());
                            if (state == true)
                            {
                                count++;
                            }
                        }
                    }
                    dt_show_count.Rows.Add(col, count);
                }
                count = 0;
            }

            gc_pers_type.DataSource = dt_show_count;
        }

        private void btn_save_pers_type_Click(object sender, EventArgs e)
        {
            if (txt_pers_type.Text == string.Empty)
            {
                txt_pers_type.ErrorText = "حقل مطلوب";
                return;
            }
            else
            {
                string sql = @"  INSERT INTO dbo.T_PERS_TYPE
                         (name)
                            VALUES(N'" + txt_pers_type.Text.Trim() + "') ";
                int done = c_db.insert_upadte_delete(sql);
                c_db.alter_pers_types("add", txt_pers_type.Text.Trim());
                c_db.insert_upadte_delete(@" update T_PERS_TYPES_TRUE_FALSE set " + txt_pers_type.Text.Trim() + " = 'false' ");

                load_data("i");

                save();
            }
            load_types();
        }

        private void btn_del_pers_type_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_pers_type.Text == string.Empty)
                {
                    MessageBox.Show("الرجاء اختيار عنصر من القائمة لحذفه");
                    return;
                }
                else
                {
                    if (Convert.ToInt32(txt_id_pers_type.Text) <= 4)
                    {
                        MessageBox.Show("لا يمكن حذف العنصر المحدد !!! الرجاء اختيار عنصر آخر لحذفه ");
                    }
                    else
                    {
                        string sql = @" DELETE FROM dbo.T_PERS_TYPE
                   WHERE      (id = " + int.Parse(txt_id_pers_type.Text) + " )";
                        int done = c_db.insert_upadte_delete(sql);
                        try
                        {
                            c_db.alter_pers_types("drop", txt_pers_type.Text.Trim());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(" " + ex);
                        }

                        load_data("d");
                        delete();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("لايمكن حذف العنصر المختار بسبب استخدامه في أماكن أخرى" + ex);
            }
            load_types();
        }

        private void lbc_pers_type_DoubleClick(object sender, EventArgs e)
        {
            txt_id_pers_type.Text = lbc_pers_type.GetItemValue(lbc_pers_type.SelectedIndex).ToString();
            txt_pers_type.Text = lbc_pers_type.GetItemText(lbc_pers_type.SelectedIndex).ToString();

        }

        private void btn_clear_pers_type_Click(object sender, EventArgs e)
        {
            clear_pers_type();
        }

        private void clear_pers_type()
        {
            txt_id_pers_type.Text = "";
            txt_pers_type.Text = "";
        }


        #endregion

        #region keep types page
        private void load_keep_types()
        {
            clear_keep_type();
            txt_id_keep_type.Text = set_auto_id_person("SELECT        id FROM   dbo.T_SOURA_KEEP_TYPE");
            dt = c_db.select(@" SELECT        id AS المعرف, name AS [نوع الحفظ]
FROM            dbo.T_SOURA_KEEP_TYPE ");
            lbc_keep_type.lbc_iniatalize_data(dt, "نوع الحفظ", "المعرف");
            lkp_keep_type_def.lkp_iniatalize_data(dt, "نوع الحفظ", "المعرف");

            if (dt.Rows.Count == 1)
            {
                lbc_keep_type.Enabled = false;
                btn_del_keep_type.Enabled = false;
                c_db.insert_upadte_delete(@"UPDATE     dbo.T_DEFULT_THWABET
                      SET           value_id = " + int.Parse(dt.Rows[0][0].ToString()) + ", " +
              " value  =N'" + dt.Rows[0][1].ToString() + "' " +
                         " WHERE        (id = 6)");
            }
            else
            {
                lbc_keep_type.Enabled = true;
                btn_del_keep_type.Enabled = true;
            }


            dt = c_db.select(@" SELECT        dbo.T_SOURA_KEEP_TYPE.name AS [نوع الحفظ], COUNT(DISTINCT dbo.T_SOURA_KEEP.pers_hafez_id) AS [عدد الأشخاص]
FROM            dbo.T_SOURA_KEEP_TYPE LEFT OUTER JOIN
                         dbo.T_SOURA_KEEP ON dbo.T_SOURA_KEEP_TYPE.id = dbo.T_SOURA_KEEP.keep_type_id
GROUP BY dbo.T_SOURA_KEEP_TYPE.name ");
            gc_pers_type.DataSource = dt;

        }
        private void btn_kep_type_save_Click(object sender, EventArgs e)
        {
            if (txt_keep_type.Text == string.Empty)
            {
                txt_keep_type.ErrorText = "حقل مطلوب";
                return;
            }
            else
            {
                string sql = @"  INSERT INTO dbo.T_SOURA_KEEP_TYPE
                         (name)
                            VALUES(N'" + txt_keep_type.Text + "') ";
                int done = c_db.insert_upadte_delete(sql);
                load_data("i");
                save();
            }
            load_keep_types();
        }

        private void btn_clear_keep_type_Click(object sender, EventArgs e)
        {
            clear_keep_type();
        }

        private void clear_keep_type()
        {
            txt_id_keep_type.Text = "";
            txt_keep_type.Text = "";
        }

        private void btn_del_keep_type_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_keep_type.Text == string.Empty)
                {
                    MessageBox.Show("الرجاء اختيار عنصر من القائمة لحذفه");
                    return;
                }
                else
                {
                    string sql = @" DELETE FROM dbo.T_SOURA_KEEP_TYPE
            WHERE      (id = " + int.Parse(txt_id_keep_type.Text) + " )";
                    int done = c_db.insert_upadte_delete(sql);
                    load_data("d");
                    delete();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("لايمكن حذف العنصر المختار بسبب استخدامه في أماكن أخرى");
            }
            load_keep_types();
        }

        private void lbc_keep_type_DoubleClick(object sender, EventArgs e)
        {
            txt_id_keep_type.Text = lbc_keep_type.GetItemValue(lbc_keep_type.SelectedIndex).ToString();
            txt_keep_type.Text = lbc_keep_type.GetItemText(lbc_keep_type.SelectedIndex).ToString();
        }

        #endregion
    }
}

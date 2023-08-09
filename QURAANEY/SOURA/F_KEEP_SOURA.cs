using QURAANEY.CLASS_TABLES;
using QURAANEY.DB;
using QURAANEY.MESSAGES;
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

namespace QURAANEY.SOURA
{
    public partial class F_KEEP_SOURA : F_INHERATENZ
    {
        DataTable dt;
        int done;
        int id_pers = 0;
        int last_aya_in_soura;
        int id_keep_to_delet;
        

        public F_KEEP_SOURA()
        {
            view_inheretanz_butomes();
            InitializeComponent();
            

            load_data("");

        }
        public F_KEEP_SOURA(int id )
        {
            view_inheretanz_butomes();
            id_pers = id;
            InitializeComponent();
            dtp_sora_date.Value = DateTime.Today;
            
        }
        private void view_inheretanz_butomes()
        {
            btn_clear.Visible = true;
            btn_delete.Visible = false;
            btn_exite.Visible = true;
            btn_new.Visible = false;
            btn_print.Visible = true;
            btn_save.Visible = true;
            btn_show.Visible = false;
        }
        #region override inheretanz methodes //توابع الوراثة
        //تحميل القوائم لأول مرة
        public override void load_data(string status_mess)
        {//lkp السور
            dt = C_DB_QUERYS.get_soura_name();
            lkp_soura.lkp_iniatalize_data(dt, "soura_name", "soura_num");
           // lkp_soura.Properties.Columns[1].Visible = false;

            //lkp السور في الآيات
            dt = C_DB_QUERYS.get_soura_name();
            lkp_aya_soura.lkp_iniatalize_data(dt, "soura_name", "soura_num");          
            //lkp_aya_soura.Properties.Columns[1].Visible = false;


            //lkp  الحافظين الاسماء
            dt = C_DB_QUERYS.get_person_name();
            lkp_pers_name.lkp_iniatalize_data(dt, "name", "id");
            //lkp_pers_name.Properties.Columns[0].Visible = false;

            if (id_pers > 0)
            {
                lkp_pers_name.EditValue = id_pers;
                load_gc_snd_tail();
            }

            //lkp الاسماء المستلمين
            dt = C_DB_QUERYS.get_person_name();
            lkp_pers_almustalem.lkp_iniatalize_data(dt, "name", "id");
            //lkp_pers_almustalem.Properties.Columns[0].Visible = false;
            //lkp_pers_almustalem.Properties.Columns["name"].Caption = "اسم المستلم";

            //lkp  التقيم
            dt = c_db.select(@"SELECT     id, name
                               FROM         T_SOURA_EVALUATION");
            lkp_evaluation.lkp_iniatalize_data(dt, "name", "id");
            //lkp_evaluation.Properties.Columns[0].Visible = false;


            //lkp  نوع الحفظ
            dt = c_db.select(@"SELECT     id, name
                              FROM         T_SOURA_KEEP_TYPE");
            lkp_keep_type.lkp_iniatalize_data(dt, "name", "id");
            //lkp_keep_type.Properties.Columns[0].Visible = false;

            dtp_sora_date.Value = DateTime.Today;
            dt = c_db.select(@"  SELECT name, value, value_id FROM dbo.T_DEFULT_THWABET");
            lkp_evaluation.EditValue = int.Parse(dt.Rows[4][2].ToString());
            lkp_keep_type.EditValue = int.Parse(dt.Rows[5][2].ToString());


            base.load_data(status_mess);
        }
        
        //شخص جديد
        public override void neew()
        {
            F_PERSON_MANEG f = new F_PERSON_MANEG();
            f.Show();
            base.neew();//يوجد get و refresh من الوراثة
        }
        //التأكد من صحة البيانات
        public override bool vallidate_data()
        {
            int number_of_errores = 0;
            number_of_errores += lkp_pers_name.is_editevalue_valid() ? 0 : 1;
            number_of_errores += lkp_pers_almustalem.is_editevalue_valid() ? 0 : 1;
            number_of_errores += lkp_evaluation.is_editevalue_valid() ? 0 : 1;
            number_of_errores += lkp_keep_type.is_editevalue_valid() ? 0 : 1;
            return (number_of_errores == 0);

        }
        //مسح البيانات
        public override void clear(Control.ControlCollection s_controls)
        {
            base.clear(s_controls);

            load_data("");
        }
 
        //الطباعة
        public override void print()
        {
            C_MASTER.print_header("تقرير" + lkp_pers_name.Text.ToString(), gc);
            base.print();
        }
        //حفظ تسميع جديد
        public override void save()
        {
            if (rdb_one_page.Checked)
                keep_one_page();
            if (rdb_many_page.Checked)
                keep_many_page();
            if (rdb_one_aya.Checked)
                keep_one_aya();
            if (rdb_many_aya.Checked)
                keep_many_aya();
            if (rdb_soura.Checked)
                keep_soura();
            else if (rdb_one_page.Checked == false &&
                rdb_many_page.Checked == false &&
                rdb_one_aya.Checked == false &&
                rdb_many_aya.Checked == false &&
                rdb_soura.Checked == false)
                MessageBox.Show("  الرجاء اختيار واحد من خيارات الحفظ  ", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information);

            last_aya_in_soura = 0;
            base.save();
        }


        #endregion

        #region methodes //توابع 
        //صفحة واحدة
        private void keep_one_page()
        {
            try
            {
                if (txt_page.Text == string.Empty)
                    txt_page.ErrorText = errore_text;
                if (vallidate_data())
                {
                    if (Convert.ToInt32(txt_page.Text) <= 604 && Convert.ToInt32(txt_page.Text) > 0)
                    {
                        string sql = @"SELECT     soura_id, pers_hafez_id
                                FROM         T_SOURA_KEEP
                                WHERE     (page_num = " + int.Parse(txt_page.Text) + ")" +
                             "   AND (pers_hafez_id = " + Convert.ToInt32(lkp_pers_name.EditValue) + ")";

                        dt = c_db.select(sql);
                        if (dt.Rows.Count == 0)
                        {
                            sql = @"SELECT     id, aya_num, page_num ,soura_num,soura_name
                            FROM         T_SOURA
                            WHERE     (page_num = " + int.Parse(txt_page.Text) + ")";
                            dt = c_db.select(sql);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                sql = @" INSERT INTO dbo.T_SOURA_KEEP
                          (keep_date, keep_type_id, pers_hafez_id, pers_mustalem_id, soura_id, aya_num, page_num, soura_num,soura_name ,evaluation_id)
                          VALUES(N'" + dtp_sora_date.Text + "'," + Convert.ToInt32(lkp_keep_type.EditValue) + "," +
                                 "" + Convert.ToInt32(lkp_pers_name.EditValue) + "," + Convert.ToInt32(lkp_pers_almustalem.EditValue) + "," +
                                 "" + int.Parse(dt.Rows[i][0].ToString()) + "," + int.Parse(dt.Rows[i][1].ToString()) + "," +
                                 "" + int.Parse(dt.Rows[i][2].ToString()) + "," + int.Parse(dt.Rows[i][3].ToString()) + "," +
                                 "N'" + dt.Rows[i][4].ToString() + "'," + Convert.ToInt32(lkp_evaluation.EditValue) + ")";
                                done = c_db.insert_upadte_delete(sql);
                            }
                            load_data("i");
                            load_gc_snd_tail();                            
                            
                        }
                        else
                            MessageBox.Show("  !!!!!!! السجل الذي تحاول ادخاله موجود مسبقا  ", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        txt_page.ErrorText = "القيمة المسموحة بين 1 و 604";
                }
            }
            catch (Exception ex)
            {
                load_data(ex.InnerException.ToString());
            }
        }
        //أكثر من صفحة 
        private void keep_many_page()
        {
            try
            {
                if (txt_fpage1.Text == string.Empty)
                    txt_fpage1.ErrorText = errore_text;
                if (txt_lpage.Text == string.Empty)
                    txt_lpage.ErrorText = errore_text;
                if (vallidate_data())
                {
                    if (Convert.ToInt32(txt_fpage1.Text) < 604 && Convert.ToInt32(txt_fpage1.Text) > 0)
                    {
                        if (Convert.ToInt32(txt_lpage.Text) < 604 && Convert.ToInt32(txt_lpage.Text) > 0)
                        {
                            int y = 0;
                            int x = int.Parse(txt_fpage1.Text);
                            int pno = int.Parse(txt_lpage.Text) - int.Parse(txt_fpage1.Text);
                            for (int j = 0; j < pno + 1; j++)
                            {
                                string sql = @"SELECT     soura_id, pers_hafez_id
                                FROM         T_SOURA_KEEP
                                WHERE     (page_num = " + x + ")" +
                                 "   AND (pers_hafez_id = " + Convert.ToInt32(lkp_pers_name.EditValue) + ")";

                                dt = c_db.select(sql);
                                if (dt.Rows.Count == 0)
                                {
                                    sql = @"SELECT     id, aya_num, page_num ,soura_num,soura_name
                                       FROM         T_SOURA
                                       WHERE     (page_num = " + x + ")";
                                    dt = c_db.select(sql);
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        sql = @" INSERT INTO dbo.T_SOURA_KEEP
                          (keep_date, keep_type_id, pers_hafez_id, pers_mustalem_id, soura_id, aya_num, page_num, soura_num,soura_name ,evaluation_id)
                          VALUES(N'" + dtp_sora_date.Text + "'," + Convert.ToInt32(lkp_keep_type.EditValue) + "," +
                                         "" + Convert.ToInt32(lkp_pers_name.EditValue) + "," + Convert.ToInt32(lkp_pers_almustalem.EditValue) + "," +
                                         "" + int.Parse(dt.Rows[i][0].ToString()) + "," + int.Parse(dt.Rows[i][1].ToString()) + "," +
                                         "" + int.Parse(dt.Rows[i][2].ToString()) + "," + int.Parse(dt.Rows[i][3].ToString()) + "," +
                                         "N'" + dt.Rows[i][4].ToString() + "'," + Convert.ToInt32(lkp_evaluation.EditValue) + ")";
                                        done = c_db.insert_upadte_delete(sql);

                                    }
                                    x++;
                                    if (x == int.Parse(txt_lpage.Text) + 1)
                                        break;

                                }
                                else
                                {
                                    MessageBox.Show("  !!!!!!! السجل الذي تحاول ادخاله موجود مسبقا  ", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    y = 1;
                                    break;//طالما في تكرار واحد على الأقل اكسير الحلقة 
                                          //و انبه حتى ما يطلع الرسالة بكل حلقة الفور
                                }


                            }
                            if (y == 0)
                            {
                                load_data("i");
                                load_gc_snd_tail();                              
                            }

                        }
                        else
                            txt_lpage.ErrorText = "القيمة المسموحة بين 1 و 604";
                    }
                    else
                        txt_fpage1.ErrorText = "القيمة المسموحة بين 1 و 604";
                }
            }
            catch (Exception ex)
            {
                load_data(ex.InnerException.ToString());
            }
        }
        //آية واحدة
        private void keep_one_aya()
        {
            try
            {
                if (txt_aya.Text == string.Empty)
                    txt_aya.ErrorText = errore_text;
                if (lkp_aya_soura.is_editevalue_valid())
                    if (vallidate_data())
                    {
                        if (Convert.ToInt32(txt_aya.Text) <= last_aya_in_soura && Convert.ToInt32(txt_aya.Text) > 0)
                        {
                            string sqll = @"SELECT     soura_id, pers_hafez_id
                                        FROM         T_SOURA_KEEP
                         WHERE     (aya_num = " + int.Parse(txt_aya.Text) + ") " +
                           " AND (pers_hafez_id = " + Convert.ToInt32(lkp_pers_name.EditValue) + ")" +
                           " AND (soura_num = " + Convert.ToInt32(lkp_aya_soura.EditValue) + ")";

                            dt = c_db.select(sqll);
                            if (dt.Rows.Count == 0)
                            {
                                string sql = @"SELECT     id, aya_num, page_num ,soura_num,soura_name
                            FROM         T_SOURA
                            WHERE     (aya_num =" + int.Parse(txt_aya.Text) + " )" +
                               "and (soura_num =" + Convert.ToInt32(lkp_aya_soura.EditValue) + ") ";
                                dt = c_db.select(sql);
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    sql = @" INSERT INTO dbo.T_SOURA_KEEP
                          (keep_date, keep_type_id, pers_hafez_id, pers_mustalem_id, soura_id, aya_num, page_num, soura_num,soura_name ,evaluation_id)
                          VALUES(N'" + dtp_sora_date.Text + "'," + Convert.ToInt32(lkp_keep_type.EditValue) + "," +
                                     "" + Convert.ToInt32(lkp_pers_name.EditValue) + "," + Convert.ToInt32(lkp_pers_almustalem.EditValue) + "," +
                                     "" + int.Parse(dt.Rows[i][0].ToString()) + "," + int.Parse(dt.Rows[i][1].ToString()) + "," +
                                     "" + int.Parse(dt.Rows[i][2].ToString()) + "," + int.Parse(dt.Rows[i][3].ToString()) + "," +
                                     "N'" + dt.Rows[i][4].ToString() + "'," + Convert.ToInt32(lkp_evaluation.EditValue) + ")";
                                    done = c_db.insert_upadte_delete(sql);
                                }
                                load_data("i");
                                load_gc_snd_tail();                               
                            }
                            else
                                MessageBox.Show("  !!!!!!! السجل الذي تحاول ادخاله موجود مسبقا  ", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            txt_aya.ErrorText = "القيمة المسموحة بين 1 و " + last_aya_in_soura;
                    }
            }
            catch (Exception ex)
            {
                load_data(ex.InnerException.ToString());
            }
            last_aya_in_soura = 0;
        }
        //أكثر من آية 
        private void keep_many_aya()
        {
            try
            {
                if (txt_f_aya.Text == string.Empty)
                    txt_f_aya.ErrorText = errore_text;
                if (txt_l_aya.Text == string.Empty)
                    txt_l_aya.ErrorText = errore_text;
                if (vallidate_data())
                {
                    if (Convert.ToInt32(txt_f_aya.Text) < last_aya_in_soura && Convert.ToInt32(txt_f_aya.Text) > 0)
                    {
                        if (Convert.ToInt32(txt_l_aya.Text) < last_aya_in_soura && Convert.ToInt32(txt_l_aya.Text) > 0)
                        {
                            int y = 0;
                            int x = int.Parse(txt_f_aya.Text);
                            int pno = int.Parse(txt_l_aya.Text) - int.Parse(txt_f_aya.Text);
                            for (int j = 0; j < pno + 1; j++)
                            {
                                string sqll = @"SELECT     soura_id, pers_hafez_id
                                        FROM         T_SOURA_KEEP
                         WHERE     (aya_num = " + x + ") " +
                         " AND (pers_hafez_id = " + Convert.ToInt32(lkp_pers_name.EditValue) + ")" +
                         " AND (soura_num = " + Convert.ToInt32(lkp_aya_soura.EditValue) + ")";

                                dt = c_db.select(sqll);
                                if (dt.Rows.Count == 0)
                                {
                                    string sql = @"SELECT     id, aya_num, page_num ,soura_num,soura_name
                            FROM         T_SOURA
                            WHERE     (aya_num =" + x + " )" +
                            "and (soura_num =" + Convert.ToInt32(lkp_aya_soura.EditValue) + ") ";
                                    dt = c_db.select(sql);
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        sql = @" INSERT INTO dbo.T_SOURA_KEEP
                          (keep_date, keep_type_id, pers_hafez_id, pers_mustalem_id, soura_id, aya_num, page_num, soura_num,soura_name ,evaluation_id)
                          VALUES(N'" + dtp_sora_date.Text + "'," + Convert.ToInt32(lkp_keep_type.EditValue) + "," +
                                         "" + Convert.ToInt32(lkp_pers_name.EditValue) + "," + Convert.ToInt32(lkp_pers_almustalem.EditValue) + "," +
                                         "" + int.Parse(dt.Rows[i][0].ToString()) + "," + int.Parse(dt.Rows[i][1].ToString()) + "," +
                                         "" + int.Parse(dt.Rows[i][2].ToString()) + "," + int.Parse(dt.Rows[i][3].ToString()) + "," +
                                         "N'" + dt.Rows[i][4].ToString() + "'," + Convert.ToInt32(lkp_evaluation.EditValue) + ")";
                                        done = c_db.insert_upadte_delete(sql);

                                    }
                                    x++;
                                    if (x == int.Parse(txt_l_aya.Text) + 1)
                                        break;

                                }
                                else
                                {
                                    MessageBox.Show("  !!!!!!! السجل الذي تحاول ادخاله موجود مسبقا  ", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    y = 1;
                                    break;//طالما في تكرار واحد على الأقل اكسير الحلقة 
                                          //و انبه حتى ما يطلع الرسالة بكل حلقة الفور
                                }
                            }
                            if (y == 0)
                            {
                                load_data("i");
                                load_gc_snd_tail();

                            }

                        }
                        else
                            txt_l_aya.ErrorText = "القيمة المسموحة بين 1 و " + last_aya_in_soura;
                    }
                    else
                        txt_f_aya.ErrorText = "القيمة المسموحة بين 1 و " + last_aya_in_soura;
                }
            }
            catch (Exception ex)
            {
                load_data(ex.InnerException.ToString());
            }
            last_aya_in_soura = 0;
        }
        //سورة
        private void keep_soura()
        {
            try
            {
                if (lkp_soura.is_editevalue_valid())
                {
                    if (vallidate_data())
                    {
                        string sql = @"SELECT     soura_id, pers_hafez_id
                                FROM         T_SOURA_KEEP
                                WHERE     (soura_num = " + Convert.ToInt32(lkp_soura.EditValue) + ")" +
                                " AND (pers_hafez_id = " + Convert.ToInt32(lkp_pers_name.EditValue) + ")";

                        dt = c_db.select(sql);
                        if (dt.Rows.Count == 0)
                        {
                            sql = @"SELECT     id, aya_num, page_num ,soura_num,soura_name
                            FROM         T_SOURA
                            WHERE     (soura_num = " + Convert.ToInt32(lkp_soura.EditValue) + ")";
                            dt = c_db.select(sql);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                sql = @" INSERT INTO dbo.T_SOURA_KEEP
                          (keep_date, keep_type_id, pers_hafez_id, pers_mustalem_id, soura_id, aya_num, page_num, soura_num,soura_name ,evaluation_id)
                          VALUES(N'" + dtp_sora_date.Text + "'," + Convert.ToInt32(lkp_keep_type.EditValue) + "," +
                                 "" + Convert.ToInt32(lkp_pers_name.EditValue) + "," + Convert.ToInt32(lkp_pers_almustalem.EditValue) + "," +
                                 "" + int.Parse(dt.Rows[i][0].ToString()) + "," + int.Parse(dt.Rows[i][1].ToString()) + "," +
                                 "" + int.Parse(dt.Rows[i][2].ToString()) + "," + int.Parse(dt.Rows[i][3].ToString()) + "," +
                                 "N'" + dt.Rows[i][4].ToString() + "'," + Convert.ToInt32(lkp_evaluation.EditValue) + ")";
                                done = c_db.insert_upadte_delete(sql);
                            }
                            load_data("i");
                            load_gc_snd_tail();
                         
                        }
                        else if (dt.Rows.Count == last_aya_in_soura)
                        {
                            MessageBox.Show("  !!!!!!! السجل الذي تحاول ادخاله موجود مسبقا  ", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else if (dt.Rows.Count < last_aya_in_soura)
                        {
                            MessageBox.Show(" تم تسميع بعض الآيات من هذه السورة  ", "معلومات", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        last_aya_in_soura = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                load_data(ex.InnerException.ToString());
            }
        }
        //تحميل الغريد كونترول
        private void load_gc_snd_tail()
        {
            string sqll = @" SELECT  T_SOURA_KEEP.id , T_SOURA_KEEP.soura_name AS [اسم السورة],  T_SOURA_KEEP.page_num AS [رقم الصفحة] , 
                      T_SOURA_KEEP.keep_date AS التاريخ, T_SOURA_KEEP_TYPE.name AS [نوع الحفظ], T_SOURA_EVALUATION.name AS التقيم, T_PERSONE_1.name AS المستلم
FROM         T_PERSONE INNER JOIN
                      T_SOURA_KEEP ON T_PERSONE.id = T_SOURA_KEEP.pers_hafez_id INNER JOIN
                      T_SOURA_KEEP_TYPE ON T_SOURA_KEEP.keep_type_id = T_SOURA_KEEP_TYPE.id INNER JOIN
                      T_SOURA_EVALUATION ON T_SOURA_KEEP.evaluation_id = T_SOURA_EVALUATION.id INNER JOIN
                      T_PERSONE AS T_PERSONE_1 ON T_SOURA_KEEP.pers_mustalem_id = T_PERSONE_1.id
WHERE     (T_SOURA_KEEP.pers_hafez_id = " + Convert.ToInt32(lkp_pers_name.EditValue) + ")" +
" ORDER BY T_SOURA_KEEP.id DESC ";

            dt = c_db.select(sqll);
            gc.DataSource = dt;
            gv.Columns[0].Visible = false;


            //تحميل التيل الحالة 
            dt = C_PERS_STATE_sql.get_last_state_by_id(Convert.ToInt32(lkp_pers_name.EditValue));
            if (dt.Rows.Count > 0)
            {
                ti_state.Elements[1].Text = dt.Rows[0][3].ToString();
            }
            else
                ti_state.Elements[1].Text = "...";
            //تحميل التيل معدل الحفظ 
            dt = C_DB_QUERYS.rate_by_id(Convert.ToInt32(lkp_pers_name.EditValue));
            if (dt.Rows.Count > 0)
            {
                ti_keep_rate.Elements[1].Text = dt.Rows[0][0].ToString();
            }
            else
                ti_keep_rate.Elements[1].Text = "...";
        }

        #endregion

        #region RDB تسلسل فتح الراديو بوتنز 
        //عند اختيار الآيات
        private void rdb_aya_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_aya.Checked)
            {
                lkp_soura.Enabled = false;
                rdb_one_page.Enabled = false;
                rdb_many_page.Enabled = false;
                rdb_many_aya.Enabled = true;
                rdb_one_aya.Enabled = true;
                lkp_soura_aya.Enabled = true;
                txt_page.Enabled = false;
                txt_aya.Enabled = false;
                txt_f_aya.Enabled = false;
                txt_l_aya.Enabled = false;
                txt_fpage1.Enabled = false;
                txt_lpage.Enabled = false;

            }
        }
        //عند اختيار الصفحات
        private void rdb_page_CheckedChanged(object sender, EventArgs e)
        {

            if (rdb_page.Checked)
            {
                lkp_soura.Enabled = false;
                rdb_one_page.Enabled = true;
                rdb_many_page.Enabled = true;
                rdb_many_aya.Enabled = false;
                rdb_one_aya.Enabled = false;
                lkp_soura_aya.Enabled = false;
                txt_page.Enabled = false;
                txt_aya.Enabled = false;
                txt_f_aya.Enabled = false;
                txt_l_aya.Enabled = false;
                txt_fpage1.Enabled = false;
                txt_lpage.Enabled = false;
            }
        }
        //عند اختيار السور
        private void rdb_soura_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_soura.Checked)
            {
                lkp_soura.Enabled = true;
                rdb_one_page.Enabled = false;
                rdb_many_page.Enabled = false;
                rdb_many_aya.Enabled = false;
                rdb_one_aya.Enabled = false;
                lkp_soura_aya.Enabled = false;
                txt_page.Enabled = false;
                txt_aya.Enabled = false;
                txt_f_aya.Enabled = false;
                txt_l_aya.Enabled = false;
                txt_fpage1.Enabled = false;
                txt_lpage.Enabled = false;
            }
        }
        //عند اختيار آية واحدة
        private void rdb_one_aya_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_one_aya.Checked)
            {
                txt_aya.Enabled = true;
                txt_f_aya.Enabled = false;
                txt_l_aya.Enabled = false;
                txt_page.Enabled = false;
                txt_fpage1.Enabled = false;
                txt_lpage.Enabled = false;

            }

        }
        //عند اختيار أكثر الآيات
        private void rdb_many_aya_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_many_aya.Checked)
            {
                txt_f_aya.Enabled = true;
                txt_l_aya.Enabled = true;
                txt_aya.Enabled = false;
                txt_page.Enabled = false;
                txt_fpage1.Enabled = false;
                txt_lpage.Enabled = false;
            }
        }
        //عند اختيار صفحة واحدة
        private void rdb_one_page_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_one_page.Checked)
            {
                txt_page.Enabled = true;
                txt_aya.Enabled = false;
                txt_f_aya.Enabled = false;
                txt_l_aya.Enabled = false;
                txt_fpage1.Enabled = false;
                txt_lpage.Enabled = false;
            }
        }
        //عند اختيار أكثر  صفحة 
        private void rdb_many_page_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_many_page.Checked)
            {
                txt_fpage1.Enabled = true;
                txt_lpage.Enabled = true;
                txt_aya.Enabled = false;
                txt_f_aya.Enabled = false;
                txt_l_aya.Enabled = false;
                txt_page.Enabled = false;

            }
        }
        #endregion

        #region events // الأحداث
        private void F_KEEP_SOURA_Load(object sender, EventArgs e)
        {
            if (id_pers != 0)
                lkp_pers_name.EditValue = id_pers;
            load_data("");
        }
        private void lkp_pers_name_EditValueChanged(object sender, EventArgs e)
        {
            load_gc_snd_tail();
        }
        private void gv_DoubleClick(object sender, EventArgs e)
        {
            F_REP_PERS_SOURA f = new F_REP_PERS_SOURA(Convert.ToInt32(lkp_pers_name.EditValue));
            f.WindowState = FormWindowState.Maximized;
            //F_MAIN m = new F_MAIN();
            //nav(f, m.pan_nav);
            f.Show();
        }
        private void lkp_aya_soura_EditValueChanged(object sender, EventArgs e)
        {
            last_aya_in_soura = C_DB_QUERYS.last_aya_in_soura(Convert.ToInt32(lkp_aya_soura.EditValue));
        }
        private void lkp_soura_EditValueChanged(object sender, EventArgs e)
        {
            last_aya_in_soura = C_DB_QUERYS.last_aya_in_soura(Convert.ToInt32(lkp_soura.EditValue));
        }
        private void menu_delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل انت متاكد انك تريد حذف السجل", "تأكيد",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (gv.RowCount > 0 )
                    foreach (int rowid in gv.GetSelectedRows())
                    {
                        int id = Convert.ToInt32(gv.GetRowCellValue(rowid, "id"));
                        c_db.insert_upadte_delete(@"   DELETE FROM dbo.T_SOURA_KEEP
                                  WHERE(id = " + id + ")");
                    }
               
                load_data("d");
                load_gc_snd_tail();
                base.delete();
            }
        }

        private void menu_pers_rep_Click(object sender, EventArgs e)
        {
            F_REP_PERS_SOURA f = new F_REP_PERS_SOURA(Convert.ToInt32(lkp_pers_name.EditValue));
            f.WindowState = FormWindowState.Maximized;
            f.Show();
        }

        private void menu_fail_Click(object sender, EventArgs e)
        {
            F_FAIL_PERS f = new F_FAIL_PERS(Convert.ToInt32(lkp_pers_name.EditValue));
            f.WindowState = FormWindowState.Maximized;
            f.Show();
        }




        #endregion

        private void gv_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button==MouseButtons.Right)
            {
                gv.SelectRow(e.RowHandle);
                id_keep_to_delet =int.Parse( gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[0]).ToString());

            }
        }

        private void lkp_pers_name_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
        {
            if (e.DisplayValue is string str && str.Trim() != string.Empty)
            {
                F_PERSON_MANEG f = new F_PERSON_MANEG(lkp_pers_name.Text);
                f.WindowState = FormWindowState.Maximized;
                f.ShowDialog();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
                F_PERSON_MANEG f = new F_PERSON_MANEG(lkp_pers_name.Text);
                f.WindowState = FormWindowState.Maximized;
                f.ShowDialog();
        }
    }
}

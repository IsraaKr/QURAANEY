using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QURAANEY.SOURA
{
    public partial class F_FAIL_PERS : F_INHERATENZ
    {
        DateTime date_chose;
        int m =0;
        int y =0;

        DataTable dt;
        DataTable dt_abcent;
        DataTable dt_fail;
        int id_pers=0;
        DateTime d = DateTime.Today;
       // int selected_month, selected_yeare;
 
        int id_keep_to_delet = 0;
        public static bool is_double_click = false;
        public static bool is_show_click = false;
        int id_doublee_click= 0;


        public F_FAIL_PERS()
        {
            InitializeComponent();
            load_data("");

        }
        public override void load_data(string status_mess)
        {
            clear(this.Controls);
            is_double_click = false;
            is_show_click = false;


            base.load_data(status_mess);
        }
        public override void clear(Control.ControlCollection s_controls)
        {
            base.clear(s_controls);
            de_month_rep.Text = string.Empty;
            gv_absend.Columns.Clear();
            gv_show_fail.Columns.Clear();
            group_names.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

        }
        public override void delete()
        {
            if (is_double_click == false)
            {
                MessageBox.Show("يجب الضغط مرتين على أي سجل من الجدول لحذفه", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (MessageBox.Show("هل انت متاكد انك تريد حذف السجل", "تأكيد",
               MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    c_db.insert_upadte_delete(@"DELETE FROM dbo.T_PERS_FAIL
                             WHERE        (id = " + id_doublee_click + ")");
                    load_data("d");

                }
                else
                    return;
            }
            catch (Exception)
            {
            }
            base.delete();
            id_doublee_click = 0;
            is_double_click = false;
        }
        private void de_month_rep_EditValueChanged(object sender, EventArgs e)
        {
            //m = de_month_rep.DateTime.Month;
            //y = de_month_rep.DateTime.Year;
            //date_chose = de_month_rep.DateTime;
            //show_absend_inmonth();
            //show_fail_inmonth();
            //show_fail_table(m,y);

        }
        private void gv_save_fail_DoubleClick(object sender, EventArgs e)
        {
            is_double_click = true;
            group_names.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            id_doublee_click = int.Parse(gv_save_fail.GetRowCellValue(gv_save_fail.FocusedRowHandle, gv_save_fail.Columns[0]).ToString());
            lkp_name.EditValue = int.Parse(gv_save_fail.GetRowCellValue(gv_save_fail.FocusedRowHandle, gv_save_fail.Columns[1]).ToString());
            txt_reson.Text = gv_save_fail.GetRowCellValue(gv_save_fail.FocusedRowHandle, gv_save_fail.Columns[6]).ToString();
        }
        private void gv_save_fail_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                gv_save_fail.SelectRow(e.RowHandle);
                id_keep_to_delet = int.Parse(gv_save_fail.GetRowCellValue(gv_save_fail.FocusedRowHandle, gv_save_fail.Columns[0]).ToString());

            }
        }
        private void show_absend_inmonth()
        {
            //التسميع بالشهر و السنة
            string s = @"SELECT   pers_hafez_id   
                                   FROM         T_SOURA_KEEP
                            WHERE     ( MONTH (keep_date) = N'" + m + "') " +
                    " AND ( year (keep_date) = N'" + y + "')   " +
                    " group by  pers_hafez_id ";

            dt = c_db.select(s);

            //الغائبين
            string s2 = @"SELECT     id , name
                                       FROM         T_PERSONE
                                     WHERE      id NOT IN (" + s + " ) " +
                   " AND (in_date <= CONVERT(DATETIME , '" + y + "-" + m + "-28" + "', 102)) " +
                   " AND (is_active = 1) ";

            dt_abcent = c_db.select(s2);
            gc_absend.DataSource = dt_abcent;

        }
        private void show_fail_inmonth()
        {
            //المقصرين  عرض 
            string sqll = @" SELECT   [معدل الحفظ] , [الاسم] , count_page AS [الصفحات المحفوظة], num - count_page AS [الصفحات الباقية]
        FROM            dbo.V_fail
        WHERE        (page in month = N'" + m + "') AND ( page in year = N'" + y + "') " +
"           and ( month change rate <= N'" + m + "') and ( yea change rate <= N'" + y + "') ";
            dt_fail = c_db.select(sqll);
            gc_show_fail.DataSource = dt_fail;
        }
        private void show_fail_table()
        {
            delet_this_month_from_T_FAIL();


            //جدول التقصير
            string s4 = @" SELECT        dbo.T_PERS_FAIL.id AS المعرف, dbo.T_PERS_FAIL.pers_id AS [رقم الشخص], dbo.T_PERSONE.name AS [اسم الشخص], dbo.T_PERS_FAIL.in_date AS [تاريخ الإدخال], 
                                     dbo.T_PERS_FAIL.month_fail AS [شهر التقصير], dbo.T_PERS_FAIL.year_fail AS [سنة التقصير], dbo.T_PERS_FAIL.reson AS السبب
            FROM            dbo.T_PERS_FAIL INNER JOIN
                                     dbo.T_PERSONE ON dbo.T_PERS_FAIL.pers_id = dbo.T_PERSONE.id
            WHERE        (dbo.T_PERS_FAIL.pers_id =" + id_pers + ") ";
            dt = c_db.select(s4);
            gc_save_fail.DataSource = dt;
            gv_save_fail.Columns[0].Visible = false;
            gv_save_fail.Columns[1].Visible = false;
        }
        private void show_fail_table(int m, int y)
        {
            delet_this_month_from_T_FAIL();


            // جدول التقصير
            string s4 = @" SELECT        dbo.T_PERS_FAIL.id AS المعرف, dbo.T_PERS_FAIL.pers_id AS [رقم الشخص], dbo.T_PERSONE.name AS [اسم الشخص], dbo.T_PERS_FAIL.in_date AS [تاريخ الإدخال], 
                                     dbo.T_PERS_FAIL.month_fail AS [شهر التقصير], dbo.T_PERS_FAIL.year_fail AS [سنة التقصير], dbo.T_PERS_FAIL.reson AS السبب
            FROM            dbo.T_PERS_FAIL INNER JOIN
                                     dbo.T_PERSONE ON dbo.T_PERS_FAIL.pers_id = dbo.T_PERSONE.id
            WHERE        (month_fail = N'" + m + "') AND (year_fail = N'" + y + "') ";
            dt = c_db.select(s4);
            gc_save_fail.DataSource = dt;
            gv_save_fail.Columns[0].Visible = false;
            gv_save_fail.Columns[1].Visible = false;
        }           
        private void delet_this_month_from_T_FAIL()
        {
            //حذف معلومات الشهر الحالي من جدول الفيل
            c_db.insert_upadte_delete(@" delete from  dbo.T_PERS_FAIL 
                           WHERE( month_fail = N'" + d.Month + "' and year_fail = N'" + d.Year + "' )");
        }
        public override void save()
        {
            //ادخال سبب التقصير
            if (is_double_click == true)
            {
                c_db.insert_upadte_delete(@"UPDATE       dbo.T_PERS_FAIL
                                             SET                reson =N'" + txt_reson.Text + "'" +
                                "WHERE        (id = " + id_doublee_click + ")");
                load_data("i");
                return;

            }
            if (is_show_click == false)
            {
                MessageBox.Show("يجب اختيار شهر لعرضه", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (is_show_click == true)
            {
                save_in_fail_table();
            }
            is_show_click = false;
            base.save();
        }
        private void save_in_fail_table()
        {
            int done = 0;

            delet_this_month_from_T_FAIL();


            //ادخال معلومات جدول المقصرين من جدول عدد الصفحات 
            string sqll = @"INSERT INTO dbo.T_PERS_FAIL
                                 ( pers_id, in_date, month_fail, year_fail)
                     SELECT        dbo.V_fail.pers_hafez_id , '" + "1-1-2023" + "' ,in_month , in_yeare " +
             " FROM             dbo.V_fail " +
             "   where  (in_month = " + m + ") AND (in_yeare = " + y + ") ";
            done += c_db.insert_upadte_delete(sqll);


            //ادخال معلومات الغائبين
            for (int i = 0; i < dt_abcent.Rows.Count; i++)
            {
                done += c_db.insert_upadte_delete(@"INSERT INTO dbo.T_PERS_FAIL
                                 ( pers_id, in_date, month_fail, year_fail,reson)
                           values ( " + dt_abcent.Rows[i][0].ToString() + " ," +
                     "  '" + "1-1-2023" + "' , '" + m + "' , '" + y + "' , '" + "غائب" + "')");

                //حذف المعلومات المكررة من جدول ال المقصرين
                string sql2 = @" delete from dbo.T_PERS_FAIL where id in ( select id from dbo.T_PERS_FAIL t 
                                  where exists ( select id from dbo.T_PERS_FAIL
                                        where pers_id =t.pers_id 
                                  and month_fail = t.month_fail 
                                  and year_fail=t.year_fail
                                     and id < t.id ))";
                int done2 = c_db.insert_upadte_delete(sql2);
                if (done != 0)
                {
                    load_data("i");
                }
            }
            show_fail_table();
        }

        private void de_month_rep_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //m = de_month_rep.DateTime.Month;
            //y = de_month_rep.DateTime.Year;
            //date_chose = de_month_rep.DateTime;
            //show_absend_inmonth();
            //show_fail_inmonth();
            //show_fail_table(m, y);

        }

        private void de_month_rep_SelectionChanged(object sender, EventArgs e)
        {
            //m = de_month_rep.DateTime.Month;
            //y = de_month_rep.DateTime.Year;
            //date_chose = de_month_rep.DateTime;
            //show_absend_inmonth();
            //show_fail_inmonth();
            //show_fail_table(m, y);
        }

        private void de_month_rep_DateTimeChanged(object sender, EventArgs e)
        {
            //m = de_month_rep.DateTime.Month;
            //y = de_month_rep.DateTime.Year;
            //date_chose = de_month_rep.DateTime;
            //show_absend_inmonth();
            //show_fail_inmonth();
            //show_fail_table(m, y);
        }

        private void de_month_rep_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            m = de_month_rep.DateTime.Month;
            y = de_month_rep.DateTime.Year;
            date_chose = de_month_rep.DateTime;
            show_absend_inmonth();
            show_fail_inmonth();
            show_fail_table(m, y);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////



    //////////////////////////////////////////////////////////////////////////////////////////////
    //        public F_FAIL_PERS(int id)
    //        {
    //            id_pers = id;
    //            is_show_click = true;
    //            InitializeComponent();
    //            load_data("");  
    //        }
    //        private void F_FAIL_PERS_Load(object sender, EventArgs e)
    //        {
    //            if (id_pers != 0)
    //            {
    //                lkp_name.EditValue = id_pers;
    //            }

    //            if (de_month_rep.Text == string.Empty)
    //                load_fail_table();
    //        }
    //        public override void load_data(string status_mess)
    //        { 
    //            clear(this.Controls);
    //            is_double_click = false;
    //            is_show_click = false;
    //            dt = C_DB_QUERYS.get_person_name_isactive();
    //            lkp_name.lkp_iniatalize_data(dt, "name", "id");
    //            if (id_pers!=0)
    //            {
    //                lkp_name.EditValue = id_pers;
    //                group_names.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
    //                is_show_click = true;
    //            }
    //            load_fail_table();

    //            base.load_data(status_mess);
    //        }
    //        public override void clear(Control.ControlCollection s_controls)
    //        { 
    //             base.clear(s_controls);
    //             de_month_rep.Text = string.Empty;
    //          //  gc_absend.DataSource = null;
    //            gv_absend.Columns.Clear();
    //          //  gc_absend.DataSource = null;
    //            gv_absend.Columns.Clear();
    //            group_names.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

    //        }
    //  private void load_gc_snd_tail()
    //        {
    //            if (id_pers == 0)
    //            {
    //                //m = de_month_rep.DateTime.Month;
    //                //y = de_month_rep.DateTime.Year;
    //                //date_chose = de_month_rep.DateTime;

    //                //التسميع بالشهر و السنة
    //                string s = @"SELECT   pers_hafez_id   
    //                           FROM         T_SOURA_KEEP
    //                    WHERE     ( MONTH (keep_date) = N'" + m + "') " +
    //                        " AND ( year (keep_date) = N'" + y + "')   " +
    //                        " group by  pers_hafez_id ";

    //                dt = c_db.select(s);
    //                //gc_keep.DataSource = dt;

    //                //الغائبين
    //                string s2 = @"SELECT     id , name
    //                               FROM         T_PERSONE
    //                             WHERE      id NOT IN (" + s + " ) " +
    //                       " AND (in_date <= CONVERT(DATETIME , '" + y + "-" + m + "-28" + "', 102)) " +
    //                       " AND (is_active = 1) ";

    //                dt_abcent = c_db.select(s2);
    //                gc_absend.DataSource = dt_abcent;

    //                //                //معدل حفظ كل شخص
    //                //                string s3 = @" SELECT dbo.T_PERSONE.id, dbo.T_PERSONE.name, dbo.T_PERS_RATE_KEEP.name AS rate_name, dbo.T_PERS_RATE_KEEP.num, dbo.T_PERS_RATE_KEEP.rate_in_days, 
    //                //                      dbo.T_PERS_RATE_KEEP_CHANGE.change_date
    //                //FROM         dbo.T_PERSONE INNER JOIN
    //                //                      dbo.T_PERS_RATE_KEEP_CHANGE ON dbo.T_PERSONE.id = dbo.T_PERS_RATE_KEEP_CHANGE.pers_id INNER JOIN
    //                //                      dbo.T_PERS_RATE_KEEP ON dbo.T_PERS_RATE_KEEP_CHANGE.rate_id = dbo.T_PERS_RATE_KEEP.id";
    //                //                dt = c_db.select(s3);
    //                //                gc_rate.DataSource = dt;

    //                //المقصرين  عرض 
    //                string sqll = @" SELECT   [معدل الحفظ] , [الاسم] , count_page AS [الصفحات المحفوظة], num - count_page AS [الصفحات الباقية]
    //FROM            dbo.V_fail
    //WHERE        (in_month = N'" + m + "') AND (in_yeare = N'" + y + "') " +
    //"           and (month <= N'"+  m  +"') and ( year <= N'"+ y +"') ";
    //                dt_fail = c_db.select(sqll);
    //                gc_show_fail.DataSource = dt_fail;

    //                load_fail_table();

    //            }
    //            else
    //            {
    //                //التسميع بالشهر و السنة
    //                string s = @"SELECT pers_hafez_id
    //                       FROM            dbo.T_SOURA_KEEP
    //                       WHERE        (pers_hafez_id = " + id_pers + ")";


    //                dt = c_db.select(s);
    //                //gc_keep.DataSource = dt;

    //                //الغائبين
    //                string s2 = @"SELECT     id ,
    //                               FROM         T_PERSONE
    //                             WHERE      id NOT IN (" + s + " ) " +                   
    //                       " AND (is_active = 1) ";

    //                dt_abcent = c_db.select(s2);
    //                gc_absend.DataSource = dt_abcent;

    //                //                //معدل حفظ كل شخص
    //                //                string s3 = @" SELECT dbo.T_PERSONE.id, dbo.T_PERSONE.name, dbo.T_PERS_RATE_KEEP.name AS rate_name, dbo.T_PERS_RATE_KEEP.num, dbo.T_PERS_RATE_KEEP.rate_in_days, 
    //                //                      dbo.T_PERS_RATE_KEEP_CHANGE.change_date
    //                //FROM         dbo.T_PERSONE INNER JOIN
    //                //                      dbo.T_PERS_RATE_KEEP_CHANGE ON dbo.T_PERSONE.id = dbo.T_PERS_RATE_KEEP_CHANGE.pers_id INNER JOIN
    //                //                      dbo.T_PERS_RATE_KEEP ON dbo.T_PERS_RATE_KEEP_CHANGE.rate_id = dbo.T_PERS_RATE_KEEP.id";
    //                //                dt = c_db.select(s3);
    //                //                gc_rate.DataSource = dt;

    //                //المقصرين  عرض 
    //                string sqll = @" SELECT        name AS [اسم الشخص], rate_name AS [معدل الحفظ], count_page AS [عدد الصفحات المحفوظة], num - count_page AS [الصفحات الباقية]
    //FROM            dbo.V_fail
    //WHERE        (pers_hafez_id = " + id_pers+") ";
    //                dt_fail = c_db.select(sqll);
    //                gc_show_fail.DataSource = dt_fail;

    //                load_fail_table();

    //            }

    //            //            else
    //            //            {
    //            //                string sqll = @" SELECT        dbo.V_fail.*, pers_hafez_id AS Expr1
    //            //FROM            dbo.V_fail
    //            //WHERE        (pers_hafez_id = "+lkp_name.EditValue+")  ";
    //            //                dt = c_db.select(sqll);
    //            //                gc_absend.DataSource = dt;
    //            //            }
    //        }
    //        public override void save()
    //        {
    //            if (id_pers == 0)
    //            {
    //                if (is_double_click == true)
    //                {
    //                    c_db.insert_upadte_delete(@"UPDATE       dbo.T_PERS_FAIL
    //                             SET                reson =N'" + txt_reson.Text + "'" +
    //                                    "WHERE        (id = " + id_doublee_click + ")");
    //                    load_data("i");
    //                    return;

    //                }
    //                if (is_show_click == false)
    //                {
    //                    MessageBox.Show("يجب اختيار شهر لعرضه", "تنبيه",
    //                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
    //                    return;
    //                }
    //                if (is_show_click == true)
    //                {
    //                    save_in_fail_table();
    //                }
    //            }
    //            else
    //            { 

    //            }

    //                base.save();
    //   }
    //        public override void delete()
    //        {
    //            if (is_double_click == false)
    //            {
    //                MessageBox.Show("يجب الضغط مرتين على أي سجل من الجدول لحذفه", "تنبيه",
    //                MessageBoxButtons.OK, MessageBoxIcon.Warning);
    //                return;
    //            }
    //            try
    //            {
    //                if (MessageBox.Show("هل انت متاكد انك تريد حذف السجل", "تأكيد",
    //               MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
    //                {
    //                    c_db.insert_upadte_delete(@"DELETE FROM dbo.T_PERS_FAIL
    //                     WHERE        (id = " + id_doublee_click + ")");
    //                    load_data("d");

    //                }
    //                else
    //                    return;
    //            }
    //            catch (Exception)
    //            {
    //            }
    //            base.delete();
    //            id_doublee_click = 0;
    //            is_double_click = false;
    //        }

    //        private void gv_save_fail_DoubleClick(object sender, EventArgs e)
    //        {
    //            is_double_click = true;
    //            group_names.Visibility =DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
    //            id_doublee_click = int.Parse(gv_save_fail.GetRowCellValue(gv_save_fail.FocusedRowHandle, gv_save_fail.Columns[0]).ToString());
    //            lkp_name.EditValue =int.Parse( gv_save_fail.GetRowCellValue(gv_save_fail.FocusedRowHandle, gv_save_fail.Columns[1]).ToString());
    //            txt_reson.Text= gv_save_fail.GetRowCellValue(gv_save_fail.FocusedRowHandle, gv_save_fail.Columns[6]).ToString();
    //        }
    //        public override void show_rep()
    //        {
    //            is_show_click = true;
    //            if (de_month_rep.Text==string.Empty)
    //            {
    //                MessageBox.Show("يجب اختيار شهر لعرضه", "تنبيه",
    //               MessageBoxButtons.OK, MessageBoxIcon.Warning);
    //                return;
    //            }

    //            load_gc_snd_tail();
    //            //try
    //            //{
    //            //    string query = "where ";
    //            //    gv_absend.Columns.Clear();

    //            //    if (lkp_name.Text != "")
    //            //    {
    //            //        if (lkp_name.Text == "لايوجد")
    //            //        {
    //            //            query += "Name1 = N' ' AND ";
    //            //        }
    //            //        else
    //            //        {
    //            //            query += "Name1 = N'" + lkp_name.Text + "' AND ";
    //            //        }
    //            //    }

    //            //    if (de_month_rep.Text != "")
    //            //    {
    //            //        if (de_month_rep.Text == "لايوجد")
    //            //        {
    //            //            query += "Name2 = N' ' AND ";
    //            //        }
    //            //        else
    //            //        {
    //            //            query += "date = convert(datetime,'" + de_month_rep.Text + "',105) AND ";
    //            //        }
    //            //    }


    //            //    query = query.Remove(query.Length - 4);

    //            //    DataTable ds = new DataTable();
    //            //    ds = c_db.select("select * from table " + query);
    //            //    gc_absend.DataSource = ds;
    //            //    query = "";
    //            //  //  SET_HEADERCOLUMNS_TABLE();

    //            //}
    //            //catch
    //            //{
    //            //}
    //            base.show_rep();
    //        }
    //        
    //        private void gv_save_fail_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
    //        {
    //            if (e.Button == MouseButtons.Right)
    //            {
    //                gv_save_fail.SelectRow(e.RowHandle);
    //                id_keep_to_delet = int.Parse(gv_save_fail.GetRowCellValue(gv_save_fail.FocusedRowHandle, gv_save_fail.Columns[0]).ToString());

    //            }
    //        }
    //           
    //        private void load_fail_table()
    //        {
    //            if (id_pers != 0)
    //            {
    //                delet_this_month_from_T_FAIL();


    //                //جدول التقصير
    //                string s4 = @" SELECT        dbo.T_PERS_FAIL.id AS المعرف, dbo.T_PERS_FAIL.pers_id AS [رقم الشخص], dbo.T_PERSONE.name AS [اسم الشخص], dbo.T_PERS_FAIL.in_date AS [تاريخ الإدخال], 
    //                         dbo.T_PERS_FAIL.month_fail AS [شهر التقصير], dbo.T_PERS_FAIL.year_fail AS [سنة التقصير], dbo.T_PERS_FAIL.reson AS السبب
    //FROM            dbo.T_PERS_FAIL INNER JOIN
    //                         dbo.T_PERSONE ON dbo.T_PERS_FAIL.pers_id = dbo.T_PERSONE.id
    //WHERE        (dbo.T_PERS_FAIL.pers_id =" + id_pers + ") ";
    //                dt = c_db.select(s4);
    //                gc_save_fail.DataSource = dt;
    //                gv_save_fail.Columns[0].Visible = false;
    //                gv_save_fail.Columns[1].Visible = false;
    //            }
    //            else
    //            {
    //                delet_this_month_from_T_FAIL();
    //                //جدول التقصير
    //                string s4 = @" SELECT        dbo.T_PERS_FAIL.id AS المعرف, dbo.T_PERS_FAIL.pers_id AS [رقم الشخص], dbo.T_PERSONE.name AS [اسم الشخص], dbo.T_PERS_FAIL.in_date AS [تاريخ الإدخال], 
    //                         dbo.T_PERS_FAIL.month_fail AS [شهر التقصير], dbo.T_PERS_FAIL.year_fail AS [سنة التقصير], dbo.T_PERS_FAIL.reson AS السبب
    //FROM            dbo.T_PERS_FAIL INNER JOIN
    //                         dbo.T_PERSONE ON dbo.T_PERS_FAIL.pers_id = dbo.T_PERSONE.id ";
    //                dt = c_db.select(s4);
    //                gc_save_fail.DataSource = dt;
    //                gv_save_fail.Columns[0].Visible = false;
    //                gv_save_fail.Columns[1].Visible = false;
    //            }
    //        }
    //        private void delet_this_month_from_T_FAIL()
    //        {
    //            //حذف معلومات الشهر الحالي من جدول الفيل
    //            c_db.insert_upadte_delete(@" delete from  dbo.T_PERS_FAIL 
    //                   WHERE( month_fail = N'" + d.Month + "' and year_fail = N'" + d.Year + "' )");
    //        }

    //        private void load_fail_table(int m , int y)
    //        {
    //            //حذف معلومات الشهر الحالي من جدول الفيل
    //            c_db.insert_upadte_delete(@" delete from  dbo.T_PERS_FAIL 
    //                   WHERE( month_fail = N'" + DateTime.Today.Month + "' and year_fail = N'" + DateTime.Today.Year + "' )");

    //            //جدول التقصير
    //            string s4 = @" SELECT        dbo.T_PERS_FAIL.id AS المعرف, dbo.T_PERS_FAIL.pers_id AS [رقم الشخص], dbo.T_PERSONE.name AS [اسم الشخص], dbo.T_PERS_FAIL.in_date AS [تاريخ الإدخال], 
    //                         dbo.T_PERS_FAIL.month_fail AS [شهر التقصير], dbo.T_PERS_FAIL.year_fail AS [سنة التقصير], dbo.T_PERS_FAIL.reson AS السبب
    //FROM            dbo.T_PERS_FAIL INNER JOIN
    //                         dbo.T_PERSONE ON dbo.T_PERS_FAIL.pers_id = dbo.T_PERSONE.id
    //WHERE        (month_fail = N'" + m + "') AND (year_fail = N'" + y + "') "; 
    //            dt = c_db.select(s4);
    //            gc_save_fail.DataSource = dt;
    //        }     
    //        private void save_in_fail_table()
    //        {
    //            int done = 0;
    //            if (id_pers==0)
    //            {
    //                try
    //                {
    //                    delet_this_month_from_T_FAIL();

    //                }
    //                catch (Exception)
    //                {

    //                    throw;
    //                }

    //                try
    //                {
    //                    //ادخال معلومات جدول المقصرين من جدول عدد الصفحات 
    //                    string sqll = @"INSERT INTO dbo.T_PERS_FAIL
    //                         ( pers_id, in_date, month_fail, year_fail)
    //             SELECT        dbo.V_fail.pers_hafez_id , '" + "1-1-2023" + "' ,in_month , in_yeare " +
    //                     " FROM             dbo.V_fail " +
    //                     "   where  (in_month = " + m + ") AND (in_yeare = " + y + ") ";
    //                    done += c_db.insert_upadte_delete(sqll);
    //                }
    //                catch (Exception)
    //                {

    //                    throw;
    //                }

    //                try
    //                {
    //                    //ادخال معلومات الغائبين
    //                    for (int i = 0; i < dt_abcent.Rows.Count; i++)
    //                    {
    //                        done += c_db.insert_upadte_delete(@"INSERT INTO dbo.T_PERS_FAIL
    //                         ( pers_id, in_date, month_fail, year_fail,reson)
    //                   values ( " + dt_abcent.Rows[i][0].ToString() + " ," +
    //                             "  '" + "1-1-2023" + "' , '" + m + "' , '" + y + "' , '" + "غائب" + "')");

    //                        //حذف المعلومات المكررة من جدول ال المقصرين
    //                        string sql2 = @" delete from dbo.T_PERS_FAIL where id in ( select id from dbo.T_PERS_FAIL t 
    //                          where exists ( select id from dbo.T_PERS_FAIL
    //                                where pers_id =t.pers_id 
    //                          and month_fail = t.month_fail 
    //                          and year_fail=t.year_fail
    //                             and id < t.id ))";
    //                        int done2 = c_db.insert_upadte_delete(sql2);
    //                        if (done != 0)
    //                        {
    //                            load_data("i");
    //                        }
    //                    }
    //                }
    //                catch (Exception)
    //                {

    //                    throw;
    //                }
    //            }


    //            load_fail_table();

    //        }




    //        private void de_month_rep_EditValueChanged(object sender, EventArgs e)
    //        {
    //            m = de_month_rep.DateTime.Month;
    //            y = de_month_rep.DateTime.Year;
    //            date_chose = de_month_rep.DateTime;
    //        }

    //        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
    //        {

    //        }




}


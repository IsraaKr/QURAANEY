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
        DataTable dt;
        DataTable dt_abcent;
        DataTable dt_fail;
        int id_pers=0;
        DateTime d = DateTime.Today;
        int selected_month, selected_yeare;
        DateTime date_chose;
        int m = 0;
        int y = 0;
        public static bool is_double_click = false;
        public static bool is_show_click = false;
        int id_doublee_click= 0;
        public F_FAIL_PERS()
        {
            InitializeComponent();
            load_data("");

        }
        public F_FAIL_PERS(int id)
        {
            id_pers = id;
            InitializeComponent();
            load_data("");  
        }
        private void F_FAIL_PERS_Load(object sender, EventArgs e)
        {
            if (id_pers != 0)
                lkp_name.EditValue = id_pers;
            if (de_month_rep.Text == string.Empty)
                load_fail_table();
        }
        public override void load_data(string status_mess)
        {
            clear(this.Controls);
            dt = C_DB_QUERYS.get_person_name_isactive();
            lkp_name.lkp_iniatalize_data(dt, "name", "id");
            load_fail_table();
            is_double_click = false;
            is_show_click = false;
            base.load_data(status_mess);
        }
        public override void save()
        {
            if (is_double_click == true)
            {
                c_db.insert_upadte_delete(@"UPDATE       dbo.T_PERS_FAIL
                             SET                reson ='"+txt_reson.Text+"'" +
                                "WHERE        (id = "+id_doublee_click+")");
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
                base.save();
        }
        public override void clear(Control.ControlCollection s_controls)
        {
            de_month_rep.Text = string.Empty;
          //  gc_absend.DataSource = null;
            gv_absend.Columns.Clear();
          //  gc_absend.DataSource = null;
            gv_absend.Columns.Clear();
            base.clear(s_controls);
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
        public override void show()
        {
            is_show_click = true;
            if (de_month_rep.Text==string.Empty)
            {
                MessageBox.Show("يجب اختيار شهر لعرضه", "تنبيه",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            load_gc_snd_tail();
            //try
            //{
            //    string query = "where ";
            //    gv_absend.Columns.Clear();

            //    if (lkp_name.Text != "")
            //    {
            //        if (lkp_name.Text == "لايوجد")
            //        {
            //            query += "Name1 = N' ' AND ";
            //        }
            //        else
            //        {
            //            query += "Name1 = N'" + lkp_name.Text + "' AND ";
            //        }
            //    }

            //    if (de_month_rep.Text != "")
            //    {
            //        if (de_month_rep.Text == "لايوجد")
            //        {
            //            query += "Name2 = N' ' AND ";
            //        }
            //        else
            //        {
            //            query += "date = convert(datetime,'" + de_month_rep.Text + "',105) AND ";
            //        }
            //    }


            //    query = query.Remove(query.Length - 4);

            //    DataTable ds = new DataTable();
            //    ds = c_db.select("select * from table " + query);
            //    gc_absend.DataSource = ds;
            //    query = "";
            //  //  SET_HEADERCOLUMNS_TABLE();

            //}
            //catch
            //{
            //}
            base.show();
        }
        private void load_gc_snd_tail()
        {
            if (id_pers==0)
            {
                 m = de_month_rep.DateTime.Month;
                 y = de_month_rep.DateTime.Year;
                date_chose = de_month_rep.DateTime;

                //التسميع بالشهر و السنة
                string s = @"SELECT   pers_hafez_id   
                           FROM         T_SOURA_KEEP
                    WHERE     ( MONTH (keep_date) = N'" + m + "') " +
                        " AND ( year (keep_date) = N'" + y + "')   " +
                        " group by  pers_hafez_id ";

                dt = c_db.select(s);
                //gc_keep.DataSource = dt;

                //الغائبين
                string s2 = @"SELECT     id
                               FROM         T_PERSONE
                             WHERE      id NOT IN (" + s + " ) " +
                       " AND (in_date <= CONVERT(DATETIME , '" + y + "-" + m + "-28" + "', 102)) " +
                       " AND (is_active = 1) ";

                dt_abcent = c_db.select(s2);
                gc_absend.DataSource = dt_abcent;

//                //معدل حفظ كل شخص
//                string s3 = @" SELECT dbo.T_PERSONE.id, dbo.T_PERSONE.name, dbo.T_PERS_RATE_KEEP.name AS rate_name, dbo.T_PERS_RATE_KEEP.num, dbo.T_PERS_RATE_KEEP.rate_in_days, 
//                      dbo.T_PERS_RATE_KEEP_CHANGE.change_date
//FROM         dbo.T_PERSONE INNER JOIN
//                      dbo.T_PERS_RATE_KEEP_CHANGE ON dbo.T_PERSONE.id = dbo.T_PERS_RATE_KEEP_CHANGE.pers_id INNER JOIN
//                      dbo.T_PERS_RATE_KEEP ON dbo.T_PERS_RATE_KEEP_CHANGE.rate_id = dbo.T_PERS_RATE_KEEP.id";
//                dt = c_db.select(s3);
//                gc_rate.DataSource = dt;

                //المقصرين  عرض 
                string sqll = @" SELECT        name, count_page, in_month, in_yeare, rate_name, num, rate_in_days, pers_hafez_id, id
FROM            dbo.V_fail
WHERE        (in_month = N'" + m + "') AND (in_yeare = N'" + y + "') ";
                dt_fail = c_db.select(sqll);
                gc_show_fail.DataSource = dt_fail;

                load_fail_table();

            }
            //            else
            //            {
            //                string sqll = @" SELECT        dbo.V_fail.*, pers_hafez_id AS Expr1
            //FROM            dbo.V_fail
            //WHERE        (pers_hafez_id = "+lkp_name.EditValue+")  ";
            //                dt = c_db.select(sqll);
            //                gc_absend.DataSource = dt;
            //            }
        }
        private void load_fail_table()
        {
            //جدول التقصير
            string s4 = @" SELECT id, pers_id, in_date, month_fail, year_fail, reson
                 FROM dbo.T_PERS_FAIL ";
            dt = c_db.select(s4);
            gc_save_fail.DataSource = dt;
        }
        private void load_fail_table(int m , int y)
        {
            //جدول التقصير
            string s4 = @" SELECT id, pers_id, in_date, month_fail, year_fail, reson
                 FROM dbo.T_PERS_FAIL 
WHERE        (month_fail = N'" + m + "') AND (year_fail = N'" + y + "') "; 
            dt = c_db.select(s4);
            gc_save_fail.DataSource = dt;
        }          
        private void save_in_fail_table()
        {
            int done = 0;
            try
            {
                //حذف معلومات الشهر الحالي من جدول الفيل
                c_db.insert_upadte_delete(@" delete from  dbo.T_PERS_FAIL 
                   WHERE( month_fail = N'" + m + "' and year_fail = N'" + y + "' )");

            }
            catch (Exception)
            {

                throw;
            }
           
            try
            {
                //ادخال معلومات جدول المقصرين من جدول عدد الصفحات 
                string sqll = @"INSERT INTO dbo.T_PERS_FAIL
                         ( pers_id, in_date, month_fail, year_fail)
             SELECT        dbo.V_fail.pers_hafez_id , '" + "1-1-2023" + "' ,in_month , in_yeare " +
                 " FROM             dbo.V_fail " +
                 "   where  (in_month = " + m + ") AND (in_yeare = " + y + ") ";
                done += c_db.insert_upadte_delete(sqll);
            }
            catch (Exception)
            {

                throw;
            }
        
            try
            {
                //ادخال معلومات الغائبين
                for (int i = 0; i < dt_abcent.Rows.Count; i++)
                {
                  done +=  c_db.insert_upadte_delete(@"INSERT INTO dbo.T_PERS_FAIL
                         ( pers_id, in_date, month_fail, year_fail,reson)
                   values ( " + dt_abcent.Rows[i][0].ToString() + " ," +
                       "  '" + "1-1-2023" + "' , '" + m + "' , '" + y + "' , '" + "غائب" + "')");

                    //////حذف المعلومات المكررة من جدول ال المقصرين
                    ////string sql2 = @" delete from dbo.T_PERS_FAIL where id in ( select id_fail from T_fail t 
                    ////  where exists ( select id_fail from T_fail
                    ////        where id_Person =t.id_Person 
                    ////  and month_fail = t.month_fail 
                    ////  and year_fail=t.year_fail
                    ////     and id_fail < t.id_fail ))";
                    ////int done2 = c_db.insert_upadte_delete(sql2);
                    if (done!=0)
                    {
                        load_data("i");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            load_fail_table();

        }
        private void de_month_rep_EditValueChanged(object sender, EventArgs e)
        {
            m = de_month_rep.DateTime.Month;
            y = de_month_rep.DateTime.Year;
            date_chose = de_month_rep.DateTime;
        }
        private void gv_save_fail_DoubleClick(object sender, EventArgs e)
        {
            is_double_click = true;
            group_names.Visibility =DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            id_doublee_click = int.Parse(gv_save_fail.GetRowCellValue(gv_save_fail.FocusedRowHandle, gv_save_fail.Columns[0]).ToString());
            lkp_name.EditValue =int.Parse( gv_save_fail.GetRowCellValue(gv_save_fail.FocusedRowHandle, gv_save_fail.Columns[1]).ToString());
            txt_reson.Text= gv_save_fail.GetRowCellValue(gv_save_fail.FocusedRowHandle, gv_save_fail.Columns[5]).ToString();
        }

        
       

    }
}

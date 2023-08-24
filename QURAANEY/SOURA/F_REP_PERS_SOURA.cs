using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QURAANEY.CLASS_TABLES;

namespace QURAANEY.SOURA
{
    public partial class F_REP_PERS_SOURA : F_INHERATENZ
    {
        public F_REP_PERS_SOURA()
        {
            view_inheretanz_butomes(false, false, false, false, true, false, true);

            InitializeComponent();
            load_data("");
        }
        public F_REP_PERS_SOURA(int id)
        {
            id_pers = id;
            InitializeComponent();
            load_data("");
        }
        DataTable dt;
        double count;
        string persent;
        int id_pers = 0;
        public override void print()
        {
            if (lkp_name.Text==string.Empty)
            {
                MessageBox.Show(" الرجاء اختيار اسم لطباعة معلوماته", "معلومات",
                MessageBoxButtons.OK , MessageBoxIcon.Information);
                return;
            }
            C_MASTER.print_header("تقرير " + lkp_name.Text, gc);
            base.print();
        }
        public override void load_data(string status_mess)
        {
            dt = C_DB_QUERYS.get_person_name();
            lkp_name.lkp_iniatalize_data(dt, "name", "id");
            base.load_data(status_mess);
        }
        private void load_all()
        {
            load_gc();
            load_tail_state();
            load_tail_keep_rate();
            load_tail_fail_count();
            load_tail_aya_keep();
            load_tail_page_keep();
            load_tail_soura_keep();
            load_chart();

        }

        private void load_gc()
        {
            soura_last_row_view();
        }

        private void load_tail_state()
        {
            //تحميل التيل الحالة 
            dt = C_PERS_STATE_sql.get_last_state_by_id(id_pers);
            if (dt.Rows.Count > 0)
            {
                ti_state.Elements[1].Text = dt.Rows[0][3].ToString();
            }
            else
                ti_state.Elements[1].Text = "...";
        }

        private void load_tail_keep_rate()
        {
            //تحميل التيل معدل الحفظ 
            dt = C_DB_QUERYS.rate_by_id(Convert.ToInt32(lkp_name.EditValue));
            if (dt.Rows.Count > 0)
            {
                ti_keep_rate.Elements[1].Text = dt.Rows[0][0].ToString();
            }
            else
                ti_keep_rate.Elements[1].Text = "...";
        }

        private void load_tail_aya_keep()
        {
            //تحميل التيل نسبة حفظ الآيات 
            dt = C_DB_QUERYS.aya_rate_by_id(Convert.ToInt32(lkp_name.EditValue));
            count = ((Convert.ToInt32(dt.Rows[0][1].ToString()) * 0.1) / 6326) / 0.1;
            persent = count.ToString("p", CultureInfo.InvariantCulture);
            if (dt.Rows.Count > 0)
            {
                ti_aya.Elements[1].Text = persent;
            }
            else
                ti_aya.Elements[1].Text = "...";
        }

        private void load_tail_fail_count()
        {
            //تحميل التيل مرات التقصير 
            dt = C_DB_QUERYS.fail_by_id(Convert.ToInt32(lkp_name.EditValue));
            if (dt.Rows.Count > 0)
            {
                ti_fail.Elements[1].Text = dt.Rows[0][1].ToString();
            }
            else
                ti_fail.Elements[1].Text = "...";
        }

        private void load_tail_page_keep()
        {
            //تحميل التيل نسبة حفظ الصفحات 
            dt = C_DB_QUERYS.full_page_count_by_id(Convert.ToInt32(lkp_name.EditValue));
            count = ((Convert.ToInt32(dt.Rows[0][2].ToString()) * 0.1) / 604) / 0.1;
            persent = count.ToString("p", CultureInfo.InvariantCulture);
            if (dt.Rows.Count > 0)
            {
                ti_pages.Elements[1].Text = persent;
            }
            else
                ti_pages.Elements[1].Text = "...";
        }

        private void load_tail_soura_keep()
        {
            //تحميل التيل نسبة حفظ السور 
            dt = C_DB_QUERYS.soura_rate_by_id(Convert.ToInt32(lkp_name.EditValue));
            count = ((Convert.ToInt32(dt.Rows[0][2].ToString()) * 0.1) / 114) / 0.1;
            persent = count.ToString("p", CultureInfo.InvariantCulture);
            if (dt.Rows.Count > 0)
            {
                ti_soura.Elements[1].Text = persent;
            }
            else
                ti_soura.Elements[1].Text = "...";
        }


        private void lkp_name_EditValueChanged(object sender, EventArgs e)
        {
            if (int.Parse(lkp_name.EditValue.ToString()) > 0)
            {
                load_all();
                rdb_page_view.Enabled = true;
                rdb_aya_view.Enabled = true;
                rdb_soura_view.Enabled = true;
            }
            else
                return;
        }

        private void tileItem8_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {

        }

        private void F_REP_PERS_SOURA_Load(object sender, EventArgs e)
        {
            if (id_pers != 0)
                lkp_name.EditValue = id_pers;

        }

        private void load_chart()
        {

            ch_page.Series["Series1"].Points.Clear();
            DataTable dt_page = C_DB_QUERYS.full_page_count_by_id(Convert.ToInt32(lkp_name.EditValue));
            if (dt_page.Rows.Count != 0)
            {
                int y = 604 - int.Parse(dt_page.Rows[0][2].ToString());

                ch_page.Series["Series1"].Points.AddXY("الصفحات المحفوظة", int.Parse(dt_page.Rows[0][2].ToString()));
                ch_page.Series["Series1"].Points.AddXY("الباقي", y);
                ch_page.Series["Series1"].Points[0].Color = Color.SteelBlue;
                ch_page.Series["Series1"].Points[1].Color = Color.LightGray;
                ///////
            }
            else
            {
                ch_page.Series["Series1"].Points.AddXY("الصفحات المحفوظة", 0);
                ch_page.Series["Series1"].Points.AddXY(" الباقي", 604);
                ch_page.Series["Series1"].Points[0].Color = Color.SteelBlue;
                ch_page.Series["Series1"].Points[1].Color = Color.LightGray;
            }

        }

     
        private void load_line()
        {
            ch_p.Series["Series1"].Points.Clear();
            DataTable dt_page = c_db.select(@"SELECT        count_page
                FROM            dbo.V_COUNT_PAGE_WITHE_DATE
            WHERE(pers_hafez_id = " + lkp_name.EditValue + ")");
            if (dt_page.Rows.Count != 0)
            {
                int y = 6400 - int.Parse(dt_page.Rows[0][0].ToString());
             
                ch_p.Series["Series1"].Points.AddXY("الآيات المحفوظة", int.Parse(dt_page.Rows[0][0].ToString()));
                ch_p.Series["Series1"].Points.AddXY("الباقي", y);
                ch_p.Series["Series1"].Points[0].Color = Color.SteelBlue;
                ch_p.Series["Series1"].Points[1].Color = Color.LightGray;
                ///////
            }
            else
            {
                ch_p.Series["Series1"].Points.AddXY("الآيات المحفوظة", 0);
                ch_p.Series["Series1"].Points.AddXY(" الباقي", 6400);
                ch_p.Series["Series1"].Points[0].Color = Color.SteelBlue;
                ch_p.Series["Series1"].Points[1].Color = Color.LightGray;
            }
        }

        private void ti_fail_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            //F_FAIL_PERS f = new F_FAIL_PERS(int.Parse(lkp_name.EditValue.ToString()));
            //f.WindowState = FormWindowState.Maximized;
            //f.Show();

        }

        private void soura_last_row_view()
        {
            string sqll = @" SELECT     T_PERSONE.id AS تسلسل, T_PERSONE.name AS الحافظ, T_SOURA_KEEP.soura_name AS [اسم السورة], T_SOURA_KEEP.page_num AS [رقم آخر الصفحة], 
                      T_SOURA_KEEP.keep_date AS التاريخ, T_SOURA_KEEP_TYPE.name AS [نوع الحفظ], T_SOURA_EVALUATION.name AS التقيم, T_PERSONE_1.name AS المستلم
FROM         T_PERSONE INNER JOIN
                      T_SOURA_KEEP ON T_PERSONE.id = T_SOURA_KEEP.pers_hafez_id INNER JOIN
                      T_SOURA_KEEP_TYPE ON T_SOURA_KEEP.keep_type_id = T_SOURA_KEEP_TYPE.id INNER JOIN
                      T_SOURA_EVALUATION ON T_SOURA_KEEP.evaluation_id = T_SOURA_EVALUATION.id INNER JOIN
                      T_PERSONE AS T_PERSONE_1 ON T_SOURA_KEEP.pers_mustalem_id = T_PERSONE_1.id
WHERE     (T_SOURA_KEEP.pers_hafez_id  = " + Convert.ToInt32(lkp_name.EditValue) + " ) " +
"  GROUP BY T_PERSONE.id, T_PERSONE.name, T_SOURA_KEEP.soura_name, T_SOURA_KEEP.page_num, T_SOURA_KEEP.keep_date, T_SOURA_KEEP_TYPE.name, " +
"   T_SOURA_EVALUATION.name, T_PERSONE_1.name  " +
"  HAVING      (T_SOURA_KEEP.page_num IN  " +
"  (SELECT     MAX(page_num) AS Expr1  " +
"  FROM         T_SOURA_KEEP AS keep2  " +
"  GROUP BY soura_num, pers_hafez_id)) ";


            dt = c_db.select(sqll);
        }

        private void page_last_row_view()
        {
            string sqll = @" 
  SELECT     T_PERSONE.id AS تسلسل, T_PERSONE.name AS الحافظ, T_SOURA_KEEP.soura_name AS [اسم السورة], T_SOURA_KEEP.page_num AS [رقم آخر الصفحة], 
 T_SOURA_KEEP.keep_date AS التاريخ, T_SOURA_KEEP_TYPE.name AS [نوع الحفظ], T_SOURA_EVALUATION.name AS التقيم, T_PERSONE_1.name AS المستلم , T_SOURA_KEEP.aya_num 
FROM         T_PERSONE INNER JOIN
                      T_SOURA_KEEP ON T_PERSONE.id = T_SOURA_KEEP.pers_hafez_id INNER JOIN
                      T_SOURA_KEEP_TYPE ON T_SOURA_KEEP.keep_type_id = T_SOURA_KEEP_TYPE.id INNER JOIN
                      T_SOURA_EVALUATION ON T_SOURA_KEEP.evaluation_id = T_SOURA_EVALUATION.id INNER JOIN
                      T_PERSONE AS T_PERSONE_1 ON T_SOURA_KEEP.pers_mustalem_id = T_PERSONE_1.id
WHERE (T_SOURA_KEEP.aya_num IN (SELECT     MAX(aya_num) AS Expr1
                             FROM         T_SOURA_KEEP AS keep2
                             where keep2.pers_hafez_id= " + Convert.ToInt32(lkp_name.EditValue) + " " +
                             " GROUP BY   keep2.page_num ))" +
                             "and T_SOURA_KEEP.pers_hafez_id= " + Convert.ToInt32(lkp_name.EditValue) + "" +
                             "ORDER BY T_SOURA_KEEP.page_num DESC ";


            dt = c_db.select(sqll);
        }

        private void aya_view()
        {
            string sqll = @" SELECT  T_SOURA_KEEP.id , T_SOURA_KEEP.soura_name AS [اسم السورة],  T_SOURA_KEEP.page_num AS [رقم الصفحة] , 
                      T_SOURA_KEEP.keep_date AS التاريخ, T_SOURA_KEEP_TYPE.name AS [نوع الحفظ], T_SOURA_EVALUATION.name AS التقيم, T_PERSONE_1.name AS المستلم
FROM         T_PERSONE INNER JOIN
                      T_SOURA_KEEP ON T_PERSONE.id = T_SOURA_KEEP.pers_hafez_id INNER JOIN
                      T_SOURA_KEEP_TYPE ON T_SOURA_KEEP.keep_type_id = T_SOURA_KEEP_TYPE.id INNER JOIN
                      T_SOURA_EVALUATION ON T_SOURA_KEEP.evaluation_id = T_SOURA_EVALUATION.id INNER JOIN
                      T_PERSONE AS T_PERSONE_1 ON T_SOURA_KEEP.pers_mustalem_id = T_PERSONE_1.id
WHERE     (T_SOURA_KEEP.pers_hafez_id = " + Convert.ToInt32(lkp_name.EditValue) + " )" +
" ORDER BY T_SOURA_KEEP.id DESC  ";


            dt = c_db.select(sqll);
        }

        private void rdb_aya_view_CheckedChanged(object sender, EventArgs e)
        {
            if (lkp_name.Text != string.Empty && rdb_aya_view.Checked)
            {
                aya_view();
                gc.DataSource = null;
                gv.Columns.Clear();
                gc.DataSource = dt;
                gv.Columns[0].Visible = false;
                gv.BestFitColumns();

            }
        }

        private void rdb_page_view_CheckedChanged(object sender, EventArgs e)
        {
            if (lkp_name.Text != string.Empty && rdb_page_view.Checked)
            {
                page_last_row_view();
                gc.DataSource = null;
                gv.Columns.Clear();
                gc.DataSource = dt;
                gv.Columns[0].Visible = false;
                gv.Columns[1].Visible = false;
                gv.BestFitColumns();
            }
        }

        private void rdb_soura_view_CheckedChanged(object sender, EventArgs e)
        {
            if (lkp_name.Text != string.Empty && rdb_soura_view.Checked)
            {
                soura_last_row_view();
                gc.DataSource = null;
                gv.Columns.Clear();
                gc.DataSource = dt;
                gv.Columns[0].Visible = false;
                gv.Columns[1].Visible = false;
                gv.BestFitColumns();

            }
        }
    }
}

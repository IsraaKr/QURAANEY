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

namespace QURAANEY.SOURA
{
    public partial class F_REP_PERS_SOURA :F_INHERATENZ
    {
        public F_REP_PERS_SOURA()
        {
            InitializeComponent();
            load_data("");
        }
        public F_REP_PERS_SOURA( int id)
        {
            id_pers = id;
            InitializeComponent();
            load_data("");
        }
        DataTable dt;
        double count;
        string persent;
        int id_pers=0;
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
            string sqll = @"SELECT        T_SOURA_KEEP.soura_name AS [اسم السورة], T_SOURA_KEEP.aya_num AS [رقم الآية], T_SOURA_KEEP.page_num AS [رقم الصفحة], 
                         T_SOURA_KEEP.keep_date AS التاريخ, T_SOURA_KEEP_TYPE.name AS [نوع الحفظ], T_SOURA_EVALUATION.name AS التقيم, T_PERSONE_1.name AS المستلم
FROM            dbo.T_PERSONE INNER JOIN
                         dbo.T_SOURA_KEEP ON T_PERSONE.id = T_SOURA_KEEP.pers_hafez_id INNER JOIN
                         dbo.T_SOURA_KEEP_TYPE ON T_SOURA_KEEP.keep_type_id = T_SOURA_KEEP_TYPE.id INNER JOIN
                         dbo.T_SOURA_EVALUATION ON T_SOURA_KEEP.evaluation_id = T_SOURA_EVALUATION.id INNER JOIN
                         dbo.T_PERSONE AS T_PERSONE_1 ON T_SOURA_KEEP.pers_mustalem_id = T_PERSONE_1.id
                       WHERE     (T_SOURA_KEEP.pers_hafez_id = " + Convert.ToInt32(lkp_name.EditValue) + ")";
            dt = c_db.select(sqll);
            gc.DataSource = dt;
        }

        private void load_tail_state()
        {
            //تحميل التيل الحالة 
            dt = C_DB_QUERYS.state_by_id(Convert.ToInt32(lkp_name.EditValue));
            if (dt.Rows.Count > 0)
            {
                ti_state.Elements[1].Text = dt.Rows[0][0].ToString();
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
            count = ((Convert.ToInt32(dt.Rows[0][1].ToString()) * 0.1) / 6500) / 0.1;
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
            dt = C_DB_QUERYS.page_rate_by_id(Convert.ToInt32(lkp_name.EditValue));
            count = ((Convert.ToInt32(dt.Rows[0][1].ToString()) * 0.1) / 604) / 0.1;
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
            count = ((Convert.ToInt32(dt.Rows[0][1].ToString()) * 0.1) / 114) / 0.1;
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
                load_all();
            else
                return;
        }

        private void tileItem8_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {

        }

        private void F_REP_PERS_SOURA_Load(object sender, EventArgs e)
        {
            if (id_pers !=0)
                lkp_name.EditValue = id_pers;

        }

        private void load_chart()
        {

            ch_page.Series["Series1"].Points.Clear();
           DataTable dt_page = c_db.select(@"SELECT        count_page
                FROM            dbo.V_COUNT_PAGE_WITHE_DATE
            WHERE(pers_hafez_id = " + lkp_name.EditValue + ")");
            if (dt_page.Rows.Count != 0)
            {
                int y = 604 - int.Parse(dt_page.Rows[0][0].ToString());

                ch_page.Series["Series1"].Points.AddXY("الصفحات المحفوظة", int.Parse(dt_page.Rows[0][0].ToString()));
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

        private void ti_fail_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            F_FAIL_PERS f = new F_FAIL_PERS( int.Parse(lkp_name.EditValue.ToString()));
            f.WindowState = FormWindowState.Maximized;
            f.Show();

        }
    }
}

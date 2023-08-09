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
    public partial class F_REP_SOURA : F_INHERATENZ
    {
        public F_REP_SOURA()
        {
            InitializeComponent();
            view_inheretanz_butomes(false, false, false, false, true, false, true);
            load_gc();
            load_data("");
            all_queary();
        }
        DataTable dt;
        string select=" SELECT ";
        string from =" FROM ";
        string where=" WHERE ( ";
        string group=" group by ";
        string having=" having ";
        string sql;

        private void load_gc()
        {
            string sqll = @" SELECT  T_PERSONE.id AS تسلسل  , T_PERSONE.name AS الحافظ, T_SOURA_KEEP.soura_name AS [اسم السورة], T_SOURA_KEEP.aya_num AS [رقم الآية], T_SOURA_KEEP.page_num AS [رقم الصفحة], 
                      T_SOURA_KEEP.keep_date AS التاريخ, T_SOURA_KEEP_TYPE.name AS [نوع الحفظ], T_SOURA_EVALUATION.name AS التقيم, T_PERSONE_1.name AS المستلم
FROM         T_PERSONE INNER JOIN
                      T_SOURA_KEEP ON T_PERSONE.id = T_SOURA_KEEP.pers_hafez_id INNER JOIN
                      T_SOURA_KEEP_TYPE ON T_SOURA_KEEP.keep_type_id = T_SOURA_KEEP_TYPE.id INNER JOIN
                      T_SOURA_EVALUATION ON T_SOURA_KEEP.evaluation_id = T_SOURA_EVALUATION.id INNER JOIN
                      T_PERSONE AS T_PERSONE_1 ON T_SOURA_KEEP.pers_mustalem_id = T_PERSONE_1.id";
            dt = c_db.select(sqll);
            gc.DataSource = dt;

        }
        public override void print()
        {
            C_MASTER.print_header("التقرير الشامل ", gc);
            base.print();
        }
        public override void load_data(string status_mess)
        {
            //lkp السور
            dt = c_db.select(@"SELECT DISTINCT soura_name, soura_num
                              FROM         T_SOURA
                               ORDER BY soura_num");
            chb_comb_soura.chb_comb_iniatalize_data(dt, "soura_name", "soura_num");
          
            base.load_data("");
        }
        private void all_queary()
        {
            select+= @"T_PERSONE.id AS تسلسل  , T_PERSONE.name AS الحافظ, T_SOURA_KEEP.soura_name AS [اسم السورة], T_SOURA_KEEP.aya_num AS [رقم الآية], T_SOURA_KEEP.page_num AS [رقم الصفحة], 
                      T_SOURA_KEEP.keep_date AS التاريخ, T_SOURA_KEEP_TYPE.name AS[نوع الحفظ], T_SOURA_EVALUATION.name AS التقيم, T_PERSONE_1.name AS المستلم";

            from += @" T_PERSONE INNER JOIN
                      T_SOURA_KEEP ON T_PERSONE.id = T_SOURA_KEEP.pers_hafez_id INNER JOIN
                      T_SOURA_KEEP_TYPE ON T_SOURA_KEEP.keep_type_id = T_SOURA_KEEP_TYPE.id INNER JOIN
                      T_SOURA_EVALUATION ON T_SOURA_KEEP.evaluation_id = T_SOURA_EVALUATION.id INNER JOIN
                      T_PERSONE AS T_PERSONE_1 ON T_SOURA_KEEP.pers_mustalem_id = T_PERSONE_1.id";
            where += 
            sql = select + from;
   

        }
        #region تسلسل التفعيل و ايقاف التفعيل لخيارات البحث
        private void chb_date_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_date.Checked)
            { 
                rdb_all.Enabled = true;
                rdb_month.Enabled = true;
                rdb_month_between.Enabled = true;
                chb_comb_mustalem.Enabled = false;
                chb_comb_soura.Enabled = false;
                chb_comb_rate.Enabled = false;
            }
        }

        private void chb_soura_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_soura.Checked)
            {
                rdb_all.Enabled = false;
                rdb_month.Enabled = false;
                rdb_month_between.Enabled = false;
                chb_comb_mustalem.Enabled = false;
                chb_comb_soura.Enabled = true;
                chb_comb_rate.Enabled = false;
            }
        }

        private void chb_mustalem_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_mustalem.Checked)
            {
                rdb_all.Enabled = false;
                rdb_month.Enabled = false;
                rdb_month_between.Enabled = false;
                chb_comb_mustalem.Enabled = true;
                chb_comb_soura.Enabled = false;
                chb_comb_rate.Enabled = false;
            }
        }

        private void chb_rate_CheckedChanged(object sender, EventArgs e)
        {
            if (chb_rate.Checked)
            {
                rdb_all.Enabled = false;
                rdb_month.Enabled = false;
                rdb_month_between.Enabled = false;
                chb_comb_mustalem.Enabled = false;
                chb_comb_soura.Enabled = false;
                chb_comb_rate.Enabled = true;
            }
        }

        private void rdb_month_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_month.Checked)
            {
                de_month.Enabled = true;
                dtp_from.Enabled = false;
                dtp_to.Enabled = false;
            }
        }

        private void rdb_month_between_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_month_between.Checked)
            {
                de_month.Enabled = false;
                dtp_from.Enabled = true;
                dtp_to.Enabled = true;
            }
        }

        private void rdb_all_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb_all.Checked)
            {
                de_month.Enabled = false;
                dtp_from.Enabled = false;
                dtp_to.Enabled = false;
            }
        }

        #endregion

        private void chb_comb_soura_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Caption=="OK")
            {
                MessageBox.Show(""+chb_comb_soura.EditValue);
            }
        }
        private void soura()
        {
            for (int i = 0; i < chb_comb_soura.Properties.Items.GetCheckedValues().Count(); i++)
            {
                int id_soura = int.Parse(chb_comb_soura.Properties.Items.GetCheckedValues()[i].ToString());
                where += " T_SOURA_KEEP.soura_num = "+id_soura+" and  ) ";
            }
        }
        private void chb_comb_soura_EditValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chb_comb_soura.Properties.Items.GetCheckedValues().Count(); i++)
            {
                int id_soura = int.Parse(chb_comb_soura.Properties.Items.GetCheckedValues()[i].ToString());
                where += " T_SOURA_KEEP.soura_num = " + id_soura + " and  ) ";

            }
        }

        private void btn_add_fail_Click(object sender, EventArgs e)
        {

            F_FAIL_PERS f = new F_FAIL_PERS();
            f.WindowState = FormWindowState.Maximized ;
            f.Show();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                gc.DataSource = null;
                gv.Columns.Clear();

                string s4 = @" SELECT        dbo.T_PERS_FAIL.id AS المعرف, dbo.T_PERS_FAIL.pers_id AS [رقم الشخص], dbo.T_PERSONE.name AS [اسم الشخص], dbo.T_PERS_FAIL.in_date AS [تاريخ الإدخال], 
                         dbo.T_PERS_FAIL.month_fail AS [شهر التقصير], dbo.T_PERS_FAIL.year_fail AS [سنة التقصير], dbo.T_PERS_FAIL.reson AS السبب
FROM            dbo.T_PERS_FAIL INNER JOIN
                         dbo.T_PERSONE ON dbo.T_PERS_FAIL.pers_id = dbo.T_PERSONE.id ";
                dt = c_db.select(s4);
            }
        }
    }
}

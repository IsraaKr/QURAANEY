using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QURAANEY.CLASS_TABLES;

namespace QURAANEY.SOURA
{
    public partial class F_Rep_Soura2 : F_INHERATENZ
    {
        DataTable dt;
        string from ;
        string sqll = @" SELECT  T_PERSONE.id AS تسلسل  , T_PERSONE.name AS الحافظ, T_SOURA_KEEP.soura_name AS [اسم السورة], T_SOURA_KEEP.aya_num AS [رقم الآية], T_SOURA_KEEP.page_num AS [رقم الصفحة], 
                      T_SOURA_KEEP.keep_date AS التاريخ, T_SOURA_KEEP_TYPE.name AS [نوع الحفظ], T_SOURA_EVALUATION.name AS التقيم, T_PERSONE_1.name AS المستلم
FROM         T_PERSONE INNER JOIN
                      T_SOURA_KEEP ON T_PERSONE.id = T_SOURA_KEEP.pers_hafez_id INNER JOIN
                      T_SOURA_KEEP_TYPE ON T_SOURA_KEEP.keep_type_id = T_SOURA_KEEP_TYPE.id INNER JOIN
                      T_SOURA_EVALUATION ON T_SOURA_KEEP.evaluation_id = T_SOURA_EVALUATION.id INNER JOIN
                      T_PERSONE AS T_PERSONE_1 ON T_SOURA_KEEP.pers_mustalem_id = T_PERSONE_1.id";
        public F_Rep_Soura2()
        {
            InitializeComponent();
            view_inheretanz_butomes(false, false, false, false, true, false, true);
            load_gc(sqll);
        }
        public override void print()
        {
            C_MASTER.print_header("التقرير الشامل ", gc);
            base.print();
        }
        public override void load_data(string status_mess)
        {
            //lkp السور
            //dt = c_db.select(@"SELECT DISTINCT soura_name, soura_num
            //                  FROM         T_SOURA
            //                   ORDER BY soura_num");
            //chb_comb_soura.chb_comb_iniatalize_data(dt, "soura_name", "soura_num");


            dt = C_EVALUATION.get_all_evaluation();
            lkp_evaluation.lkp_iniatalize_data(dt,"name","id");

            dt = C_SOURA_sql.get_soura_name();
            lkp_soura.lkp_iniatalize_data(dt, "soura_name", "soura_num");

            dt = C_SOURA_sql.get_mustalem_name();
            lkp_mustalem.lkp_iniatalize_data(dt, "name", "id");

            dt = C_KEEP_TYPE.get_all_keep_type();
            lkp_keep_type.lkp_iniatalize_data(dt, "name", "id");


            dt = C_PERSON_sql.get_pers_id_name();
            lkp_hafez.lkp_iniatalize_data(dt, "name", "id");


            base.load_data("");
        }
        private void load_gc( string query)
        {
            gc.DataSource = null;
            gv.Columns.Clear();
            dt = c_db.select(query);
            gc.DataSource = dt;
            gv.Columns[0].Visible = false;
            gv.Columns[3].Visible = false;

        }

        private void lkp_evaluation_EditValueChanged(object sender, EventArgs e)
        {
           string s =  sqll + " where T_SOURA_EVALUATION.id =" + Convert.ToInt32(lkp_evaluation.EditValue) + "";
            load_gc(s);
       
           // lkp_evaluation.EditValue= -1 ;
            lkp_soura.EditValue = -1;
            lkp_mustalem.Text = string.Empty;
            lkp_keep_type.lkp_iniatalize_data(dt, "name", "id");
            lkp_hafez.lkp_iniatalize_data(dt, "name", "id");
        }

        private void lkp_soura_EditValueChanged(object sender, EventArgs e)
        {
            string s = sqll + " where T_SOURA_KEEP.soura_num  =" + Convert.ToInt32(lkp_soura.EditValue) + "";
            load_gc(s);
        }

        private void lkp_mustalem_EditValueChanged(object sender, EventArgs e)
        {
            string s = sqll + " where T_SOURA_KEEP.pers_mustalem_id  =" + Convert.ToInt32(lkp_mustalem.EditValue) + "";
            load_gc(s);
        }

        private void lkp_keep_type_EditValueChanged(object sender, EventArgs e)
        {
            string s = sqll + " where T_SOURA_KEEP_TYPE.id  =" + Convert.ToInt32(lkp_keep_type.EditValue) + "";
            load_gc(s);
        }

        private void lkp_hafez_EditValueChanged(object sender, EventArgs e)
        {
            string s = sqll + " where T_SOURA_KEEP.pers_hafez_id  =" + Convert.ToInt32(lkp_hafez.EditValue) + "";
            load_gc(s);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            load_gc(sqll);
        }


        private void de_month_EditValueChanged(object sender, EventArgs e)
        {
            string s = sqll + " WHERE(month (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Month + "' " +
              " and  year (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Year + "' ) ";
            load_gc(s);
        }

        private void dtp_from_ValueChanged(object sender, EventArgs e)
        {
            string s = sqll + " WHERE(T_SOURA_KEEP.keep_date between N'" + dtp_from.Text + "' and N'" + dtp_to.Text + "')";
            load_gc(s);

        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            string s = sqll + " WHERE(T_SOURA_KEEP.keep_date between N'" + dtp_from.Text + "' and N'" + dtp_to.Text + "')";
            load_gc(s);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dtp_from.Enabled = true;
                dtp_to.Enabled = true;
            }
            else if (checkBox1.Checked == false)
            {
                dtp_from.Enabled = false;
                dtp_to.Enabled = false;
            }
        }
    }
}

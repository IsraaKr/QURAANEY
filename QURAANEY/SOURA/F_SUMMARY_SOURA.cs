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
    public partial class F_SUMMARY_SOURA : F_INHERATENZ
    {
        public F_SUMMARY_SOURA()
        {
            InitializeComponent();
            load_data("");
            view_inheretanz_butomes();
        }
        private void view_inheretanz_butomes()
        {
            btn_clear.Visible = false;
            btn_delete.Visible = false;
            btn_exite.Visible = true;
            btn_new.Visible = false;
            btn_print.Visible = true;
            btn_save.Visible = false;
            btn_show.Visible = true;

        }
        DataTable dt;
        DataTable dt_state ;
        public override void load_data(string status_mess)
        {
            dt_state = c_db.select(@" SELECT id, name FROM  dbo.T_PERS_STATE");
            ch_comb_state.chb_comb_iniatalize_data(dt_state, "name","id");

            dt = c_db.select(@" SELECT   id, name FROM  dbo.T_USERS_TYPES");
            ch_comb_user_type.chb_comb_iniatalize_data(dt, "name", "id");
            dt = c_db.select(@"  SELECT id, name, num, rate_in_days
                            FROM dbo.T_PERS_RATE_KEEP");
            ch_comb_keep_rate.chb_comb_iniatalize_data(dt, "name", "id");
            dt = c_db.select(@" SELECT        id, name
                      FROM dbo.T_SOURA_EVALUATION");
            ch_comb_evaluation.chb_comb_iniatalize_data(dt, "name", "id");
            dt = c_db.select(@"  SELECT id, name
                      FROM dbo.T_PERS_TYPE");
            ch_comb_pers_type.chb_comb_iniatalize_data(dt, "name", "id");
            dt = c_db.select(@" SELECT        id, name
                FROM            dbo.T_SOURA_KEEP_TYPE ");
            ch_comb_keep_type.chb_comb_iniatalize_data(dt, "name", "id");
            dt = c_db.select(@" SELECT        id, name
                FROM            dbo.T_SOURA_KEEP_TYPE ");
            ch_comb_keep_type.chb_comb_iniatalize_data(dt, "name", "id");
            dt = c_db.select(@"  SELECT DISTINCT soura_name, soura_num
                              FROM         T_SOURA
                               ORDER BY soura_num ");
            ch_comb_soura.chb_comb_iniatalize_data(dt, "soura_name", "soura_num");

//            //تحميل التيل الحالة 
//            dt = c_db.select(@"SELECT        COUNT(pers_id) AS count_1
//FROM            dbo.T_PERS_STATE_CHANGE
//WHERE        (state_id = 1)");
//            if (dt.Rows.Count > 0)
//            {
//                ti_murashah.Elements[1].Text = dt.Rows[0][0].ToString();
//            }
//            else
//                ti_murashah.Elements[1].Text = "...";
            base.load_data(status_mess);
        }
        public override void show()
        {
            // Create a DataTable and add two Columns to it
            DataTable gc_dt = new DataTable();
            gc_dt.Columns.Add("group", typeof(string));
            gc_dt.Columns.Add("Name", typeof(string));
            gc_dt.Columns.Add("count", typeof(int));

            for (int i = 0; i < ch_comb_state.Properties.Items.GetCheckedValues().Count(); i++)
            {
                dt = c_db.select(@"   SELECT        COUNT(dbo.T_PERS_STATE_CHANGE.pers_id) AS count, dbo.T_PERS_STATE.name
FROM            dbo.T_PERS_STATE_CHANGE FULL OUTER JOIN
                         dbo.T_PERS_STATE ON dbo.T_PERS_STATE_CHANGE.state_id = dbo.T_PERS_STATE.id
                               WHERE(state_id = " + int.Parse(ch_comb_state.Properties.Items.GetCheckedValues()[i].ToString()) + ")" +
                               " GROUP BY dbo.T_PERS_STATE.name " );
                MessageBox.Show(""+dt_state.Rows[i][1].ToString() );
               gc_dt.Rows.Add("الحالات", ch_comb_state.SelectedText[i].ToString(), int.Parse(dt.Rows[0][0].ToString()));
            }
           //   MessageBox.Show(""+ ch_comb_state.Properties.GetDisplayText(ch_comb_state.Properties.Items.GetCheckedValues()[i].ToString()));

            gc.DataSource = gc_dt;
            base.show();
        }
    }
}

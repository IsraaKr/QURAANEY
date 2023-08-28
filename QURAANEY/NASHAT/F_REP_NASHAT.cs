using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QURAANEY.NASHAT
{
    public partial class F_REP_NASHAT : F_INHERATENZ
    {
        public F_REP_NASHAT()
        {
            view_inheretanz_butomes(false, false, false, false, true, false, true);
            InitializeComponent();
            load_data("");
        }
        DataTable dt;
        public override void print()
        {
            C_MASTER.print_header("تقرير النشاطات", gc);
            base.print();
        }
        public override void load_data(string status_mess)
        {
            dt = c_db.select(@" SELECT        id, name
              FROM            V_ALL_NASAT_AND_NAMES
                  GROUP BY id,name ");
            lkp_nashat_name.lkp_iniatalize_data(dt, "name", "id");

            //تحميل التيل عدد النشاطاتة 
            dt = c_db.select(@"SELECT        COUNT(DISTINCT id) AS Expr1
FROM            V_ALL_NASAT_AND_NAMES");
            if (dt.Rows.Count > 0)
            {
                ti_nashat_count.Elements[1].Text = dt.Rows[0][0].ToString();
            }
            else
                ti_nashat_count.Elements[1].Text = "...";
            if (lkp_nashat_name.Text != string.Empty)
                load_graid(Convert.ToInt32(lkp_nashat_name.EditValue));
            else
                load_graid(0);

            base.load_data(status_mess);
        }

        private void load_graid(int index)
        {
            gc.DataSource = null;
            gv.Columns.Clear();
            if (index > 0)
            {
                dt = c_db.select(@"SELECT id, name, hafez_id, hafez_name, done, resone
                         FROM V_ALL_NASAT_AND_NAMES 
                  where id = " + Convert.ToInt32(lkp_nashat_name.EditValue) + " ");

                gc.DataSource = dt;
                gv.Columns[0].Visible = false;
                gv.Columns[1].Caption = "اسم النشاط";
                gv.Columns[2].Visible = false;
                gv.Columns[3].Caption = "اسم الحافظ";
                gv.Columns[4].Caption = "الحضور";
                gv.Columns[5].Caption = "السبب";
            }

            else
            {
                dt = c_db.select(@"SELECT id, name, hafez_id, hafez_name, done, resone
                         FROM V_ALL_NASAT_AND_NAMES");

                gc.DataSource = dt;
                gv.Columns[0].Visible = false;
                gv.Columns[1].Caption = "اسم النشاط";
                gv.Columns[2].Visible = false;
                gv.Columns[3].Caption = "اسم الحافظ";
                gv.Columns[4].Caption = "الحضور";
                gv.Columns[5].Caption = "السبب";
            }

        }

        private void load_tail_false()
        {
            //تحميل التيل عدد الغياب 
          //  ti_false.Elements.Clear();
            dt = c_db.select(@"SELECT     COUNT(hafez_id) AS Expr1
FROM         V_ALL_NASAT_AND_NAMES
WHERE     (done = 'false') AND (id=" + Convert.ToInt32(lkp_nashat_name.EditValue) + ") ");
            if (dt.Rows.Count > 0)
            {
                ti_false.Elements[1].Text = dt.Rows[0][0].ToString();
            }
            else
                ti_false.Elements[1].Text = "...";
        }

        private void load_tail_true()
        {
            //تحميل التيل عدد الحضور 

          //  ti_true.Elements.Clear();
            dt = c_db.select(@"SELECT     COUNT(hafez_id) AS Expr1
FROM         V_ALL_NASAT_AND_NAMES
WHERE     (done = 'true') AND (id=" + Convert.ToInt32(lkp_nashat_name.EditValue) + ") ");
            if (dt.Rows.Count > 0)
            {
                ti_true.Elements[1].Text = dt.Rows[0][0].ToString();
            }
            else
                ti_true.Elements[1].Text = "...";
        }

        private void load_tail_all_pers()
        {
            //تحميل التيل عدد الغياب 
            //  ti_false.Elements.Clear();
            dt = c_db.select(@"SELECT     COUNT( distinct hafez_id) AS Expr1
FROM         V_ALL_NASAT_AND_NAMES ");
            if (dt.Rows.Count > 0)
            {
                tileItem1.Elements[1].Text = dt.Rows[0][0].ToString();
            }
            else
                tileItem1.Elements[1].Text = "...";
        }
        private void lkp_nashat_name_EditValueChanged(object sender, EventArgs e)
        {
         //   load_nashat_data();
            load_tail_true();
            load_tail_false();
            load_data("");

            tileGroup2.Visible = true;

        }
        private void load_nashat_data()
        {
            //تحميل التيل عدد الحضور 
            dt = c_db.select(@"SELECT        COUNT(DISTINCT id) AS Expr1
FROM            V_ALL_NASAT_AND_NAMES
WHERE        (done = 'true' and id=" + int.Parse(lkp_nashat_name.EditValue.ToString()) + ")");
            if (dt.Rows.Count > 0)
            {
                ti_true.Elements[1].Text = dt.Rows[0][0].ToString();
            }
            else
                ti_true.Elements[1].Text = "...";


            //تحميل التيل عدد الغياب 
            dt = c_db.select(@"SELECT        COUNT(DISTINCT id) AS Expr1
                          FROM            V_ALL_NASAT_AND_NAMES
                                WHERE        (done = 'false' and id=" + int.Parse(lkp_nashat_name.EditValue.ToString()) + ")");
            if (dt.Rows.Count > 0)
            {
                ti_false.Elements[1].Text = dt.Rows[0][0].ToString();
            }
            else
                ti_false.Elements[1].Text = "...";

            dt = c_db.select(@"SELECT id, name, hafez_id, hafez_name, done, resone
                         FROM V_ALL_NASAT_AND_NAMES 
                     WHERE        ( id=" + int.Parse(lkp_nashat_name.EditValue.ToString()) + ")");

            gc.DataSource = dt;
        }

        private void ti_false_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {

        }
    }
}

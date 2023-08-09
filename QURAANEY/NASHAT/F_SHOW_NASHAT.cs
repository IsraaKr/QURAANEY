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
    public partial class F_SHOW_NASHAT : F_INHERATENZ
    {
        public F_SHOW_NASHAT()
        {
            view_inheretanz_butomes(true, false, false, false, true, false, true);

            InitializeComponent();
        }
        public override void print()
        {
            C_MASTER.print_header("نشاط "+lkp_name.Text, gc);
            base.print();
        }
        int id = 0;
        DataTable dt;
        string sqll;
        int done;
        Boolean is_double_click = false;
        int id_doublee_click = 0;

        public F_SHOW_NASHAT(int nashat_id)
        {
            id = nashat_id;
            InitializeComponent();
            load_data("");
        }
        public override void load_data(string status_mess)
        {
            dt = c_db.select(@"select id , name  from  dbo.T_NASHAT ");
            lkp_name.lkp_iniatalize_data(dt, "name", "id");

            if (id != 0)
            {
                load_by_id();
                load_names_in_namshat();

            }

            base.load_data(status_mess);
        }

        public override void save()
        {
            try
            {
                List<int> name_ids = new List<int>();
                for (int i = 0; i < chlb_names.ItemCount; i++)
                {
                    name_ids.Add(Convert.ToInt32(chlb_names.GetItemValue(i)));
                }
                for (int i = 0; i < name_ids.Count; i++)
                {
                    var state = Convert.ToBoolean(chlb_names.GetItemCheckState(i));
                    sqll = @"UPDATE       dbo.T_NASHAT_KEEP
                        SET                done = '" + state + "' " +
                       "     WHERE        (nashat_id = " + id + ") AND (pers_id = " + name_ids[i] + " ) ";
                    done = c_db.insert_upadte_delete(sqll);

                }
               
                if (is_double_click)
                {
                    if (txt_resone.Text == string.Empty)
                    {
                        txt_resone.ErrorText = "الرحاء إدخال سبب الغياب";
                        return;
                    }
                    sqll = @"UPDATE       dbo.T_NASHAT_KEEP
                        SET               resone  = N'" + txt_resone.Text + "'    " +
                        "  WHERE        (nashat_id = " + id + ") AND (pers_id = " + id_doublee_click + " )";
                    done = c_db.insert_upadte_delete(sqll);

                }
                load_data("i");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "");
            }
            base.save();
            is_double_click = false;
        }

        private void load_by_id()
        {
           DataTable dt1 = c_db.select(@"SELECT     T_NASHAT.id , T_NASHAT.name , T_NASHAT.start_date ,
                T_NASHAT.end_date , T_NASHAT.pers_create , T_PERSONE.name AS Expr1
                 FROM         T_NASHAT INNER JOIN
                      T_PERSONE ON T_NASHAT.pers_create= T_PERSONE.id
                      where T_NASHAT.id  = " + id + " ");
            lkp_name.EditValue = id;
            dtp_date_end.Text = dt1.Rows[0][3].ToString();
            dtp_date_start.Text = dt1.Rows[0][2].ToString();
            txt_mustalem.Text = dt1.Rows[0][5].ToString();
        }

        private void load_names_in_namshat()
        {
            chlb_names.DataSource = null;
            chlb_names.Items.Clear();
            dt = c_db.select(@"SELECT        hafez_id, hafez_name ,done ,resone
                    FROM            dbo.V_ALL_NASAT_AND_NAMES
                      where id=" + id + "");
            chlb_names.chbl_iniatalize_data(dt, "hafez_name", "hafez_id");

            gc.DataSource = dt;
            gv.Columns[0].Visible = false;
            gv.Columns[1].Caption = "اسم الحافظ";
            gv.Columns[2].Caption = "الحضور";
            gv.Columns[3].Caption = "السبب";

            for (int i = 0; i < chlb_names.ItemCount; i++)
            {

                Boolean state = Convert.ToBoolean(dt.Rows[i][2].ToString());
                chlb_names.SetItemChecked(i, state);
            }
            // chlb_names.UnCheckAll();
        }

        private void lkp_name_EditValueChanged(object sender, EventArgs e)
        {
            id = int.Parse(lkp_name.EditValue.ToString());
            load_by_id();
            load_names_in_namshat();
            is_double_click = false;
        
        }

        private void gv_DoubleClick(object sender, EventArgs e)
        {
            is_double_click = true;
            group_reson.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            id_doublee_click = int.Parse(gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[0]).ToString());
            txt_pers_name.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[1]).ToString();
            txt_resone.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[3]).ToString();
        }
    }

}

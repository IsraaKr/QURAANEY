using DevExpress.XtraGrid.Views.Tile.ViewInfo;
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
    public partial class F_ALL_NASHAT : F_INHERATENZ
    {
        public F_ALL_NASHAT()
        {

            view_inheretanz_butomes(false, false, false, false, true, false, true);
            InitializeComponent();
            load_data("");
        }
        int nashat_id;
        DataTable dt;
        DateTime todaye = DateTime.Today ;
        public override void load_data(string status_mess)
        {
            dt = c_db.select(@"SELECT     T_NASHAT.id, T_NASHAT.name, T_NASHAT.start_date, T_NASHAT.end_date, T_NASHAT.pers_create, T_PERSONE.name AS Expr1
                      FROM         T_NASHAT INNER JOIN
                      T_PERSONE ON T_NASHAT.pers_create= T_PERSONE.id ORDER BY T_NASHAT.end_date DESC");
            gridControl1.DataSource = dt;

            //int [] id_less =new int[dt.Rows.Count];
            //int[] id_equel = new int[dt.Rows.Count];
            //int[] id_more = new int[dt.Rows.Count];
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    DateTime e_date =Convert.ToDateTime( dt.Rows[i][3].ToString());

            //  int res=  DateTime.Compare(e_date , todaye);
            //    if (res <0)
            //    {
            //        MessageBox.Show("<0" + dt.Rows[i][3].ToString());
            //       // id_less[i] = Convert.ToInt32(dt.Rows[i][0].ToString());
            //        tileView1.Appearance.ItemNormal.BackColor = Color.IndianRed;
            //    }
            //    else if (res == 0)
            //    {
            //        MessageBox.Show("==" + dt.Rows[i][3].ToString());
            //      //  id_equel[i] = Convert.ToInt32(dt.Rows[i][0].ToString());
            //        tileView1.Appearance.ItemNormal.BackColor = Color.Khaki;

            //    }
            //    else if (res > 0)
            //    {
            //        MessageBox.Show(">0" + dt.Rows[i][3].ToString());
            //      //  id_more[i] = Convert.ToInt32(dt.Rows[i][0].ToString());
            //        tileView1.Appearance.ItemNormal.BackColor = Color.DarkSeaGreen;
            //    }

            //    for (int j = 0; j < tileView1.RowCount; j++)
            //    {
                    
            //    }
            //}
            base.load_data(status_mess);
        }
        public override void print()
        {
            C_MASTER.print_header( "كل النشاطات ", gridControl1);
            base.print();
        }


        private void tileView1_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs mouseArgs = (e as MouseEventArgs);
            TileViewHitInfo hitInfo = tileView1.CalcHitInfo(mouseArgs.Location);

            if (hitInfo.InItem)
            {
                object SelectedValue = tileView1.GetRowCellValue(hitInfo.RowHandle, "id");
                nashat_id = int.Parse(SelectedValue.ToString());
                F_SHOW_NASHAT f = new F_SHOW_NASHAT(nashat_id);
                f.WindowState = FormWindowState.Maximized;
                f.Show();
            }


        }

      
    }
}

﻿using DevExpress.XtraGrid.Views.Tile.ViewInfo;
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
        public override void load_data(string status_mess)
        {
            dt = c_db.select(@"SELECT     T_NASHAT.id, T_NASHAT.name, T_NASHAT.start_date, T_NASHAT.end_date, T_NASHAT.pers_create, T_PERSONE.name AS Expr1
FROM         T_NASHAT INNER JOIN
                      T_PERSONE ON T_NASHAT.pers_create= T_PERSONE.id");
            gridControl1.DataSource = dt;
            base.load_data(status_mess);
        }
        public override void print()
        {
            C_MASTER.print_header( "كل النشاطات ", gridControl1);
            base.print();
        }
        private void tileView1_ItemClick(object sender, DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventArgs e)
        {

            //   nashat_id =int.Parse( e.Item.GetElementByName("id").ToString());

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
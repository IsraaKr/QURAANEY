﻿using DevExpress.XtraEditors;
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
    public partial class F_SOURA_GRID : F_INHERATENZ
    {
        public F_SOURA_GRID()
        {
            InitializeComponent();
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
            btn_show.Visible = false;

        }

        DataTable dt;
        private void F_SOURA_GRID_Load(object sender, EventArgs e)
        {
            load_gc();
        }
        //تحميل الغريد كونترول
        private void load_gc()
        {
            /*  string sqll = @" SELECT  T_PERSONE.id AS تسلسل ,T_PERSONE.name AS الحافظ, T_SOURA_KEEP.soura_name AS [اسم السورة], T_SOURA_KEEP.aya_num AS [رقم الآية], T_SOURA_KEEP.page_num AS [رقم الصفحة], 
                        T_SOURA_KEEP.keep_date AS التاريخ
  FROM         T_PERSONE INNER JOIN
                        T_SOURA_KEEP ON T_PERSONE.id = T_SOURA_KEEP.pers_hafez_id INNER JOIN
                        T_SOURA_KEEP_TYPE ON T_SOURA_KEEP.keep_type_id = T_SOURA_KEEP_TYPE.id INNER JOIN
                        T_SOURA_EVALUATION ON T_SOURA_KEEP.evaluation_id = T_SOURA_EVALUATION.id INNER JOIN
                        T_PERSONE AS T_PERSONE_1 ON T_SOURA_KEEP.pers_mustalem_id = T_PERSONE_1.id";
            */
            dt = c_db.select("select id from T_SOURA_KEEP ");
            if (dt.Rows.Count > 0)
            {
                string sqll = @"SELECT        T_PERSONE.id AS تسلسل, T_PERSONE.name AS الحافظ, COUNT(DISTINCT T_SOURA_KEEP.soura_num) AS [عدد السور], COUNT(T_SOURA_KEEP.aya_num) AS [عدد الآيات], 
                         MAX(T_SOURA_KEEP.page_num) AS [آخر صفحة]
FROM            dbo.T_PERSONE LEFT OUTER JOIN
                         dbo.T_SOURA_KEEP ON T_PERSONE.id = T_SOURA_KEEP.pers_hafez_id LEFT OUTER JOIN
                         dbo.T_SOURA_KEEP_TYPE ON T_SOURA_KEEP.keep_type_id = T_SOURA_KEEP_TYPE.id LEFT OUTER JOIN
                         dbo.T_SOURA_EVALUATION ON T_SOURA_KEEP.evaluation_id = T_SOURA_EVALUATION.id LEFT OUTER JOIN
                         dbo.T_PERSONE AS T_PERSONE_1 ON T_SOURA_KEEP.pers_mustalem_id = T_PERSONE_1.id
WHERE        (dbo.T_PERSONE.is_active = 1)
GROUP BY T_PERSONE.name, T_PERSONE.id, dbo.T_PERSONE.is_active"
    ;
                dt = c_db.select(sqll);
                gc.DataSource = dt;
                gv.Columns[0].Visible = false;
            }
        }

        private void gv_DoubleClick(object sender, EventArgs e)
        {
            //جلب اي دي السطر الذي عملنا عليه دبل كليك
            int id = Convert.ToInt32(gv.GetFocusedRowCellValue("تسلسل"));
            F_KEEP_SOURA f = new F_KEEP_SOURA(id);
           
            f.WindowState = FormWindowState.Maximized;
           f.Show();
            
        }
        public override void close()
        {
            base.close();
        }
        public override void print()
        {
            C_MASTER.print_header("إجمالي الحفظ تقرير" , gc);

            base.print();
        }
    }
}

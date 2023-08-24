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
        //        string sqll = @" SELECT  T_PERSONE.id AS تسلسل  , T_PERSONE.name AS الحافظ, T_SOURA_KEEP.soura_name AS [اسم السورة], T_SOURA_KEEP.aya_num AS [رقم الآية], T_SOURA_KEEP.page_num AS [رقم الصفحة], 
        //                      T_SOURA_KEEP.keep_date AS التاريخ, T_SOURA_KEEP_TYPE.name AS [نوع الحفظ], T_SOURA_EVALUATION.name AS التقيم, T_PERSONE_1.name AS المستلم
        //FROM         T_PERSONE INNER JOIN
        //                      T_SOURA_KEEP ON T_PERSONE.id = T_SOURA_KEEP.pers_hafez_id INNER JOIN
        //                      T_SOURA_KEEP_TYPE ON T_SOURA_KEEP.keep_type_id = T_SOURA_KEEP_TYPE.id INNER JOIN
        //                      T_SOURA_EVALUATION ON T_SOURA_KEEP.evaluation_id = T_SOURA_EVALUATION.id INNER JOIN
        //                      T_PERSONE AS T_PERSONE_1 ON T_SOURA_KEEP.pers_mustalem_id = T_PERSONE_1.id";

        string sqll = @"  SELECT T_PERSONE.id AS تسلسل, T_PERSONE.name AS الحافظ, T_SOURA_KEEP.soura_name AS [اسم السورة], T_SOURA_KEEP.page_num AS [رقم آخر الصفحة] ,
                      T_SOURA_KEEP.keep_date AS التاريخ, T_SOURA_KEEP_TYPE.name AS [نوع الحفظ], T_SOURA_EVALUATION.name AS التقيم, T_PERSONE_1.name AS المستلم
FROM         T_PERSONE INNER JOIN
                      T_SOURA_KEEP ON T_PERSONE.id = T_SOURA_KEEP.pers_hafez_id INNER JOIN
                      T_SOURA_KEEP_TYPE ON T_SOURA_KEEP.keep_type_id = T_SOURA_KEEP_TYPE.id INNER JOIN
                      T_SOURA_EVALUATION ON T_SOURA_KEEP.evaluation_id = T_SOURA_EVALUATION.id INNER JOIN
                      T_PERSONE AS T_PERSONE_1 ON T_SOURA_KEEP.pers_mustalem_id = T_PERSONE_1.id  ";

        string group = @"  group by  T_PERSONE.id , T_PERSONE.name , T_SOURA_KEEP.soura_name , T_SOURA_KEEP.page_num  ,
                      T_SOURA_KEEP.keep_date , T_SOURA_KEEP_TYPE.name , T_SOURA_EVALUATION.name, T_PERSONE_1.name  ";
                      
         string having = @" having page_num in (select max(page_num) from T_SOURA_KEEP as keep2
                                              
                                            group by keep2.soura_num , keep2.pers_hafez_id
                                          ) ";

        string where = "  ";

        string evaluation = "  ";

        string soura = "  ";
        public F_Rep_Soura2()
        {
            InitializeComponent();
            view_inheretanz_butomes(false, false, false, false, true, false, true);
            load_gc(sqll  + group + having );
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

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            load_gc(sqll + group + having);
            clear(this.Controls);
            chb_fail.Checked = false;
            chb_from_to.Checked = false;
        }

        private void chb_from_to_CheckedChanged(object sender, EventArgs e)
        {
            clear(this.Controls);
            if (chb_from_to.Checked)
            {
                dtp_from.Enabled = true;
                dtp_to.Enabled = true;
            }
            else if (chb_from_to.Checked == false)
            {
                dtp_from.Enabled = false;
                dtp_to.Enabled = false;
            }
        }

        private void chb_fail_CheckedChanged(object sender, EventArgs e)
        {
          //  clear(this.Controls);
            if (chb_fail.Checked)
            {
                string s = @"SELECT        الاسم, [page in month], [page in year]
                   FROM dbo.V_fail ";
                gc.DataSource = null;
                gv.Columns.Clear();
                dt = c_db.select(s);
                gc.DataSource = dt;
                gv.Columns[1].Caption = "شهر التقصير";
                gv.Columns[2].Caption = "سنة التقصير";

            }
        }



        //private void lkp_evaluation_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        //{
        //    ////clear(this.Controls);
        //    ////string s = sqll + " where T_SOURA_EVALUATION.id =" + Convert.ToInt32(lkp_evaluation.EditValue) + " " + group + having;
        //    //////string s = sqll + " and T_SOURA_EVALUATION.id =" + Convert.ToInt32(lkp_evaluation.EditValue) + "";

        //    ////load_gc(s);
        //}
        //private void lkp_evaluation_EditValueChanged(object sender, EventArgs e)
        //{
        //    //clear(this.Controls);
        //    //string s = sqll + " where T_SOURA_EVALUATION.id =" + Convert.ToInt32(lkp_evaluation.EditValue) + " " + group + having;
        //    ////string s = sqll + " and T_SOURA_EVALUATION.id =" + Convert.ToInt32(lkp_evaluation.EditValue) + "";

        //    //load_gc(s);
        //}
       
        //private void lkp_soura_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        //{
        //    ////clear(this.Controls);
        //    ////string s = sqll + " where T_SOURA_KEEP.soura_num  =" + Convert.ToInt32(lkp_soura.EditValue) + "" + group + having;

        //    //////string s = sqll + " and  T_SOURA_KEEP.soura_num  =" + Convert.ToInt32(lkp_soura.EditValue) + "";
        //    ////load_gc(s);
        //}
        //private void lkp_soura_EditValueChanged(object sender, EventArgs e)
        //{
        //    //clear(this.Controls);
        //    //string s = sqll + " where T_SOURA_KEEP.soura_num  =" + Convert.ToInt32(lkp_soura.EditValue) + "" + group + having;

        //    ////string s = sqll + " and  T_SOURA_KEEP.soura_num  =" + Convert.ToInt32(lkp_soura.EditValue) + "";
        //    //load_gc(s);
        //}


        //private void lkp_mustalem_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        //{
        //    clear(this.Controls);
        //    //string s = sqll + " where T_SOURA_KEEP.pers_mustalem_id  =" + Convert.ToInt32(lkp_mustalem.EditValue) + "";

        //    string s = sqll + " and T_SOURA_KEEP.pers_mustalem_id  =" + Convert.ToInt32(lkp_mustalem.EditValue) + "";
        //    load_gc(s);
        //}
        //private void lkp_mustalem_EditValueChanged(object sender, EventArgs e)
        //{
        //    //clear(this.Controls);
        //    ////string s = sqll + " where T_SOURA_KEEP.pers_mustalem_id  =" + Convert.ToInt32(lkp_mustalem.EditValue) + "";

        //    //string s = sqll + " and T_SOURA_KEEP.pers_mustalem_id  =" + Convert.ToInt32(lkp_mustalem.EditValue) + "";
        //    //load_gc(s);
        //}
      
        ////private void lkp_keep_type_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        ////{
        ////    clear(this.Controls);
        ////    //string s = sqll + " where T_SOURA_KEEP_TYPE.id  =" + Convert.ToInt32(lkp_keep_type.EditValue) + "";
        ////    string s = sqll + " and T_SOURA_KEEP_TYPE.id  =" + Convert.ToInt32(lkp_keep_type.EditValue) + "";

        ////    load_gc(s);
        ////}
        ////private void lkp_keep_type_EditValueChanged(object sender, EventArgs e)
        ////{
        ////    //clear(this.Controls);
        ////    ////string s = sqll + " where T_SOURA_KEEP_TYPE.id  =" + Convert.ToInt32(lkp_keep_type.EditValue) + "";
        ////    //string s = sqll + " and T_SOURA_KEEP_TYPE.id  =" + Convert.ToInt32(lkp_keep_type.EditValue) + "";

        ////    //load_gc(s);
        ////}
      
 
        ////private void lkp_hafez_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        ////{
        ////    clear(this.Controls);
        ////    // string s = sqll + " where T_SOURA_KEEP.pers_hafez_id  =" + Convert.ToInt32(lkp_hafez.EditValue) + "";
        ////    string s = sqll + " and T_SOURA_KEEP.pers_hafez_id  =" + Convert.ToInt32(lkp_hafez.EditValue) + "";

        ////    load_gc(s);
        ////}
        ////private void lkp_hafez_EditValueChanged(object sender, EventArgs e)
        ////{
        ////    //clear(this.Controls);
        ////    //// string s = sqll + " where T_SOURA_KEEP.pers_hafez_id  =" + Convert.ToInt32(lkp_hafez.EditValue) + "";
        ////    //string s = sqll + " and T_SOURA_KEEP.pers_hafez_id  =" + Convert.ToInt32(lkp_hafez.EditValue) + "";

        ////    //load_gc(s);
        ////}

     //   private void de_month_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
     //   {
     //       clear(this.Controls);
     //       string s = sqll + " WHERE(month (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Month + "' " +
     //       " and  year (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Year + "' ) ";
     ////       string s = sqll + " and (month (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Month + "' " +
     ////" and  year (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Year + "' ) ";


     //       load_gc(s);
     //   }
     //   private void de_month_EditValueChanged(object sender, EventArgs e)
     //   {
     ////       clear(this.Controls);
     ////       string s = sqll + " WHERE(month (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Month + "' " +
     ////       " and  year (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Year + "' ) ";
     //////       string s = sqll + " and (month (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Month + "' " +
     //////" and  year (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Year + "' ) ";


     ////       load_gc(s);
     //   }

        private void lkp_evaluation_TextChanged(object sender, EventArgs e)
        {
          //  clear(this.Controls);
            string s = sqll + " where T_SOURA_EVALUATION.id =" + Convert.ToInt32(lkp_evaluation.EditValue) + " " + group + having;
            //string s = sqll + " and T_SOURA_EVALUATION.id =" + Convert.ToInt32(lkp_evaluation.EditValue) + "";
            evaluation = lkp_evaluation.Text;
            load_gc(s);

            ////lkp_evaluation.Text = null;
            ////lkp_evaluation.EditValue = null;
            //lkp_soura.Text = null;
            //lkp_soura.EditValue = null;
            //lkp_hafez.Text = null;
            //lkp_hafez.EditValue = null;
            //lkp_keep_type.Text = null;
            //lkp_keep_type.EditValue = null;
            //lkp_mustalem.Text = null;
            //lkp_mustalem.EditValue = null;
            //lkp_pers_type.Text = null;
            //lkp_pers_type.EditValue = null;
            ////de_month.Text = null;
            ////de_month.EditValue = null;
            ////dtp_from.Text = null;
            ////dtp_to.Text = null;
        }

        private void lkp_soura_TextChanged(object sender, EventArgs e)
        {
           // clear(this.Controls);
            string s = sqll + " where T_SOURA_KEEP.soura_num  =" + Convert.ToInt32(lkp_soura.EditValue) + "" + group + having;
            soura = lkp_soura.Text;
            //string s = sqll + " and  T_SOURA_KEEP.soura_num  =" + Convert.ToInt32(lkp_soura.EditValue) + "";
            load_gc(s);

            //lkp_evaluation.Text = null;
            //lkp_evaluation.EditValue = null;
            ////lkp_soura.Text = null;
            ////lkp_soura.EditValue = null;
            //lkp_hafez.Text = null;
            //lkp_hafez.EditValue = null;
            //lkp_keep_type.Text = null;
            //lkp_keep_type.EditValue = null;
            //lkp_mustalem.Text = null;
            //lkp_mustalem.EditValue = null;
            //lkp_pers_type.Text = null;
            //lkp_pers_type.EditValue = null;
            ////de_month.Text = null;
            ////de_month.EditValue = null;
            ////dtp_from.Text = null;
            ////dtp_to.Text = null;
        }

        private void lkp_mustalem_TextChanged(object sender, EventArgs e)
        {
          
            //string s = sqll + " where T_SOURA_KEEP.pers_mustalem_id  =" + Convert.ToInt32(lkp_mustalem.EditValue) + "";

            string s = sqll + " and T_SOURA_KEEP.pers_mustalem_id  =" + Convert.ToInt32(lkp_mustalem.EditValue) + "" + group + having;
            load_gc(s);

                 lkp_evaluation.Text = null;
            lkp_evaluation.EditValue = null;
            lkp_soura.Text = null;
            lkp_soura.EditValue = null;
            lkp_hafez.Text = null;
            lkp_hafez.EditValue = null;
            lkp_keep_type.Text = null;
            lkp_keep_type.EditValue = null;
            //lkp_mustalem.Text = null;
            //lkp_mustalem.EditValue = null;
            lkp_pers_type.Text = null;
            lkp_pers_type.EditValue = null;
            //de_month.Text = null;
            //de_month.EditValue = null;
            //dtp_from.Text = null;
            //dtp_to.Text = null;
        }

        private void lkp_keep_type_TextChanged(object sender, EventArgs e)
        {
         
            //string s = sqll + " where T_SOURA_KEEP_TYPE.id  =" + Convert.ToInt32(lkp_keep_type.EditValue) + "";
            string s = sqll + " and T_SOURA_KEEP_TYPE.id  =" + Convert.ToInt32(lkp_keep_type.EditValue) + "" + group + having;

            load_gc(s);

            lkp_evaluation.Text = null;
            lkp_evaluation.EditValue = null;
            lkp_soura.Text = null;
            lkp_soura.EditValue = null;
            lkp_hafez.Text = null;
            lkp_hafez.EditValue = null;
            //lkp_keep_type.Text = null;
            //lkp_keep_type.EditValue = null;
            lkp_mustalem.Text = null;
            lkp_mustalem.EditValue = null;
            lkp_pers_type.Text = null;
            lkp_pers_type.EditValue = null;
            //de_month.Text = null;
            //de_month.EditValue = null;
            //dtp_from.Text = null;
            //dtp_to.Text = null;
        }

        private void lkp_hafez_TextChanged(object sender, EventArgs e)
        {
           
            // string s = sqll + " where T_SOURA_KEEP.pers_hafez_id  =" + Convert.ToInt32(lkp_hafez.EditValue) + "";
            string s = sqll + " and T_SOURA_KEEP.pers_hafez_id  =" + Convert.ToInt32(lkp_hafez.EditValue) + "" + group + having;

            load_gc(s);
            lkp_evaluation.Text = null;
            lkp_evaluation.EditValue = null;
            lkp_soura.Text = null;
            lkp_soura.EditValue = null;
            //lkp_hafez.Text = null;
            //lkp_hafez.EditValue = null;
            lkp_keep_type.Text = null;
            lkp_keep_type.EditValue = null;
            lkp_mustalem.Text = null;
            lkp_mustalem.EditValue = null;
            lkp_pers_type.Text = null;
            lkp_pers_type.EditValue = null;
            //de_month.Text = null;
            //de_month.EditValue = null;
            //dtp_from.Text = null;
            //dtp_to.Text = null;
        }

        private void de_month_TextChanged(object sender, EventArgs e)
        {
           // clear(this.Controls);
            string s = sqll + " WHERE(month (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Month + "' " +
            " and  year (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Year + "' ) " + group + having;
            //       string s = sqll + " and (month (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Month + "' " +
            //" and  year (T_SOURA_KEEP.keep_date) = N'" + de_month.DateTime.Year + "' ) ";


            load_gc(s);

            lkp_evaluation.Text = null;
            lkp_evaluation.EditValue = null;
            lkp_soura.Text = null;
            lkp_soura.EditValue = null;
            lkp_hafez.Text = null;
            lkp_hafez.EditValue = null;
            lkp_keep_type.Text = null;
            lkp_keep_type.EditValue = null;
            lkp_mustalem.Text = null;
            lkp_mustalem.EditValue = null;
            lkp_pers_type.Text = null;
            lkp_pers_type.EditValue = null;
            //de_month.Text = null;
            //de_month.EditValue = null;
            //dtp_from.Text = null;
            //dtp_to.Text = null;
        }
        private void dtp_from_ValueChanged(object sender, EventArgs e)
        {
            ////  string s = sqll + " WHERE(T_SOURA_KEEP.keep_date between N'" + dtp_from.Text + "' and N'" + dtp_to.Text + "')";
            //string s = sqll + " and (T_SOURA_KEEP.keep_date between N'" + dtp_from.Text + "' and N'" + dtp_to.Text + "')" + group + having;

            //load_gc(s);

        }

        private void dtp_to_ValueChanged(object sender, EventArgs e)
        {
            // string s = sqll + " and (T_SOURA_KEEP.keep_date between N'" + dtp_from.Text + "' and N'" + dtp_to.Text + "')";
            //string s = sqll + " WHERE(T_SOURA_KEEP.keep_date between N'" + dtp_from.Text + "' and N'" + dtp_to.Text + "')" + group + having;

            //load_gc(s);
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //    string s = sqll + " WHERE(T_SOURA_KEEP.keep_date between N'" + dtp_from.Text + "' and N'" + dtp_to.Text + "')" + group + having;

            //  load_gc(s);

            string s = sqll + "where";// + " WHERE(T_SOURA_KEEP.keep_date between N'" + dtp_from.Text + "' and N'" + dtp_to.Text + "')" + group + having;



            if (lkp_soura.Text != string.Empty)
            {
                 s = s + "  T_SOURA_KEEP.soura_num  =" + Convert.ToInt32(lkp_soura.EditValue) + " AND " ;

             
            }
            
            if (lkp_evaluation.Text != string.Empty)
            {
                 s = s +"  T_SOURA_EVALUATION.id =" + Convert.ToInt32(lkp_evaluation.EditValue) + " AND " ;

              
            }
            s = s.Substring(0,s.Length-4);
            s= s+ group + having;
            load_gc(s);
        }
    }
}

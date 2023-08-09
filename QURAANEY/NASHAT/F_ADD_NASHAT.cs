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
    public partial class F_ADD_NASHAT : F_INHERATENZ
    {
        public F_ADD_NASHAT()
        {
            view_inheretanz_butomes(true, true, true, true, true, false, true);
            view_inheretanz_butomes();
            InitializeComponent();
        }

        private string maxid;

        string sqll;
        string pers_name = "";
        int nashat_id = 0;
        DataTable dt;
        int done;
        Boolean is_double_click = false;


        public override void load_data(string status_mess)
        {
            clear(this.Controls);
            set_date_edite();
            set_auto_id_person();

            dt = c_db.select(@"select id , name  from T_PERS_STATE");
            lkp_sate.lkp_iniatalize_data(dt, "name", "id");

            dt = c_db.select(@"select id , name  from T_PERS_TYPE");
            lkp_type.lkp_iniatalize_data(dt, "name", "id");

            dt = C_DB_QUERYS.get_soura_name();
            lkp_soura.lkp_iniatalize_data(dt, "soura_name", "soura_num");

            dt = C_DB_QUERYS.get_person_name();
            lkp_mustalem.lkp_iniatalize_data(dt, "name", "id");

            load_gc_snd_tail();
            base.load_data(status_mess);
        }
        public override void print()
        {
            C_MASTER.print_header(" النشاطات", gc);
            base.print();
        }
        public override void save()
        {
            if (vallidate_data())
            {
                dt = c_db.select(@"select id from  dbo.T_NASHAT 
                      where id=" + int.Parse(txt_id.Text) + "");
                if (dt.Rows.Count <= 0)
                {

                    if (chlb_names.DataSource == null || chlb_names.CheckedItems.Count == 0)
                    {
                        MessageBox.Show("الرجاء اختيار أسماء المشاركين");
                        return;
                    }
                    insert_nashat();
                    insert_names();

                }
                else if (dt.Rows.Count > 0)
                {
                    update_nashat();
                    update_names();
                }
                load_data("i");
                is_double_click = false;

            }
            base.save();
        }
        public override void neew()
        {
            clear(this.Controls);
            set_auto_id_person();
            base.neew();
        }
        public override void delete()
        {
            if (is_double_click == false)
            {
                MessageBox.Show("يجب اختيار سجل لحذفه", "تنبيه",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult res = MessageBox.Show("   هل انت متاكد انك تريد حذف المعلومات بشكل نهائي ", "تأكيد",
               MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            try
            {
                if (res == DialogResult.Yes)
                {
                    delete_nashat();
                    delete_names();
                    load_data("d");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex);
            }
            base.delete();
            is_double_click = false;

        }
        public override bool vallidate_data()
        {
            int number_of_errores = 0;

            number_of_errores += lkp_mustalem.is_editevalue_valid() ? 0 : 1;
            number_of_errores += txt_name.is_text_valid() ? 0 : 1;

            return (number_of_errores == 0);

        }
        public override void clear(Control.ControlCollection s_controls)
        {
            base.clear(s_controls);
        }
        private void set_date_edite()
        {
            dtp_start_date.Text = DateTime.Today.ToShortDateString();
            dtp_end_date.Text = DateTime.Today.ToShortDateString();
        }
        private void view_inheretanz_butomes()
        {
            btn_clear.Visible = true;
            btn_delete.Visible = true;
            btn_exite.Visible = true;
            btn_new.Visible = true;
            btn_print.Visible = true;
            btn_save.Visible = true;
            btn_show.Visible = false;
        }
        private void set_auto_id_person()
        {
            maxid = c_db.max("SELECT  id from dbo.T_NASHAT");
            int x = 0;
            if (maxid == null || maxid == "")
                x = 0;
            else
                x = int.Parse(maxid);
            x++;
            txt_id.Text = x.ToString();
        }
        private void load_gc_snd_tail()
        {
            string sqll = @" SELECT        T_NASHAT.id AS المعرف, T_NASHAT.name AS [اسم النشاط], T_NASHAT.start_date AS [بداية النشاط], T_NASHAT.end_date AS [نهاية النشاط], T_NASHAT.pers_create AS [تسلسل المستلم], T_PERSONE.name AS [اسم المستلم], 
                         T_NASHAT.count_pers AS [عدد المشاركين]
FROM            T_NASHAT INNER JOIN
                         T_PERSONE ON T_NASHAT.pers_create = T_PERSONE.id";

            dt = c_db.select(sqll);
            gc.DataSource = dt;
            gv.Columns[0].Visible = false;
            gv.Columns[4].Visible = false;



        }
        private void gv_DoubleClick(object sender, EventArgs e)
        {
            is_double_click = true;
            chlb_names.DataSource = null;
            chlb_names.Items.Clear();

            txt_id.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[0]).ToString();
            nashat_id = int.Parse(txt_id.Text);
            txt_name.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[1]).ToString();
            dtp_start_date.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[2]).ToString();
            dtp_end_date.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[3]).ToString();
            lkp_mustalem.Text = gv.GetRowCellValue(gv.FocusedRowHandle, gv.Columns[5]).ToString();

            sqll = (@"  SELECT dbo.T_NASHAT_KEEP.nashat_id, dbo.T_NASHAT_KEEP.pers_id, dbo.T_PERSONE.name
             FROM            dbo.T_NASHAT_KEEP INNER JOIN
                         dbo.T_PERSONE ON dbo.T_NASHAT_KEEP.pers_id = dbo.T_PERSONE.id
              WHERE(dbo.T_NASHAT_KEEP.nashat_id = " + nashat_id + ") ");

            dt = c_db.select(sqll);

            chlb_names.chbl_iniatalize_data(dt, "name", "pers_id");
            chlb_names.CheckAll();

        }
        private int insert_nashat()
        {
            try
            {
                sqll = @"INSERT INTO dbo.T_NASHAT
                         (name, start_date, end_date, pers_create ,count_pers)
                           VALUES        (N'" + txt_name.Text + "' ," +
                           " N'" + dtp_start_date.Text + "'," +
                           " N'" + dtp_end_date.Text + "'," +
                           " " + Convert.ToInt32(lkp_mustalem.EditValue) + " , " +
                           "  " + chlb_names.CheckedItemsCount + "   )";
                done = c_db.insert_upadte_delete(sqll);

                dt = c_db.select("SELECT  id from dbo.T_NASHAT where name = N'" + txt_name.Text + "'");
                nashat_id = int.Parse(dt.Rows[0][0].ToString());
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                return 0;
            }

        }
        private int insert_names()
        {
            try
            {
                List<int> name_ids = new List<int>();
                foreach (var item_check in chlb_names.CheckedIndices)
                {
                    name_ids.Add(Convert.ToInt32(chlb_names.GetItemValue(item_check)));
                }
                for (int i = 0; i < name_ids.Count; i++)
                {
                    sqll = @"INSERT INTO dbo.T_NASHAT_KEEP
                             (nashat_id, pers_id,done )
                               VALUES        (" + nashat_id + " ," +
                               " " + name_ids[i] + "," +
                               " '" + false + "' )";
                    done = c_db.insert_upadte_delete(sqll);
                }
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                return 0;
            }

        }
        private void update_nashat()
        {
            try
            {
                sqll = @"UPDATE       dbo.T_NASHAT
              SET                name =N'" + txt_name.Text + "'," +
                    " start_date =N'" + dtp_start_date.Text + "'," +
                    " end_date =N'" + dtp_end_date.Text + "'," +
                    " pers_create = " + Convert.ToInt32(lkp_mustalem.EditValue) + ", " +
                    " count_pers=" + chlb_names.CheckedItemsCount + " " +
                     " WHERE(id = " + nashat_id + ")";

                done = c_db.insert_upadte_delete(sqll);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "");
            }

        }
        private void update_names()
        {
            try
            {
                //List<int> name_ids = new List<int>();
                //foreach (var item_check in chlb_names.CheckedIndices)
                //{
                //    name_ids.Add(Convert.ToInt32(chlb_names.GetItemValue(item_check)));
                //}
                //for (int i = 0; i < name_ids.Count; i++)
                //{
                //    sqll = @"UPDATE       dbo.T_NASHAT_KEEP" +
                //      "   set     pers_id =   " + name_ids[i] + " " +
                //      "  WHERE(nashat_id = " + nashat_id + ")  ";

                //    done = c_db.insert_upadte_delete(sqll);
                //}

                delete_names();
                insert_names();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex + "");
            }

        }
        private int delete_nashat()
        {
            try
            {
                sqll = @"DELETE FROM  dbo.T_NASHAT
                         WHERE(id = " + nashat_id + ")";
                done = c_db.insert_upadte_delete(sqll);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }


        }
        private int delete_names()
        {
            try
            {
                sqll = @"DELETE FROM   dbo.T_NASHAT_KEEP
                         WHERE(nashat_id = " + nashat_id + ")";
                done = c_db.insert_upadte_delete(sqll);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }


        }
        private void rb_all_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_all.Checked)
            {
                lkp_soura.Enabled = false;
                lkp_sate.Enabled = false;
                lkp_type.Enabled = false;

                chlb_names.Items.Clear();

                dt = C_DB_QUERYS.get_person_name_isactive();

                chlb_names.chbl_iniatalize_data(dt, "name", "id");
                chlb_names.CheckAll();

            }
        }
        private void rb_soura_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_soura.Checked)
            {
                lkp_soura.Enabled = true;
                lkp_sate.Enabled = false;
                lkp_type.Enabled = false;
            }
        }
        private void rb_state_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_state.Checked)
            {
                lkp_soura.Enabled = false;
                lkp_sate.Enabled = true;
                lkp_type.Enabled = false;
            }

        }
        private void rb_type_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_type.Checked)
            {

                lkp_soura.Enabled = false;
                lkp_sate.Enabled = false;
                lkp_type.Enabled = true;

            }

        }
        private void lkp_soura_EditValueChanged(object sender, EventArgs e)
        {

            sqll = (@"    SELECT dbo.T_SOURA_KEEP.pers_hafez_id, dbo.T_PERSONE.name, dbo.T_SOURA.soura_num, dbo.T_SOURA.soura_name
FROM         dbo.T_SOURA_KEEP INNER JOIN
                      dbo.T_PERSONE ON dbo.T_SOURA_KEEP.pers_hafez_id = dbo.T_PERSONE.id FULL OUTER JOIN
                      dbo.T_SOURA ON dbo.T_SOURA_KEEP.soura_id = dbo.T_SOURA.id
WHERE(dbo.T_SOURA_KEEP.aya_num IN
                          (SELECT     max_aya
                             FROM         dbo.V_MAX_AYA_NUM_BY_SOURA
                             WHERE(soura_num = dbo.T_SOURA.soura_num))) 
AND(dbo.T_SOURA_KEEP.soura_num = " + Convert.ToInt32(lkp_soura.EditValue) + ")");


            dt = c_db.select(sqll);
            chlb_names.Items.Clear();
            chlb_names.chbl_iniatalize_data(dt, "name", "pers_hafez_id");
            chlb_names.CheckAll();
        }
        private void lkp_sate_EditValueChanged(object sender, EventArgs e)
        {

            chlb_names.Items.Clear();

            sqll = (@"  SELECT dbo.T_PERSONE.id AS pers_id, dbo.T_PERSONE.name AS pers_name, dbo.T_PERS_STATE.id AS state_id, dbo.T_PERS_STATE.name AS state_name, 
                      dbo.T_PERS_STATE_CHANGE.change_date, dbo.T_PERS_STATE_CHANGE.pers_id_deside
FROM         dbo.T_PERS_STATE INNER JOIN
                      dbo.T_PERS_STATE_CHANGE ON dbo.T_PERS_STATE.id = dbo.T_PERS_STATE_CHANGE.state_id INNER JOIN
                      dbo.T_PERSONE ON dbo.T_PERS_STATE_CHANGE.pers_id = dbo.T_PERSONE.id
WHERE(state_id = " + Convert.ToInt32(lkp_sate.EditValue) + " and   " +
" dbo.T_PERS_STATE_CHANGE.change_date IN (SELECT     MAX(change_date) AS Expr1 " +
"FROM         dbo.T_PERS_STATE_CHANGE AS T_PERS_STATE_CHANGE_1" +
"  GROUP BY pers_id" +
" HAVING(pers_id = dbo.T_PERSONE.id)))");

            dt = c_db.select(sqll);

            chlb_names.chbl_iniatalize_data(dt, "pers_name", "pers_id");
            chlb_names.CheckAll();
        }
        private void lkp_type_EditValueChanged(object sender, EventArgs e)
        {

            chlb_names.DataSource = null;
            chlb_names.Items.Clear();

            sqll = (@"    SELECT T_PERSONE.name, T_PERS_TYPES_TRUE_FALSE.*
                        FROM         T_PERSONE INNER JOIN
                  T_PERS_TYPES_TRUE_FALSE ON T_PERSONE.id = T_PERS_TYPES_TRUE_FALSE.pers_id ");

            dt = c_db.select(sqll);



            DataTable dt_names_by_typ = new DataTable();
            dt_names_by_typ.Columns.Add("id");
            dt_names_by_typ.Columns.Add("name");
            foreach (DataColumn col in dt.Columns)
            {
                int index = dt.Columns.IndexOf(col);
                int val = Convert.ToInt32(lkp_type.ItemIndex);
                if (index == val + 3)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][index].ToString() != null && dt.Rows[i][index].ToString() != string.Empty)
                        {
                            Boolean state = Convert.ToBoolean(dt.Rows[i][index].ToString());
                            if (state == true)
                            {
                                dt_names_by_typ.Rows.Add(dt.Rows[i][2].ToString(), dt.Rows[i][0].ToString());
                            }
                        }
                    }
                    break;
                }
            }

            chlb_names.chbl_iniatalize_data(dt_names_by_typ, "name", "id");
            chlb_names.CheckAll();

        }
        private void btn_clear_pers_Click(object sender, EventArgs e)
        {
            chlb_names.DataSource = null;
            chlb_names.Items.Clear();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QURAANEY
{
  
    class C_TEST
    {
        /*  public override void neew()
        {
            base.neew();
        }
        public override void Refresh()
        {
            base.Refresh();
        }
        public override void get_data()
        {
            base.get_data();
        }
        public override void set_data()
        {
            base.set_data();
        }
        public override void save()
        {
            base.save();
        }

        public override void edite()
        {
            base.edite();
        }
        public override void delete()
        {
            base.delete();
        }
        public override void clear(Control.ControlCollection s_controls)
        {
            base.clear(s_controls);
        }
        public override void serche()
        {
            base.serche();
        }
        public override void print()
        {
            base.print();
        }
        public override void fill_controls()
        {
            base.fill_controls();
        }
        public override void fill_entitey()
        {
            base.fill_entitey();
        }
       public override bool validaate_data()
        {
            return true;
            base.validaate_data();
        }
        public override void max_id()
      {
          base.max_id();
      }

      *****************************
                  mAIN_CASH.cash_start_date = Convert.ToDateTime(string.Format(dtp_date.DateTime.ToShortDateString(), "yyyy/MM/dd"));
                        dtp_date.DateTime = (DateTime)mAIN_CASH.cash_start_date;

      *****************************
      *
      *****************************

      public override string delete_data(long s_id)
      {
          if (s_id > 0)
          {
              if (bANK != null)
                  cmd_bank.delet_data(bANK);
              get_data("d");
              base.delete_data();
              s_id = 0;
              return "";
          }
          try
          {
              if (base.delete_data() == "true")
              {
                  //  if (gv_details.RowCount > 0 && txt_id.Text != string.Empty && txt_name.Text != string.Empty)
                  if (gv_details.RowCount > 0)
                  {
                       if (bANK != null)
                           cmd_bank.delet_data(bANK);
                       get_data("d");
                  }
                  else
                      // check_data();
                      return "";
              }
          }
          catch (Exception ex)
          {
              get_data(ex.InnerException.ToString() + "/" + ex.Message);
          }
          get_data("");
          return "";
      }

      private void gv_details_DoubleClick(object sender, EventArgs e)
      {
          if (gv_details.RowCount > 0)
          {
              long id = Convert.ToInt64(gv_details.GetRowCellValue(gv_details.FocusedRowHandle, gv_details.Columns["id"]));
              bANK = cmd_bank.get_by(m => m.bank_id == id).FirstOrDefault();
              fill_controls();
          }
      }
      private void gv_details_KeyDown(object sender, KeyEventArgs e)
      {
          try
          {
              if (gv_details.RowCount > 0 && e.KeyData == Keys.Delete)
                  foreach (int rowid in gv_details.GetSelectedRows())
                  {
                      long id = Convert.ToInt64(gv_details.GetRowCellValue(rowid, "id"));
                      bANK = cmd_bank.get_by(m => m.bank_id == id).FirstOrDefault();
                      delete_data(id);
                  }
              get_data("d");

          }
          catch (Exception )
          {

          }
      }
        *****************************************************
        * T_DOCTORS doctor;//انستنانس اوبجكت من جدول الدوكتور
        public void fill_controls()
        {
            txt_doc_name.Text = doctor.doc_name.ToString();
            txt_doc_meger.Text = doctor.doc_meger.ToString();
            txt_doc_phone.Text = doctor.doc_phone.ToString();
            txt_doc_adress.Text = doctor.doc_adress.ToString();
            comb_clinic_id.Text = doctor.clinic_id.ToString();
        }
        public void fill_entity()
        {
            doctor.doc_name = txt_doc_name.Text.Trim();
            doctor.doc_meger = txt_doc_meger.Text.Trim();
            doctor.doc_phone = txt_doc_phone.Text.Trim();
            doctor.doc_adress = txt_doc_adress.Text.Trim();
            doctor.clinic_id = 1;
        }
        public override void get_data()
        {
            /*  gc_doc.DataSource= cmd_doctor.get_all(); هذه أو
            gv_doc.Columns.Remove(gv_doc.Columns["T_CLINICS"]);
            gv_doc.Columns.Remove(gv_doc.Columns["T_VISIT_CLINIC"]);
            //هدول العمودين جابون من كلاس الدكتور لانو عامل ربط مع هالجدولين


        //هذه
        gc_doc.DataSource = (from doc in cmd_doctor.get_all()
                                   select new
                                       {
                                           id = doc.doc_id,
                                           name = doc.doc_name,
                                           meger = doc.doc_meger,
                                           phone = doc.doc_phone,
                                           adress = doc.doc_adress
    }).OrderBy(d_id => d_id.id);
    gv_doc.Columns["id"].Caption = "الرقم";
            gv_doc.Columns["name"].Caption = "الاسم";
            gv_doc.Columns["meger"].Caption = "الاختصاص";
            gv_doc.Columns["phone"].Caption = "الهاتف";
            gv_doc.Columns["adress"].Caption = "العنوان";

            base.get_data();
    var max = cmd_doctor.get_all().Where(id => id.doc_id == cmd_doctor.get_all().Max(max1 => max1.doc_id)).FirstOrDefault();
    txt_doc_id.Text = max == null ? "1" : (max.doc_id + 1).ToString();
}
public override void clear_data(Control.ControlCollection s_controls)
{
    base.clear_data(s_controls);
    get_data();
}
private void f_doctor_data_Load(object sender, EventArgs e)
{
    get_data();
}
public override void insert_data(string status_mess)
{
    try
    {
        if (txt_doc_name.Text.Trim() != string.Empty)
        {
            doctor = new T_DOCTORS();
            doctor.doc_id = int.Parse(txt_doc_id.Text);
            fill_entity();

            cmd_doctor.insert_data(doctor);
            base.insert_data("done");
            get_data();
        }
    }
    catch (Exception ex)
    {
        base.insert_data(ex.InnerException.InnerException.ToString());
        get_data();
    }

}
public override void update_data(string status_mess)
{
    try
    {
        if (gv_doc.RowCount > 0 && txt_doc_id.Text.Trim() != string.Empty && txt_doc_name.Text.Trim() != string.Empty)
        {
            int id = int.Parse(txt_doc_id.Text);
            doctor = cmd_doctor.get_by(d_id => d_id.doc_id == id).FirstOrDefault();
            if (doctor != null)
            {
                fill_entity();
                cmd_doctor.update_data(doctor);
                base.update_data("done");
                get_data();
            }
        }
        else
            txt_doc_name.ErrorText = "يرجى اختيار اسم طبيب";
    }
    catch (Exception ex)
    {
        base.update_data(ex.InnerException.InnerException.ToString());
        get_data();
    }
}
public override void delete_data(string status_mess)
{
    try
    {
        if (gv_doc.RowCount > 0 && txt_doc_id.Text.Trim() != string.Empty && txt_doc_name.Text.Trim() != string.Empty)
        {
            int id = int.Parse(txt_doc_id.Text);
            doctor = cmd_doctor.get_by(d_id => d_id.doc_id == id).FirstOrDefault();
            if (doctor != null)
            {
                fill_controls();
                cmd_doctor.delet_data(doctor);
                base.delete_data("done");
                get_data();
            }
            else
                base.delete_data("");
        }
        else if (txt_doc_name.Text.Trim() == string.Empty)
            txt_doc_name.ErrorText = "يرجى اختيار اسم طبيب";
    }
    catch (Exception ex)
    {
        base.delete_data(ex.InnerException.InnerException.ToString());
        get_data();
    }

}
private void gv_doc_DoubleClick(object sender, EventArgs e)
{
    if (gv_doc.RowCount > 0)
    {
        int id = int.Parse(gv_doc.GetRowCellValue(gv_doc.FocusedRowHandle, gv_doc.Columns["id"]).ToString());
        doctor = cmd_doctor.get_by(d_id => d_id.doc_id == id).FirstOrDefault();
        if (doctor != null)
        {
            txt_doc_id.Text = doctor.doc_id.ToString();
            fill_controls();
        }
    }

}

private void txt_doc_phone_EditValueChanged(object sender, EventArgs e)
{

}
        *****************************************
          *****************************************
          *****************************************
          *  *****************************************
          *  
         private void f_main_cash_Load(object sender, EventArgs e)
        {
            get_data("");
        }
        public void fill_controls()
        {
            if (mAIN_CASH != null)
            {
                txt_id.Text = mAIN_CASH.cash_id.ToString();
                txt_code.Text = mAIN_CASH.cash_code.ToString();
                txt_name.Text = mAIN_CASH.cash_name;
                txt_start_balance.Text = mAIN_CASH.cash_start_balance.ToString();
                txt_zero_balance.Text = mAIN_CASH.cash_zero_balance.ToString();
                txt_cash_balance.Text = mAIN_CASH.cash_balance.ToString();
                txt_note.Text = mAIN_CASH.cash_note;
                dtp_date.DateTime = (DateTime)mAIN_CASH.cash_start_date;
                che_state.Checked = (bool)mAIN_CASH.cash_state;

                bRANCH = cmd_branch.get_by(b => b.bran_id==mAIN_CASH.bran_id).FirstOrDefault();
                comb_bran_id.Text = bRANCH.bran_name;
                comb_bran_id.EditValue = bRANCH.bran_id;
            }
        }
        public void fill_entitey()
        {
            mAIN_CASH.cash_id = Convert.ToInt64(txt_id.Text);
            mAIN_CASH.cash_code = txt_code.Text == string.Empty ? 0 : Convert.ToInt64(txt_code.Text);
            mAIN_CASH.cash_name = txt_name.Text.Trim();
            mAIN_CASH.cash_start_date = Convert.ToDateTime(string.Format(dtp_date.DateTime.ToShortDateString(), "yyyy/MM/dd"));
            mAIN_CASH.cash_start_balance = Convert.ToDecimal( txt_start_balance.Text.Trim());
            mAIN_CASH.cash_zero_balance = Convert.ToDecimal(txt_zero_balance.Text.Trim());
            mAIN_CASH.cash_balance = Convert.ToDecimal(txt_cash_balance.Text.Trim());
            mAIN_CASH.cash_state = Convert.ToBoolean(che_state.CheckState);
            mAIN_CASH.cash_note = txt_note.Text.Trim(); 
            mAIN_CASH.bran_id= Convert.ToInt64(comb_bran_id.EditValue);
        }
        public void check_data()
        {
            if (txt_code.Text == string.Empty)
                txt_code.ErrorText = "يرجى اختيار عنصر حقل مطلوب";
            if (txt_name.Text == string.Empty)
                txt_name.ErrorText = "يرجى اختيار عنصر حقل مطلوب";
        }
        public override void get_data(string status_mess)
        {try
            {
                var test = cmd_main_cash.get_all().ToList();
                if (test.Count > 0)
                {//الجوين لتجنب خطاء عند الادخال لفرع غير مدخل من قبل 
                    gc_details.DataSource = (from main_cash in cmd_main_cash.get_all()
                                             join bran in cmd_branch.get_all() on main_cash.bran_id equals bran.bran_id
                                             select new
                                             {
                                                 id = main_cash.cash_id,
                                                 name = main_cash.cash_name,
                                                 balance = main_cash.cash_balance,
                                                 state = main_cash.cash_state,
                                                 branch_id = bran.bran_id,
                                                 branch_name = bran.bran_name
                                             }).OrderBy(c_id => c_id.id);
                    gv_details.Columns["id"].Caption = "الرقم";
                    gv_details.Columns["name"].Caption = "اسم الصندوق";
                    gv_details.Columns["balance"].Caption = "الرصيد";
                    gv_details.Columns["state"].Caption = "الحالة";
                    gv_details.Columns["branch_id"].Caption = "رقم الفرع";
                    gv_details.Columns["branch_name"].Caption = "اسم الفرع";

                    gv_details.BestFitColumns();
                }
                base.get_data(status_mess);

                comb_bran_id.Properties.DataSource = (from bran in cmd_branch.get_all().Where(b => b.bran_stat == true)
                                                      select new
                                                      {
                                                          branch_id = bran.bran_id,
                                                          branch_name = bran.bran_name
                                                      }).OrderBy(b_id => b_id.branch_id);
                comb_bran_id.Properties.DisplayMember = "branch_name";
                comb_bran_id.Properties.ValueMember = "branch_id";
                comb_bran_id.Properties.PopulateColumns();
                comb_bran_id.Properties.Columns[0].Caption = "الرقم";
                comb_bran_id.Properties.Columns[1].Caption = "الاسم";

                var max = cmd_main_cash.get_all().Where(c_id => c_id.cash_id == cmd_main_cash.get_all().Max(m => m.cash_id)).FirstOrDefault();
                txt_id.Text = max == null ? "1" : (max.cash_id + 1).ToString();
                txt_code.Text = txt_id.Text;
            }
            catch (Exception ex)
            {
                get_data(ex.InnerException.ToString() + "/" + ex.Message);

            }
        }
        public override void insert_data()
        {
            try
            {
                if (txt_code.Text !=string.Empty && txt_name.Text != string.Empty)
                {
                    mAIN_CASH = new T_MAIN_CASH();
                    mAIN_CASH.cash_id = Convert.ToInt64(txt_id.Text);
                    fill_entitey();
                    cmd_main_cash.insert_data(mAIN_CASH);
                    get_data("i");
                    clear_data(this.Controls);
                    base.insert_data();
                }               
            }
            catch (Exception ex)
            {
                get_data(ex.InnerException.ToString() + "/" + ex.Message);
            }
        }

        private void gv_details_DoubleClick(object sender, EventArgs e)
        {
            if (gv_details.RowCount>0)
            {
                long id = Convert.ToInt64(gv_details.GetRowCellValue(gv_details.FocusedRowHandle, gv_details.Columns["id"]));
                mAIN_CASH = cmd_main_cash.get_by(m => m.cash_id==id).FirstOrDefault();
                fill_controls();
            }
        }
        public override void update_data()
        {
            try
            {
                if (gv_details.RowCount > 0 && txt_id.Text != string.Empty && txt_name.Text != string.Empty)
                {
                    if (mAIN_CASH != null)
                        fill_entitey();
                    cmd_main_cash.update_data(mAIN_CASH);
                    get_data("u");
                    base.update_data();
                }
                else
                    check_data();
            }
            catch (Exception ex)
            {
                get_data(ex.InnerException.ToString() + "/" + ex.Message);
            }
        }
        public override string delete_data( long s_id)
        {        
                if (s_id > 0)
                {
                    if (mAIN_CASH != null)
                        cmd_main_cash.delet_data(mAIN_CASH);
                    get_data("d");
                    base.delete_data();
                    s_id = 0;
                    return "";
                }
            try
            {
                if (base.delete_data() == "true")
                {
                    if (gv_details.RowCount > 0 && txt_id.Text != string.Empty && txt_name.Text != string.Empty)
                    {
                        if (mAIN_CASH != null)
                            cmd_main_cash.delet_data(mAIN_CASH);
                        get_data("d");
                    }
                    else
                        check_data();
                }
            }
            catch (Exception ex)
            {
                get_data(ex.InnerException.ToString() + "/" + ex.Message);
            }
           
            return "";
           
        }
        public override void clear_data(Control.ControlCollection s_controls)
        {
            base.clear_data(this.Controls);
            var max = cmd_main_cash.get_all().Where(c_id => c_id.cash_id == cmd_main_cash.get_all().Max(m => m.cash_id)).FirstOrDefault();
            txt_id.Text = max == null ? "1" : (max.cash_id + 1).ToString();

        }
        //حذف الاسطر المحددة عند الضغط عل زر ديليت
        private void gv_details_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (gv_details.RowCount > 0 && e.KeyData == Keys.Delete)
                    foreach (int rowid in gv_details.GetSelectedRows())
                    {
                        long id = Convert.ToInt64(gv_details.GetRowCellValue(rowid, "id"));
                        mAIN_CASH = cmd_main_cash.get_by(m => m.cash_id == id).FirstOrDefault();
                        delete_data( id);
                    }
                get_data("d");

            }
            catch(Exception )
            {

            }
        }
    }
}

*/
    }
}

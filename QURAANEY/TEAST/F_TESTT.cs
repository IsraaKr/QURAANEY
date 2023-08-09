using QURAANEY.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QURAANEY
{
    public partial class F_TESTT : F_INHERATENZ
    {
        T_TEST test;
        T_PERS_STATE pERS_STATE;
        public F_TESTT()
        {
            InitializeComponent();
            dtp_date.EditValue = DateTime.Today.ToShortDateString();
            neew();
        }

        private void F_TESTT_Load(object sender, EventArgs e)
        {

            load_data("");
            lkp_state.ReadOnly = false;
            lkp_state.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;

            gv.OptionsBehavior.Editable = false;//ايقاف التعديل
            gv.Columns[nameof(test.id)].Caption = "الرقم";
            gv.Columns[nameof(test.name)].Caption = "الاسم";
            gv.Columns[nameof(test.phone)].Caption = "الهاتف";
            gv.Columns[nameof(test.date)].Caption = "التاريخ";

            //الأحداث          
            gv.DoubleClick += Gv_DoubleClick;
            lkp_state.ProcessNewValue += Lkp_state_ProcessNewValue;

        }
        //إضافة قيمة جديدة عند الكتابة في lkp
        private void Lkp_state_ProcessNewValue(object sender, DevExpress.XtraEditors.Controls.ProcessNewValueEventArgs e)
        {
            if (e.DisplayValue is string s && s.Trim() != string.Empty)
            {//التحقق هل هو نص و هلو هو فارغ
                //إعطاء القيم للاوبجكت الجديد
                var new_state = new T_PERS_STATE() { name = s };
                using (var db = new DBDataContext())
                {
                    db.T_PERS_STATEs.InsertOnSubmit(new_state);
                    db.SubmitChanges();
                }
               //إضافة العنصر الجديد إلى lkp
               ((List<T_PERS_STATE>)lkp_state.Properties.DataSource).Add(new_state);
                e.Handled = true;

            }
        }
        private void Gv_DoubleClick(object sender, EventArgs e)
        {
            using (var db = new DBDataContext())
            {
                //جلب اي دي السطر الذي عملنا عليه دبل كليك
                int id = Convert.ToInt32(gv.GetFocusedRowCellValue("id"));
                //جلب البيانات بناء على الايدي الذي عملنا عليه دبل كليك
                test = db.T_TESTs.Where(x => x.id == id).First();//filter cartirial
                fill_controls();
                load_data("");
            }
        }
        public override void neew()
        {
            test = new T_TEST();
            dtp_date.Text = DateTime.Now.ToShortDateString();
            base.neew();//يوجد get و refresh من الوراثة
        }
        public override void save()
        {
            if (vallidate_data() == false)
                return;
            using (var db = new DBDataContext())
            {
                if (test.id == 0)//اذا جديد
                {
                    db.T_TESTs.InsertOnSubmit(test);
                }
                else//اذا تعديل
                {
                    db.T_TESTs.Attach(test);
                    //pERS_STATE = db.T_PERS_STATEs.Where(x => x.id == test.pers_state_id).First();
                    //pERS_STATE = db.T_PERS_STATEs.Single(x => x.id == test.pers_state_id);

                }
                set_data();
                db.SubmitChanges();//احفظ التغيرات

                load_data("");
            }
            base.save();
        }
        public override void delete()
        {
            using (var db = new DBDataContext())
            {
                if (MessageBox.Show("هل تريد الحذف", "تأكيد الحذف",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    db.T_TESTs.Attach(test);
                    db.T_TESTs.DeleteOnSubmit(test);
                    db.SubmitChanges();
                    neew();
                }
                load_data("");
            }
            base.delete();
        }
        public override void load_data(string status_mess)
        {
            using (var db = new DBDataContext())
            {
                gc.DataSource = db.T_TESTs.ToList();

                //اكستنشن انا عاملتو في كلاس الماستر
                lkp_state.lkp_iniatalize_data(db.T_PERS_STATEs.ToList(), nameof(pERS_STATE.name), nameof(pERS_STATE.id));
            }
            dtp_date.Text = DateTime.Now.ToShortDateString();
            base.load_data(status_mess);
        }
        public override void get_data()
        {
            fill_controls();
            base.get_data();
        }
        public override void set_data()
        {
            fill_entitey();
            base.set_data();
        }
        public override void fill_controls()
        {
            txt_id.Text = test.id.ToString();
            txt_name.Text = test.name;
            txt_phone.Text = test.phone;
            dtp_date.Text = test.date.ToString();
            che_is_active.Checked = Convert.ToBoolean(test.is_active);
            lkp_state.EditValue = test.pers_state_id;
            base.fill_controls();
        }
        public override void fill_entitey()
        {
            test.name = txt_name.Text;
            test.phone = txt_phone.Text;
            test.date = Convert.ToDateTime(string.Format(dtp_date.DateTime.ToShortDateString(), "yyyy/MM/dd"));
            test.is_active = che_is_active.Checked;
            test.pers_state_id = Convert.ToInt32(lkp_state.EditValue);
            base.fill_entitey();
        }
        public override bool vallidate_data()
        {
            using (var db = new DBDataContext())
            {
                if (txt_name.Text.Trim() == string.Empty)
                {//التأكد من حقل فارغ
                    txt_name.ErrorText = errore_text;
                    return false;
                }
                if (db.T_TESTs.Where(x => x.name.Trim() == txt_name.Text.Trim() && x.id != test.id).Count() > 0)
                {//التأكد من عدم تكرار الاسم
                    txt_name.ErrorText = "هذا الاسم موجود";
                    return false;
                }
                if (lkp_state.EditValue is int == false)
                //  if (lkp_state.EditValue == string.Empty)
                {//التأكد من عدم اختيار حالة في الكومبو بوكس
                    lkp_state.ErrorText = errore_text;
                    return false;
                }
            }
            return base.vallidate_data();
        }

        private void dtp_date_EditValueChanged(object sender, EventArgs e)
        {

        }

        /*  private void F_TEST_KeyDown(object sender, KeyEventArgs e)
          {
              if (e.KeyCode == Keys.F1)
              {
                 // save();
              }
          }

          private void F_TEST_Load(object sender, EventArgs e)
          {
              dtp_date.Text = DateTime.Now.ToShortDateString();
              get_data();
          }
          /*  public override void get_data()
            {
                // الاتصال بقاعدة البيانات
                DBDataContext db = new DBDataContext();

                tEST = db.T_TESTs.FirstOrDefault();//select top(1) جلب الداتا عند البداية
                if (tEST == null) return;//إذا الجدول فاضي
                fill_controls();

                base.get_data();
            }

            public override void save()
            {

                vallidate_data();
                DBDataContext db = new DBDataContext();//الاتصال بقاعدة البيانات         
                tEST = db.T_TESTs.FirstOrDefault();
                if (tEST ==null)//اذا كان الجدول فاضي ادخل جديد
                {
                    tEST = new T_TEST();//انستانس جديد من الجدول            
                    db.T_TESTs.InsertOnSubmit(tEST);//ادخل عندما نعمل سابميت تشينج
                }
                fill_entitey();
                db.SubmitChanges();//اعتمد التغيرات
                base.save();
            }


            public override bool vallidate_data()
            {
                if (txt_name.Text.Trim()==string.Empty)
                {
                    txt_name.ErrorText = errore_text;
                    return false;
                }
                return base.vallidate_data();
            }
            public override void fill_entitey()
            {
                tEST.name = txt_name.Text;
                tEST.phone = txt_phone.Text;
                tEST.date = Convert.ToDateTime(string.Format(dtp_date.DateTime.ToShortDateString(), "yyyy/MM/dd"));

                base.fill_entitey();
            }
            public override void fill_controls()
            {
                txt_id.Text = tEST.id.ToString();
                txt_name.Text = tEST.name;
                txt_phone.Text = tEST.phone;
                dtp_date.DateTime = (DateTime)tEST.date;
                base.fill_controls();
            }*/
    }
}

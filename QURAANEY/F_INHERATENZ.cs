using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
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
    public partial class F_INHERATENZ : Form
    {
        public F_INHERATENZ()
        {
            InitializeComponent();
        }
        public void change_states_message(string status_mess)
        {
            bar_states.Caption = "...";
            bar_states.ItemAppearance.Normal.BackColor = F_INHERATENZ.DefaultBackColor;
            if (status_mess == "")
            {
                return;
            }
            else if (status_mess == "i")
            {
                bar_states.Caption = "             تم الحفظ بنجاح              ";
                bar_states.ItemAppearance.Normal.BackColor = Color.DarkSeaGreen;
            }
           
            else if (status_mess == "d")
            {
                bar_states.Caption = "             تم الحذف بنجاح              ";
                bar_states.ItemAppearance.Normal.BackColor = Color.IndianRed;
            }
            else
            {
                MessageBox.Show(status_mess);
                bar_states.Caption = "            فشل الإجراء             ";
                bar_states.ItemAppearance.Normal.BackColor = Color.DarkOrange;
            }
        }
        public virtual void nav(Form f, PanelControl p)
        {
            f.TopLevel = false;
            f.Size = p.Size;
            f.Dock = DockStyle.Fill;
            p.Controls.Clear();
            p.Controls.Add(f);
            f.Show();
        }
        //  public static string eerrore_text = "هذا الحقل مطلوب";
        public string errore_text { get { return "هذا الحقل مطلوب"; } }
        public virtual void get_data()
        {
            
        }
        public virtual void set_data()
        {

        }
        public virtual void save()
        {
            timer_states_bar.Enabled = true;         
        }
        public virtual void neew()
        {
            get_data();
            load_data("");
        }
        public virtual void delete()
        {
            timer_states_bar.Enabled = true;
            //if (MessageBox.Show("هل تريد حذف البيانات ", "تنبيه !!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //    return "true";
            //else
            //    return "false";
        }
        public virtual void close()
        {
            this.Close();
        }
        public virtual void show()
        {        
        }
        //لتفريغ الحقول و لتغير جملة الستاتس
        public virtual void clear(Control.ControlCollection s_controls)
        {
       //كود لتفريغ كل محتوى الكونترولات
            Action<Control.ControlCollection> func = null;
            func = (controls) =>
            {
                foreach (Control c in controls)
                    if (c is TextBox)
                        (c as TextBox).Clear();
                    else if (c is DateEdit)
                       // (c as DateEdit).DateTime = DateTime.Now;
                            (c as DateEdit).Text = string.Empty;
                    else if (c is TimeEdit)
                        (c as TimeEdit).Time = DateTime.Now;
                    else if (c is LookUpEdit)
                         (c as LookUpEdit).EditValue=-1;
                        // (c as LookUpEdit).Text = (c as LookUpEdit).Properties.NullText;
                    //    (c as LookUpEdit).Text = string.Empty;
                    else if (c is DateTimePicker)
                        (c as DateTimePicker).Value = DateTime.Now;
                    else if (c is System.Windows.Forms.ComboBox)
                        (c as System.Windows.Forms.ComboBox).SelectedIndex = -1;
                    else if (c is GridControl)
                           (c as GridControl).DataSource = null;
                    //else if (c is GridView)
                    //    (c as GridView).Columns.Clear() ;
                    else
                        func(c.Controls);
            };
            func(s_controls);
        }
        public virtual void searche()
        {

        }
        public virtual void print()
        {

        }
        //تحديث
        public virtual void load_data(string status_mess)
        {
            change_states_message(status_mess);
        }
        //تعبئة البيانات من الانتتي إلى الكونترولز في الشاشة
        public virtual void fill_controls()
        {

        }
        //تعبئة البيانات في الأنتتي
        public virtual void fill_entitey()
        {

        }
        //التأكد من صحة البيانات
        public virtual bool vallidate_data()
        {
            return true;
        }
        //ماكس
        public virtual void max_id()
        {

        }
      
        private void F_INHERATENZ_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.F1)
                save();
            if (e.KeyCode == Keys.F2)
                neew();
            if (e.KeyCode == Keys.F3)
                delete();
            if (e.KeyCode == Keys.F4)
                load_data("");

        }

        private void F_INHERATENZ_Load(object sender, EventArgs e)
        {
            load_data("");
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            save();
            timer_states_bar.Enabled = true;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            neew();
            load_data("");
            timer_states_bar.Enabled = true;
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            delete();
            timer_states_bar.Enabled = true;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            clear(this.Controls);
            load_data("");
            timer_states_bar.Enabled = true;
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            print();
        }

        private void btn_exite_Click(object sender, EventArgs e)
        {
            close();
        }

        private void timer_states_bar_Tick(object sender, EventArgs e)
        {
            change_states_message("");
            timer_states_bar.Enabled = false;
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            show();
        }
    }
}

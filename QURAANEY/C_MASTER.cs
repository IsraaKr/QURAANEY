using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QURAANEY
{
    public static class C_MASTER
    {
      
        //تبديل الأرقام بنصوص
        public class value_and_id
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        //ليست للأسماء
        public static List<value_and_id> xxx = new List<value_and_id>()
        {
            new value_and_id(){id=0,name=""},
            new value_and_id(){id=1,name=""}
        };
       public enum keep_rate_dayes
        {
            أسبوع =7,
            شهر =30,
            سنة =365             
        }
        #region validation
        public static bool is_text_valid(this TextEdit txt)
        {
            if (txt.Text.Trim()==string.Empty)
            {
                txt.ErrorText = " هذا الحقل مطلوب  ";
                return false;
            }
            return true; 
        }
        public static bool is_editevalue_valid(this LookUpEditBase lkpb)
        {
            if (lkpb.is_editevalue_oftype_int() == false || lkpb.is_editevalue_valid_and_not_zero()==false)
            {
                lkpb.ErrorText = " هذا الحقل مطلوب  ";
                return false;
            }
            return true;
        }
        public static bool is_editevalue_valid_and_not_zero(this LookUpEditBase lkpb)
        {
            if (lkpb.is_editevalue_oftype_int() == false || Convert.ToInt32(lkpb.EditValue)==0)
            {
                lkpb.ErrorText = " هذا الحقل مطلوب  ";
                return false;
            }
            return true;
        }
        public static bool is_editevalue_oftype_int(this LookUpEditBase lkpb)
        {
            return (lkpb.EditValue is int ||
                lkpb.EditValue is byte || 
                Convert.ToInt32(lkpb.EditValue) < 0 == false ||
                lkpb.Text ==string.Empty ||
                lkpb.Text==null);
      
        }
        #endregion

        //طباعة هيدر في التقرير
        public static void print_header(string header, DevExpress.XtraGrid.GridControl gc)
        {
            PrintingSystem print = new PrintingSystem();

            PrintableComponentLink link = new PrintableComponentLink(print);
            print.Links.Add(link);
            link.Component = gc;

            string _printheadear = header;
            PageHeaderFooter phf = link.PageHeaderFooter as PageHeaderFooter;
            phf.Header.Content.Clear();
            phf.Header.Content.AddRange(new string[] { "", _printheadear, "" });
            phf.Header.Font = new System.Drawing.Font("song Ti", 14, System.Drawing.FontStyle.Bold);
            phf.Header.LineAlignment = BrickAlignment.Center;

            string strfooter = DateTime.Now.ToShortDateString();
            phf.Footer.Content.Clear();
            phf.Footer.Content.AddRange(new string[] { "", strfooter, "" });
            phf.Footer.Font = new System.Drawing.Font("song Ti", 14, System.Drawing.FontStyle.Bold);
            phf.Footer.LineAlignment = BrickAlignment.Near;

            link.CreateDocument();
            print.Document.RightToLeftLayout = true;
            print.PreviewRibbonFormEx.StartPosition = FormStartPosition.CenterScreen;
            print.PreviewRibbonFormEx.Show();
        }
        #region look_up_edite iniatalize_data
        //extenshes جعل التابع الذي ننشئه يظهر بعد الدوت في اسم العنصر
        //this هي من اجل الاكستنشنز
        public static void lkp_iniatalize_data(this LookUpEdit lkp , object data_source)
        {          
           lkp.lkp_iniatalize_data( data_source, "name", "id");//استدعاء الميثود التي بعده
        }
        //overloade إذا بنا نغير الاسم و ال الآي دي 
        //هي الأساسية
        public static void lkp_iniatalize_data(this LookUpEdit lkp, object data_source,
            string DisplayMember , string ValueMember)
        {
            lkp.Properties.DataSource = data_source;
            lkp.Properties.DisplayMember = DisplayMember;
            lkp.Properties.ValueMember = ValueMember;
            lkp.Properties.PopulateColumns();
           // lkp.Properties.Columns[ValueMember].Visible = false;
        }

        #endregion

        #region check_box_list  iniatalize_data
        //extenshes جعل التابع الذي ننشئه يظهر بعد الدوت في اسم العنصر
        //this هي من اجل الاكستنشنز
        public static void chbl_iniatalize_data(this CheckedListBoxControl  chbl, object data_source)
        {
            chbl.chbl_iniatalize_data(data_source, "name", "id");//استدعاء الميثود التي بعده
        }
        //overloade إذا بنا نغير الاسم و ال الآي دي 
        //هي الأساسية
        public static void chbl_iniatalize_data(this CheckedListBoxControl chbl, object data_source,
            string DisplayMember, string ValueMember)
        {
            chbl.DataSource = data_source;
            chbl.DisplayMember = DisplayMember;
            chbl.ValueMember = ValueMember;
          
        }

        #endregion

        #region check box combo box   iniatalize_data
        //extenshes جعل التابع الذي ننشئه يظهر بعد الدوت في اسم العنصر
        //this هي من اجل الاكستنشنز
        public static void chb_comb_iniatalize_data(this CheckedComboBoxEdit chb_comb, object data_source)
        {
            chb_comb.chb_comb_iniatalize_data(data_source, "name", "id");//استدعاء الميثود التي بعده
        }
        //overloade إذا بنا نغير الاسم و ال الآي دي 
        //هي الأساسية
        public static void chb_comb_iniatalize_data(this CheckedComboBoxEdit chb_comb, object data_source,
            string DisplayMember, string ValueMember)
        {
            chb_comb.Properties.DataSource = data_source;
            chb_comb.Properties.DisplayMember = DisplayMember;
            chb_comb.Properties.ValueMember = ValueMember;

        }

        #endregion

        #region list box control   iniatalize_data
        //extenshes جعل التابع الذي ننشئه يظهر بعد الدوت في اسم العنصر
        //this هي من اجل الاكستنشنز
        public static void lbc_iniatalize_data(this ListBoxControl lbc, object data_source)
        {
            lbc.lbc_iniatalize_data(data_source, "name", "id");//استدعاء الميثود التي بعده
        }
        //overloade إذا بنا نغير الاسم و ال الآي دي 
        //هي الأساسية
        public static void lbc_iniatalize_data(this ListBoxControl lbc, object data_source,
            string DisplayMember, string ValueMember)
        {
            lbc.DataSource = data_source;
            lbc.DisplayMember = DisplayMember;
            lbc.ValueMember = ValueMember;

        }

        #endregion
    }
}

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

namespace QURAANEY.SETTING
{
    public partial class F_SOURA_MANEGE :F_INHERATENZ
    {
        public F_SOURA_MANEGE()
        {
            InitializeComponent();
            neew();

        }
        T_SOURA sOURA;
        public override void neew()
        {

            Refresh();
            base.neew();
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
        public override void Refresh()
        {
            T_SOURA ins = new T_SOURA();
            var db = new DBDataContext();
            dt = c_db.select(@"SELECT        dbo.T_SOURA.*
FROM            dbo.T_SOURA");
            gc.DataSource = dt;

            #region تسمية أعمدة الغريد
            gv.Columns[nameof(ins.id)].Caption = "الرقم";
            gv.Columns[nameof(ins.soura_name)].Caption = "اسم السورة";
            gv.Columns[nameof(ins.aya_num)].Caption = "رقم الآية";
            gv.Columns[nameof(ins.aya_text)].Caption = "نص الآية";
            gv.Columns[nameof(ins.gozaa_num)].Caption = "رقم الجزء";
            gv.Columns[nameof(ins.page_num)].Caption = "رقم الصفحة";
            gv.Columns[nameof(ins.hezeb_num)].Caption = "رقم الحزب";
            gv.Columns[nameof(ins.robe_num)].Caption = "رقم الربع";
            gv.Columns[nameof(ins.soura_num)].Caption = "رقم السورة";
            gv.Columns[nameof(ins.code)].Caption = "الكود ";
            #endregion

            //gv.Columns[nameof(ins.soura_name)].Group();
            gv.OptionsFind.AllowFindPanel = true;
            gv.OptionsFind.AlwaysVisible = true;
            gv.ApplyColumnsFilter() ;
            gv.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = true;
            gv.OptionsFilter.ShowAllTableValuesInFilterPopup = true;
            
            // gv.ApplyFindFilter();
            base.Refresh();
        }
      
    }
}

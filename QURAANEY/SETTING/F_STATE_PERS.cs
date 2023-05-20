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
    public partial class F_STATE_PERS :F_INHERATENZ
    {
        public F_STATE_PERS()
        {
            InitializeComponent();
            view_inheretanz_butomes();
            load_data("");
        }
        int pers_id;
        DataTable dt;
        public F_STATE_PERS( int id)
        {
            pers_id = id;
            InitializeComponent();
            view_inheretanz_butomes();
            load_data("");
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
        public override void load_data(string status_mess)
        {

            string sqll = @" SELECT        dbo.T_PERSONE.id, dbo.T_PERSONE.name, dbo.T_PERS_STATE.name AS Expr1, dbo.T_PERS_STATE_CHANGE.pers_id, 
                         dbo.T_PERS_STATE_CHANGE.change_date, dbo.T_PERS_STATE_CHANGE.pers_id_deside
FROM            dbo.T_PERSONE LEFT OUTER JOIN
                         dbo.T_PERS_STATE_CHANGE ON dbo.T_PERSONE.id = dbo.T_PERS_STATE_CHANGE.pers_id LEFT OUTER JOIN
                         dbo.T_PERS_STATE ON dbo.T_PERS_STATE_CHANGE.state_id = dbo.T_PERS_STATE.id
WHERE        (dbo.T_PERSONE.id = "+pers_id+")";
            dt = c_db.select(sqll);
            gc.DataSource = dt;
            base.load_data(status_mess);
        }
    }
}

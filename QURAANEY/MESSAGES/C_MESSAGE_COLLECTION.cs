using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QURAANEY.MESSAGES
{
    public static class C_MESSAGE_COLLECTION
    {
        //messages
        public static void show_emptey_data_message(string mesg_text, string mesg_caption)
        {
            MessageBox.Show(mesg_text, mesg_caption,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //notification
        public static void show_add_note()
        {
            F_NOTIFICATION f = new F_NOTIFICATION();
            f.lbl_note.Text = "تمت الإضافة بنجاح ";
            f.Show();
        }
        public static void show_update_note()
        {
            F_NOTIFICATION f = new F_NOTIFICATION();
            f.lbl_note.Text = "تم التعديل  بنجاح ";
            f.Show();
        }
        public static void show_delete_note()
        {
            F_NOTIFICATION f = new F_NOTIFICATION();
            f.lbl_note.Text = "تم الحذف  بنجاح ";
            f.Show();
        }


    }
}

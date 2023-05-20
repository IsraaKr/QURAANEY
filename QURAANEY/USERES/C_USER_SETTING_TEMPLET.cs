using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QURAANEY.USERES
{
   public class C_USER_SETTING_TEMPLET
    {
        int profile_id { get; set; }
        public C_USER_SETTING_TEMPLET(int pro_id)//كونستراكتر لإدخال  رقم البروفايل
        {
            profile_id = pro_id;
            general = new general_setting(profile_id);
            privet = new privet_setting(profile_id);
        }
        public general_setting general;
        public privet_setting privet;
    }
    //كلاس من اجل كل تبويب في الأوكرديون كونترول
    public class general_setting
    {
        int profile_id { get; set; }
        public general_setting(int proid)
        {
            profile_id = proid;
        }
        //بروبرتيز من أجل كل صلاحية  
        public bool canChange_store { get; set; }
        public int defult_store { get; set; }
        public bool canChange_drower { get; set; }
        public int defult_drower { get; set; }
        public decimal max_page_num { get; set; }
    }
    public class privet_setting
    {
        int profile_id { get; set; }
        public privet_setting(int proid)
        {
            profile_id = proid;
        }

        public bool canChange_customer { get; set; }
        public int dafult_customer { get; set; }
        public bool canChange_vendore { get; set; }
        public int dafult_vendore { get; set; }

        public decimal max_page_num { get; set; }

    }
}

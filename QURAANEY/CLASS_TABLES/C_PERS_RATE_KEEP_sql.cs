using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace QURAANEY.CLASS_TABLES
{
  public  class C_PERS_RATE_KEEP_sql
    {
        public static DataTable dt;

        public static DataTable get_all_rate_keep()
        {
            return dt = c_db.select(@"select id , name  from T_PERS_RATE_KEEP");
        }
        public static DataTable get_all_rates_by_id_from_view( int pers_id)
        {
            return dt = c_db.select(@"SELECT        الاسم, [معدل الحفظ], [تاريخ التعديل]
FROM            V_PERS_RATE
         WHERE     (id = " + pers_id + ")");
        }
    }
}

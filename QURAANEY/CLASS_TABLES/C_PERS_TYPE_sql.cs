using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QURAANEY.CLASS_TABLES
{
    public class C_PERS_TYPE_sql
    {
        public static DataTable dt;
        public static DataTable get_all_types()
        {
            return dt = c_db.select(@"select id , name  from T_PERS_TYPE");
        }
        public static DataTable get_all_types_true_false_by_id(int pers_id)
        {
            return dt = c_db.select(@" SELECT dbo.T_PERS_TYPES_TRUE_FALSE.*
                                   FROM            dbo.T_PERS_TYPES_TRUE_FALSE
                              WHERE(pers_id = " + pers_id + ")");
        }
    }
}

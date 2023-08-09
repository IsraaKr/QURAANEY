using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QURAANEY.CLASS_TABLES
{
    class C_DEFULTES_sql
    {
        public static DataTable dt;
        public static DataTable get_all_defult()
        {
            return dt = c_db.select(@"  SELECT name, value, value_id FROM dbo.T_DEFULT_THWABET");
        }
    }
}

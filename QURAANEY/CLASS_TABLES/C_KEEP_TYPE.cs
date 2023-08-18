using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QURAANEY.CLASS_TABLES
{
   public class C_KEEP_TYPE
    {
        public static DataTable dt;
       
        public static DataTable get_all_keep_type()
        {
            dt = c_db.select(@"SELECT     id, name
FROM         T_SOURA_KEEP_TYPE");
            return dt;
        }
    }
}

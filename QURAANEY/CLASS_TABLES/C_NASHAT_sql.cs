using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QURAANEY.CLASS_TABLES
{
    public static class C_NASHAT_sql
    {
        public static DataTable dt;
        public static DataTable get_nashat_id_name()
        {
            return dt = c_db.select(@"select id , name  from dbo.T_NASHAT  ");
        }
        public static DataTable get_nashat_id_name_by_id(int nashat_id)
        {
            return dt = c_db.select(@"select id from  dbo.T_NASHAT 
                      where id=" + nashat_id + "");
        }
        public static DataTable is_finish(int nashat_id)
        {
            return dt = c_db.select(@"select finish from  dbo.T_NASHAT 
                      where id=" + nashat_id + "");
        }

    }
}

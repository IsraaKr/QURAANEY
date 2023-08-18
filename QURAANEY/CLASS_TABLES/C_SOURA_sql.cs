using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QURAANEY.CLASS_TABLES
{
  public  class C_SOURA_sql
    {
        public static DataTable dt;
        //اسماء السور
        public static DataTable get_soura_name()
        {
            dt = c_db.select(@"SELECT DISTINCT soura_name, soura_num
                              FROM         T_SOURA
                               ORDER BY soura_num");
            return dt;
        }

        public static DataTable get_mustalem_name()
        {
            dt = c_db.select(@"SELECT     T_PERSONE.name, T_PERSONE.id
FROM         T_PERSONE INNER JOIN
                      T_SOURA_KEEP ON T_PERSONE.id = T_SOURA_KEEP.pers_mustalem_id");
            return dt;
        }
    }
}

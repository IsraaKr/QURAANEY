using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QURAANEY.CLASS_TABLES
{
    public class C_PERSON_sql
    {
        public static DataTable dt;

        public static DataTable get_all_pers()
        {
            return dt = c_db.select(@"SELECT    T_PERSONE.id AS التسلسل , T_PERSONE.name AS الاسم, T_PERSONE.phone AS الهاتف, T_PERSONE.adress AS العنوان, T_PERSONE.email AS الايميل, T_PERSONE.studey AS الدراسة, 
                      T_PERSONE.woke AS العمل, T_PERSONE.in_date AS [تاريخ الالتحاق], T_PERSONE.is_active AS فعال, T_PERSONE.inviting_pers AS الداعي
                          FROM        T_PERSONE ");
        }
        public static DataTable get_pers_id_name()
        {
            return dt = c_db.select(@"select id , name  from T_PERSONE ");
        }
        public static DataTable get_pers_id_name_by_id(int pers_id  )
        {
            return dt = c_db.select(@"select id , name  from T_PERSONE
                      where id ="+pers_id+"");
        }
    }
}

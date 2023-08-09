using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QURAANEY.CLASS_TABLES
{
  public  class C_PERS_STATE_sql
    {
      public static  DataTable  dt ;
        public static DataTable get_last_state_by_id ( int pers_id)
        {   
        return   dt = c_db.select(@"    SELECT        pers_id, pers_name, state_id, state_name, change_date, pers_id_deside
                              FROM            dbo.V_STATE_MAX_DATE
                                   WHERE        (pers_id =  " + pers_id + ")")  ;
        }
        public static DataTable get_all_state()
        {
            return dt = c_db.select(@"select id , name  from T_PERS_STATE");
        }
         public static DataTable get_last_state_by_id_with_alias ( int pers_id)
        {   
        return   dt = c_db.select(@" SELECT        dbo.T_PERSONE.name AS [اسم الحافظ], dbo.T_PERS_STATE.name AS الحالة, dbo.T_PERS_STATE_CHANGE.change_date AS [تاريخ الحالة], 
                         dbo.T_PERS_STATE_CHANGE.pers_id_deside AS [مغير الحالة]
FROM            dbo.T_PERSONE LEFT OUTER JOIN
                         dbo.T_PERS_STATE_CHANGE ON dbo.T_PERSONE.id = dbo.T_PERS_STATE_CHANGE.pers_id LEFT OUTER JOIN
                         dbo.T_PERS_STATE ON dbo.T_PERS_STATE_CHANGE.state_id = dbo.T_PERS_STATE.id
WHERE        (dbo.T_PERSONE.id = " + pers_id + ")")  ;
        }
    }
}

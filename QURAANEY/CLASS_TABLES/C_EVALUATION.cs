using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace QURAANEY.CLASS_TABLES
{
    public class C_EVALUATION
    {
        public static DataTable dt;

        public static DataTable get_all_evaluation()
        {
            return dt = c_db.select(@"SELECT        id, name
FROM            dbo.T_SOURA_EVALUATION");
        }
    }
}

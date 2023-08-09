using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QURAANEY
{
    public class C_DB_QUERYS
    {
        //آخر أية من السورة
        public static int last_aya_in_soura(int soura_num)
        {
            if (soura_num > 0)
            {
                string sql = @"  SELECT soura_num, MAX( aya_num)
                            FROM T_SOURA
                            group by soura_num
                            having soura_num = " + soura_num + " ";
                DataTable dt = c_db.select(sql);
                return int.Parse(dt.Rows[0][1].ToString());
            }
            return -1;

        }

        //آخر أية من الصفحة
        public static int last_aya_in_page(int page_num)
        {
            if (page_num > 0)
            {
                string sql = @"   SELECT MAX(aya_num) AS Expr1, page_num
                             FROM dbo.T_SOURA
                            GROUP BY page_num
                            HAVING        (page_num = " + page_num + ")";

                DataTable dt = c_db.select(sql);
                return int.Parse(dt.Rows[0][1].ToString());
            }
            return -1;
        }
       
        //اسماء السور
        public static DataTable get_soura_name()
        {
          DataTable  dt = c_db.select(@"SELECT DISTINCT soura_name, soura_num
                              FROM         T_SOURA
                               ORDER BY soura_num");
            return dt;
        }
        //اسماء الأشخاص
        public static DataTable get_person_name()
        {
            DataTable dt = c_db.select(@"SELECT     id, name
                                      FROM  T_PERSONE ");
            return dt;
        }
        public static DataTable get_person_name_isactive()
        {
            DataTable dt = c_db.select(@"SELECT     id, name
                                      FROM  T_PERSONE  
                                       where (is_active = 1) ");
            return dt;
        }
       
        //الحالات بناء على رقم الشخص
        public static DataTable state_by_id( int pers_id)
        {
            DataTable dt = c_db.select(@"SELECT     T_PERS_STATE.name
                           FROM         T_PERS_STATE INNER JOIN
                      T_PERS_STATE_CHANGE ON T_PERS_STATE.id = T_PERS_STATE_CHANGE.state_id
                         WHERE     (T_PERS_STATE_CHANGE.pers_id = " +pers_id + ")");
            return dt;
        }
        //معدل الحفظ بناء على رقم الشخص
        public static DataTable rate_by_id(int pers_id)
        {
            DataTable dt = c_db.select(@"SELECT        dbo.T_PERS_RATE_KEEP.name
FROM            dbo.T_PERS_RATE_KEEP INNER JOIN
                         dbo.T_PERS_RATE_KEEP_CHANGE ON dbo.T_PERS_RATE_KEEP.id = dbo.T_PERS_RATE_KEEP_CHANGE.rate_id
WHERE        (dbo.T_PERS_RATE_KEEP_CHANGE.pers_id =  " + pers_id + ")");
            return dt;
        }
        //التقصير بناء على رقم الشخص
        public static DataTable fail_by_id(int pers_id)
        {
            DataTable dt = c_db.select(@"SELECT        dbo.T_PERSONE.name, COUNT(dbo.T_PERS_FAIL.pers_id) AS Expr1
FROM            dbo.T_PERSONE LEFT OUTER JOIN
                         dbo.T_PERS_FAIL ON dbo.T_PERSONE.id = dbo.T_PERS_FAIL.pers_id
WHERE        (dbo.T_PERSONE.id =" + pers_id + ")" +
"GROUP BY dbo.T_PERSONE.name ");
                    
            return dt;
        }
        //نسبة حفظ الآيات بناء  على رقم الشخص
        public static DataTable aya_rate_by_id(int pers_id)
        {
            DataTable dt = c_db.select(@" SELECT dbo.T_PERSONE.name, COUNT(dbo.T_SOURA_KEEP.aya_num) AS c
FROM dbo.T_PERSONE LEFT OUTER JOIN
                         dbo.T_SOURA_KEEP ON dbo.T_PERSONE.id = dbo.T_SOURA_KEEP.pers_hafez_id
WHERE        (dbo.T_PERSONE.id = " + pers_id + ")" +
"GROUP BY dbo.T_PERSONE.name ");

            return dt;
        }
        //نسبة حفظ الصفحات بناء  على رقم الشخص
        public static DataTable full_page_count_by_id(int pers_id)
        {
            DataTable dt = c_db.select(@" SELECT     T_PERSONE.id, T_PERSONE.name, V_COUNT_FULL_PAGE.count_page
FROM         T_PERSONE INNER JOIN
                      V_COUNT_FULL_PAGE ON T_PERSONE.id = V_COUNT_FULL_PAGE.pers_hafez_id 
                      where T_PERSONE.id = "+pers_id+" ");

            return dt;
        }
        //نسبة حفظ السور  بناء  على رقم الشخص
        public static DataTable soura_rate_by_id(int pers_id)
        {
            DataTable dt = c_db.select(@"                       SELECT     T_PERSONE.id, T_PERSONE.name, V_COUNT_FULL_SOURA_KEEP.count_soura
FROM         T_PERSONE INNER JOIN
                      V_COUNT_FULL_SOURA_KEEP ON T_PERSONE.id = V_COUNT_FULL_SOURA_KEEP.pers_hafez_id 
                      where T_PERSONE.id = "+pers_id+" ");

            return dt;
        }
    }
}


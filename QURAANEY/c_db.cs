using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QURAANEY
{
    class c_db
    {
        //public static SqlConnection con;
        public static SqlCommand comnd;
        public static SqlDataReader dr;
        //  public static SqlDataAdapter da;
        public static DataTable dt;
        public static int done;

        public static SqlConnection _Con;
        public static SqlConnection con  //عند كل استخدام ل كون نستخدم التابع و ليس الخاصية
        {
            get
            {
                if (_Con.State != ConnectionState.Open)
                    _Con.Open();
                return _Con;
            }
            set
            {
                _Con = value;
            }
        }
        //جلب اسم السيرفر
        public static string get_server_name()
        {
            string server_name = "";
            var registaryviewarray = new[] { RegistryView.Registry32, RegistryView.Registry64 };
            foreach (var registryview in registaryviewarray)
            {
                using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryview))
                using (var key = hklm.OpenSubKey(@"software\microsoft\microsoft sql server"))
                {
                    var instances = (string[])key?.GetValue("InstalledInstances");
                    if (instances != null)
                        foreach (var element in instances)
                            if (element == "MSSQLSERVER")
                                server_name = System.Environment.MachineName;
                            else
                                server_name = System.Environment.MachineName + @"\" + element;
                }
            }
            return server_name;
        }
        //الاتصال بالقاعدة
        public static void server_connection(string ser_name)
        {
            con = new SqlConnection(@"Data Source=" + ser_name + "; Integrated Security=true;");
        }
        //إنشاء قاعدة البيانات
        public static void create_DB(string db_name)
        {
            comnd = new SqlCommand("create database " + db_name, con);
            comnd.ExecuteNonQuery();

        }
        //الاتصال بقاعدة البيانات
        public static void db_conection(string server_name, string db_name)
        {
            con = new SqlConnection(@"Data Source=" + server_name + "; Initial Catalog=" + db_name + "; Integrated Security=true;");
        }
        //select
        public static DataTable select(string sql)
        {
            comnd = new SqlCommand(sql, con);
            dr = comnd.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        //insert_upadte_delete
        public static int insert_upadte_delete(string sql)
        {
            done = 0;
            comnd = new SqlCommand(sql, con);
            done = comnd.ExecuteNonQuery();
            return done;
        }
        //max id
        public static string max(string sql)
        {
            dr.Close();
            int x = 0;
            comnd = new SqlCommand(sql, con);
            dr = comnd.ExecuteReader();
            while (dr.Read())
            {
                if (x < Int32.Parse(dr[0].ToString()))
                    x = Int32.Parse(dr[0].ToString());
            }

            dr.Close();
            return x.ToString();
        }
        //إنشاء الجداول
        public static void alter_pers_types(string state, string col_name)
        {
            if (state == "add")
            {
                string sql_tb1 = "alter table T_PERS_TYPES_TRUE_FALSE add " + col_name + " bit ";

                SqlCommand tb1 = new SqlCommand(sql_tb1, con);
                tb1.ExecuteNonQuery();
            }
            if (state == "drop")
            {
                string sql_tb1 = "alter table T_PERS_TYPES_TRUE_FALSE drop  column " + col_name + " ";
                SqlCommand tb1 = new SqlCommand(sql_tb1, con);
                tb1.ExecuteNonQuery();
            }
        }
        public static void teste()
        {
            dr.Close();
            int x = 0;
            string sql = @"USE [MY_QURAAN]
GO
/****** Object:  Table [dbo].[T_DEFULT_THWABET]    Script Date: 04/30/2023 13:23:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_DEFULT_THWABET](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
	[value] [nvarchar](max) NULL,
	[value_id] [int] NULL,
 CONSTRAINT [PK_T_DEFULT_THWABET] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[T_DEFULT_THWABET] ON
INSERT [dbo].[T_DEFULT_THWABET] ([id], [name], [value], [value_id]) VALUES (1, N'pers_state', N'مرشح', 1)
INSERT [dbo].[T_DEFULT_THWABET] ([id], [name], [value], [value_id]) VALUES (2, N'pers_type', N'حافظ', 1)
INSERT [dbo].[T_DEFULT_THWABET] ([id], [name], [value], [value_id]) VALUES (3, N'keep_rate', N'أربع صفحات بالشهر', 1)
INSERT [dbo].[T_DEFULT_THWABET] ([id], [name], [value], [value_id]) VALUES (4, N'user_type', N'مستخدم ', 1)
INSERT [dbo].[T_DEFULT_THWABET] ([id], [name], [value], [value_id]) VALUES (5, N'keep_evaluation', N'جيد', 1)
INSERT [dbo].[T_DEFULT_THWABET] ([id], [name], [value], [value_id]) VALUES (6, N'keep_type', N'شهري', 1)
SET IDENTITY_INSERT [dbo].[T_DEFULT_THWABET] OFF";
            comnd = new SqlCommand(sql, con);
            dr = comnd.ExecuteReader();
            //while (dr.Read())
            //{
            //    if (x < Int32.Parse(dr[0].ToString()))
            //        x = Int32.Parse(dr[0].ToString());
            //}

            dr.Close();

        }


    }
}

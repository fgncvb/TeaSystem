using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Common_Module.StringTool;

namespace DataBase_Module
{

    public class MYSQLDBHelper
    {
        /// <summary>
        /// 设置数据库连接信息
        /// 日期：2014年12月21日 11:52:43
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <returns>SqlConnection</returns>
        private static SqlConnection GetConnection(string databasename)
        {
            string dynamicEncryptionKey = ConfigurationManager.AppSettings["DynamicEncryptionKey"]; // 动态加解密常量Key
            if (string.IsNullOrEmpty(dynamicEncryptionKey))
            {
                return null;
            }
            dynamicEncryptionKey = dynamicEncryptionKey.ToString().Trim();

            SqlConnection conn = null;
            DynamicEncryptionHelper deh = new DynamicEncryptionHelper(dynamicEncryptionKey);
            string connStr = ConfigurationManager.AppSettings[databasename].ToString().Trim();

            if (string.IsNullOrEmpty(connStr))
            {
                return conn;
            }
            //connStr = deh.DecryptString(connStr);
            conn = new SqlConnection(connStr);
            return conn;
        }

        /// <summary>
        /// 执行SQL语句，并返回单行单列数据(Object类型)
        /// 日期：2014年12月21日 11:51:32
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="sql"></param>
        /// <returns>object</returns>
        public static object ExecuteScalar(string databasename, string sql)
        {
            return ExecuteScalar(databasename, sql, null);
        }

        /// <summary>
        /// 执行SQL语句，并返回单行单列数据(Object类型)
        /// 日期：2014年12月21日 11:50:58
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>object</returns>
        public static object ExecuteScalar(string databasename, string sql, SqlParameter[] parameters)
        {
            object result = null;
            try
            {
                using (SqlConnection connection = GetConnection(databasename))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        connection.Open();
                        result = cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 执行SQL语句，并返回受影响的行数
        /// 日期：2014年12月21日 11:49:45
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="sql"></param>
        /// <returns>int</returns>
        public static int ExecuteNonQuery(string databasename, string sql)
        {
            return ExecuteNonQuery(databasename, sql, null);
        }

        /// <summary>
        /// 执行SQL语句，并返回受影响的行数
        /// 日期：2014年12月21日 11:48:42
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>int</returns>
        public static int ExecuteNonQuery(string databasename, string sql, SqlParameter[] parameters)
        {
            int result = -1;
            try
            {
                using (SqlConnection connection = GetConnection(databasename))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        connection.Open();
                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 执行SQL语句，并返回SqlDataReader类型
        /// 日期：2014年12月21日 11:48:14
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="sql"></param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string databasename, string sql)
        {
            return ExecuteReader(databasename, sql, null);
        }

        /// <summary>
        /// 执行SQL语句，并返回SqlDataReader类型
        /// 日期：2014年12月21日 11:47:39
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string databasename, string sql, SqlParameter[] parameters)
        {
            SqlDataReader result = null;
            try
            {
                SqlConnection connection = GetConnection(databasename);
                SqlCommand cmd = new SqlCommand(sql, connection);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                connection.Open();
                result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 执行SQL语句，并返回DataTable类型
        /// 日期：2014年12月21日 11:46:24
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="sql"></param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteTable(string databasename, string sql)
        {
            return ExecuteTable(databasename, sql, null);
        }

        /// <summary>
        /// 执行SQL语句，并返回DataTable类型
        /// 日期：2014年12月21日 11:46:47
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteTable(string databasename, string sql, SqlParameter[] parameters)
        {
            DataTable result = null;
            try
            {
                using (SqlConnection connection = GetConnection(databasename))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        connection.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        result = new DataTable();
                        da.Fill(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 执行存储过程，并返回多个值
        /// 日期：2014年12月21日 11:37:45
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="procedureName"></param>
        /// <returns>Hashtable</returns>
        public static Hashtable RunProcedure(string databasename, String procedureName)
        {
            return RunProcedure(databasename, procedureName, null);
        }

        /// <summary>
        /// 执行存储过程，并返回多个值
        /// 日期：2014年12月21日 11:37:15
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns>int</returns>
        public static Hashtable RunProcedure(string databasename, String procedureName, SqlParameter[] parameters)
        {
            Hashtable result = null;
            try
            {
                using (SqlConnection connection = GetConnection(databasename))
                {
                    using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        result = new Hashtable();

                        foreach (SqlParameter param in cmd.Parameters)
                        {
                            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput || param.Direction == ParameterDirection.ReturnValue)
                            {
                                result.Add(param.ParameterName, param.Value);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static int ExecProcedure(string databasename, String procedureName)
        {
            return ExecProcedure(databasename, procedureName, null);
        }

        /// <summary>
        /// 执行存储过程，并返回受影响的行数
        /// 日期：2014年12月21日 11:38:38
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns>int</returns>
        public static int ExecProcedure(string databasename, String procedureName, SqlParameter[] parameters)
        {
            int result = -1;
            try
            {
                using (SqlConnection connection = GetConnection(databasename))
                {
                    using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        connection.Open();
                        result = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 执行存储过程，并返回单行单列值(Object类型)
        /// 日期：2014年12月21日 11:39:50
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="procedureName"></param>
        /// <returns>Object</returns>
        public static Object ProcedureScalar(string databasename, String procedureName)
        {
            return ExecProcedure(databasename, procedureName, null);
        }

        /// <summary>
        /// 执行存储过程，并返回单行单列值(Object类型)
        /// 日期：2014年12月21日 11:40:11
        /// 陈宇龙
        /// <param name="databasename"></param>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns>Object</returns>
        public static Object ProcedureScalar(string databasename, String procedureName, SqlParameter[] parameters)
        {
            Object result = null;
            try
            {
                using (SqlConnection connection = GetConnection(databasename))
                {
                    using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        connection.Open();
                        result = cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        /// <summary>
        /// 执行存储过程，并返回SqlDataReader对象
        /// 日期：2014年12月21日 11:40:52
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="procedureName"></param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ProcedureReader(string databasename, String procedureName)
        {
            return ProcedureReader(databasename, procedureName, null);
        }

        /// <summary>
        /// 执行存储过程，并返回SqlDataReader对象
        /// 日期：2014年12月21日 11:41:13
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ProcedureReader(string databasename, String procedureName, SqlParameter[] parameters)
        {
            SqlDataReader result = null;
            try
            {
                SqlConnection connection = GetConnection(databasename);
                SqlCommand cmd = new SqlCommand(procedureName, connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                connection.Open();
                result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 执行存储过程，并返回DataTable类型
        /// 日期：2014年12月21日 11:42:52
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="procedureName"></param>
        /// <returns>DataTable</returns>
        public static DataTable ProcedureTable(string databasename, String procedureName)
        {
            return ProcedureTable(databasename, procedureName, null);
        }

        /// <summary>
        /// 执行存储过程，并返回DataTable类型
        /// 日期：2014年12月21日 11:43:13
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns>DataTable</returns>
        public static DataTable ProcedureTable(string databasename, String procedureName, SqlParameter[] parameters)
        {
            DataTable result = null;
            try
            {
                using (SqlConnection connection = GetConnection(databasename))
                {
                    using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        connection.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        result = new DataTable();
                        da.Fill(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 执行存储过程，并返回DataTable类型(通常用于数据分页查询)
        /// 日期：2014年12月21日 11:43:57
        /// 陈宇龙
        /// </summary>
        /// <param name="databasename"></param>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <param name="recordCount"></param>
        /// <returns>DataTable</returns>
        public static DataTable ProcedureTable(string databasename, String procedureName, SqlParameter[] parameters, out int recordCount)
        {
            DataTable dt = null;
            recordCount = 0;
            try
            {
                using (SqlConnection connection = GetConnection(databasename))
                {
                    using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        connection.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        dt = new DataTable();
                        da.Fill(dt);
                        recordCount = int.Parse(cmd.Parameters["@RecordCount"].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Google.Protobuf.Collections;

namespace GrpcServer
{
    public class DBOperationHelper
    {
        static string str = ConfigurationManager.ConnectionStrings["BisSqlConnection_ReadWrite"].ConnectionString;
        public static IDbConnection CreateConnection(IDbConnection conn = null, string Path = "")
        {
            if (conn == null)
            {
                conn = new SqlConnection(str);
            }
            if (conn != null && conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }


        public static IEnumerable<T> GetList<T>(string sql, object param = null) where T : class
        {
            using (IDbConnection connection = CreateConnection())
            {
                return connection.Query<T>(sql, param);
            }
        }
        public static IEnumerable<T> GetList<T>(IDbConnection connection, string sql, object param = null) where T : class
        {
            return connection.Query<T>(sql, param);
        }

        public static async Task<IEnumerable<T>> GetListAsync<T>(string sql, object param = null) where T : class
        {
            try
            {
                using (IDbConnection connection = CreateConnection())
                {
                    return await connection.QueryAsync<T>(sql, param).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<RepeatedField<T>> GetRepeatedFieldListAsync<T>(string sql, object param = null) where T : class
        {
            using (IDbConnection connection = CreateConnection())
            {
                var items = await connection.QueryAsync<T>(sql, param);
                return items as RepeatedField<T>;
            }
        }

        public static async Task<IEnumerable<T>> GetListAsync<T>(IDbConnection connection, string sql, object param = null) where T : class
        {
            return await connection.QueryAsync<T>(sql, param).ConfigureAwait(false);
        }


        public static int Execute(string sql, object param = null)
        {
            using (IDbConnection connection = CreateConnection())
            {
                return connection.Execute(sql, param);
            }
        }


        public static int Execute(IDbConnection connection, string sql, object param = null)
        {
            return connection.Execute(sql, param);
        }


        public static async Task<int> ExecuteAsync(string sql, object param = null)
        {
            using (IDbConnection connection = CreateConnection())
            {
                return await connection.ExecuteAsync(sql, param).ConfigureAwait(false);
            }
        }

        public static async Task<int> ExecuteAsync(IDbConnection connection, string sql, object param = null)
        {

            return await connection.ExecuteAsync(sql, param).ConfigureAwait(false);
        }


        public static T ExecuteScalar<T>(string sql, object param = null)
        {
            using (IDbConnection connection = CreateConnection())
            {
                return connection.ExecuteScalar<T>(sql, param);
            }
        }


        public static T ExecuteScalar<T>(IDbConnection connection, string sql, object param = null)
        {
            return connection.ExecuteScalar<T>(sql, param);
        }


        public static async Task<T> ExecuteScalarAsync<T>(string sql, object param = null)
        {
            using (IDbConnection connection = CreateConnection())
            {
                return await connection.ExecuteScalarAsync<T>(sql, param).ConfigureAwait(false);
            }
        }


        public static async Task<T> ExecuteScalarAsync<T>(IDbConnection connection, string sql, object param = null)
        {
            return await connection.ExecuteScalarAsync<T>(sql, param).ConfigureAwait(false);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
/* 作者：道法自然  
 * 个人邮件：myyangbin@sina.cn
 * 2014-10-1
 */

using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq.Expressions;
using AiThinker.AiExpression;

namespace ConsoleApplication34
{
    public class City
    {
        public int Id { set; get; }

        public int NationId { set; get; }

        public int? ProvinceId { get; set; }

        public string CnName { get; set; }

        public string EnName { get; set; }

        public string Comment { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }

    public static class DbCustomersTest
    {
        /// <summary>
        /// 构建一个表达式方法 
        /// </summary>
        /// <param name="aiExp">表达式</param>
        /// <returns></returns>
        public static Customers GetCustomers(Expression<Func<IQueryable<Customers>, IQueryable<Customers>>> aiExp)
        {
            AiExpConditions<Customers> expc = new AiExpConditions<Customers>();
            expc.Add(aiExp);
            return Customers.Data_Fetch(expc.Where(), expc.OrderBy());
        }

        public static string GetUserSql()
        {
            AiExpConditions<City> expc = new AiExpConditions<City>();
            expc.AddAndWhere(x => x.CnName.StartsWith("abc") && x.LastModificationTime > DateTime.Now);
            expc.AddAndWhere(x => x.NationId == 123);
            expc.AddOrWhere(x => x.NationId == 234);
            var where = expc.Where();
            return where;
        }

    }



    /// <summary>
    /// 客户管理
    /// </summary>
    public class Customers
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        public static Customers Data_Fetch(string aiWhere, string aiOrderBy)
        {
            var cnstr = ConfigurationManager.ConnectionStrings["DemoConnectionString"].ConnectionString;
            Customers aiCustomers = new Customers();
            StringBuilder aisd = new StringBuilder();
            using (SqlConnection cn = new SqlConnection(cnstr))
            {
                #region 数据访问
                #region 查询SQL语句
                aisd.Append(" SELECT ");
                aisd.Append(" CustomerID,CompanyName,Address ");
                aisd.Append(" FROM  Customers ");
                aisd.Append(aiWhere);
                aisd.Append(aiOrderBy);
                #endregion
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(aisd.ToString(), cn))
                {
                    using (SqlDataReader myDataReader = cmd.ExecuteReader())
                    {
                        if (myDataReader.HasRows)
                        {
                            myDataReader.Read();
                            aiCustomers = new Customers
                            {
                                #region  类赋值
                                CustomerID = (string)myDataReader["CustomerID"],
                                CompanyName = (string)myDataReader["CompanyName"],
                                Address = (string)myDataReader["Address"]

                                #endregion
                            };
                        }

                    };
                }

                #endregion

            }
            return aiCustomers;
        }

    }


}

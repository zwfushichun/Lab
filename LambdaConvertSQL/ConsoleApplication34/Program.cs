/* 作者：道法自然  
 * 个人邮件：myyangbin@sina.cn
 * 2014-10-1
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AiThinker.AiExpression;

namespace ConsoleApplication34
{
    

    class Program
    {
        static void Main(string[] args)
        {
            string sql = DbCustomersTest.GetUserSql();
            //Customers aic = DbCustomersTest.GetCustomers(c => c.Where(o => o.CustomerID == "ALFKI"));

            //Customers aic2 = DbCustomersTest.GetCustomers(c => c.Where(o => o.CustomerID.Contains("CO")).OrderBy(o=>o.CompanyName));
            //Expression<Func<IQueryable<MyTestA>, IQueryable<MyTestA>>> exp
            //    = o => o.Where(w => (w.EmployeeID > (2 + 3)) && (w.LastName.StartsWith("a"))
            //        ).OrderBy(k => k.LastName).OrderByDescending(k => k.EmployeeID).OrderBy(k => k.Name).Where(m => m.EmployeeID < 100);


            ////Console.WriteLine(ZnExpTurnSql.BizLambdToSql(exp));
            ////Console.WriteLine(ZnExpTurnSql.BizLambdToSqlOrder(exp));
            ////Console.WriteLine(ZnExpTurnSql.BizLambdToSqlWhere(exp));

            //MyTestB b = new MyTestB() { EmployeeID = 20 };


            //AiExpConditions<MyTestA> expc = new AiExpConditions<MyTestA>();

            ////expc.AddAndWhere(() => { return b.EmployeeID > 20; }, w => w.EmployeeID > 0);
            ////expc.AddAndWhere(() => { return b.EmployeeID <= 20; }, w => w.EmployeeID <20);


            //expc.AddAndWhere(() => { return b.EmployeeID > 20; },



            //    w => w.EmployeeID > 0, w => w.EmployeeID < 20);
            //expc.AddOrWhere(w => w.LastName.StartsWith("a"));

            //expc.AddOrderBy<string>(w => w.Name);




            ////if (1 == 1)
            ////{
            ////    expc.Add(o => o.Where(w => (w.EmployeeID > (2 + 3)) && (w.LastName.StartsWith("a"))));

            ////}
            ////if (2 == 2)
            ////{
            ////    expc.Add(o => o.OrderBy(k => k.LastName).OrderByDescending(k => k.EmployeeID).OrderBy(k => k.LastName).Where(m => m.EmployeeID < 100));
            ////}

            ////if (3 == 3)
            ////{
            ////    expc.Add(o => o.Where(w => (w.EmployeeID > (2 + 3)) && (w.LastName.StartsWith("a"))), BizExpUnion.bizOr);

            ////}
            //var s1 = expc.Where();
            //var s2 = expc.OrderBy();
            //Console.WriteLine("");
            //Console.WriteLine(s1);
            //Console.WriteLine(s2);

            //mytest<MyTestA>(o => o.Where(w => (w.EmployeeID > (2 + 3)) && (w.LastName.StartsWith("a"))
            //        ).OrderBy(k => k.LastName).OrderByDescending(k => k.EmployeeID).OrderBy(k => k.Name).Where(m => m.EmployeeID < b.EmployeeID));
            Console.ReadLine();
        }
        static void mytest<T>(Expression<Func<IQueryable<T>, IQueryable<T>>> bizExp)
        {
            AiExpConditions<T> expc = new AiExpConditions<T>();
            expc.Add(bizExp);

            var s1 = expc.Where();
            var s2 = expc.OrderBy();
            Console.WriteLine("");
            Console.WriteLine(s1);
            Console.WriteLine(s2);
        }

    }

}

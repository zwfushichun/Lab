/* 作者：道法自然  
 * 个人邮件：myyangbin@sina.cn
 * 2014-10-1
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication34
{
    [Serializable]
    public class MyTest
    {
        public string LastName { get; set; }

        public int EmployeeID { get; set; }


    }

    [Serializable]
    public class MyTestA
    {
        public string LastName { get; set; }

        public int EmployeeID { get; set; }

        public string Name { get; set; }


    }

    [Serializable]
    public class MyTestB
    {
        public string LastName { get; set; }

        public int EmployeeID { get; set; }


    }



}

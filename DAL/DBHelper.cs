using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class DBHelper
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection("Data Source=KHIZEEEE\\SQLEXPRESS;Initial Catalog=apidemo;Integrated Security=True;Trust Server Certificate=True ");
        }
    }
}

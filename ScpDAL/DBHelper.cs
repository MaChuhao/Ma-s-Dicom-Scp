using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Resources;
using System.Reflection;

namespace MCHDAL
{
    public class DBHelper
    {
        public static SqlConnection conn = new SqlConnection(Resources.SQLConnectionStr);     
    }
}

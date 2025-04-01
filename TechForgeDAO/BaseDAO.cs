using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechForgeDAO
{
  public class BaseDAO
  {
    protected readonly string connStr;
    public BaseDAO(string _connStr)
    {
      connStr = !string.IsNullOrWhiteSpace(_connStr) ? _connStr : throw new ArgumentNullException(nameof(_connStr), "Connection string cannot be null or empty.");
    }
    protected SqlConnection CreateConnection()
    {
      return new SqlConnection(connStr);
    }
  }
}

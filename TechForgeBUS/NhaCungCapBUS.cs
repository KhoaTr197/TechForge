using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechForgeDAO;
using TechForgeDTO;

namespace TechForgeBUS
{
  public class NhaCungCapBUS
  {
    private readonly NhaCungCapDAO DAO;
    public NhaCungCapBUS(string _connStr)
    {
      this.DAO = new NhaCungCapDAO(_connStr);
    }
    public DataSet GetAllDisconnected(DataSet ds)
    {
        return this.DAO.GetAllDisconnected(ds);
    }
    public List<NhaCungCapDTO> GetAllConnected()
    {
      return this.DAO.GetAllConnected();
    }
  }
}
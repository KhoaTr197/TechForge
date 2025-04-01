using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechForgeDAO;
using TechForgeDTO;

namespace TechForgeBUS
{
  public class NguoiDungBUS
  {
    private readonly NguoiDungDAO DAO;
    public NguoiDungBUS(string _connStr)
    {
      this.DAO = new NguoiDungDAO(_connStr);
    }
    public List<NguoiDungDTO> GetAllConnected()
    {
      return this.DAO.GetAllConnected();
    }
  }
}

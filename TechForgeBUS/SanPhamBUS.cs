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
  public class SanPhamBUS
  {
    private readonly SanPhamDAO DAO;
    public SanPhamBUS(string _connStr) {
      this.DAO = new SanPhamDAO(_connStr);
    }
    public List<SanPhamDTO> GetAllConnected()
    {
      return this.DAO.GetAllConnected();
    }
    public DataSet GetAllDisconnected()
    {
      return this.DAO.GetAllDisconnected();
    }
    public int Add(SanPhamDTO sp)
    {
      return this.DAO.Add(sp);
    }
    public bool Update(SanPhamDTO sp)
    {
      return this.DAO.Update(sp);
    }
    public bool Delete(int id)
    {
      return this.DAO.Delete(id);
    }
  }
}

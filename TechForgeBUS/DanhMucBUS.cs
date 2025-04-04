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
  public class DanhMucBUS
  {
    private readonly DanhMucDAO DAO;
    public DanhMucBUS(string _connStr)
    {
      this.DAO = new DanhMucDAO(_connStr);
    }
    public List<DanhMucDTO> GetAllConnected()
    {
      return this.DAO.GetAllConnected();
    }
    public DataSet GetAllDisconnected(DataSet ds)
    {
      return this.DAO.GetAllDisconnected(ds);
    }
    public int Add(DanhMucDTO newCategory)
    {
      return this.DAO.Add(newCategory);
    }
    public bool Update(DanhMucDTO newCategory)
    {
      return this.DAO.Update(newCategory);
    }
    public bool Delete(int id)
    {
      return this.DAO.Delete(id);
    }
  }
}

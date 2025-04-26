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
  public class HangSanXuatBUS
  {
    private readonly HangSanXuatDAO DAO;
    public HangSanXuatBUS(string _connStr)
    {
      this.DAO = new HangSanXuatDAO(_connStr);
    }
    public List<HangSanXuatDTO> GetAllConnected()
    {
      return this.DAO.GetAllConnected();
    }
    public DataSet GetAllDisconnected(DataSet ds)
    {
      return this.DAO.GetAllDisconnected(ds);
    }
    public int Add(HangSanXuatDTO newManufacturer)
    {
      return this.DAO.Add(newManufacturer);
    }
    public bool Update(HangSanXuatDTO newManufacturer)
    {
      return this.DAO.Update(newManufacturer);
    }
    public bool Delete(int id)
    {
      return this.DAO.Delete(id);
    }

    public int GetNextId()
    {
      return this.DAO.GetNextId();
    }
  }
}

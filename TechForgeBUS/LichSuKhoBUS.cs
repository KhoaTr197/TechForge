using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechForgeDAO;
using TechForgeDTO;

namespace TechForgeBUS
{
  public class LichSuKhoBUS
  {
    private LichSuKhoDAO DAO;
    public LichSuKhoBUS(string connStr)
    {
      DAO = new LichSuKhoDAO(connStr);
    }

    public List<LichSuKhoDTO> GetAllConnected()
    {
      return this.DAO.GetAllConnected();
    }

    public List<ChiTietLichSuKhoDTO> GetDetail(int logId)
    {
      return this.DAO.GetDetail(logId);
    }

    public int Add(LichSuKhoDTO newLog)
    {
      return this.DAO.Add(newLog);
    }

    public bool Update(LichSuKhoDTO newLog)
    {
      return this.DAO.Update(newLog);
    }

    public bool Delete(int logId)
    {
      return this.DAO.Delete(logId);
    }

    public int GetNextId()
    {
      return this.DAO.GetNextId();
    }

    public List<LichSuKhoDTO> FindByAnyProperty(string keyword)
    {
      return this.DAO.FindByAnyProperty(keyword);
    }
  }
}

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

    public List<ChiTietLichSuKhoDTO> GetDetail(int hoaDonId)
    {
      return this.DAO.GetDetail(hoaDonId);
    }

    public int AddNewReceipt(LichSuKhoDTO newLog)
    {
      return this.DAO.Add(newLog);
    }

    public bool UpdateReceipt(LichSuKhoDTO updatedLog)
    {
      return this.DAO.Update(updatedLog);
    }

    public bool DeleteReceipt(int hoaDonId)
    {
      return this.DAO.Delete(hoaDonId);
    }
  }
}

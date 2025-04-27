using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechForgeDAO;
using TechForgeDTO;

namespace TechForgeBUS
{
  public class LichSuHoatDongBUS
  {
    private readonly LichSuHoatDongDAO DAO;
    public LichSuHoatDongBUS(string _connStr)
    {
      this.DAO = new LichSuHoatDongDAO(_connStr);
    }
    public List<LichSuHoatDongDTO> GetRecentAllConnected(string maND="")
    {
      return this.DAO.GetRecentAllConnected(maND);
    }
    public int Add(LichSuHoatDongDTO newEntry)
    {
      return this.DAO.Add(newEntry);
    }
  }
}

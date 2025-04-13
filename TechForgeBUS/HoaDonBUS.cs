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
  public class HoaDonBUS
  {
    private readonly HoaDonDAO DAO;
    public HoaDonBUS(string _connStr)
    {
      this.DAO = new HoaDonDAO(_connStr);
    }
    public List<HoaDonDTO> GetAllConnected()
    {
      return this.DAO.GetAllConnected();
    }
    public List<ChiTietHoaDonDTO> GetDetail(HoaDonDTO receipt)
    {
      receipt.Cthd = this.DAO.GetDetail(receipt.MaHD);
      return receipt.Cthd;
    }
    public List<ChiTietHoaDonDTO> GetDetailWithProducts(HoaDonDTO receipt)
    {
      receipt.Cthd = this.DAO.GetDetailWithProducts(receipt.MaHD);
      return receipt.Cthd;
    }
    public int Add(HoaDonDTO newReiceipt)
    {
      return this.DAO.Add(newReiceipt);
    }
    public bool Update(HoaDonDTO newReiceipt)
    {
      return this.DAO.Update(newReiceipt);
    }
    public bool Delete(int id)
    {
      return this.DAO.Delete(id);
    }
  }
}

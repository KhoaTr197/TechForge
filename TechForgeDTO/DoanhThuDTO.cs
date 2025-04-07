using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechForgeDTO
{
  public class DoanhThuTheoTG
  {
    public string ThoiGian { get; set; }
    public decimal TongTien { get; set; }
  }
  public class DoanhThuDTO
  {
    public DateTime NgBatDau { get; set; }
    public DateTime NgKetThuc { get; set; }
    public int SoNgay { get; set; }
    public int SoHoiVien { get; set; }
    public int SoNCC { get; set; }
    public int SoSanPham { get; set; }
    public List<KeyValuePair<string, int>> DsSPBanChay { get; set; }
    public List<KeyValuePair<string, int>> DsSPTonKho { get; set; }
    public List<DoanhThuTheoTG> DsDoanhThu { get; set; }
    public int SoHoaDon { get; set; }
    public decimal TongDoanhThu { get; set; }
    //public decimal TongLoiNhuan { get; set; }
  }
}

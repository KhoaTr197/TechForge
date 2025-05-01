using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;

namespace TechForgeDTO
{
  public class HoaDonDTO
  {
    //[MAHD] INT IDENTITY(1, 1) NOT NULL,
    //[MAHV]     INT NULL,
    //[HOTEN]    NVARCHAR(50)  NULL,
    //[SDT] VARCHAR(10)   NULL,
    //[DCHI] NVARCHAR(100) NULL,
    //[NvLapHD] VARCHAR(10)   NULL,
    //[TONGTIEN] DECIMAL(18)   NULL,
    //[NGLAPHD] DATETIME DEFAULT(getdate()) NULL,
    public int MaHD { get; set; }
    public int? MaHV { get; set; }
    public string HoTen { get; set; }
    public string Sdt { get; set; }
    public string DiaChi { get; set; }
    public string NvLapHD { get; set; }
    public decimal TongTien { get; set; }
    public DateTime NgLapHD { get; set; }
    public List<ChiTietHoaDonDTO> Cthd { get; set; }

    public HoaDonDTO()
    {
      Cthd = new List<ChiTietHoaDonDTO>();
    }
  }
  public class ChiTietHoaDonDTO
  {
    //[MAHD] INT NOT NULL,
    //[MASP] INT NOT NULL,
    //[GIA] DECIMAL(18) NULL,
    //[SOTIENKM] DECIMAL(18) NULL,
    //[GIACUOICUNG] DECIMAL(18) NULL,
    //[SL] INT NULL,
    //[THANHTIEN]   DECIMAL(18) NULL,
    public int MaHD { get; set; }
    public int MaSP { get; set; }
    public decimal Gia { get; set; }
    public decimal SoTienKm { get; set; }
    public decimal GiaCuoiCung { get; set; }
    public int SoLuong { get; set; }
    public decimal ThanhTien { get; set; }

    // Product information
    public string TenSP { get; set; }
    public string HinhAnh { get; set; }
    public decimal KhuyenMai { get; set; }
  }
}

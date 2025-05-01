using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechForgeDTO
{
  public class LichSuKhoDTO
  {
    //[MALS] INT IDENTITY(1, 1) NOT NULL,
    //[TONGTIEN] DECIMAL(18) NULL,
    //[THOIGIAN] DATETIME DEFAULT(getdate()) NULL,
    //[MAND] VARCHAR(10) NULL,
    //[HOATDONG] BIT NULL, 0 - Nhap, 1 - Xuat
    public int MaLS { get; set; }
    public decimal TongTien { get; set; }
    public DateTime ThoiGian { get; set; }
    public string MaND { get; set; }
    public bool HoatDong { get; set; } // 0 - Nhap, 1 - Xuat
    public List<ChiTietLichSuKhoDTO> Ctlsk { get; set; } = new List<ChiTietLichSuKhoDTO>();
    public LichSuKhoDTO()
    {
      Ctlsk = new List<ChiTietLichSuKhoDTO>();
    }
  }
  public class ChiTietLichSuKhoDTO
  {
    //[MALS] INT NOT NULL,
    //[MASP] INT NOT NULL,
    //[GIA] DECIMAL(18) NULL,
    //[HOATDONG] BIT NULL,
    //[SL]        INT NULL,
    //[THANHTIEN] DECIMAL(18) NULL,
    public int MaLS { get; set; }
    public int MaSP { get; set; }
    public string HinhAnh { get; set; }
    public int SoLuong { get; set; }
    public bool HoatDong { get; set; }
    public decimal? ThanhTien { get; set; }
    public string TenSP { get; set; }
    public decimal? Gia { get; set; }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechForgeDTO
{
    public class SanPhamDTO
    {
      //[MASP] INT NOT NULL PRIMARY KEY,
      //[TENSP] NVARCHAR(100),
      //[GIANHAP] DECIMAL CHECK([GIANHAP] > 0),
      //[GIA] DECIMAL CHECK([GIA] > 0),
      //[KHUYENMAI] DECIMAL(2),
      //[MOTA] nvarchar(max),
      //[SL] INT DEFAULT(0),
      //[DANHMUC] INT,
      //[HSX] INT,
      //[NGSX] date,
      //[TRANGTHAI] bit
      public int MaSP { get; set; }
      public string TenSP { get; set; }
      public decimal GiaNhap { get; set; }
      public decimal Gia { get; set; }
      public decimal KhuyenMai { get; set; }
      public string MoTa { get; set; }
      public int SoLuong { get; set; } = 0;
      public int DanhMuc { get; set; }
      public int Hsx { get; set; }
      public DateTime NgSx { get; set; }
      public bool TrangThai { get; set; }
  }
}

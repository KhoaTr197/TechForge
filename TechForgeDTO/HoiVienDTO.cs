using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechForgeDTO
{
  public class HoiVienDTO
  {
    //[MAHV] INT NOT NULL,
    //[HOTEN] NVARCHAR(50) NULL,
    //[GIOITINH] BIT NULL,
    //[SDT] VARCHAR(10) NULL,
    //[DCHI] NVARCHAR(100) NULL,
    //[TRANGTHAI] BIT NULL,
    public int MaHV { get; set; }
    public string HoTen { get; set; }
    public bool GioiTinh { get; set; }
    public string Sdt { get; set; }
    public string Dchi { get; set; }
    public bool TrangThai { get; set; }
  }
}

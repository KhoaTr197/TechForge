using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    //[NVLAPHD] VARCHAR(10)   NULL,
    //[TONGTIEN] DECIMAL(18)   NULL,
    //[NGLAPHD] DATETIME DEFAULT(getdate()) NULL,
    public int MaHD { get; set; }
    public int MaHV { get; set; }
    public string HoTen { get; set; }
    public string Sdt { get; set; }
    public string DiaChi { get; set; }
    public string NvlapHD { get; set; }
    public decimal TongTien { get; set; }
    public DateTime NgLapHD { get; set; }
  }
}

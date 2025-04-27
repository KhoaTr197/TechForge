using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechForgeDTO
{
  public class LichSuHoatDongDTO
  {
    //[MALSHD] INT IDENTITY(1, 1) NOT NULL,
    //[MAND]     VARCHAR(10)   NOT NULL,
    //[THOIGIAN] DATETIME DEFAULT(getdate()) NULL,
    //[NOIDUNG] NVARCHAR(MAX) NULL,
    //[VAITRO] NVARCHAR(25)  NULL,
    public int MaLSHD { get; set; }
    public string MaND { get; set; }
    public DateTime ThoiGian { get; set; }
    public string NoiDung { get; set; }
    public string VaiTro { get; set; }
  }
}

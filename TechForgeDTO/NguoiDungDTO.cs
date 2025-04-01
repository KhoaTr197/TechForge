using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TechForgeDTO
{
  public class NguoiDungDTO
  {
    //[MAND] VARCHAR(10)   NOT NULL,
    //[HOTEN]    NVARCHAR(50)  NULL,
    //[NGSINH]
    //DATE NULL,
    //[GIOITINH] BIT NULL,
    //[CCCD]     VARCHAR(12)   NULL,
    //[SDT] VARCHAR(10)   NULL,
    //[DCHI] NVARCHAR(100) NULL,
    //[VAITRO] NVARCHAR(25)  NULL,
    //[NGVAOLAM] DATE
    public string MaND { get; set; }
    public string HoTen { get; set; }
    public DateTime NgSinh { get; set; }
    public bool GioiTinh { get; set; }
    public string Cccd { get; set; }
    public string Sdt { get; set; }
    public string Dchi { get; set; }
    public string VaiTro { get; set; }
    public DateTime NgVaoLam { get; set; }
}
}

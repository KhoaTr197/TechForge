using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TechForgeDTO
{
  public class NhaCungCapDTO
  {
    //[MANCC] INT NOT NULL,
    //[TENNCC] NVARCHAR(100) NULL,
    //[NDD] NVARCHAR(50)  NULL,
    //[SDT] VARCHAR(10)   NULL,
    //[EMAIL] VARCHAR(50)   NULL,
    //[TRANGTHAI] BIT NULL,
    public int MaNCC { get; set; }
    public string TenNCC { get; set; }
    public string Ndd { get; set; }
    public string Sdt { get; set; }
    public string Email { get; set; }
    public bool TrangThai { get; set; }
  }
}
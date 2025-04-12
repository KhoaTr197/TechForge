using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechForgeDTO
{
    public class SanPhamDTO
    {
        /*
        [MASP] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
        [TENSP] NVARCHAR(100),
        [GIANHAP] DECIMAL DEFAULT 0,
        [GIA] DECIMAL DEFAULT 0,
        [KHUYENMAI] DECIMAL(2),
        [MOTA] nvarchar(max),
        [SL] INT DEFAULT (0),
        [DONVITINH] nvarchar(20), 
        [HINHANH] nvarchar(max),
        [DANHMUC] INT,
        [HSX] INT,
        [NCC] INT, 
        [NGSX] date,
        [TRANGTHAI] bit
         */
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal GiaNhap { get; set; } = 0;
        public decimal Gia { get; set; } = 0;
        public decimal KhuyenMai { get; set; }
        public string MoTa { get; set; }
        public int SoLuong { get; set; } = 0;
        public string DonViTinh { get; set; }
        public string HinhAnh {  get; set; }
        public int DanhMuc { get; set; }
        public int Hsx { get; set; }
        public int Ncc { get; set; }
        public DateTime NgSx { get; set; }
        public bool TrangThai { get; set; }
  }
}

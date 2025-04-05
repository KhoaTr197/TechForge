using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeBUS;
using TechForgeDTO;
using TechForgeGUI.BaseForms;

namespace TechForgeGUI.SubForms
{
  public partial class UserManagerFormGUI : ManagePage
  {
    private NguoiDungBUS bus { get; set; }
    public UserManagerFormGUI()
    {
      InitializeComponent();
      InitializeBUS();
      LoadData();
    }
    protected override void InitializeBUS()
    {
      bus = new NguoiDungBUS(this.connStr);
    }
    protected override void LoadData()
    {
      dgvMainListRef.BindingData(bus.GetAllConnected().Cast<object>().ToList());
            var columnMappings = new Dictionary<string, (string, bool)>{
                { "MaND", ("Mã Người Dùng", true) },
                { "HoTen", ("Họ Tên", true) },
                { "NgSinh", ("Ngày Sinh", true) },
                { "GioiTinh", ("Giới Tính", true) },
                { "Cccd", ("Số Căn Cước", true) },
                { "Sdt", ("Số Điện Thoại", true) },
                { "Dchi", ("Địa Chỉ", true) },
                { "VaiTro", ("Vai Trò", true) },
                { "NgVaoLam", ("Ngày Vào Làm", true) },
            };
            dgvMainListRef.SetColumnNames(columnMappings);
            dgvMainListRef.ConfigureDataGridViewColumns();
        }
  }
}

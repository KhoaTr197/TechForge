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
  public partial class SupplierManagePageGUI : ManagePage
  {
    private DataSet ds;
    private List<NhaCungCapDTO> dsNhaCungCap { get; set; }
    private NhaCungCapBUS bus { get; set; }
    public SupplierManagePageGUI()
    {
      InitializeComponent();
      InitializeBUS();
      GetData();
      LoadData();
    }
    sealed protected override void InitializeBUS()
    {
      bus = new NhaCungCapBUS(this.connStr);
    }
    private void GetData()
    {
      ds = new DataSet();

      // Map data to DTOs
      dsNhaCungCap = bus.GetAllConnected();
    }
    sealed protected override void LoadData()
    {
      dgvMainListRef.BindingData(dsNhaCungCap);

      var columnMappings = new Dictionary<string, (string, bool)>{
        { "MaNCC", ("Mã Nhà Cung Cấp", true) },
        { "TenNCC", ("Tên Nhà Cung Cấp", true) },
        { "Ndd", ("Tên Người Đại Diện", true) },
        { "Sdt", ("Số Điện Thoại", true) },
        { "TrangThai",  ("Trạng thái", false)},
      };
      dgvMainListRef.SetColumnNames(columnMappings);
    }
  }
}
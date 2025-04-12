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
  public partial class UserManagePageGUI : ManagePage
  {
    private DataSet ds;
    private List<NguoiDungDTO> dsNguoiDung { get; set; }
    private NguoiDungBUS bus { get; set; }
    public UserManagePageGUI()
    {
      InitializeComponent();
      InitializeBUS();
      GetData();
      LoadData();
    }
    sealed protected override void InitializeBUS()
    {
      bus = new NguoiDungBUS(this.connStr);
    }
    protected void GetData()
    {
      ds = new DataSet();

      // Map data to DTOs
      dsNguoiDung = bus.GetAllConnected();
    }
    protected override void LoadData()
    {
      dgvMainList.BindingData(dsNguoiDung);
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
      dgvMainList.SetColumnNames(columnMappings);
    }
    protected void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null)
      {
        if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "TRANGTHAI")
        {
          bool status = (bool)e.Value;
          if (status)
          {
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.BackColor = Color.Green;
            e.Value = "Đang kinh doanh";
          }
          else
          {
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.BackColor = Color.Red;
            e.Value = "Ngừng kinh doanh";
          }
        }
        else if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "GIANHAP" ||
                 dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "GIA")
        {
          decimal price = (decimal)e.Value;
          e.Value = price.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
          e.FormattingApplied = true;
        }
      }
    }
  }
}

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
using TechForgeGUI.BaseControls;
using TechForgeGUI.BaseForms;
using TechForgeGUI.Utils;

namespace TechForgeGUI.SubPages
{
  public partial class ProductDetailFormGUI : DetailFormGUI
  {
    private SanPhamDTO thongTinSanPham { get; set; }
    private List<DanhMucDTO> dsDanhMuc { get; set; }
    private List<HangSanXuatDTO> dsHangSanXuat { get; set; }
    private SanPhamBUS BUS { get; set; }
    private FlowLayoutPanel flpInfoPanel;
        private AlertForm alert;
    public ProductDetailFormGUI(SanPhamDTO _thongTinSanPham, List<DanhMucDTO> _dsDanhMuc, List<HangSanXuatDTO> _dsHangSanXuat, SanPhamBUS _BUS)
    {
      InitializeComponent();

      this.thongTinSanPham = _thongTinSanPham;
      this.dsDanhMuc = _dsDanhMuc;
      this.dsHangSanXuat = _dsHangSanXuat;
      this.BUS = _BUS;
      this.Text = "Chi tiết sản phẩm";
      this.btnAdd.Visible = false;
      this.btnAdd.Enabled = false;

      flpInfoPanel = new FlowLayoutPanel
      {
        BackColor = Color.FromArgb(240, 240, 240),
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.TopDown,
        Padding = new Padding(4, 32, 4, 64),
      };

      LoadInfo();

      this.Controls.Add(flpInfoPanel);

      btnEdit.Click += btnEdit_Click;
    }
    private void LoadInfo()
    {
      Dictionary<string, string> displayNames = new Dictionary<string, string>
        {
            { "MaSP", "Mã Sản Phẩm" },
            { "TenSP", "Tên Sản Phẩm" },
            { "GiaNhap", "Giá Nhập" },
            { "Gia", "Giá" },
            { "KhuyenMai", "Khuyến Mãi" },
            { "MoTa", "Mô Tả" },
            { "SoLuong", "Số Lượng" },
            { "DanhMuc", "Danh Mục" },
            { "Hsx", "Hãng Sản Xuất" },
            { "NgSx", "Ngày Sản Xuất" },
            { "TrangThai", "Trạng Thái" }
        };

      foreach (var prop in thongTinSanPham.GetType().GetProperties())
      {
        FlowLayoutPanel panel = new FlowLayoutPanel
        {
          AutoSize = true,
          Height = 48,
          FlowDirection = FlowDirection.LeftToRight,
        };

        Label lbl = new Label
        {
          Width = 128,
          Font = new Font(DefaultFontName, 10),
          TextAlign = ContentAlignment.MiddleLeft,
          Text = displayNames.ContainsKey(prop.Name) ? displayNames[prop.Name] + ":" : prop.Name + ":",
        };

        Control control;

        if (prop.Name == "TrangThai")
        {
          continue;
        }
        else if (prop.Name == "MaSP")
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Font = new Font(DefaultFontName, 10),
            Text = prop.GetValue(thongTinSanPham)?.ToString(),
            Enabled = false,
          };
        }
        else if (prop.Name == "DanhMuc" || prop.Name == "Hsx")
        {
          ComboBox comboBox = new ComboBox
          {
            Name = "cbo" + prop.Name,
            Font = new Font(DefaultFontName, 10),
            DropDownStyle = ComboBoxStyle.DropDownList,
          };
          if (prop.Name == "DanhMuc")
          {
            comboBox.Items.AddRange(dsDanhMuc.Select(dm => dm.TenDM).ToArray());
            comboBox.SelectedItem = dsDanhMuc.FirstOrDefault(dm => dm.MaDM == (int)prop.GetValue(thongTinSanPham))?.TenDM;
          }
          else
          {
            comboBox.Items.AddRange(dsHangSanXuat.Select(hsx => hsx.TenHSX).ToArray());
            comboBox.SelectedItem = dsHangSanXuat.FirstOrDefault(hsx => hsx.MaHSX == (int)prop.GetValue(thongTinSanPham))?.TenHSX;
          }
          control = comboBox;
        }
        else if (prop.PropertyType == typeof(DateTime))
        {
          control = new DateTimePicker
          {
            Name = "dtp" + prop.Name,
            Value = (DateTime)prop.GetValue(thongTinSanPham),
            Font = new Font(DefaultFontName, 10),
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy",
          };
        }
        else if (prop.PropertyType == typeof(decimal))
        {
          decimal value = Convert.ToDecimal(prop.GetValue(thongTinSanPham));
          decimal minimum = 0;
          decimal maximum = 250000000;
          control = new NumericUpDown
          {
            Name = "nud" + prop.Name,
            Font = new Font(DefaultFontName, 10),
            ThousandsSeparator = true,
            Increment = prop.Name == "KhuyenMai" ? 1 : 100000,
            Minimum = minimum,
            Maximum = prop.Name == "KhuyenMai" ? 100 : maximum,
            Value = value,
          };
        }
        else if (prop.PropertyType == typeof(int))
        {
          decimal value = Convert.ToInt64(prop.GetValue(thongTinSanPham));
          decimal minimum = 0;
          control = new NumericUpDown
          {
            Name = "nud" + prop.Name,
            Font = new Font(DefaultFontName, 10),
            ThousandsSeparator = true,
            Increment = 5,
            Minimum = minimum,
            Value = value,
          };
        }
        else
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            BackColor = Color.White,
            Size = prop.Name == "MoTa" ? new Size(320, 160) : new Size(320, 48),
            Multiline = prop.Name == "MoTa",
            ScrollBars = prop.Name == "MoTa" ? ScrollBars.Vertical : ScrollBars.None,
            Font = new Font(DefaultFontName, 10),
            Text = prop.GetValue(thongTinSanPham)?.ToString(),
          };
        }

        panel.Controls.Add(lbl);
        panel.Controls.Add(control);
        flpInfoPanel.Controls.Add(panel);
      }
    }
    private void btnAdd_Click(object sender, EventArgs e)
    {
      SanPhamDTO newSanPham = new SanPhamDTO()
      {
        MaSP = thongTinSanPham.MaSP,
        TenSP = ((TextBox)GetControlByName(flpInfoPanel, "txtTenSP")).Text,
        GiaNhap = (decimal)((NumericUpDown)GetControlByName(flpInfoPanel, "nudGiaNhap")).Value,
        Gia = (decimal)((NumericUpDown)GetControlByName(flpInfoPanel, "nudGia")).Value,
        KhuyenMai = (int)((NumericUpDown)GetControlByName(flpInfoPanel, "nudKhuyenMai")).Value,
        MoTa = ((TextBox)GetControlByName(flpInfoPanel, "txtMoTa")).Text,
        SoLuong = (int)((NumericUpDown)GetControlByName(flpInfoPanel, "nudSoLuong")).Value,
        DanhMuc = dsDanhMuc[((ComboBox)GetControlByName(flpInfoPanel, "cboDanhMuc")).SelectedIndex].MaDM,
        Hsx = dsHangSanXuat[((ComboBox)GetControlByName(flpInfoPanel, "cboHsx")).SelectedIndex].MaHSX,
        NgSx = ((DateTimePicker)GetControlByName(flpInfoPanel, "dtpNgSx")).Value,
        TrangThai = true,
      };

      if (BUS.Add(newSanPham) != -1)
        OnAddSubmit(new DetailFormAddSubmitEventArgs());
    }
    private void btnEdit_Click(object sender, EventArgs e)
    {
      SanPhamDTO updatedSanPham = new SanPhamDTO()
      {
        MaSP = thongTinSanPham.MaSP,
        TenSP = ((TextBox)GetControlByName(flpInfoPanel, "txtTenSP")).Text,
        GiaNhap = (decimal)((NumericUpDown)GetControlByName(flpInfoPanel, "nudGiaNhap")).Value,
        Gia = (decimal)((NumericUpDown)GetControlByName(flpInfoPanel, "nudGia")).Value,
        KhuyenMai = (int)((NumericUpDown)GetControlByName(flpInfoPanel, "nudKhuyenMai")).Value,
        MoTa = ((TextBox)GetControlByName(flpInfoPanel, "txtMoTa")).Text,
        SoLuong = (int)((NumericUpDown)GetControlByName(flpInfoPanel, "nudSoLuong")).Value,
        DanhMuc = dsDanhMuc[((ComboBox)GetControlByName(flpInfoPanel, "cboDanhMuc")).SelectedIndex].MaDM,
        Hsx = dsHangSanXuat[((ComboBox)GetControlByName(flpInfoPanel, "cboHsx")).SelectedIndex].MaHSX,
        NgSx = ((DateTimePicker)GetControlByName(flpInfoPanel, "dtpNgSx")).Value,
        TrangThai = true,
      };

      if (BUS.Update(thongTinSanPham, updatedInfo))
            {
                alert = new AlertForm("Cap nhat thanh cong");
                alert.Show();
                OnEditSubmit(new DetailFormEditSubmitEventArgs());
            }
        
    }
  }
}

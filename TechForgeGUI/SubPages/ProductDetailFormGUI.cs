using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
    private List<NhaCungCapDTO> dsNhaCungCap { get; set; }
    private SanPhamBUS BUS { get; set; }
    private FlowLayoutPanel flpInfoPanel;
    private Notification notify;
    public ProductDetailFormGUI(SanPhamDTO _thongTinSanPham, List<DanhMucDTO> _dsDanhMuc, List<HangSanXuatDTO> _dsHangSanXuat, SanPhamBUS _BUS, List<NhaCungCapDTO> _dsNhaCungCap)
    {
      InitializeComponent();

      this.thongTinSanPham = _thongTinSanPham;
      this.dsDanhMuc = _dsDanhMuc;
      this.dsHangSanXuat = _dsHangSanXuat;
      this.dsNhaCungCap = _dsNhaCungCap;
      this.BUS = _BUS;
      this.Text = "Chi tiết sản phẩm";

      flpInfoPanel = new FlowLayoutPanel
      {
        Name= "flpInfoPanel",
        BackColor = Color.FromArgb(240, 240, 240),
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.TopDown,
        Padding = new Padding(4, 32, 4, 64),
      };

      Dictionary<string, string> inputLabels = new Dictionary<string, string>
        {
            { "MaSP", "Mã Sản Phẩm" },
            { "TenSP", "Tên Sản Phẩm" },
            { "GiaNhap", "Giá Nhập" },
            { "Gia", "Giá" },
            { "KhuyenMai", "Khuyến Mãi" },
            { "MoTa", "Mô Tả" },
            { "SoLuong", "Số Lượng" },
            { "DonViTinh", "Đơn Vị Tính" },
            { "HinhAnh", "Hình Ảnh" },
            { "DanhMuc", "Danh Mục" },
            { "Hsx", "Hãng Sản Xuất" },
            { "Ncc", "Nhà Cung Cấp" },
            { "NgSx", "Ngày Sản Xuất" },
            { "TrangThai", "Trạng Thái" }
        };

      if (thongTinSanPham == null)
      {
        this.btnEdit.Visible = false;
        this.btnEdit.Enabled = false;
        this.btnDelete.Visible = false;
        this.btnDelete.Enabled = false;


        //LoadAddForm(inputLabels);
      }
      else
      {
        this.Size = new Size(1000, 400);

        this.btnAdd.Visible = false;
        this.btnAdd.Enabled = false;


        LoadDetailForm(inputLabels);
      }

      this.Controls.Add(flpInfoPanel);

      btnAdd.Click += btnAdd_Click;
      btnEdit.Click += btnEdit_Click;
    }
    private void LoadDetailForm(Dictionary<string, string> inputLabels)
    {
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
          Margin = new Padding(0, 4, 0, 0),
          Font = new Font(DefaultFontName, 12),
          TextAlign = ContentAlignment.MiddleLeft,
          Text = inputLabels.ContainsKey(prop.Name) ? inputLabels[prop.Name] + ":" : prop.Name + ":",
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
            Font = new Font(DefaultFontName, 12),
            Text = prop.GetValue(thongTinSanPham)?.ToString(),
            Enabled = false,
          };
        }
        else if (prop.Name == "DanhMuc" || prop.Name == "Hsx" || prop.Name == "Ncc")
        {
          ComboBox comboBox = new ComboBox
          {
            Name = "cbo" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 320,
            MaxDropDownItems = 5,
            DropDownHeight = 200,
          };
          if (prop.Name == "DanhMuc")
          {
            comboBox.Items.AddRange(dsDanhMuc.Select(dm => dm.TenDM).ToArray());
            comboBox.SelectedItem = dsDanhMuc.FirstOrDefault(dm => dm.MaDM == (int)prop.GetValue(thongTinSanPham))?.TenDM;
          }
          else if (prop.Name == "Hsx")
          {
            comboBox.Items.AddRange(dsHangSanXuat.Select(hsx => hsx.TenHSX).ToArray());
            comboBox.SelectedItem = dsHangSanXuat.FirstOrDefault(hsx => hsx.MaHSX == (int)prop.GetValue(thongTinSanPham))?.TenHSX;
          }
          else
          {
            comboBox.Items.AddRange(dsNhaCungCap.Select(ncc => ncc.TenNCC).ToArray());
            comboBox.SelectedItem = dsNhaCungCap.FirstOrDefault(ncc => ncc.MaNCC == (int)prop.GetValue(thongTinSanPham))?.TenNCC;
            comboBox.DropDownWidth = 400;
          }
          control = comboBox;
        }
        else if (prop.Name == "HinhAnh")
        {
          PictureBox picSelected = new PictureBox()
          {
            Name = "picSelected",
            SizeMode = PictureBoxSizeMode.Zoom,
            BorderStyle = BorderStyle.FixedSingle,
            Dock = DockStyle.Fill,
          };
          string imagePath = Path.Combine(Application.StartupPath, "Resources", "ProductImages", $"{thongTinSanPham.HinhAnh}.png");
          if (File.Exists(imagePath))
          {
            picSelected.Image = Image.FromFile(imagePath);
            picSelected.Tag = thongTinSanPham.HinhAnh;
          }
          Button btnSelectImg = new Button()
          {
            Name = "btnSelectImg",
            Text = "Chọn Hình Ảnh",
            Size = new Size(100, 30),
            Dock = DockStyle.Bottom,
          };
          Panel pnlSelectImg = new Panel()
          {
            Name = "pnlSelectImg",
            Size = new Size(320, 230)
          };
          btnSelectImg.Tag = picSelected;
          btnSelectImg.Click += BtnSelectImg_Click;

          pnlSelectImg.Controls.Add(picSelected);
          pnlSelectImg.Controls.Add(btnSelectImg);
          control = pnlSelectImg;
        }
        else if (prop.PropertyType == typeof(DateTime))
        {
          control = new DateTimePicker
          {
            Name = "dtp" + prop.Name,
            Width = 320,
            Value = (DateTime)prop.GetValue(thongTinSanPham),
            Font = new Font(DefaultFontName, 12),
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
            Font = new Font(DefaultFontName, 12),
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
            Font = new Font(DefaultFontName, 12),
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
            Font = new Font(DefaultFontName, 12),
            Text = prop.GetValue(thongTinSanPham)?.ToString(),
          };
        }

        panel.Controls.Add(lbl);
        panel.Controls.Add(control);
        flpInfoPanel.Controls.Add(panel);
      }
    }

    private void BtnSelectImg_Click(object sender, EventArgs e)
    {
      Button btn = sender as Button;
      PictureBox picBox = btn.Tag as PictureBox;
      if (picBox != null)
      {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
          openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
          openFileDialog.Title = "Chọn một hình ảnh";
          openFileDialog.Multiselect = false;

          if (openFileDialog.ShowDialog() == DialogResult.OK)
          {
            try
            {
              picBox.Image = Image.FromFile(openFileDialog.FileName);
              picBox.Tag = openFileDialog.FileName;
            }
            catch (Exception ex)
            {
              MessageBox.Show("Lỗi khi tải hình ảnh: " + ex.Message);
            }
          }
        }
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
        DonViTinh = ((TextBox)GetControlByName(flpInfoPanel, "txtDonViTinh")).Text,
        HinhAnh = ((PictureBox)GetControlByName(flpInfoPanel, "picSelected")).Tag?.ToString(),
        DanhMuc = dsDanhMuc[((ComboBox)GetControlByName(flpInfoPanel, "cboDanhMuc")).SelectedIndex].MaDM,
        Ncc = dsNhaCungCap[((ComboBox)GetControlByName(flpInfoPanel, "cboNcc")).SelectedIndex].MaNCC,
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
        DonViTinh = ((TextBox)GetControlByName(flpInfoPanel, "txtDonViTinh")).Text,
        HinhAnh = ((PictureBox)GetControlByName(flpInfoPanel, "picSelected")).Tag?.ToString(),
        DanhMuc = dsDanhMuc[((ComboBox)GetControlByName(flpInfoPanel, "cboDanhMuc")).SelectedIndex].MaDM,
        Hsx = dsHangSanXuat[((ComboBox)GetControlByName(flpInfoPanel, "cboHsx")).SelectedIndex].MaHSX,
        Ncc = dsNhaCungCap[((ComboBox)GetControlByName(flpInfoPanel, "cboNcc")).SelectedIndex].MaNCC,
        NgSx = ((DateTimePicker)GetControlByName(flpInfoPanel, "dtpNgSx")).Value,
        TrangThai = true,
      };

      if (BUS.Update(thongTinSanPham, updatedSanPham))
      {
        notify = new Notification("Cap nhat thanh cong");
        notify.Show();
        OnEditSubmit(new DetailFormEditSubmitEventArgs());
      }
    }
    private void btnDelete_Click(object sender, EventArgs e)
    {
      if (BUS.Delete(thongTinSanPham.MaSP))
      {
        OnDeleteSubmit(new DetailFormDeleteSubmitEventArgs());
      }
    }
  }
}

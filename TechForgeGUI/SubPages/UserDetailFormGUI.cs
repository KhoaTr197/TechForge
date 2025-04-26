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

namespace TechForgeGUI.SubPages
{
  public partial class UserDetailFormGUI : DetailFormGUI
  {
    private NguoiDungBUS BUS { get; set; }
    private TaiKhoanBUS taiKhoanBUS { get; set; }
    private NguoiDungDTO thongTinNguoiDung { get; set; }
    private TaiKhoanDTO thongTinTaiKhoan { get; set; }
    private List<string> dsVaiTro { get; set; }
    private RolePermissions permissions { get; set; }
    private FlowLayoutPanel flpInfoPanel { get; set; }
    private Notification notify;
    public UserDetailFormGUI(RolePermissions _permissions, NguoiDungBUS _BUS, TaiKhoanBUS _taiKhoanBUS, NguoiDungDTO _thongTinNguoiDung = null, TaiKhoanDTO _thongTinTaiKhoan = null)
    {
      InitializeComponent();

      this.BUS = _BUS;
      this.thongTinNguoiDung = _thongTinNguoiDung;
      this.taiKhoanBUS = _taiKhoanBUS;
      this.thongTinTaiKhoan = _thongTinTaiKhoan;
      this.dsVaiTro = BUS.GetAllRoles();
      this.permissions = _permissions;

      if (thongTinNguoiDung == null && thongTinTaiKhoan == null)
      {
        type = "Add";
      }
      else if (thongTinNguoiDung != null && thongTinTaiKhoan != null)
      {
        type = "Detail";
      }
      else
      {
        return;
      }

      this.Text = "Chi tiết người dùng";

      this.btnDelete.Visible = false;
      this.btnDelete.Enabled = false;

      flpInfoPanel = new FlowLayoutPanel
      {
        Name = "flpInfoPanel",
        BackColor = Color.FromArgb(240, 240, 240),
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.TopDown,
        Padding = new Padding(4, 32, 4, 64),
      };

      Dictionary<string, string> inputLabels = new Dictionary<string, string>
        {
        { "MaND", "Mã ND" },
        { "HoTen", "Họ Tên" },
        { "NgSinh", "Ngày Sinh" },
        { "GioiTinh", "Giới Tính" },
        { "Cccd", "CCCD" },
        { "Dchi", "Địa Chỉ" },
        { "Sdt", "SĐT" },
        { "VaiTro", "Vai Trò" },
        { "NgVaoLam", "Ngày Vào Làm" },
        { "TenTK", "Tên Tài Khoản" },
        { "MatKhau", "Mật Khẩu" },
        { "TrangThai", "Trạng Thái" }
      };

      if (type == "Add")
      {
        this.btnEdit.Visible = false;
        this.btnEdit.Enabled = false;
        this.btnDelete.Visible = false;
        this.btnDelete.Enabled = false;


        LoadAddForm(inputLabels);
      }
      else
      {
        this.btnAdd.Visible = false;
        this.btnAdd.Enabled = false;


        LoadDetailForm(inputLabels);
      }

      if (permissions.Role == "Cashier")
      {
        this.btnAdd.Visible = false;
        this.btnAdd.Enabled = false;
        this.btnEdit.Visible = false;
        this.btnEdit.Enabled = false;
      }
      else if (permissions.Role == "WarehouseStaff")
      {
        this.btnAdd.Visible = false;
        this.btnAdd.Enabled = false;
        this.btnEdit.Visible = false;
        this.btnEdit.Enabled = false;
      }
      else if (permissions.Role == "Manager")
      {
        if (type == "Detail")
        {
          this.btnAdd.Visible = false;
          this.btnAdd.Enabled = false;
        }
        else
        {
          this.btnAdd.Visible = true;
          this.btnAdd.Enabled = true;
          this.btnEdit.Visible = false;
          this.btnEdit.Enabled = false;
        }
        this.btnEdit.Visible = true;
        this.btnEdit.Enabled = true;
      }

        this.Controls.Add(flpInfoPanel);

        btnAdd.Click += btnAdd_Click;
        btnEdit.Click += btnEdit_Click;
      }
    private void LoadAddForm(Dictionary<string, string> inputLabels)
    {
      foreach (var input in inputLabels)
      {
        string controlName = input.Key;
        string labelName = input.Value;

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
          Text = labelName + ":",
        };

        Control control;

        if (controlName == "MaND" || controlName == "TrangThai")
          continue;
        else if ( controlName == "HoTen" || controlName == "Cccd" || controlName == "Sdt" || controlName == "Dchi")
        {
          control = new TextBox
          {
            Name = "txt" + controlName,
            Font = new Font(DefaultFontName, 12),
            Text = "",
            Width = 300,
          };
        }
        else if (controlName == "GioiTinh")
        {
          control = new FlowLayoutPanel()
          {
            Name = "flp" + controlName,
            AutoSize = true,
          };
          RadioButton radNam = new RadioButton()
          {
            Text = "Nam",
            Name = "radNam",
            Font = new Font(DefaultFontName, 12),
            Checked = true,
          };
          RadioButton radNu = new RadioButton()
          {
            Text = "Nữ",
            Name = "radNu",
            Font = new Font(DefaultFontName, 12),
          };
          control.Controls.Add(radNam);
          control.Controls.Add(radNu);
        }
        else if (controlName == "VaiTro")
        {
          ComboBox comboBox = new ComboBox
          {
            Name = "cbo" + controlName,
            Font = new Font(DefaultFontName, 12),
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 320,
            MaxDropDownItems = 5,
            DropDownHeight = 200,
          };
          comboBox.Items.AddRange(dsVaiTro.ToArray());
          control = comboBox;
        }
        else if (controlName == "NgSinh")
        {
          control = new DateTimePicker
          {
            Name = "dtp" + controlName,
            Font = new Font(DefaultFontName, 12),
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy",
            Value = DateTime.Now.AddYears(-18),
            Width = 300,
          };
        }
        else if (controlName == "NgVaoLam")
        {
          control = new DateTimePicker
          {
            Name = "dtp" + controlName,
            Font = new Font(DefaultFontName, 12),
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy",
            Value = DateTime.Now,
            Width = 300,
          };
        }
        else if (controlName == "TrangThai") {
          ComboBox comboBox = new ComboBox
          {
            Name = "cbo" + controlName,
            Font = new Font(DefaultFontName, 12),
            DropDownStyle = ComboBoxStyle.DropDownList,
            Width = 320,
            MaxDropDownItems = 5,
            DropDownHeight = 200,
          };
          comboBox.Items.AddRange(new String[]{ "Kích Hoạt", "Vô Hiệu Hóa" });
          control = comboBox;
        }
        else
        {
          control = new TextBox
          {
            Name = "txt" + controlName,
            Font = new Font(DefaultFontName, 12),
            Text = "",
            Width = 300,
          };
        }

        panel.Controls.Add(lbl);
        panel.Controls.Add(control);
        flpInfoPanel.Controls.Add(panel);
      }
    }
    private void LoadDetailForm(Dictionary<string, string> inputLabels)
    {
      foreach (var prop in thongTinNguoiDung.GetType().GetProperties())
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

        if (prop.Name == "MaND" || prop.Name == "HoTen" || prop.Name == "Dchi")
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            Text = prop.GetValue(thongTinNguoiDung)?.ToString(),
            Width = 300,
          };
        }
        else if (prop.Name == "Sdt")
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            Text = prop.GetValue(thongTinNguoiDung)?.ToString(),
            MaxLength = 10,
            Width = 300,
          };
        }
        else if (prop.Name == "Cccd")
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            Text = prop.GetValue(thongTinNguoiDung)?.ToString(),
            MaxLength = 12,
            Width = 300,
          };
        }
        else if (prop.Name == "GioiTinh")
        {
          control = new FlowLayoutPanel()
          {
            Name = "flp" + prop.Name,
            AutoSize = true,
          };
          RadioButton radNam = new RadioButton()
          {
            Text = "Nam",
            Name = "radNam",
            Font = new Font(DefaultFontName, 12),
            Checked = (bool)prop.GetValue(thongTinNguoiDung),
          };
          RadioButton radNu = new RadioButton()
          {
            Text = "Nữ",
            Name = "radNu",
            Font = new Font(DefaultFontName, 12),
            Checked = !(bool)prop.GetValue(thongTinNguoiDung)
          };
          control.Controls.Add(radNam);
          control.Controls.Add(radNu);
        }
        else if (prop.Name == "VaiTro")
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
          comboBox.Items.AddRange(dsVaiTro.ToArray());
          comboBox.SelectedItem = dsVaiTro.FirstOrDefault(x => x == prop.GetValue(thongTinNguoiDung)?.ToString());
          control = comboBox;
        }
        else if (prop.Name == "NgSinh")
        {
          control = new DateTimePicker
          {
            Name = "dtp" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy",
            Value = (DateTime)prop.GetValue(thongTinNguoiDung),
            Width = 300,
          };
        }
        else if (prop.Name == "NgVaoLam")
        {
          control = new DateTimePicker
          {
            Name = "dtp" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd/MM/yyyy",
            Value = (DateTime)prop.GetValue(thongTinNguoiDung),
            Width = 300,
          };
        }
        else
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            Text = "",
            Width = 300,
          };
        }

        panel.Controls.Add(lbl);
        panel.Controls.Add(control);
        flpInfoPanel.Controls.Add(panel);
      }

      foreach (var prop in thongTinTaiKhoan.GetType().GetProperties())
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

        if (prop.Name == "MaND")
        {
          continue;
        }
        else if (prop.Name == "TenTK")
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            Text = thongTinTaiKhoan.TenTK,
            Width = 300,
          };
        }
        else if (prop.Name == "MatKhau")
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            Text = thongTinTaiKhoan.MatKhau,
            Width = 300,
          };
        }
        else if (prop.Name == "TrangThai")
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
          comboBox.Items.AddRange(new String[] { "Kích Hoạt", "Vô Hiệu Hóa" });
          comboBox.SelectedIndex = thongTinTaiKhoan.TrangThai ? 0 : 1;
          control = comboBox;
        }
        else
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            Text = thongTinTaiKhoan.MatKhau,
          };
        }

        panel.Controls.Add(lbl);
        panel.Controls.Add(control);
        flpInfoPanel.Controls.Add(panel);
      }
    }
    private void btnAdd_Click(object sender, EventArgs e)
    {
      NguoiDungDTO newNguoiDung = new NguoiDungDTO {
        HoTen = GetControlByName(flpInfoPanel, "txtHoTen").Text,
        NgSinh = ((DateTimePicker)GetControlByName(flpInfoPanel, "dtpNgSinh")).Value,
        GioiTinh = ((RadioButton)GetControlByName(flpInfoPanel, "radNam")).Checked,
        Sdt = ((TextBox)GetControlByName(flpInfoPanel, "txtSdt")).Text,
        Cccd = ((TextBox)GetControlByName(flpInfoPanel, "txtCccd")).Text,
        Dchi = ((TextBox)GetControlByName(flpInfoPanel, "txtDchi")).Text,
        VaiTro = dsVaiTro[((ComboBox)GetControlByName(flpInfoPanel, "cboVaiTro")).SelectedIndex],
        NgVaoLam = ((DateTimePicker)GetControlByName(flpInfoPanel, "dtpNgVaoLam")).Value
      };
      newNguoiDung.MaND = BUS.GetNextId(newNguoiDung.VaiTro);
      TaiKhoanDTO newTaiKhoan = new TaiKhoanDTO
      {
        MaND = newNguoiDung.MaND,
        TenTK = GetControlByName(flpInfoPanel, "txtTenTK").Text,
        MatKhau = GetControlByName(flpInfoPanel, "txtMatKhau").Text,
        TrangThai = true,
      };

      if (String.IsNullOrWhiteSpace(newTaiKhoan.TenTK) || String.IsNullOrWhiteSpace(newTaiKhoan.MatKhau))
      {
        MessageBox.Show("Tài khoản, mật khẩu khônng được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      if (BUS.Add(newNguoiDung) != -1 && taiKhoanBUS.Add(newTaiKhoan))
      {
        notify = new Notification("Thêm người dùng thành công");
        notify.Show();
        OnAddSubmit(new DetailFormAddSubmitEventArgs());
      }
    }
    private void btnEdit_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Bạn có chắc chắn sửa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
        return;

      NguoiDungDTO updatedNguoiDung = new NguoiDungDTO
      {
        MaND = thongTinNguoiDung.MaND,
        HoTen = GetControlByName(flpInfoPanel, "txtHoTen").Text,
        NgSinh = ((DateTimePicker)GetControlByName(flpInfoPanel, "dtpNgSinh")).Value,
        GioiTinh = ((RadioButton)GetControlByName(flpInfoPanel, "radNam")).Checked,
        Sdt =  ((TextBox)GetControlByName(flpInfoPanel, "txtSdt")).Text,
        Cccd = ((TextBox)GetControlByName(flpInfoPanel, "txtCccd")).Text,
        Dchi = ((TextBox)GetControlByName(flpInfoPanel, "txtDchi")).Text,
        VaiTro = dsVaiTro[((ComboBox)GetControlByName(flpInfoPanel, "cboVaiTro")).SelectedIndex],
        NgVaoLam = ((DateTimePicker)GetControlByName(flpInfoPanel, "dtpNgVaoLam")).Value
      };
      TaiKhoanDTO updatedTaiKhoan = new TaiKhoanDTO
      {
        MaND = thongTinNguoiDung.MaND,
        TenTK = GetControlByName(flpInfoPanel, "txtTenTK").Text,
        MatKhau = GetControlByName(flpInfoPanel, "txtMatKhau").Text,
        TrangThai = ((ComboBox)GetControlByName(flpInfoPanel, "cboTrangThai")).SelectedItem.ToString() == "Kích Hoạt"
      };

      bool result;

      if (updatedTaiKhoan.TrangThai)
        result = taiKhoanBUS.Active(thongTinTaiKhoan.MaND);
      else
        result = taiKhoanBUS.Deactive(thongTinTaiKhoan.MaND);


      if (result && BUS.Update(thongTinNguoiDung, updatedNguoiDung) && taiKhoanBUS.Update(thongTinTaiKhoan, updatedTaiKhoan))
      {
        notify = new Notification("Cập nhật người dùng thành công");
        notify.Show();
        OnEditSubmit(new DetailFormEditSubmitEventArgs());
      }
    }
  }
}

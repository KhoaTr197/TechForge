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

namespace TechForgeGUI.SubPages
{
  public partial class CustomerDetailFormGUI : DetailFormGUI
  {
    private HoiVienDTO thongTinHoiVien { get; set; }
    private HoiVienBUS BUS { get; set; }
    private FlowLayoutPanel flpInfo;
    public CustomerDetailFormGUI(HoiVienBUS _BUS, HoiVienDTO _thongTinHoiVien = null)
    {
      InitializeComponent();

      Dictionary<string, string> inputLabels = new Dictionary<string, string>
      {
        { "MaHV", "Mã Hội Viên" },
        { "HoTen", "Họ Tên" },
        { "GioiTinh", "Giới Tính" },
        { "Sdt", "Số Điện Thoại" },
        { "Dchi", "Địa Chỉ" },
      };

      this.thongTinHoiVien = _thongTinHoiVien;
      this.BUS = _BUS;
      this.Text = "Thêm Hội Viên";
      this.MinimumSize = new Size(0, 0);
      this.Size = new Size(500, 340);
      this.Location = Form.ActiveForm != null ? Form.ActiveForm.PointToScreen(new Point((Form.ActiveForm.Width - this.Width) / 2, (Form.ActiveForm.Height - this.Height) / 2)) : new Point(0, 0);

      flpInfo = new FlowLayoutPanel
      {
        BackColor = Color.FromArgb(240, 240, 240),
        Dock = DockStyle.Fill,
        Padding = new Padding(8, 32, 8, 64),
      };

      if (thongTinHoiVien == null)
      {
        this.btnEdit.Visible = false;
        this.btnEdit.Enabled = false;
        this.btnDelete.Visible = false;
        this.btnDelete.Enabled = false;


        LoadAddForm(inputLabels);
      }
      else
      {
        this.Size = new Size(500, 432);

        this.btnAdd.Visible = false;
        this.btnAdd.Enabled = false;


        LoadDetailForm(inputLabels);
      }

      this.btnAdd.Click += btnAdd_Click;
      this.btnEdit.Click += btnEdit_Click;
      this.btnDelete.Click += btnDelete_Click;

      this.Controls.Add(flpInfo);
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

        if (controlName == "MaHV")
        {
          continue;
        }
        if (controlName == "HoTen" || controlName == "Sdt")
        {
          control = new TextBox
          {
            Name = "txt" + controlName,
            Font = new Font(DefaultFontName, 12),
            Text = "",
            MaxLength = controlName == "Sdt" ? 10 : 100,
            Width = 240,
          };
        }
        else if(controlName == "Dchi")
        {
          control = new TextBox
          {
            Name = "txt" + controlName,
            Font = new Font(DefaultFontName, 12),
            Multiline = true,
            Text = "",
            Width = 240,
            Height = 80,
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
        else if (controlName == "TrangThai")
        {
          control = new ComboBox()
          {
            Name = "cbo" + controlName,
            Font = new Font(DefaultFontName, 12),
            Width = 240,
            DropDownStyle = ComboBoxStyle.DropDownList,
          };
          ((ComboBox)control).Items.AddRange(new string[] { "Hoạt động", "Không hoạt động" });
          ((ComboBox)control).SelectedIndex = 0;
        }
        else
        {
          control = new TextBox
          {
            Name = "txt" + controlName,
            Font = new Font(DefaultFontName, 10),
            Text = "",
          };
        }

        panel.Controls.Add(lbl);
        panel.Controls.Add(control);
        flpInfo.Controls.Add(panel);
      }
    }
    private void LoadDetailForm(Dictionary<string, string> inputLabels)
    {
      foreach (var prop in thongTinHoiVien.GetType().GetProperties())
      {
        string controlName = prop.Name;
        FlowLayoutPanel panel = new FlowLayoutPanel
        {
          AutoSize = true,
          FlowDirection = FlowDirection.LeftToRight,
        };

        Label lbl = new Label
        {
          Width = 128,
          Margin = new Padding(0, 4, 0, 0),
          Font = new Font(DefaultFontName, 12),
          TextAlign = ContentAlignment.MiddleLeft,
          Text = inputLabels.ContainsKey(controlName) ? inputLabels[controlName] + ":" : prop.Name + ":",
        };

        Control control;
        
        if (controlName == "MaHV" || controlName == "HoTen" || controlName == "Sdt")
        {
          control = new TextBox
          {
            Name = "txt" + controlName,
            Font = new Font(DefaultFontName, 12),
            Text = prop.GetValue(thongTinHoiVien)?.ToString(),
            MaxLength = controlName == "Sdt" ? 10 : 100,
            Width = 240,
          };
        }
        else if (controlName == "Dchi")
        {
          control = new TextBox
          {
            Name = "txt" + controlName,
            Font = new Font(DefaultFontName, 12),
            Multiline = true,
            Text = prop.GetValue(thongTinHoiVien)?.ToString(),
            Width = 240,
            Height = 80,
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
            Checked = (bool)prop.GetValue(thongTinHoiVien),
          };
          RadioButton radNu = new RadioButton()
          {
            Text = "Nữ",
            Name = "radNu",
            Font = new Font(DefaultFontName, 12),
            Checked = !(bool)prop.GetValue(thongTinHoiVien)
          };
          control.Controls.Add(radNam);
          control.Controls.Add(radNu);
        }
        else if (controlName == "TrangThai")
        {
          control = new ComboBox()
          {
            Name = "cbo" + controlName,
            Font = new Font(DefaultFontName, 12),
            Width = 240,
            DropDownStyle = ComboBoxStyle.DropDownList,
          };
          ((ComboBox)control).Items.AddRange(new string[] { "Hoạt động", "Không hoạt động" });
          ((ComboBox)control).SelectedIndex = ((bool)prop.GetValue(thongTinHoiVien)) ? 0 : 1;
        }
        else
        {
          control = new TextBox
          {
            Name = "txt" + controlName,
            Font = new Font(DefaultFontName, 10),
            Text = "",
          };
        }

        panel.Controls.Add(lbl);
        panel.Controls.Add(control);
        flpInfo.Controls.Add(panel);
      }
    }
    private void btnAdd_Click(object sender, EventArgs e)
    {
      HoiVienDTO newHoiVien = new HoiVienDTO()
      {
        HoTen = ((TextBox)GetControlByName(flpInfo, "txtHoTen")).Text,
        Sdt = ((TextBox)GetControlByName(flpInfo, "txtSdt")).Text,
        Dchi = ((TextBox)GetControlByName(flpInfo, "txtDchi")).Text,
        GioiTinh = ((RadioButton)flpInfo.Controls.Find("radNam", true)[0]).Checked,
        TrangThai = true,
      };

      if (BUS.Add(newHoiVien) != -1)
        OnAddSubmit(new DetailFormAddSubmitEventArgs(this));
    }
    private void btnEdit_Click(object sender, EventArgs e)
    {
      HoiVienDTO updatedHoiVien = new HoiVienDTO()
      {
        MaHV = thongTinHoiVien.MaHV,
        HoTen = ((TextBox)GetControlByName(flpInfo, "txtHoTen")).Text,
        Sdt = ((TextBox)GetControlByName(flpInfo, "txtSdt")).Text,
        Dchi = ((TextBox)GetControlByName(flpInfo, "txtDchi")).Text,
        GioiTinh = ((RadioButton)flpInfo.Controls.Find("radNam", true)[0]).Checked,
        TrangThai = true,
      };

      if (BUS.Update(thongTinHoiVien, updatedHoiVien))
        OnEditSubmit(new DetailFormEditSubmitEventArgs(this));
    }
    private void btnDelete_Click(object sender, EventArgs e)
    {
      if (BUS.Delete(thongTinHoiVien.MaHV))
        OnDeleteSubmit(new DetailFormDeleteSubmitEventArgs(this));
    }
  }
}

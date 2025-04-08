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
    private TableLayoutPanel tlpInfo;
    public CustomerDetailFormGUI(HoiVienBUS _BUS, HoiVienDTO _thongTinHoiVien = null)
    {
      InitializeComponent();

      Dictionary<string, string> inputLabels = new Dictionary<string, string>
      {
        { "HoTen", "Họ Tên" },
        { "Sdt", "Số Điện Thoại" },
        { "Dchi", "Địa Chỉ" },
        { "GioiTinh", "Giới Tính" },
      };

      this.thongTinHoiVien = _thongTinHoiVien;
      this.BUS = _BUS;
      this.Text = "Thêm Hội Viên";
      this.btnEdit.Visible = false;
      this.btnEdit.Enabled = false;
      this.btnDelete.Visible = false;
      this.btnDelete.Enabled = false;
      this.MinimumSize = new Size(0, 0);
      this.Size = new Size(500, 300);
      this.Location = Form.ActiveForm != null ? Form.ActiveForm.PointToScreen(new Point((Form.ActiveForm.Width - this.Width) / 2, (Form.ActiveForm.Height - this.Height) / 2)) : new Point(0, 0);

      tlpInfo = new TableLayoutPanel
      {
        BackColor = Color.FromArgb(240, 240, 240),
        Dock = DockStyle.Fill,
        Padding = new Padding(8, 32, 8, 64),
        ColumnCount = 1,
        RowCount = inputLabels.Count,
        ColumnStyles = {
          new ColumnStyle(SizeType.Percent, 100),
        },
      };
      for (int i = 0; i < inputLabels.Count; i++)
      {
        tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / inputLabels.Count));
      }

      if (thongTinHoiVien == null)
      {
        LoadAddForm(inputLabels);
      }
      else
      {
        LoadDetailForm();
      }

      this.btnAdd.Click += btnAdd_Click;

      this.Controls.Add(tlpInfo);
    }
    private void LoadAddForm(Dictionary<string, string> inputLabels)
    {
      int rowIdx = 0;

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
        tlpInfo.Controls.Add(panel, 0, rowIdx++);
      }
    }
    private void LoadDetailForm()
    {
    }
    private void btnAdd_Click(object sender, EventArgs e)
    {
      HoiVienDTO newHoiVien = new HoiVienDTO()
      {
        HoTen = ((TextBox)GetControlByName(tlpInfo, "txtHoTen")).Text,
        Sdt = ((TextBox)GetControlByName(tlpInfo, "txtSdt")).Text,
        Dchi = ((TextBox)GetControlByName(tlpInfo, "txtDchi")).Text,
        GioiTinh = ((RadioButton)tlpInfo.Controls.Find("radNam", true)[0]).Checked,
        TrangThai = true,
      };

      if (BUS.Add(newHoiVien) != -1)
        OnAddSubmit(new DetailFormAddSubmitEventArgs());
    }
  }
}

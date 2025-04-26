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
  public partial class SupplierDetailFormGUI : DetailFormGUI
  {
    private NhaCungCapDTO thongTinNcc { get; set; }
    private NhaCungCapBUS BUS { get; set; }
    private FlowLayoutPanel flpInfoPanel { get; set; }
    private RolePermissions permissions { get; set; }
    private Notification notify;
    public SupplierDetailFormGUI(RolePermissions _permissions, NhaCungCapBUS _BUS, NhaCungCapDTO _thongTinNcc = null)
    {
      InitializeComponent();

      this.thongTinNcc = _thongTinNcc;

      if (thongTinNcc != null)
        type = "Detail";
      else
        type = "Add";

      this.BUS = _BUS;
      this.permissions = _permissions;
      this.Text = "Chi tiết nhà cung cấp";

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
        { "MaNCC", "Mã NCC" },
        { "TenNCC", "Tên NCC" },
        { "Ndd", "Người ĐD" },
        { "Sdt", "SĐT" },
        { "Email", "Email" },
        { "TrangThai", "Trạng Thái" },
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
        this.btnDelete.Visible = false;
        this.btnDelete.Enabled = false;


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
          this.btnEdit.Visible = true;
          this.btnEdit.Enabled = true;
        }
        else
        {
          this.btnAdd.Visible = true;
          this.btnAdd.Enabled = true;
          this.btnEdit.Visible = false;
          this.btnEdit.Enabled = false;
        }
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

        if (controlName == "MaNCC" || controlName == "TrangThai")
        {
          continue;
        }
        else if (controlName == "TenNCC" || controlName == "Ndd")
        {
          control = new TextBox
          {
            Name = "txt" + controlName,
            Font = new Font(DefaultFontName, 12),
            Text = "",
            Width = 300,
          };
        }
        else if (controlName == "Sdt")
        {
          control = new TextBox
          {
            Name = "txt" + controlName,
            Font = new Font(DefaultFontName, 12),
            Text = "",
            Width = 300,
          };
        }
        else if (controlName == "Email")
        {
          control = new TextBox
          {
            Name = "txt" + controlName,
            Font = new Font(DefaultFontName, 12),
            Text = "",
            Width = 300,
          };
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
      foreach (var prop in thongTinNcc.GetType().GetProperties())
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

        if (prop.Name == "MaNCC")
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            Text = prop.GetValue(thongTinNcc)?.ToString(),
            Width = 300,
            Enabled = false,
          };
        }
        else if (prop.Name == "TenNCC" || prop.Name == "Ndd" || prop.Name == "Sdt" || prop.Name == "Email")
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            Text = prop.GetValue(thongTinNcc)?.ToString(),
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
          comboBox.Items.AddRange(new String[] { "Hợp tác", "Ngừng hợp tác" });
          comboBox.SelectedIndex = thongTinNcc.TrangThai ? 0 : 1;
          control = comboBox;
        }
        else
        {
          control = new TextBox
          {
            Name = "txt" + prop.Name,
            Font = new Font(DefaultFontName, 12),
            Text = "",
            Enabled = false,
          };
        }

        panel.Controls.Add(lbl);
        panel.Controls.Add(control);
        flpInfoPanel.Controls.Add(panel);
      }
    }
    private void btnAdd_Click(object sender, EventArgs e)
    {
      NhaCungCapDTO newNcc = new NhaCungCapDTO
      {
        MaNCC = BUS.GetNextId(),
        TenNCC = GetControlByName(flpInfoPanel, "txtTenNCC").Text,
        Ndd = GetControlByName(flpInfoPanel, "txtNdd").Text,
        Sdt = GetControlByName(flpInfoPanel, "txtSdt").Text,
        Email = GetControlByName(flpInfoPanel, "txtEmail").Text,
        TrangThai = true,
      };
      if (BUS.Add(newNcc) != -1)
      {
        notify = new Notification("Them thanh cong");
        notify.Show();
        OnAddSubmit(new DetailFormAddSubmitEventArgs());
      }
    }
    private void btnEdit_Click(object sender, EventArgs e)
    {
      NhaCungCapDTO updatedNcc = new NhaCungCapDTO
      {
        MaNCC = thongTinNcc.MaNCC,
        TenNCC = GetControlByName(flpInfoPanel, "txtTenNCC").Text,
        Ndd = GetControlByName(flpInfoPanel, "txtNdd").Text,
        Sdt = GetControlByName(flpInfoPanel, "txtSdt").Text,
        Email = GetControlByName(flpInfoPanel, "txtEmail").Text,
        TrangThai = ((ComboBox)GetControlByName(flpInfoPanel, "cboTrangThai")).SelectedItem.ToString() == "Hợp tác"
      };

      if (updatedNcc.TrangThai)
        BUS.Active(thongTinNcc.MaNCC);
      else
        BUS.Deactive(thongTinNcc.MaNCC);


      if (BUS.Update(thongTinNcc, updatedNcc))
      {
        notify = new Notification("Cap nhat thanh cong");
        notify.Show();
        OnEditSubmit(new DetailFormEditSubmitEventArgs());
      }
    }
  }
}

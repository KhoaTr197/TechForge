using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeDTO;
using TechForgeGUI.BaseForms;

namespace TechForgeGUI.SubPages
{
  public partial class ProductDetailFormGUI : DetailFormGUI
  {
    private SanPhamDTO info { get; set; }
    private FlowLayoutPanel flpInfoPanel;
    public ProductDetailFormGUI(SanPhamDTO sanPham)
    {
      InitializeComponent();

      info = sanPham;
      Text = "Chi tiết sản phẩm";

      flpInfoPanel = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.TopDown,
        Padding = new Padding(4, 32, 4, 64),
      };

      foreach (var prop in info.GetType().GetProperties())
      {
        FlowLayoutPanel panel = new FlowLayoutPanel
        {
          AutoSize = true,
          Height = 32,
          FlowDirection = FlowDirection.LeftToRight,
          BackColor = Color.FromArgb(240, 240, 240),
        };

        Label lbl = new Label
        {
          Width = 96,
          Font = new Font(DefaultFontName, 10),
          TextAlign = ContentAlignment.MiddleLeft,
          Text = prop.Name + ":",
        };

        Control control;
        if (prop.Name == "MaSp")
        {
          control = new TextBox
          {
            Font = new Font(DefaultFontName, 10),
            Text = prop.GetValue(info)?.ToString(),
            ReadOnly = true,
          };
        }
        else if (prop.PropertyType == typeof(DateTime))
        {
          control = new DateTimePicker
          {
            Value = (DateTime)prop.GetValue(info),
            Font = new Font(DefaultFontName, 10),
            Enabled = false,
          };
        }
        else if (prop.PropertyType == typeof(bool))
        {
          control = new ComboBox
          {
            Font = new Font(DefaultFontName, 10),
            DropDownStyle = ComboBoxStyle.DropDownList,
            Items = { "True", "False" },
            SelectedItem = prop.GetValue(info).ToString(),
            Enabled = false,
          };
        }
        else if (prop.PropertyType == typeof(decimal))
        {
          decimal value = Convert.ToDecimal(prop.GetValue(info));
          decimal minimum = 0;
          decimal maximum = 250000000;
          control = new NumericUpDown
          {
            Font = new Font(DefaultFontName, 10),
            ThousandsSeparator = true,
            Increment = prop.Name == "KhuyenMai" ? 1 : 100000,
            Minimum = minimum,
            Maximum = prop.Name == "KhuyenMai" ? 100 : maximum,
            Value = value,
          };
        }
        else
        {
          control = new TextBox
          {
            BackColor = Color.White,
            Size = prop.Name == "MoTa" ? new Size(320, 160) : new Size(320, 48),
            Multiline = prop.Name == "MoTa",
            ScrollBars = prop.Name == "MoTa" ? ScrollBars.Vertical : ScrollBars.None,
            Font = new Font(DefaultFontName, 10),
            Text = prop.GetValue(info)?.ToString(),
            ReadOnly = true,
          };
        }

        panel.Controls.Add(lbl);
        panel.Controls.Add(control);
        flpInfoPanel.Controls.Add(panel);
      }


      this.Controls.Add(flpInfoPanel);
    }
  }
}

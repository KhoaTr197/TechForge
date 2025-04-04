using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechForgeGUI.BaseForms
{
  public partial class DetailFormGUI : Form
  {
    private FlowLayoutPanel flpActionsPanel;
    public Button btnEdit;
    public Button btnDelete;
    protected string DefaultFontName = "Segoe UI";
    public DetailFormGUI()
    {
      InitializeComponent();

      this.TopMost = true;
      this.MinimumSize = new Size(900, 500);
      StartPosition = FormStartPosition.CenterParent;

      flpActionsPanel = new FlowLayoutPanel
      {
        BackColor = Color.Orange,
        AutoSize = true,
        Dock = DockStyle.Bottom,
        FlowDirection = FlowDirection.RightToLeft,
      };
      btnEdit = new Button
      {
        BackColor = Color.White,
        Text = "Sửa",
        AutoSize = true,
        Font = new Font(DefaultFontName, 10),
        Dock = DockStyle.Right,
        Margin = new Padding(4),
      };
      btnDelete = new Button
      {
        BackColor = Color.White,
        Text = "Xóa",
        AutoSize = true,
        Font = new Font(DefaultFontName, 10),
        Dock = DockStyle.Right,
        Margin = new Padding(4),
      };
      flpActionsPanel.Controls.Add(btnDelete);
      flpActionsPanel.Controls.Add(btnEdit);
      this.Controls.Add(flpActionsPanel);
    }
  }
}

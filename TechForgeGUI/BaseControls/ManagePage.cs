using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TechForgeBUS;
using TechForgeGUI.BaseControls;
using TechForgeGUI.Utils;

namespace TechForgeGUI.BaseForms
{
  public partial class ManagePage : UserControl
  {
    public Form FormParent { get; set; }
    private TableLayoutPanel tlpMain;
    protected Button btnAdd;
    protected CustomDataGridView dgvMainList;

    protected readonly string connStr = "Data Source=.;Initial Catalog=TECHFORGE;Integrated Security=True;";

    protected string DefaultFontName = "Segoe UI";
    public ManagePage()
    {
      InitializeComponent();
      InitalizeLayout();
      IntializeHeader();
      InitalizeDgvMainList();

      this.Font = new Font(DefaultFontName, 10);
      this.Dock = DockStyle.Fill;

    }
    private void InitalizeLayout()
    {
      tlpMain = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 2,
        ColumnStyles = {
          new ColumnStyle(SizeType.Percent, 70),
          new ColumnStyle(SizeType.Percent, 30)
        },
        RowStyles = {
          new RowStyle(SizeType.Percent, 20),
          new RowStyle(SizeType.Percent, 80)
        },
        Size = new Size(this.Width, this.Height / 100 * 10),
      };
      this.Controls.Add(tlpMain);
    }
    private void IntializeHeader()
    {
      FlowLayoutPanel flpSearchFilter = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.LeftToRight,
        Padding = new Padding(0),
        Margin = new Padding(0),
      };
      TextBox txtSearhBar = new TextBox
      {
        Dock = DockStyle.Fill,
        Font = new Font(DefaultFontName, 12),
        Text = "Tìm kiếm",
        BorderStyle = BorderStyle.None,
        BackColor = Color.Red,
      };

      FlowLayoutPanel flpActions = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.LeftToRight,
      };
      btnAdd = new Button()
      {
        Name = "btnAdd",
        Text = "Thêm",
        Font = new Font(DefaultFontName, 10),
        ImageList = GlobalStatics.iconList,
        ImageKey = "add_icon",
        TextImageRelation = TextImageRelation.ImageBeforeText,
      };

      flpSearchFilter.Controls.Add(txtSearhBar);
      flpActions.Controls.Add(btnAdd);

      tlpMain.Controls.Add(flpSearchFilter, 0, 0);
      tlpMain.Controls.Add(flpActions, 1, 0);
    }
    private void InitalizeDgvMainList()
    {
      dgvMainList = new CustomDataGridView() {
        Dock = DockStyle.Fill,
      };

      tlpMain.Controls.Add(dgvMainList);
      tlpMain.SetColumnSpan(dgvMainList, 2);
    }
    private void dgvMainList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
      //When data binding is completed, do sth here!
    }
    protected virtual void InitializeBUS()
    {
      //Initialize BUS method here!
    }
    protected virtual void LoadData() {
      //Use BUS method to load data here!
    }
  }
}

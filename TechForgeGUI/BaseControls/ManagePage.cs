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
using System.Windows.Forms.DataVisualization.Charting;
using TechForgeBUS;
using TechForgeGUI.BaseControls;
using TechForgeGUI.Utils;

namespace TechForgeGUI.BaseForms
{
  public partial class ManagePage : UserControl
  {
    public Form FormParent { get; set; }
    private TableLayoutPanel tlpMain;
    public Button btnAdd;
    protected CustomDataGridView dgvMainList;
    
    // Summary cards section
    private FlowLayoutPanel flpSummaryCards;
    protected SummaryCards summaryCards;

    protected readonly string connStr = "Data Source=.;Initial Catalog=TECHFORGE;Integrated Security=True;";

    protected string DefaultFontName = "Segoe UI";
    public ManagePage()
    {
      InitializeComponent();
      InitalizeLayout();
      InitializeSummarySection();
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
        RowCount = 3,
        ColumnStyles = {
          new ColumnStyle(SizeType.Percent, 70),
          new ColumnStyle(SizeType.Percent, 30)
        },
        RowStyles = {
          new RowStyle(SizeType.AutoSize),
          new RowStyle(SizeType.Percent, 20),
          new RowStyle(SizeType.Percent, 80)
        },
        Size = new Size(this.Width, this.Height / 100 * 10),
      };
      this.Controls.Add(tlpMain);
    }
    private void InitializeSummarySection()
    {
      flpSummaryCards = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.LeftToRight,
        WrapContents = false,
        Margin = new Padding(4),
      };
      
      summaryCards = new SummaryCards(flpSummaryCards, 4);

      tlpMain.Controls.Add(flpSummaryCards, 0, 0);
      tlpMain.SetColumnSpan(flpSummaryCards, 2);
    }
    private void IntializeHeader()
    {
      // Left panel with search functionality
      FlowLayoutPanel flpSearch = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.LeftToRight,
      };
      
      // Search label
      Label lblSearch = new Label
      {
        AutoSize = true,
        Text = "Tìm kiếm:",
        Font = new Font(DefaultFontName, 10),
        TextAlign = ContentAlignment.MiddleLeft,
      };
      
      // Search textbox with border
      TextBox txtSearchBar = new TextBox
      {
        Name = "txtSearchBar",
        Font = new Font(DefaultFontName, 12),
        Size = new Size(240, 32),
        BorderStyle = BorderStyle.FixedSingle
      };
      
      // Search button
      Button btnSearch = new Button
      {
        Name = "btnSearch",
        Text = "Tìm",
        Font = new Font(DefaultFontName, 10),
        AutoSize=true,
        FlatStyle = FlatStyle.Flat,
        FlatAppearance = {
          BorderSize = 0
        },
        BackColor = Color.FromArgb(0, 120, 215),
        ForeColor = Color.White,
        ImageList = GlobalStatics.iconList,
        ImageKey = "search_icon",
        TextImageRelation = TextImageRelation.ImageBeforeText,
        ImageAlign = ContentAlignment.MiddleLeft,
        Cursor = Cursors.Hand,
      };
      
      // Add controls to search panel
      flpSearch.Controls.Add(lblSearch);
      flpSearch.Controls.Add(txtSearchBar);
      flpSearch.Controls.Add(btnSearch);
      
      // Right panel with action buttons
      FlowLayoutPanel pnlActions = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
      };
      
      // Add button
      btnAdd = new Button
      {
        Size = new Size(100, 32),
        Name = "btnAdd",
        Text = "Thêm mới",
        Font = new Font(DefaultFontName, 10),
        FlatStyle = FlatStyle.Flat,
        FlatAppearance = {
          BorderSize = 0
        },
        BackColor = Color.FromArgb(46, 139, 87),
        ForeColor = Color.White,
        ImageList = GlobalStatics.iconList,
        ImageKey = "add_icon",
        TextImageRelation = TextImageRelation.ImageBeforeText,
        Cursor = Cursors.Hand,
        Anchor = AnchorStyles.Right
      };
      
      // Add controls to action panel
      pnlActions.Controls.Add(btnAdd);

      // Add panels to table layout
      tlpMain.Controls.Add(flpSearch, 0, 1);
      tlpMain.Controls.Add(pnlActions, 1, 1);
      
      // Attach search functionality
      txtSearchBar.KeyDown += (sender, e) => {
        if (e.KeyCode == Keys.Enter)
        {
          PerformSearch(txtSearchBar.Text);
        }
      };
      
      btnSearch.Click += (sender, e) => {
        PerformSearch(txtSearchBar.Text);
      };
    }
    
    private void InitalizeDgvMainList()
    {
      dgvMainList = new CustomDataGridView() {
        Dock = DockStyle.Fill,
      };

      tlpMain.Controls.Add(dgvMainList, 0, 2);
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
    
    // Virtual search method
    protected virtual void PerformSearch(string searchText)
    {
      if (dgvMainList != null && dgvMainList.dgvList != null && dgvMainList.dgvList.DataSource != null)
      {
        try
        {
          BindingSource bs = dgvMainList.dgvList.DataSource as BindingSource;
          if (bs != null)
          {
            bs.Filter = GenerateSearchFilter(searchText);
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show("Không thể thực hiện tìm kiếm. Lỗi: " + ex.Message,
            "Lỗi tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }
    
    // Create virtual default filter string
    protected virtual string GenerateSearchFilter(string searchText)
    {
      return string.Empty;
    }
  }
}

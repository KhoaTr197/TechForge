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
    protected Button btnAdd;
    protected CustomDataGridView dgvMainList;
    
    // Summary cards section
    private FlowLayoutPanel flpSummaryCards;
    private List<SummaryCard> summaryCards = new List<SummaryCard>();

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
        AutoSize = true,
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.LeftToRight,
        WrapContents = false,
        AutoScroll = true,
        Margin = new Padding(4),
      };
      
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

    // Method to add summary cards
    protected void AddSummaryCards(SummaryCard[] cards)
    {
      // Clear existing cards
      flpSummaryCards.Controls.Clear();
      summaryCards.Clear();

      // Limit to maximum 4 cards
      int cardCount = Math.Min(cards.Length, 4);
      if (cardCount == 0) return;

      // Calculate width percentage for each card
      int widthPercentage = 100 / cardCount;
      int cardWidth = (flpSummaryCards.Width - 20) / cardCount;

      // Create and add cards
      for (int i = 0; i < cardCount; i++)
      {
        var cardInfo = cards[i];
        var card = new SummaryCard(cardInfo)
        {
          Width = cardWidth,
          Height = 80,
          Margin = new Padding(5),
        };
        summaryCards.Add(card);
        flpSummaryCards.Controls.Add(card);
      }
    }
    // Method to update summary cards
    protected void UpdateSummaryCards(SummaryCard[] cards)
    {
      int count = Math.Min(cards.Length, summaryCards.Count);
      for (int i = 0; i < count; i++)
      {
        summaryCards[i] = cards[i];
      }
    }
    // Resize cards when container resizes
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      ResizeSummaryCards();
    }

    private void ResizeSummaryCards()
    {
      if (flpSummaryCards == null || flpSummaryCards.Controls.Count == 0) return;

      int cardCount = flpSummaryCards.Controls.Count;
      int cardWidth = (flpSummaryCards.Width - (cardCount * 10)) / cardCount;

      foreach (Control card in flpSummaryCards.Controls)
      {
        card.Width = cardWidth;
      }
    }
  }
 
  // Class for summary card UI
  public class SummaryCard : Panel
  {
    private Label lblTitle;
    private Label lblValue;

    public string Title { get; set; }
    public string Value { get; set; }
    public string Icon { get; set; }
    public SummaryCard(string title, string value, string icon, Color cardColor)
    {
      Title = title;
      Value = value;
      Icon = icon;
      BackColor = cardColor;

      InitalizeSummaryCard();    
    }
    public SummaryCard(SummaryCard card)
    {
      Title = card.Title;
      Value = card.Value;
      Icon = card.Icon;
      BackColor = card.BackColor;

      InitalizeSummaryCard();
    }

    private void InitalizeSummaryCard()
    {
      // Configure panel
      this.Padding = new Padding(10);
      this.BorderStyle = BorderStyle.None;
      
      // Value label
      lblValue = new Label
      {
        Text = Value,
        Font = new Font("Segoe UI", 18, FontStyle.Bold),
        ForeColor = Color.White,
        AutoSize = true,
        TextAlign = ContentAlignment.MiddleLeft,
        Location = new Point(10, 10)
      };
      
      // Title label
      lblTitle = new Label
      {
        Text = Title,
        Font = new Font("Segoe UI", 10),
        ForeColor = Color.WhiteSmoke,
        AutoSize = true,
        TextAlign = ContentAlignment.MiddleLeft,
        Location = new Point(10, 45)
      };
      
      this.Controls.Add(lblValue);
      this.Controls.Add(lblTitle);
    }
  }
}

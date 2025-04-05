using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeDTO;

namespace TechForgeGUI.BaseControls
{
  public partial class CustomDataGridView : UserControl
  {
    //Properties
    private int currentPage;
    private int itemPerPage;
    private int totalPages;
    protected string DefaultFontName = "Segoe UI";

    //Base Controls
    private BindingSource bindSrc;
    public DataGridView List;
    private FlowLayoutPanel flpPagination;
    private Button btnPrev;
    private Button btnNext;
    private Label pageLabel;
    public CustomDataGridView()
    {
      InitializeComponent();

      //Adjust this CustomDataGridView's properites
      this.Margin = new Padding(0);
      this.Size = new Size(500, 250);
      this.Font = new Font(DefaultFontName, 10);
      this.SizeChanged += CustomDataGridView_SizeChanged;
      this.DockChanged += CustomDataGridView_DockChanged;

      //Initialize Binding Source
      bindSrc = new BindingSource();

      //Initialize Base Controls
      List = new DataGridView()
      {
        Dock = DockStyle.Top,
        Margin = new Padding(0),
        Size = new Size(this.Width, this.Height / 100 * 90),
        ReadOnly = true,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        AllowUserToOrderColumns = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None,
        ScrollBars = ScrollBars.Both
      };
      List.RowPrePaint += dgvList_RowPrePaint;
            List.CellFormatting += CustomDataGridView_CellFormatting;

        //Initialize flpPagination
        flpPagination = new FlowLayoutPanel() {
        AutoSize = true,
        Dock = DockStyle.Top,
        BackColor = Color.Transparent,
        FlowDirection = FlowDirection.RightToLeft,
      };

      //Initialize btnPrev, btnNext, pageLabel
      btnPrev = new Button
      {
        Text = "Prev",
        Width = 48
      };
      btnPrev.Click += PrevButton_Click;

      btnNext = new Button
      {
        Text = "Next",
        Width = 48
      };
      btnNext.Click += NextButton_Click;

      pageLabel = new Label
      {
        Text = "Page 1",
        Width = 80,
        Margin = new Padding(0, 2, 0, 2),
        TextAlign = ContentAlignment.MiddleCenter
      };

      //Add these controls to flpPagination
      flpPagination.Controls.Add(btnNext);
      flpPagination.Controls.Add(pageLabel);
      flpPagination.Controls.Add(btnPrev);

      //Add these controls to this CustomDataGridView
      this.Controls.Add(flpPagination);
      this.Controls.Add(List);
    }
    //Event Handlers when Size changed
    private void CustomDataGridView_SizeChanged(object sender, EventArgs e)
    {
      List.Size = new Size(this.Width, this.Height - flpPagination.Height);
    }
    //Event Handlers when Dock changed
    private void CustomDataGridView_DockChanged(object sender, EventArgs e)
    {
      List.Size = Size = new Size(this.Width, this.Height - flpPagination.Height);
    }
    //Event Handlers when PrevButton and NextButton clicked
    private void PrevButton_Click(object sender, EventArgs e)
    {
      if (currentPage > 1)
      {
        currentPage--;
        UpdateDataGridView();
      }
    }
    private void NextButton_Click(object sender, EventArgs e)
    {
      if (currentPage < totalPages)
      {
        currentPage++;
        UpdateDataGridView();
      }
    }
    //Set up Pagination Properties
    public void SetUpPagination(int _totalItems, int _itemPerPage = 5)
    {
      itemPerPage = _itemPerPage;
      totalPages = (int)Math.Ceiling((double)_totalItems / _itemPerPage);
      currentPage = 1;
    }
    //Binding Data method
    public void BindingData(List<object> dataList)
    {
      SetUpPagination(dataList.Count);

      bindSrc.DataSource = dataList;
      List.DataSource = bindSrc;

      UpdateDataGridView();
    }
    public void BindingData(DataTable table)
    {
      SetUpPagination(table.Rows.Count);

      bindSrc.DataSource = table;
      List.DataSource = bindSrc;

      UpdateDataGridView();
    }
    //Update DataGridView displayed data
    private void UpdateDataGridView()
    {
      int startIndex = (currentPage - 1) * itemPerPage;

      if (this.bindSrc.DataSource is List<object> dataList)
      {
        int endIndex = Math.Min(startIndex + itemPerPage, dataList.Count);

        List<object> newDataList = dataList.GetRange(startIndex, endIndex - startIndex);
        this.List.DataSource = newDataList;
      }
      else if (this.bindSrc.DataSource is DataTable table)
      {
        int endIndex = Math.Min(startIndex + itemPerPage, table.Rows.Count);

        DataTable newTable = table.Select().Skip(startIndex).Take(endIndex - startIndex).CopyToDataTable();
        this.List.DataSource = newTable;
      }

      pageLabel.Text = $"Page {currentPage} of {totalPages}";
    }
    //Event Handlers when Row Pre Paint
    private void dgvList_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
    {
      int rowIndex = e.RowIndex;
      var row = List.Rows[rowIndex];

      if ((rowIndex & 1) == 0) //bitwise AND operator out performs % operator in large data scenarios
      {
        row.DefaultCellStyle.BackColor = Color.LightGray;
      }
      else
      {
        row.DefaultCellStyle.BackColor = Color.White;
      }
    }
        //Column name assignment method
        public void SetColumnNames(Dictionary<string, (string HeaderText, bool Visible)> columnMappings)
        {
            foreach (DataGridViewColumn column in List.Columns)
            {
                if (columnMappings.ContainsKey(column.Name))
                {
                    var (headerText, visible) = columnMappings[column.Name];
                    column.HeaderText = headerText;
                    column.Visible = visible;
                }
            }
        }
        //Enable Header Wrap Mode Method
        public void EnableHeaderWrapMode(bool enable)
        {
            List.ColumnHeadersDefaultCellStyle.WrapMode = enable ? DataGridViewTriState.True : DataGridViewTriState.False;
        }
        //Set Column Headers Height
        public int ColumnHeadersHeight
        {
            get => List.ColumnHeadersHeight;
            set
            {
                List.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                List.ColumnHeadersHeight = value;
            }
        }
        //Config Dgv Columns
        public void ConfigureDataGridViewColumns()
        {
            int colIndex = -1;
            foreach (DataGridViewColumn col in List.Columns)
            {
                if(col.HeaderText == "Giới Tính")
                {
                    if(col is DataGridViewCheckBoxColumn)
                    {
                        colIndex = col.Index;
                        List.Columns.Remove(col);
                        break;
                    }
                }
            }
            if(colIndex > -1)
            {
                DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn
                {
                    Name = "GioiTinh",
                    HeaderText = "Giới Tính",
                    DataPropertyName = "GioiTinh" // link to GioiTinh in NguoiDungDTO
                };
                List.Columns.Insert(colIndex, textColumn);
            }
        }
        public void CustomDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (List.Columns[e.ColumnIndex].HeaderText == "Giới Tính" && e.Value != null)
            {
                try
                {
                    bool gioiTinh = (bool)e.Value;
                    e.Value = gioiTinh ? "Nam" : "Nữ";
                    e.FormattingApplied = true;
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

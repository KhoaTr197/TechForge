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
using TechForgeGUI.BaseControls;

namespace TechForgeGUI.SubPages
{
  public partial class ManufacturerManagePageGUI : ManagePage
  {
    private DataSet ds;
    private List<HangSanXuatDTO> dsHangSanXuat { get; set; }
    private HangSanXuatBUS bus { get; set; }
    private RolePermissions permissions;
    public ManufacturerManagePageGUI(string role)
    {
      InitializeComponent();

      // Initialize permissions
      permissions = RolePermissions.GetPermissions(role);

      InitializeBUS();
      GetData();
      LoadData();
      ModifyData();
      SetUpFeature();

      // Attach event handler for cell click
      dgvMainList.dgvList.CellClick += dgvList_CellClick;

      btnAdd.Click += BtnAdd_Click;
    }
    private void SetUpFeature()
    {
      if (permissions.Role == "Cashier")
      {
        this.btnAdd.Visible = false;
        this.btnAdd.Enabled = false;

        summaryCards.Add(new SummaryCard[] {
          new SummaryCard("Tổng hãng", dsHangSanXuat.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
        });
      }
      else if (permissions.Role == "WarehouseStaff")
      {
        this.btnAdd.Visible = true;
        this.btnAdd.Enabled = true;

        summaryCards.Add(new SummaryCard[]
        {
          new SummaryCard("Tổng hãng", dsHangSanXuat.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard("Hãng đang dùng", dsHangSanXuat.Count.ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
          new SummaryCard("Hãng trống", "0", "warning_icon", Color.FromArgb(231, 76, 60)),
          new SummaryCard("Hãng mới", "0", "money_icon", Color.FromArgb(155, 89, 182))
        });
      }
      else if (permissions.Role == "Manager")
      {
        this.btnAdd.Visible = true;
        this.btnAdd.Enabled = true;

        summaryCards.Add(new SummaryCard[]
        {
          new SummaryCard("Tổng hãng", dsHangSanXuat.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard("Hãng đang dùng", dsHangSanXuat.Count.ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
          new SummaryCard("Hãng trống", "0", "warning_icon", Color.FromArgb(231, 76, 60)),
          new SummaryCard("Hãng mới", "0", "money_icon", Color.FromArgb(155, 89, 182))
        });
      }
    }
    sealed protected override void InitializeBUS()
    {
      bus = new HangSanXuatBUS(this.connStr);
    }

    protected void GetData()
    {
      ds = new DataSet();

      // Map data to DTOs
      dsHangSanXuat = bus.GetAllConnected();
    }

    protected override void LoadData()
    {
      dgvMainList.BindingData(dsHangSanXuat);
    }

    private void ModifyData()
    {
      this.SuspendLayout();

      // Add columns to DataGridView
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "MaHSX",
        HeaderText = "Mã hãng",
        DataPropertyName = "MaHSX",
        FillWeight = 32,
        Visible = true
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "TenHSX",
        HeaderText = "Tên hãng",
        DataPropertyName = "TenHSX",
        FillWeight = 200,
        Visible = true
      });

      // Attach cell formatting event handler
      dgvMainList.dgvList.CellFormatting += dgvList_CellFormatting;

      this.ResumeLayout();
    }

    // Format DataGridView cells
    protected void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {   
    }
    private void BtnAdd_Click(object sender, EventArgs e)
    {
      ManufacturerDetailFormGUI detailsForm = new ManufacturerDetailFormGUI(permissions, bus);

      detailsForm.Show();

      detailsForm.AddSubmit += DetailsForm_AddSubmit;
    }
    // Handle cell click event
    protected void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
      {
        DataGridView dgvMainList = (DataGridView)sender;

        if (dgvMainList.SelectedRows.Count > 0)
        {
          DataGridViewRow selectedRow = dgvMainList.SelectedRows[0];
          HangSanXuatDTO hangSanXuat = dsHangSanXuat.Find(sp => sp.MaHSX == (int)selectedRow.Cells[0].Value);

          ManufacturerDetailFormGUI detailsForm = new ManufacturerDetailFormGUI(permissions, bus, hangSanXuat);
          detailsForm.parentForm = this;

          detailsForm.Show(Form.ActiveForm);

          // Assign event handler for submits
          detailsForm.AddSubmit += DetailsForm_AddSubmit;
          detailsForm.EditSubmit += DetailsForm_EditSubmit;
          detailsForm.DeleteSubmit += DetailsForm_DeleteSubmit;
        }
      }
    }
    private void DetailsForm_AddSubmit(object sender, DetailFormAddSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when new manufacturers are added
      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng hãng", dsHangSanXuat.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
        new SummaryCard("Hãng đang dùng", dsHangSanXuat.Count.ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Hãng trống", "0", "warning_icon", Color.FromArgb(231, 76, 60)),
        new SummaryCard("Hãng mới", "0", "money_icon", Color.FromArgb(155, 89, 182))
      });
    }

    private void DetailsForm_EditSubmit(object sender, DetailFormEditSubmitEventArgs e)
    {
      GetData();
      LoadData();

      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng hãng", dsHangSanXuat.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
        new SummaryCard("Hãng đang dùng", dsHangSanXuat.Count.ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Hãng trống", "0", "warning_icon", Color.FromArgb(231, 76, 60)),
        new SummaryCard("Hãng mới", "0", "money_icon", Color.FromArgb(155, 89, 182))
      });
    }
    private void DetailsForm_DeleteSubmit(object sender, DetailFormDeleteSubmitEventArgs e)
    {
      GetData();
      LoadData();

      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng hãng", dsHangSanXuat.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
        new SummaryCard("Hãng đang dùng", dsHangSanXuat.Count.ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Hãng trống", "0", "warning_icon", Color.FromArgb(231, 76, 60)),
        new SummaryCard("Hãng mới", "0", "money_icon", Color.FromArgb(155, 89, 182))
      });
    }
  }
}

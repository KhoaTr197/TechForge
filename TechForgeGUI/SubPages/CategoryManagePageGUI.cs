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
  public partial class CategoryManagePageGUI : ManagePage
  {
    private List<DanhMucDTO> dsDanhMuc { get; set; }
    private DanhMucBUS bus { get; set; }
    private RolePermissions permissions;
    public CategoryManagePageGUI(string role)
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

        //Add summary cards with category statistics
        summaryCards.Add(new SummaryCard[] {
          new SummaryCard("Tổng danh mục", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(52, 152, 219)),
        });
      }
      else if (permissions.Role == "WarehouseStaff")
      {
        this.btnAdd.Visible = true;
        this.btnAdd.Enabled = true;

        summaryCards.Add(new SummaryCard[]
        {
          new SummaryCard ("Tổng danh mục", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard ("Danh mục đang dùng", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(46, 204, 113)),
          new SummaryCard ("Danh mục trống", "0", "warning_icon", Color.FromArgb(231, 76, 60)),
          new SummaryCard ("Danh mục mới", "0", "money_icon", Color.FromArgb(155, 89, 182))
        });
      }
      else if (permissions.Role == "Manager")
      {
        this.btnAdd.Visible = true;
        this.btnAdd.Enabled = true;

        summaryCards.Add(new SummaryCard[]
        {
          new SummaryCard("Tổng danh mục", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard("Danh mục đang dùng", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(46, 204, 113)),
          new SummaryCard("Danh mục trống", "0", "warning_icon", Color.FromArgb(231, 76, 60)),
          new SummaryCard("Danh mục mới", "0", "money_icon", Color.FromArgb(155, 89, 182))
        });
      }
    }
    protected void InitializeBUS()
    {
      bus = new DanhMucBUS(this.connStr);
    }
    protected void GetData()
    {
      // Map data to DTOs
      dsDanhMuc = bus.GetAllConnected();
    }
    protected void LoadData()
    {
      dgvMainList.Binding(dsDanhMuc);
    }
    private void ModifyData()
    {
      this.SuspendLayout();

      // Add columns to DataGridView
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "MADM",
        DataPropertyName = "MaDM",
        HeaderText = "Mã",
        FillWeight = 48,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "TENDM",
        DataPropertyName = "TenDM",
        HeaderText = "Tên danh mục",
        FillWeight = 240,
      });

      // Attach event handler for cell formatting
      dgvMainList.dgvList.CellFormatting += dgvList_CellFormatting;

      this.ResumeLayout();
    }
    private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null)
      {
        if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "TRANGTHAI")
        {
          bool status = (bool)e.Value;
          if (status)
          {
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.BackColor = Color.Green;
            e.Value = "Đang kinh doanh";
          }
          else
          {
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.BackColor = Color.Red;
            e.Value = "Ngừng kinh doanh";
          }
        }
      }
    }

    private void BtnAdd_Click(object sender, EventArgs e)
    {
      CategoryDetailFormGUI DetailForm = new CategoryDetailFormGUI(permissions, bus);
      OverlayFormGUI Overlay = new OverlayFormGUI(Form.ActiveForm, DetailForm);

      Overlay.Show(Form.ActiveForm);
      DetailForm.Show(Form.ActiveForm);

      DetailForm.AddSubmit += DetailsForm_AddSubmit;
    }
    private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
      {
        DataGridView dgvMainList = (DataGridView)sender;

        if (dgvMainList.SelectedRows.Count > 0)
        {
          DataGridViewRow selectedRow = dgvMainList.SelectedRows[0];
          DanhMucDTO danhMuc = dsDanhMuc.Find(sp => sp.MaDM == (int)selectedRow.Cells[0].Value);

          CategoryDetailFormGUI DetailForm = new CategoryDetailFormGUI(permissions, bus, danhMuc);
          OverlayFormGUI Overlay = new OverlayFormGUI(Form.ActiveForm, DetailForm);

          Overlay.Show(Form.ActiveForm);
          DetailForm.Show(Form.ActiveForm);

          // Assign event handler for submits
          DetailForm.AddSubmit += DetailsForm_AddSubmit;
          DetailForm.EditSubmit += DetailsForm_EditSubmit;
          DetailForm.DeleteSubmit += DetailsForm_DeleteSubmit;
        }
      }
    }
    private void DetailsForm_AddSubmit(object sender, DetailFormAddSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when new categories are added
      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng danh mục", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(52, 152, 219)),
        new SummaryCard("Danh mục đang dùng", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Danh mục trống", "0", "warning_icon", Color.FromArgb(231, 76, 60)),
        new SummaryCard("Danh mục mới", "0", "money_icon", Color.FromArgb(155, 89, 182))
      });
    }
    private void DetailsForm_EditSubmit(object sender, DetailFormEditSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when categories are edited
      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng danh mục", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(52, 152, 219)),
        new SummaryCard("Danh mục đang dùng", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Danh mục trống", "0", "warning_icon", Color.FromArgb(231, 76, 60)),
        new SummaryCard("Danh mục mới", "0", "money_icon", Color.FromArgb(155, 89, 182))
      });
    }
    private void DetailsForm_DeleteSubmit(object sender, DetailFormDeleteSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when categories are edited
      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng danh mục", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(52, 152, 219)),
        new SummaryCard("Danh mục đang dùng", dsDanhMuc.Count.ToString(), "category_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Danh mục trống", "0", "warning_icon", Color.FromArgb(231, 76, 60)),
        new SummaryCard("Danh mục mới", "0", "money_icon", Color.FromArgb(155, 89, 182))
      });
    }
  }
}

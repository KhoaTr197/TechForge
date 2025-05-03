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
  public partial class CustomerManagePageGUI : ManagePage
  {
    private DataSet ds;
    private List<HoiVienDTO> dsHoiVien { get; set; }
    private HoiVienBUS bus { get; set; }
    private RolePermissions permissions;

    public CustomerManagePageGUI(string role)
    {
      InitializeComponent();

      // Initialize permissions
      permissions = RolePermissions.GetPermissions(role);

      InitializeBUS();
      GetData();
      AddColumns();
      LoadData();
      SetUpFeature();

      //// Attach event handler for cell click
      dgvMainList.dgvList.CellClick += dgvList_CellClick;

      btnAdd.Click += BtnAdd_Click;
    }
    private void SetUpFeature()
    {
      if (permissions.Role == "Cashier")
      {
        summaryCards.Add(new SummaryCard[] {
          new SummaryCard("Số khách hàng", dsHoiVien.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard("Số khách hàng hoạt động gần đây", dsHoiVien.Where(hv => hv.TrangThai).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
        });
      }
      else if (permissions.Role == "WarehouseStaff")
      {
        this.btnAdd.Visible = true;
        this.btnAdd.Enabled = true;
        summaryCards.Add(new SummaryCard[]
        {
          new SummaryCard("Số khách hàng", dsHoiVien.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard("Số khách hàng hoạt động gần đây", dsHoiVien.Where(hv => hv.TrangThai).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
        });
      }
      else if (permissions.Role == "Manager")
      {
        this.btnAdd.Visible = true;
        this.btnAdd.Enabled = true;
        summaryCards.Add(new SummaryCard[]
        {
          new SummaryCard("Số khách hàng", dsHoiVien.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard("Số khách hàng hoạt động gần đây", dsHoiVien.Where(hv => hv.TrangThai).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
        });
      }
    }
    protected void InitializeBUS()
    {
      bus = new HoiVienBUS(this.connStr);
    }
    protected void GetData()
    {
      ds = new DataSet();

      // Map data to DTOs
      dsHoiVien = bus.GetAllConnected();
    }
    protected void LoadData()
    {
      dgvMainList.Binding(dsHoiVien);
    }
    private void AddColumns()
    {
      this.SuspendLayout();

      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "MaHV",
        HeaderText = "Mã",
        DataPropertyName = "MaHV",
        FillWeight = 32,
        Visible = true
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "HoTen",
        HeaderText = "Họ Tên",
        DataPropertyName = "HoTen",
        Visible = true
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "GioiTinh",
        HeaderText = "Giới Tính",
        DataPropertyName = "GioiTinh",
        Visible = true
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Sdt",
        HeaderText = "Số Điện Thoại",
        DataPropertyName = "Sdt",
        Visible = true
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Dchi",
        HeaderText = "Địa Chỉ",
        DataPropertyName = "Dchi",
        Visible = true
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "TrangThai",
        HeaderText = "Trạng Thái",
        DataPropertyName = "TrangThai",
        Visible = true
      });

      // Attach event handler for cell formatting
      dgvMainList.dgvList.CellFormatting += dgvList_CellFormatting;

      this.ResumeLayout();
    }
    protected void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null)
      {
        if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "GioiTinh")
        {
          bool status = (bool)e.Value;
          if (status)
          {
            e.Value = "Nam";
          }
          else
          {
            e.Value = "Nữ";
          }
        }
        if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "TrangThai")
        {
          bool status = (bool)e.Value;
          if (status)
          {
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.BackColor = Color.Green;
            e.Value = "Hoạt động";
          }
          else
          {
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.BackColor = Color.Red;
            e.Value = "Ít hoạt động";
          }
        }
      }
    }
    private void BtnAdd_Click(object sender, EventArgs e)
    {
      CustomerDetailFormGUI DetailForm = new CustomerDetailFormGUI(bus);
      OverlayFormGUI Overlay = new OverlayFormGUI(Form.ActiveForm, DetailForm);

      Overlay.Show(Form.ActiveForm);
      DetailForm.Show(Form.ActiveForm);

      DetailForm.AddSubmit += DetailsForm_AddSubmit;
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
          HoiVienDTO hoiVien = dsHoiVien.Find(hv => hv.MaHV == (int)selectedRow.Cells[0].Value);

          CustomerDetailFormGUI DetailForm = new CustomerDetailFormGUI(bus, hoiVien);
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

      summaryCards.Add(new SummaryCard[] {
          new SummaryCard("Tổng khách hàng", dsHoiVien.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard("Số khách hàng hoạt động gần đây", dsHoiVien.Where(hv => hv.TrangThai).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
        });
    }
    private void DetailsForm_EditSubmit(object sender, DetailFormEditSubmitEventArgs e)
    {
      GetData();
      LoadData();

      summaryCards.Add(new SummaryCard[] {
          new SummaryCard("Tổng khách hàng", dsHoiVien.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
          new SummaryCard("Số khách hàng hoạt động gần đây", dsHoiVien.Where(hv => hv.TrangThai).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
      });
    }
    private void DetailsForm_DeleteSubmit(object sender, DetailFormDeleteSubmitEventArgs e)
    {
      GetData();
      LoadData();

      summaryCards.Add(new SummaryCard[] {
        new SummaryCard("Tổng khách hàng", dsHoiVien.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
        new SummaryCard("Số khách hàng hoạt động gần đây", dsHoiVien.Where(hv => hv.TrangThai).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
      });
    }
  }
}

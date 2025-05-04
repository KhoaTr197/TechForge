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
using TechForgeGUI.SubPages;

namespace TechForgeGUI.SubForms
{
  public partial class UserManagePageGUI : ManagePage
  {
    private List<NguoiDungDTO> DsNguoiDung { get; set; }
    private NguoiDungBUS BUS { get; set; }
    private TaiKhoanBUS TaiKhoanBUS { get; set; }
    private RolePermissions permissions;
    public UserManagePageGUI(string role)
    {
      InitializeComponent();

      // Initialize permissions
      permissions = RolePermissions.GetPermissions(role);

      InitializeBUS();
      GetData();
      AddColumns();
      LoadData();
      SetUpFeature();

      // Attach event handler for cell click
      dgvMainList.dgvList.CellClick += dgvList_CellClick;

      btnAdd.Click += BtnAdd_Click;

      btnSearch.Click += btnSearch_Click;
    }

    private void InitializeBUS()
    {
      BUS = new NguoiDungBUS(this.connStr);
      TaiKhoanBUS = new TaiKhoanBUS(this.connStr);
    }
    private void GetData()
    {
      // Map data to DTOs
      DsNguoiDung = BUS.GetAllConnected();
    }
    private void LoadData()
    {
      dgvMainList.dgvList.AutoGenerateColumns = false;
      dgvMainList.Binding(DsNguoiDung);
    }
    private void SetUpFeature()
    {
      summaryCards.Add(new SummaryCard[] {
        new SummaryCard("Tổng người dùng", DsNguoiDung.Count.ToString(), "users_icon", Color.FromArgb(52, 152, 219)),
      });
    }
    private void AddColumns()
    {
      this.SuspendLayout();

      // Add columns to DataGridView
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "MAND",
        DataPropertyName = "MaND",
        HeaderText = "Mã ND",
        FillWeight = 48,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "HOTEN",
        DataPropertyName = "HoTen",
        HeaderText = "Họ Tên",
        FillWeight = 160,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "NGSINH",
        DataPropertyName = "NgSinh",
        HeaderText = "Ngày Sinh",
        FillWeight = 160,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "GIOITINH",
        DataPropertyName = "GioiTinh",
        HeaderText = "Giới Tính",
        FillWeight = 160,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "VAITRO",
        DataPropertyName = "VaiTro",
        HeaderText = "Vai Trò",
        FillWeight = 160,
      });
      dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "NGVAOLAM",
        DataPropertyName = "NgVaoLam",
        HeaderText = "Ngày Vào Làm",
        FillWeight = 160,
      });

      // Attach event handler for cell formatting
      dgvMainList.dgvList.CellFormatting += dgvList_CellFormatting;

      this.ResumeLayout();
    }
    private void BtnAdd_Click(object sender, EventArgs e)
    {
      UserDetailFormGUI DetailForm = new UserDetailFormGUI(permissions, BUS, TaiKhoanBUS);
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
          NguoiDungDTO nguoiDung = DsNguoiDung.Find(nd => nd.MaND == selectedRow.Cells[0].Value.ToString());
          TaiKhoanDTO taiKhoan = TaiKhoanBUS.GetCredential(nguoiDung.MaND);

          UserDetailFormGUI DetailForm = new UserDetailFormGUI(permissions, BUS, TaiKhoanBUS, nguoiDung, taiKhoan);
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
    protected void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
      if (e.Value != null)
      {
        if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "GIOITINH")
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
        if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "NGSINH" || dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "NGVAOLAM")
        {
          e.Value = ((DateTime)e.Value).ToString("dd/MM/yyyy");
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
        new SummaryCard("Tổng người dùng", DsNguoiDung.Count.ToString(), "category_icon", Color.FromArgb(52, 152, 219)),
      });
    }
    private void DetailsForm_EditSubmit(object sender, DetailFormEditSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when categories are edited
      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng người dùng", DsNguoiDung.Count.ToString(), "category_icon", Color.FromArgb(52, 152, 219)),
      });
    }
    private void DetailsForm_DeleteSubmit(object sender, DetailFormDeleteSubmitEventArgs e)
    {
      GetData();
      LoadData();

      // Update summary cards when categories are edited
      summaryCards.Update(new SummaryCard[]
      {
        new SummaryCard("Tổng người dùng", DsNguoiDung.Count.ToString(), "category_icon", Color.FromArgb(52, 152, 219)),
      });
    }
    private void btnSearch_Click(object sender, EventArgs e)
    {
      List<NguoiDungDTO> newDsNguoiDung = BUS.FindByAnyProperty(txtSearch.Text.Trim().ToLower());
      if (newDsNguoiDung.Count == 0)
      {
        MessageBox.Show("Không có kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }
      DsNguoiDung = newDsNguoiDung;

      LoadData();
    }
  }
}

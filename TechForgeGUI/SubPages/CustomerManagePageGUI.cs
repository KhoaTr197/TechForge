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

namespace TechForgeGUI.SubPages
{
  public partial class CustomerManagePageGUI : ManagePage
  {
    private DataSet ds;
    private List<HoiVienDTO> dsHoiVien { get; set; }
    private HoiVienBUS bus { get; set; }
    public CustomerManagePageGUI()
    {
      InitializeComponent();
      InitializeBUS();
      GetData();
      LoadData();
      ModifyData();

      // Attach event handler for cell click
      dgvMainList.dgvList.CellClick += dgvList_CellClick;

      btnAdd.Click += BtnAdd_Click;
    }
    sealed protected override void InitializeBUS()
    {
      bus = new HoiVienBUS(this.connStr);
    }
    protected void GetData()
    {
      ds = new DataSet();

      // Map data to DTOs
      dsHoiVien = bus.GetAllConnected();
    }
    protected override void LoadData()
    {
      dgvMainList.BindingData(dsHoiVien);
    }
    private void ModifyData()
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
            e.Value = "Đang kích hoạt";
          }
          else
          {
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.BackColor = Color.Red;
            e.Value = "Vô hiệu hóa";
          }
        }
      }
    }
    private void BtnAdd_Click(object sender, EventArgs e)
    {
      CustomerDetailFormGUI detailsForm = new CustomerDetailFormGUI(bus);

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
          HoiVienDTO hoiVien = dsHoiVien.ElementAt(e.RowIndex);

          CustomerDetailFormGUI detailsForm = new CustomerDetailFormGUI(bus, hoiVien);

          detailsForm.Show(Form.ActiveForm);

          // Assign event handler for submits
          detailsForm.EditSubmit += DetailsForm_EditSubmit;
        }
      }
    }
    private void DetailsForm_AddSubmit(object sender, DetailFormAddSubmitEventArgs e)
    {
    }
    private void DetailsForm_EditSubmit(object sender, DetailFormEditSubmitEventArgs e)
    {
      GetData();
      LoadData();
    }
  }
}

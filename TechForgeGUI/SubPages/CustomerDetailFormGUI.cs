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
  public partial class CustomerDetailFormGUI : DetailFormGUI
  {
    private HoiVienDTO ThongTinHoiVien { get; set; }
    private HoiVienBUS BUS { get; set; }
    public CustomerDetailFormGUI(HoiVienBUS _BUS, HoiVienDTO _thongTinHoiVien = null)
    {
      InitializeComponent();

      this.ThongTinHoiVien = _thongTinHoiVien;
      this.BUS = _BUS;
      this.Text = "Thêm Hội Viên";

      if (ThongTinHoiVien == null)
      {
        this.btnEdit.Visible = false;
        this.btnEdit.Enabled = false;
        this.btnDelete.Visible = false;
        this.btnDelete.Enabled = false;


        LoadAddForm();
      }
      else
      {
        this.Size = new Size(500, 432);

        this.btnAdd.Visible = false;
        this.btnAdd.Enabled = false;


        LoadDetailForm();
      }

      this.btnAdd.Click += btnAdd_Click;
      this.btnEdit.Click += btnEdit_Click;
      this.btnDelete.Click += btnDelete_Click;
    }
    private void LoadAddForm()
    {
      txtMaHV.Text = BUS.GetNextID().ToString();

      radNam.Checked = true;
      radNu.Checked = false;

      cboTrangThai.Items.Add(new string[] { "Hoạt động", "Ít hoạt động" });
      cboTrangThai.SelectedIndex = 0;
    }
    private void LoadDetailForm()
    {
      txtMaHV.Text = ThongTinHoiVien.MaHV.ToString();
      txtMaHV.Enabled = false;

      txtHoTen.Text = ThongTinHoiVien.HoTen.ToString();
      txtSdt.Text = ThongTinHoiVien.Sdt.ToString();
      txtDchi.Text = ThongTinHoiVien.Dchi.ToString();

      radNam.Checked = ThongTinHoiVien.GioiTinh ? true : false;
      radNu.Checked = ThongTinHoiVien.GioiTinh ? false : true;

      cboTrangThai.Items.Add(new string[] { "Hoạt động", "Ít hoạt động" });
      cboTrangThai.SelectedIndex = ThongTinHoiVien.TrangThai ? 0 : 1;
    }
    private void btnAdd_Click(object sender, EventArgs e)
    {
      HoiVienDTO newHoiVien = new HoiVienDTO()
      {
      };

      if (BUS.Add(newHoiVien) != -1)
        OnAddSubmit(new DetailFormAddSubmitEventArgs(this));
    }
    private void btnEdit_Click(object sender, EventArgs e)
    {
      HoiVienDTO updatedHoiVien = new HoiVienDTO()
      {
      };

      if (BUS.Update(ThongTinHoiVien, updatedHoiVien))
        OnEditSubmit(new DetailFormEditSubmitEventArgs(this));
    }
    private void btnDelete_Click(object sender, EventArgs e)
    {
      if (BUS.Delete(ThongTinHoiVien.MaHV))
        OnDeleteSubmit(new DetailFormDeleteSubmitEventArgs(this));
    }
  }
}

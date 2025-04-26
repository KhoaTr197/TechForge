using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeDAO;
using TechForgeDTO;


namespace TechForgeBUS
{
  public class SanPhamBUS
  {
    private readonly SanPhamDAO DAO;
    public SanPhamBUS(string _connStr) {
      this.DAO = new SanPhamDAO(_connStr);
    }
    public List<SanPhamDTO> GetAllConnected()
    {
      return this.DAO.GetAllConnected();
    }
    public DataSet GetAllDisconnected(DataSet ds)
    {
      return this.DAO.GetAllDisconnected(ds);
    }
    public void GetQuantity()
    {
    }
    public List<SanPhamDTO> GetList(int[] ids)
    {
      return this.DAO.GetList(ids);
    }
    public int Add(SanPhamDTO sp)
    {
      return this.DAO.Add(sp);
    }
    public bool Update(SanPhamDTO sp, SanPhamDTO spMoi)
    {
      List<string> fieldsToUpdate = new List<string>();

      if (spMoi.TenSP != sp.TenSP) fieldsToUpdate.Add("TenSP");
      if (spMoi.GiaNhap != sp.GiaNhap) fieldsToUpdate.Add("GiaNhap");
      if (spMoi.Gia != sp.Gia) fieldsToUpdate.Add("Gia");
      if (spMoi.KhuyenMai != sp.KhuyenMai) fieldsToUpdate.Add("KhuyenMai");
      if (spMoi.MoTa != sp.MoTa) fieldsToUpdate.Add("MoTa");
      if (spMoi.SoLuong != sp.SoLuong) fieldsToUpdate.Add("SoLuong");
            if (spMoi.DonViTinh != sp.DonViTinh) fieldsToUpdate.Add("DonViTinh");
            if (spMoi.HinhAnh != sp.HinhAnh) fieldsToUpdate.Add("HinhAnh");
      if (spMoi.DanhMuc != sp.DanhMuc) fieldsToUpdate.Add("DanhMuc");
      if (spMoi.Hsx != sp.Hsx) fieldsToUpdate.Add("Hsx");
      if (spMoi.Ncc != sp.Ncc) fieldsToUpdate.Add("Ncc");
      if (spMoi.NgSx != sp.NgSx) fieldsToUpdate.Add("NgSx");
      if (spMoi.TrangThai != sp.TrangThai) fieldsToUpdate.Add("TrangThai");

      if (fieldsToUpdate.Count == 0)
      {
        MessageBox.Show("Không có thay đổi nào để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return false;
      }

      if (this.DAO.Update(spMoi)) {
        return true;
      } else
      {
        MessageBox.Show("Cập nhật thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }
    public bool Delete(int id)
    {
      return this.DAO.Delete(id);
    }
  }
}

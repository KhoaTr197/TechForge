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
  public class HoiVienBUS
  {
    private readonly HoiVienDAO DAO;
    public HoiVienBUS(string _connStr)
    {
      this.DAO = new HoiVienDAO(_connStr);
    }
    public List<HoiVienDTO> GetAllConnected()
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
    //public DataSet GetSync()
    //{
    //  return this.DAO.GetSync();
    //}
    public int Add(HoiVienDTO hv)
    {
      if (String.IsNullOrEmpty(hv.HoTen) || String.IsNullOrEmpty(hv.Sdt) || String.IsNullOrEmpty(hv.Dchi))
      {
        MessageBox.Show("Vui lòng nhập đầy đủ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return -1;
      } else if (hv.Sdt.Length != 10 || hv.Sdt.Any(Char.IsLetter))
      {
        MessageBox.Show("Số điện thoại không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return -1;
      }

      int id = this.DAO.Add(hv);

      if (id == -1)
      {
        MessageBox.Show("Thêm thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return -1;
      }
      else
      {
        MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return id;
      }
    }
    public bool Update(HoiVienDTO hv, HoiVienDTO hvMoi)
    {
      List<string> fieldsToUpdate = new List<string>();

      if (hvMoi.HoTen != hv.HoTen) fieldsToUpdate.Add("HoTen");
      if (hvMoi.Sdt != hv.Sdt) fieldsToUpdate.Add("Sdt");
      if (hvMoi.GioiTinh != hv.GioiTinh) fieldsToUpdate.Add("GioiTinh");
      if (hvMoi.Dchi != hv.Dchi) fieldsToUpdate.Add("Dchi");
      if (hvMoi.TrangThai != hv.TrangThai) fieldsToUpdate.Add("TrangThai");

      if (fieldsToUpdate.Count == 0)
      {
        MessageBox.Show("Không có thay đổi nào để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return false;
      }

      if (this.DAO.Update(hvMoi))
      {
        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return true;
      }
      else
      {
        MessageBox.Show("Cập nhật thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }
    public bool Delete(int id)
    {
      return this.DAO.Delete(id);
    }

    public int GetNextID()
    {
      return this.DAO.GetNextId();
    }

    public List<HoiVienDTO> FindByIdOrName(string searchText)
    {
      return this.DAO.FindByIdOrName(searchText);
    }

        public List<HoiVienDTO> FindByAnyProperty(string searchText)
        {
            return this.DAO.FindByAnyProperty(searchText);
        }
  }
}

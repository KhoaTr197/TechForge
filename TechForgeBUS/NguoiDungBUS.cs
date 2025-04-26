using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeDAO;
using TechForgeDTO;

namespace TechForgeBUS
{
  public class NguoiDungBUS
  {
    private readonly NguoiDungDAO DAO;
    public NguoiDungBUS(string _connStr)
    {
      this.DAO = new NguoiDungDAO(_connStr);
    }
    public List<NguoiDungDTO> GetAllConnected()
    {
      return this.DAO.GetAllConnected();
    }
    public NguoiDungDTO GetByID(string id)
    {
        return this.DAO.GetById(id);
    }
    public List<string> GetAllRoles() {
      return this.DAO.GetAllRoles();
    }

    public int Add(NguoiDungDTO newNguoiDung)
    {
      return this.DAO.Add(newNguoiDung);
    }

    public bool Update(NguoiDungDTO thongTinNguoiDung, NguoiDungDTO updatedNguoiDung)
    {
      List<string> fieldsToUpdate = new List<string>();

      if (updatedNguoiDung.HoTen != thongTinNguoiDung.HoTen) fieldsToUpdate.Add("HoTen");
      if (updatedNguoiDung.NgSinh != thongTinNguoiDung.NgSinh) fieldsToUpdate.Add("NgSinh");
      if (updatedNguoiDung.GioiTinh != thongTinNguoiDung.GioiTinh) fieldsToUpdate.Add("GioiTinh");
      if (updatedNguoiDung.Cccd != thongTinNguoiDung.Cccd) fieldsToUpdate.Add("Cccd");
      if (updatedNguoiDung.Sdt != thongTinNguoiDung.Sdt) fieldsToUpdate.Add("Sdt");
      if (updatedNguoiDung.Dchi != thongTinNguoiDung.Dchi) fieldsToUpdate.Add("Dchi");
      if (updatedNguoiDung.VaiTro != thongTinNguoiDung.VaiTro) fieldsToUpdate.Add("VaiTro");
      if (updatedNguoiDung.NgVaoLam != thongTinNguoiDung.NgVaoLam) fieldsToUpdate.Add("NgVaoLam");

      if (fieldsToUpdate.Count == 0)
      {
        MessageBox.Show("Không có thay đổi nào để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return false;
      }

      if (this.DAO.Update(updatedNguoiDung))
      {
        return true;
      }
      else
      {
        MessageBox.Show("Cập nhật thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }
    public string GetNextId(string vaiTro)
    {
      Dictionary<string, string> dict = new Dictionary<string, string>
      {
        { "Thu Ngân", "TNG" },
        { "Quản Lý Kho", "KHO" },
        { "ADMIN", "ADM" },
      };

      string role;

      dict.TryGetValue(vaiTro, out role);

      return this.DAO.GetNextId(role);
    }
  }
}

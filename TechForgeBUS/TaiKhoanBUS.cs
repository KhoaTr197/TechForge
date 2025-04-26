using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechForgeDTO;
using TechForgeDAO;
using System.Windows.Forms;

namespace TechForgeBUS
{
  public class TaiKhoanBUS
  {
    private readonly TaiKhoanDAO DAO;

    public TaiKhoanBUS(string _connStr)
    {
      this.DAO = new TaiKhoanDAO(_connStr);
    }

    public TaiKhoanDTO GetCredential(string id)
    {
      return this.DAO.GetCredential(id);
    }

    public TaiKhoanDTO Login(string username, string password)
    {
      return DAO.Login(username, password);
    }
    public bool Update(TaiKhoanDTO tk, TaiKhoanDTO newTk)
    {
      List<string> fieldsToUpdate = new List<string>();

      if (tk.TenTK != newTk.TenTK) fieldsToUpdate.Add("TenTK");
      if (tk.MatKhau != newTk.MatKhau) fieldsToUpdate.Add("MatKhau");

      if (fieldsToUpdate.Count == 0)
      {
        MessageBox.Show("Không có thay đổi nào để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return false;
      }

      if (this.DAO.Update(newTk))
      {
        return true;
      }
      else
      {
        MessageBox.Show("Cập nhật thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
      }
    }
    public bool Active(string id)
    {
      return this.DAO.Active(id);
    }
    public bool Deactive(string id)
    {
      return this.DAO.Deactive(id);
    }

    public List<TaiKhoanDTO> GetAllConnected()
    {
      return this.DAO.GetAllConnected();
    }

    public bool Add(TaiKhoanDTO newTaiKhoan)
    {
      return this.DAO.Add(newTaiKhoan);
    }
  }
}
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
  public class NhaCungCapBUS
  {
    private readonly NhaCungCapDAO DAO;
    public NhaCungCapBUS(string _connStr)
    {
      this.DAO = new NhaCungCapDAO(_connStr);
    }
    public DataSet GetAllDisconnected(DataSet ds)
    {
        return this.DAO.GetAllDisconnected(ds);
    }
    public List<NhaCungCapDTO> GetAllConnected()
    {
      return this.DAO.GetAllConnected();
    }
    public int Add(NhaCungCapDTO ncc)
    {
      return this.DAO.Add(ncc);
    }
    public bool Update(NhaCungCapDTO ncc, NhaCungCapDTO nccMoi)
    {
      List<string> fieldsToUpdate = new List<string>();

      if (nccMoi.TenNCC != ncc.TenNCC) fieldsToUpdate.Add("TenNCC");
      if (nccMoi.Ndd != ncc.Ndd) fieldsToUpdate.Add("Ndd");
      if (nccMoi.Sdt != ncc.Sdt) fieldsToUpdate.Add("Sdt");
      if (nccMoi.Email != ncc.Email) fieldsToUpdate.Add("Email");
      if (nccMoi.TrangThai != ncc.TrangThai) fieldsToUpdate.Add("TrangThai");

      if (fieldsToUpdate.Count == 0)
      {
        MessageBox.Show("Không có thay đổi nào để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return false;
      }

      if (this.DAO.Update(nccMoi))
      {
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

    public int GetNextId()
    {
      return this.DAO.GetNextId();
    }
    public bool Active(int id)
    {
      return this.DAO.Active(id);
    }
    public bool Deactive(int id)
    {
      return this.DAO.Deactive(id);
    }
  }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechForgeDTO;

namespace TechForgeDAO
{
  public class NguoiDungDAO : BaseDAO
  {
    public NguoiDungDAO(string _connStr) : base(_connStr)
    {
    }
    public List<NguoiDungDTO> GetAllConnected()
    {
      try
      {
        List<NguoiDungDTO> users = new List<NguoiDungDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOIDUNG", conn);

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              users.Add(new NguoiDungDTO()
              {
                MaND = reader.GetString(0),
                HoTen = reader.GetString(1),
                NgSinh = reader.GetDateTime(2),
                GioiTinh = reader.GetBoolean(3),
                Cccd = reader.GetString(4),
                Sdt = reader.GetString(5),
                Dchi = reader.GetString(6),
                VaiTro = reader.GetString(7),
                NgVaoLam = reader.GetDateTime(8)
              });
            }
          }
        }

        return users;
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
  }
}

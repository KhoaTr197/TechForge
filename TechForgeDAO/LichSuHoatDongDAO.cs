using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechForgeDTO;

namespace TechForgeDAO
{
  public class LichSuHoatDongDAO : BaseDAO
  {
    public LichSuHoatDongDAO(string _connStr) : base(_connStr)
    {
    }
    public List<LichSuHoatDongDTO> GetRecentAllConnected(string MaND="") {
      try
      {
        List<LichSuHoatDongDTO> entries = new List<LichSuHoatDongDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd;

          if (MaND != "")
          {
            cmd = new SqlCommand("SELECT * FROM LSHD WHERE MAND = @MAND AND THOIGIAN >= DATEADD(week, -1, GETDATE())", conn);
            cmd.Parameters.AddWithValue("@MAND", MaND);
          } else
          {
            cmd = new SqlCommand("SELECT * FROM LSHD ORDER BY MAND ASC", conn);
          }

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read()) {
              entries.Add(new LichSuHoatDongDTO()
              {
                MaLSHD = reader.GetInt32(0),
                MaND = reader.GetString(1),
                ThoiGian = reader.GetDateTime(2),
                NoiDung = reader.GetString(3),
                VaiTro = reader.GetString(4),
              });
            }
          }
        }

        return entries;
      }
      catch (Exception ex) {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public int Add(LichSuHoatDongDTO newEntry)
    {
      if (newEntry == null) {
        throw new ArgumentNullException("Log entry cannot be null");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();

          SqlCommand cmd = new SqlCommand("INSERT INTO LSHD (MAND, THOIGIAN, NOIDUNG, VAITRO) VALUES (@MAND, @THOIGIAN, @NOIDUNG, @VAITRO)", conn);
          cmd.Parameters.AddWithValue("@MAND", newEntry.MaND);
          cmd.Parameters.AddWithValue("@THOIGIAN", newEntry.ThoiGian);
          cmd.Parameters.AddWithValue("@NOIDUNG", newEntry.NoiDung);
          cmd.Parameters.AddWithValue("@VAITRO", newEntry.VaiTro);

          int newId = Convert.ToInt32(cmd.ExecuteScalar());
          newEntry.MaLSHD = newId;
          return newId;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while adding data from the database.", ex);
      }
    }
  }
}

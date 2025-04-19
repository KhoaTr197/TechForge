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
  public class LichSuKhoDAO : BaseDAO
  {
    public LichSuKhoDAO(string _connStr) : base(_connStr)
    {
    }
    public List<LichSuKhoDTO> GetAllConnected()
    {
      try
      {
        List<LichSuKhoDTO> receipts = new List<LichSuKhoDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM LSKHO", conn);

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              receipts.Add(new LichSuKhoDTO()
              {
                MaLS = reader.GetInt32(0),
                TongTien = reader.GetDecimal(1),
                ThoiGian = reader.GetDateTime(2),
                MaND = reader.GetString(3),
                HoatDong = reader.GetBoolean(4)
              });
            }
          }
        }

        return receipts;
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public List<ChiTietLichSuKhoDTO> GetDetail(int id)
    {
      try
      {
        List<ChiTietLichSuKhoDTO> logDetails = new List<ChiTietLichSuKhoDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT CTLSKHO.*, SANPHAM.TENSP, SANPHAM.HINHANH FROM CTLSKHO INNER JOIN SANPHAM on CTLSKHO.MASP = SANPHAM.MASP WHERE MALS = @MALS", conn);
          cmd.Parameters.AddWithValue("@MALS", id);

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              logDetails.Add(new ChiTietLichSuKhoDTO()
              {
                MaLS = reader.GetInt32(0),
                MaSP = reader.GetInt32(1),
                Gia = reader.IsDBNull(2) ? (decimal?)null : reader.GetDecimal(2),
                HoatDong = reader.GetBoolean(3),
                SoLuong = reader.GetInt32(4),
                ThanhTien = reader.IsDBNull(5) ? (decimal?)null : reader.GetDecimal(5),
                TenSP = reader.GetString(6),
                HinhAnh = reader.GetString(7),
              });
            }
          }
        }

        return logDetails;
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public int Add(LichSuKhoDTO newLog)
    {
      if (newLog == null)
      {
        throw new ArgumentNullException("Log entry cannot be null.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("INSERT INTO LSKHO (TONGTIEN, THOIGIAN, MAND, HOATDONG) VALUES (@TONGTIEN, @THOIGIAN, @MAND, @HOATDONG)", conn);
          cmd.Parameters.AddWithValue("@TONGTIEN", newLog.TongTien);
          cmd.Parameters.AddWithValue("@THOIGIAN", newLog.ThoiGian);
          cmd.Parameters.AddWithValue("@MAND", newLog.MaND);
          cmd.Parameters.AddWithValue("@HOATDONG", newLog.HoatDong);

          int newId = Convert.ToInt32(cmd.ExecuteScalar());
          newLog.MaLS = newId;
          return newId;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while adding data to the database.", ex);
      }
    }
    public bool Update(LichSuKhoDTO updatedLog)
    {
      if (updatedLog == null)
      {
        throw new ArgumentNullException("Log entry cannot be null.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE LSKHO SET TONGTIEN = @TONGTIEN, THOIGIAN = @THOIGIAN, MAND = @MAND, HOATDONG = @HOATDONG WHERE MALS = @MALS", conn);
          cmd.Parameters.AddWithValue("@MALS", updatedLog.MaLS);
          cmd.Parameters.AddWithValue("@TONGTIEN", updatedLog.TongTien);
          cmd.Parameters.AddWithValue("@THOIGIAN", updatedLog.ThoiGian);
          cmd.Parameters.AddWithValue("@MAND", updatedLog.MaND);
          cmd.Parameters.AddWithValue("@HOATDONG", updatedLog.HoatDong);

          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while updating data in the database.", ex);
      }
    }
    public bool Delete(int id)
    {
      if (id <= 0)
      {
        throw new ArgumentException("Log ID must be a positive value.", nameof(id));
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("DELETE FROM LSKHO WHERE MALS = @MALS", conn);
          cmd.Parameters.AddWithValue("@MALS", id);

          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while deleting data from the database.", ex);
      }
    }

    public int GetNextId()
    {
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('LSKHO') + 1", conn);
          object result = cmd.ExecuteScalar();
          if (result != null && result != DBNull.Value)
          {
            return Convert.ToInt32(result);
          }
          return 0;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting the next ID from the database.", ex);
      }
    }
  }
}

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
        List<LichSuKhoDTO> logs = new List<LichSuKhoDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM LSKHO", conn);

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              logs.Add(new LichSuKhoDTO()
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

        return logs;
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
          // Start a transaction (ensure that both interactions need to be succeed in applying)
          using (SqlTransaction transaction = conn.BeginTransaction())
          {
            try
            {
              // Insert into LSKHO and get the new ID
              using (SqlCommand cmd = new SqlCommand(
                  "INSERT INTO LSKHO (TONGTIEN, THOIGIAN, MAND, HOATDONG) " +
                  "VALUES (@TONGTIEN, @THOIGIAN, @MAND, @HOATDONG); SELECT SCOPE_IDENTITY();", conn, transaction))
              {
                cmd.Parameters.AddWithValue("@TONGTIEN", newLog.TongTien);
                cmd.Parameters.AddWithValue("@THOIGIAN", newLog.ThoiGian);
                cmd.Parameters.AddWithValue("@MAND", newLog.MaND);
                cmd.Parameters.AddWithValue("@HOATDONG", newLog.HoatDong);

                int newId = Convert.ToInt32(cmd.ExecuteScalar());
                newLog.MaLS = newId;

                // Insert into CTLSKHO
                foreach (var item in newLog.Ctlsk)
                {
                  using (SqlCommand cmdDetail = new SqlCommand(
                      "INSERT INTO CTLSKHO (MALS, MASP, GIA, HOATDONG, SL, THANHTIEN) " +
                      "VALUES (@MALS, @MASP, @GIA, @HOATDONG, @SL, @THANHTIEN)", conn, transaction))
                  {
                    cmdDetail.Parameters.AddWithValue("@MALS", newId);
                    cmdDetail.Parameters.AddWithValue("@MASP", item.MaSP);
                    cmdDetail.Parameters.AddWithValue("@GIA", item.Gia);
                    cmdDetail.Parameters.AddWithValue("@HOATDONG", item.HoatDong);
                    cmdDetail.Parameters.AddWithValue("@SL", item.SoLuong);
                    cmdDetail.Parameters.AddWithValue("@THANHTIEN", item.ThanhTien);

                    cmdDetail.ExecuteNonQuery();
                  }
                  // Update the product quantity in the SANPHAM table
                  using (SqlCommand cmdUpdate = new SqlCommand(
                      $"UPDATE SANPHAM SET SL = SL {(newLog.HoatDong ? "-" : "+")} @SL WHERE MASP = @MASP", conn, transaction))
                  {
                    cmdUpdate.Parameters.AddWithValue("@SL", item.SoLuong);
                    cmdUpdate.Parameters.AddWithValue("@MASP", item.MaSP);
                    cmdUpdate.ExecuteNonQuery();
                  }
                }

                transaction.Commit();
                return newId;
              }
            }
            catch (Exception ex)
            {
              transaction.Rollback();
              throw new DataException("Failed to insert log entry and details into the database.", ex);
            }
          }
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
          using (SqlTransaction transaction = conn.BeginTransaction())
          {
            try
            {
              // Update LSKHO
              using (SqlCommand cmd = new SqlCommand(
                  "UPDATE LSKHO SET TONGTIEN = @TONGTIEN, THOIGIAN = @THOIGIAN, MAND = @MAND, HOATDONG = @HOATDONG " +
                  "WHERE MALS = @MALS", conn, transaction))
              {
                cmd.Parameters.AddWithValue("@TONGTIEN", updatedLog.TongTien);
                cmd.Parameters.AddWithValue("@THOIGIAN", updatedLog.ThoiGian);
                cmd.Parameters.AddWithValue("@MAND", updatedLog.MaND);
                cmd.Parameters.AddWithValue("@HOATDONG", updatedLog.HoatDong);
                cmd.Parameters.AddWithValue("@MALS", updatedLog.MaLS);

                cmd.ExecuteNonQuery();
              }

              // Delete existing CTLSKHO records for this MALS
              using (SqlCommand cmdDelete = new SqlCommand(
                  "DELETE FROM CTLSKHO WHERE MALS = @MALS", conn, transaction))
              {
                cmdDelete.Parameters.AddWithValue("@MALS", updatedLog.MaLS);
                cmdDelete.ExecuteNonQuery();
              }

              // Insert updated CTLSKHO records
              foreach (var item in updatedLog.Ctlsk)
              {
                using (SqlCommand cmdDetail = new SqlCommand(
                    "INSERT INTO CTLSKHO (MALS, MASP, GIA, HOATDONG, SL, THANHTIEN) " +
                    "VALUES (@MALS, @MASP, @GIA, @HOATDONG, @SL, @THANHTIEN)", conn, transaction))
                {
                  cmdDetail.Parameters.AddWithValue("@MALS", updatedLog.MaLS);
                  cmdDetail.Parameters.AddWithValue("@MASP", item.MaSP);
                  cmdDetail.Parameters.AddWithValue("@GIA", item.Gia);
                  cmdDetail.Parameters.AddWithValue("@HOATDONG", item.HoatDong);
                  cmdDetail.Parameters.AddWithValue("@SL", item.SoLuong);
                  cmdDetail.Parameters.AddWithValue("@THANHTIEN", item.ThanhTien);

                  cmdDetail.ExecuteNonQuery();
                }
              }

              transaction.Commit();
              return true;
            }
            catch (Exception)
            {
              transaction.Rollback();
              throw;
            }
          }
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
    public List<LichSuKhoDTO> FindByAnyProperty(string searchText)
    {
      try
      {
        List<LichSuKhoDTO> logs = new List<LichSuKhoDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          string query = @"
            SELECT * FROM LSKHO
            WHERE MALS LIKE @SEARCH_TEXT OR
                  TONGTIEN LIKE @SEARCH_TEXT OR
                  THOIGIAN LIKE @SEARCH_TEXT OR
                  MAND LIKE @SEARCH_TEXT OR
                  HOATDONG LIKE @SEARCH_TEXT
          ";

          using (SqlCommand cmd = new SqlCommand(query, conn))
          {
            cmd.Parameters.AddWithValue("@SEARCH_TEXT", $"%{searchText}%");

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
              while (reader.Read())
              {
                logs.Add(new LichSuKhoDTO
                {
                  MaLS = reader.GetInt32(0),
                  TongTien = reader.GetDecimal(1),
                  ThoiGian = reader.GetDateTime(2),
                  MaND = reader.GetString(3),
                  HoatDong = reader.GetBoolean(4),
                  Ctlsk = GetDetail(reader.GetInt32(0))
                });
              }
            }
          }
        }

        return logs;
      }
      catch (SqlException ex)
      {
        throw new DataException("Database error occurred while searching for logs.", ex);
      }
      catch (Exception ex)
      {
        throw new DataException("An unexpected error occurred while searching for logs.", ex);
      }
    }
  }
}

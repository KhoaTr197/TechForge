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
  public class HoaDonDAO : BaseDAO
  {
    public HoaDonDAO(string _connStr) : base(_connStr)
    {
    }
    public List<HoaDonDTO> GetAllConnected()
    {
      try
      {
        List<HoaDonDTO> receipts = new List<HoaDonDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM HOADON", conn);

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              receipts.Add(new HoaDonDTO()
              {
                MaHD = reader.GetInt32(0),
                MaHV = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1),
                HoTen = reader.GetString(2),
                Sdt = reader.GetString(3),
                DiaChi = reader.GetString(4),
                NvLapHD = reader.GetString(5),
                TongTien = reader.GetDecimal(6),
                NgLapHD = reader.GetDateTime(7)
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
    public List<ChiTietHoaDonDTO> GetDetail(int id)
    {
      try
      {
        List<ChiTietHoaDonDTO> receiptDetails = new List<ChiTietHoaDonDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM CTHD WHERE MAHD = @MAHD", conn);
          cmd.Parameters.AddWithValue("@MAHD", id);

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              receiptDetails.Add(new ChiTietHoaDonDTO()
              {
                MaHD = reader.GetInt32(0),
                MaSP = reader.GetInt32(1),
                Gia = reader.GetDecimal(2),
                SoTienKm = reader.GetDecimal(3),
                GiaCuoiCung = reader.GetDecimal(4),
                SoLuong = reader.GetInt32(5),
                ThanhTien = reader.GetDecimal(6)
              });
            }
          }
        }

        return receiptDetails;
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public int Add(HoaDonDTO newReceipt)
    {
      if (newReceipt == null)
      {
        throw new ArgumentNullException("Receipt cannot be null.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("INSERT INTO HOADON (MAHV, HOTEN, SDT, DCHI, NvLapHD, TONGTIEN, NGLAPHD) VALUES (@MAHV, @HOTEN, @SDT, @DCHI, @NvLapHD, @TONGTIEN, @NGLAPHD)", conn);
          cmd.Parameters.AddWithValue("@MAHV", newReceipt.MaHV);
          cmd.Parameters.AddWithValue("@HOTEN", newReceipt.HoTen);
          cmd.Parameters.AddWithValue("@SDT", newReceipt.Sdt);
          cmd.Parameters.AddWithValue("@DCHI", newReceipt.DiaChi);
          cmd.Parameters.AddWithValue("@NvLapHD", newReceipt.NgLapHD);
          cmd.Parameters.AddWithValue("@TONGTIEN", newReceipt.TongTien);

          int newId = Convert.ToInt32(cmd.ExecuteScalar());
          newReceipt.MaHD = newId;
          return newId;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while adding data to the database.", ex);
      }
    }
    public bool Update(HoaDonDTO updatedReceipt)
    {
      if (updatedReceipt == null)
      {
        throw new ArgumentNullException("Receipt cannot be null.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE HANGSANXUAT SET MAHV = @MAHV, HOTEN = @HOTEN, SDT = @SDT, DCHI = @DCHI, NvLapHD = @NvLapHD, TONGTIEN = @TONGTIEN, NGLAPHD = @NGLAPHD WHERE MAHD = @MAHD", conn);
          cmd.Parameters.AddWithValue("@MAHV", updatedReceipt.MaHV);
          cmd.Parameters.AddWithValue("@HOTEN", updatedReceipt.HoTen);
          cmd.Parameters.AddWithValue("@SDT", updatedReceipt.Sdt);
          cmd.Parameters.AddWithValue("@DCHI", updatedReceipt.DiaChi);
          cmd.Parameters.AddWithValue("@NvLapHD", updatedReceipt.NgLapHD);
          cmd.Parameters.AddWithValue("@TONGTIEN", updatedReceipt.TongTien);

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
        throw new ArgumentException("Receipt ID must be a positive value.", nameof(id));
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("DELETE FROM HOADON WHERE MAHD = @MAHD", conn);
          cmd.Parameters.AddWithValue("@MAHD", id);

          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while deleting data from the database.", ex);
      }
    }
    public List<ChiTietHoaDonDTO> GetDetailWithProducts(int id)
    {
      try
      {
        List<ChiTietHoaDonDTO> receiptDetails = new List<ChiTietHoaDonDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          string query = @"SELECT CTHD.*, SP.TENSP, SP.HINHANH, SP.KHUYENMAI 
                          FROM CTHD 
                          INNER JOIN SANPHAM SP ON SP.MASP = CTHD.MASP 
                          WHERE CTHD.MAHD = @MAHD";
          SqlCommand cmd = new SqlCommand(query, conn);
          cmd.Parameters.AddWithValue("@MAHD", id);

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              receiptDetails.Add(new ChiTietHoaDonDTO()
              {
                MaHD = reader.GetInt32(0),
                MaSP = reader.GetInt32(1),
                Gia = reader.GetDecimal(2),
                SoTienKm = reader.GetDecimal(3),
                GiaCuoiCung = reader.GetDecimal(4),
                SoLuong = reader.GetInt32(5),
                ThanhTien = reader.GetDecimal(6),
                TenSP = reader.GetString(7),
                HinhAnh = reader.IsDBNull(8) ? "" : reader.GetString(8),
                KhuyenMai = reader.GetDecimal(9)
              });
            }
          }
        }

        return receiptDetails;
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting receipt details with products from the database.", ex);
      }
    }
  }
}

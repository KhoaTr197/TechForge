using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechForgeDTO;

namespace TechForgeDAO
{
  public class HoiVienDAO : BaseDAO
  {
    public HoiVienDAO(string _connStr) : base(_connStr) { }
    public List<HoiVienDTO> GetAllConnected()
    {
      try
      {
        List<HoiVienDTO> customers = new List<HoiVienDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM HOIVIEN ORDER BY TRANGTHAI DESC", conn);
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              customers.Add(new HoiVienDTO()
              {
                MaHV = reader.GetInt32(0),
                HoTen = reader.GetString(1),
                GioiTinh = reader.GetBoolean(2),
                Sdt = reader.GetString(3),
                Dchi = reader.GetString(4),
                TrangThai = reader.GetBoolean(5)
              });
            }
          }
        }
        return customers;
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public DataSet GetAllDisconnected(DataSet ds)
    {
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM HOIVIEN", conn);
          SqlDataAdapter adapter = new SqlDataAdapter(cmd);
          adapter.Fill(ds, "HOIVIEN");
        }
        return ds;
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public int Add(HoiVienDTO newCustomer)
    {
      if (newCustomer == null)
      {
        throw new ArgumentNullException("Customer cannot be null.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("INSERT INTO HOIVIEN(HOTEN, GIOITINH, SDT, DCHI, TRANGTHAI) VALUES(@HOTEN, @GIOITINH, @SDT, @DCHI, @TRANGTHAI)", conn);
          cmd.Parameters.AddWithValue("@HOTEN", newCustomer.HoTen);
          cmd.Parameters.AddWithValue("@GIOITINH", newCustomer.GioiTinh);
          cmd.Parameters.AddWithValue("@SDT", newCustomer.Sdt);
          cmd.Parameters.AddWithValue("@DCHI", newCustomer.Dchi);
          cmd.Parameters.AddWithValue("@TRANGTHAI", newCustomer.TrangThai);

          int newId = Convert.ToInt32(cmd.ExecuteScalar());
          newCustomer.MaHV = newId;
          return newId;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while adding data to the database.", ex);
      }
    }
    public bool Update(HoiVienDTO updatedCustomer)
    {
      if (updatedCustomer == null)
      {
        throw new ArgumentNullException("Customer cannot be null.");
      }
      if (updatedCustomer.MaHV <= 0)
      {
        throw new ArgumentNullException("Customer ID must be a positive value.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE HOIVIEN SET HOTEN = @HOTEN, GIOITINH = @GIOITINH, SDT = @SDT, DCHI = @DCHI, TRANGTHAI = @TRANGTHAI WHERE MAHV = @MAHV", conn);
          cmd.Parameters.AddWithValue("@MAHV", updatedCustomer.MaHV);
          cmd.Parameters.AddWithValue("@HOTEN", updatedCustomer.HoTen);
          cmd.Parameters.AddWithValue("@GIOITINH", updatedCustomer.GioiTinh);
          cmd.Parameters.AddWithValue("@SDT", updatedCustomer.Sdt);
          cmd.Parameters.AddWithValue("@DCHI", updatedCustomer.Dchi);
          cmd.Parameters.AddWithValue("@TRANGTHAI", updatedCustomer.TrangThai);

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
        throw new ArgumentException("Customer ID must be a positive value.", nameof(id));
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE HOIVIEN SET TRANGTHAI = 0 WHERE MAHV = @MAHV", conn);
          cmd.Parameters.AddWithValue("@MAHV", id);

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
          SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('HOIVIEN') + 1", conn);
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

    public List<HoiVienDTO> FindByIdOrName(string searchText)
    {
      try
      {
        List<HoiVienDTO> customers = new List<HoiVienDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand($"SELECT * FROM HOIVIEN WHERE MAHV = @MAHV OR HOTEN LIKE @NAME", conn);
          if (int.TryParse(searchText, out int mahv))
          {
            cmd.Parameters.AddWithValue("@MAHV", mahv);
          }
          else
          {
            cmd.Parameters.AddWithValue("@MAHV", DBNull.Value);
          }
          cmd.Parameters.AddWithValue("@NAME", $"%{searchText}%");

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              customers.Add(new HoiVienDTO
              {
                MaHV = reader.GetInt32(0),
                HoTen = reader.GetString(1),
                GioiTinh = reader.GetBoolean(2),
                Sdt = reader.GetString(3),
                Dchi = reader.GetString(4),
              });
            }
          }
        }

        return customers;
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
  }
}

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
  public class NhaCungCapDAO : BaseDAO
  {
    public NhaCungCapDAO(string _connStr) : base(_connStr)
    {
    }
    public DataSet GetAllDisconnected(DataSet ds)
    {
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM NHACUNGCAP", conn);
          SqlDataAdapter adapter = new SqlDataAdapter(cmd);
          adapter.Fill(ds, "NHACUNGCAP");
        }
        return ds;
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public List<NhaCungCapDTO> GetAllConnected(bool active = false)
    {
      try
      {
        List<NhaCungCapDTO> providers = new List<NhaCungCapDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand($"SELECT * FROM NHACUNGCAP {(active ? "WHERE TRANGTHAI = 1" : "")}", conn);

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              providers.Add(new NhaCungCapDTO()
              {
                MaNCC = reader.GetInt32(0),
                TenNCC = reader.GetString(1),
                Ndd = reader.GetString(2),
                Sdt = reader.GetString(3),
                Email = reader.GetString(4),
                TrangThai = reader.GetBoolean(5)
              });
            }
          }
        }

        return providers;
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public int Add(NhaCungCapDTO newSupplier)
    {
      if (newSupplier == null)
      {
        throw new ArgumentNullException("Supplier cannot be null.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("INSERT INTO NHACUNGCAP (TENNCC, NDD, SDT, EMAIL, TRANGTHAI) VALUES (@TENNCC, @NDD, @SDT, @EMAIL, @TRANGTHAI)", conn);
          cmd.Parameters.AddWithValue("@TENNCC", newSupplier.TenNCC);
          cmd.Parameters.AddWithValue("@NDD", newSupplier.Ndd);
          cmd.Parameters.AddWithValue("@SDT", newSupplier.Sdt);
          cmd.Parameters.AddWithValue("@EMAIL", newSupplier.Email);
          cmd.Parameters.AddWithValue("@TRANGTHAI", newSupplier.TrangThai);

          int newId = Convert.ToInt32(cmd.ExecuteScalar());
          newSupplier.MaNCC = newId;
          return newId;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while adding data to the database.", ex);
      }
    }
    public bool Update(NhaCungCapDTO ncc)
    {
      if (ncc == null)
      {
        throw new ArgumentNullException("Supplier cannot be null.");
      }
      if (ncc.MaNCC <= 0)
      {
        throw new ArgumentNullException("Supplier id must be a positive value.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE NHACUNGCAP SET TENNCC = @TENNCC, NDD = @NDD, SDT = @SDT, EMAIL = @EMAIL, TRANGTHAI = @TRANGTHAI WHERE MANCC = @MANCC", conn);
          cmd.Parameters.AddWithValue("@MANCC", ncc.MaNCC);
          cmd.Parameters.AddWithValue("@TENNCC", ncc.TenNCC);
          cmd.Parameters.AddWithValue("@NDD", ncc.Ndd);
          cmd.Parameters.AddWithValue("@SDT", ncc.Sdt);
          cmd.Parameters.AddWithValue("@EMAIL", ncc.Email);
          cmd.Parameters.AddWithValue("@TRANGTHAI", ncc.TrangThai);

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
        throw new ArgumentException("Supplier ID must be a positive value.", nameof(id));
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE NHACUNGCAP SET TRANGTHAI = 0 WHERE MANCC = @MANCC", conn);
          cmd.Parameters.AddWithValue("@MANCC", id);

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
          SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('NHACUNGCAP') + 1", conn);
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

    public bool Active(int id)
    {
      if (id <= 0)
      {
        throw new ArgumentNullException("Supplier Id must be positive value");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE NHACUNGCAP SET TRANGTHAI = 1 WHERE MANCC = @MANCC", conn);
          cmd.Parameters.AddWithValue("@MANCC", id);

          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }

    public bool Deactive(int id)
    {
      if (id <= 0)
      {
        throw new ArgumentNullException("Supplier Id must be positive value");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE NHACUNGCAP SET TRANGTHAI = 0 WHERE MANCC = @MANCC", conn);
          cmd.Parameters.AddWithValue("@MANCC", id);

          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public List<NhaCungCapDTO> FindByAnyProperty(string searchText)
    {
      try
      {
        List<NhaCungCapDTO> result = new List<NhaCungCapDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          string query = @"
            SELECT * FROM NHACUNGCAP 
            WHERE TENNCC LIKE @SEARCH_TEXT OR
                  NDD LIKE @SEARCH_TEXT OR
                  SDT LIKE @SEARCH_TEXT OR
                  EMAIL LIKE @SEARCH_TEXT
          ";

          using (SqlCommand cmd = new SqlCommand(query, conn))
          {
            cmd.Parameters.AddWithValue("@SEARCH_TEXT", $"%{searchText}%");

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
              while (reader.Read())
              {
                result.Add(new NhaCungCapDTO
                {
                  MaNCC = reader.GetInt32(0),
                  TenNCC = reader.GetString(1),
                  Ndd = reader.GetString(2),
                  Sdt = reader.GetString(3),
                  Email = reader.GetString(4),
                  TrangThai = reader.GetBoolean(5)
                });
              }
            }
          }
        }

        return result;
      }
      catch (SqlException ex)
      {
        throw new DataException("Database error occurred while searching for suppliers.", ex);
      }
      catch (Exception ex)
      {
        throw new DataException("An unexpected error occurred while searching for suppliers.", ex);
      }
    }
  }
}
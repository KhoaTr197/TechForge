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
  public class HangSanXuatDAO : BaseDAO
  {
    public HangSanXuatDAO(string _connStr) : base(_connStr)
    {
    }
    public List<HangSanXuatDTO> GetAllConnected()
    {
      try
      {
        List<HangSanXuatDTO> manufacturers = new List<HangSanXuatDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM HANGSANXUAT", conn);

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              manufacturers.Add(new HangSanXuatDTO()
              {
                MaHSX = reader.GetInt32(0),
                TenHSX = reader.GetString(1)
              });
            }
          }
        }

        return manufacturers;
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
          SqlCommand cmd = new SqlCommand("SELECT * FROM HANGSANXUAT", conn);
          SqlDataAdapter adapter = new SqlDataAdapter(cmd);
          adapter.Fill(ds, "HANGSANXUAT");
        }
        return ds;
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public int Add(HangSanXuatDTO newManufacturer)
    {
      if (newManufacturer == null)
      {
        throw new ArgumentNullException("Manufacturer cannot be null.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("INSERT INTO HANGSANXUAT (TENHSX) VALUES (@TENHSX)", conn);
          cmd.Parameters.AddWithValue("@TENHSX", newManufacturer.TenHSX);

          int newId = Convert.ToInt32(cmd.ExecuteScalar());
          newManufacturer.MaHSX = newId;
          return newId;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while adding data to the database.", ex);
      }
    }
    public bool Update(HangSanXuatDTO updatedManufacturer)
    {
      if (updatedManufacturer == null)
      {
        throw new ArgumentNullException("Manufacturer cannot be null.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE HANGSANXUAT SET TENHSX = @TENHSX WHERE MAHSX = @MAHSX", conn);
          cmd.Parameters.AddWithValue("@MAHSX", updatedManufacturer.MaHSX);
          cmd.Parameters.AddWithValue("@TENHSX", updatedManufacturer.TenHSX);

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
        throw new ArgumentException("Manufacturer ID must be a positive value.", nameof(id));
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("DELETE FROM HANGSANXUAT WHERE MAHSX = @MAHSX", conn);
          cmd.Parameters.AddWithValue("@MAHSX", id);

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
          SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('HANGSANXUAT') + 1", conn);
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

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
  public class DanhMucDAO : BaseDAO
  {
    public DanhMucDAO(string _connStr) : base(_connStr) { }
    public List<DanhMucDTO> GetAllConnected()
    {
      try
      {
        List<DanhMucDTO> categories = new List<DanhMucDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM DANHMUC", conn);
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              categories.Add(new DanhMucDTO()
              {
                MaDM = reader.GetInt32(0),
                TenDM = reader.GetString(1),
              });
            }
          }
        }
        return categories;
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
          SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM DANHMUC", conn);
          adapter.Fill(ds, "DANHMUC");
        }
        return ds;
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public int Add(DanhMucDTO newCategory)
    {
      if (newCategory == null)
      {
        throw new ArgumentNullException("Category cannot be null.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("INSERT INTO DANHMUC (TENDM) VALUES (@TENDM)", conn);
          cmd.Parameters.AddWithValue("@TENDM", newCategory.TenDM);

          int newId = Convert.ToInt32(cmd.ExecuteScalar());
          newCategory.MaDM = newId;
          return newId;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while adding data to the database.", ex);
      }
    }
    public bool Update(DanhMucDTO updatedCategory)
    {
      if (updatedCategory == null)
      {
        throw new ArgumentNullException("Category cannot be null.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE DANHMUC SET TENDM = @TENDM WHERE MADM = @MADM", conn);
          cmd.Parameters.AddWithValue("@MADM", updatedCategory.MaDM);
          cmd.Parameters.AddWithValue("@TENDM", updatedCategory.TenDM);

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
        throw new ArgumentException("Category ID must be a positive value.", nameof(id));
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("DELETE FROM DANHMUC WHERE MADM = @MADM", conn);
          cmd.Parameters.AddWithValue("@MADM", id);

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
          SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('DANHMUC') + 1", conn);
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

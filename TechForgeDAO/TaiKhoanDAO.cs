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
    public class TaiKhoanDAO : BaseDAO
    {
        public TaiKhoanDAO(string _connStr) : base(_connStr)
        {
        }

    public TaiKhoanDTO GetCredential(string id)
    {
      if(id == null)
      {
        throw new ArgumentNullException("User Id cannot be null");
      }
      try
      {
        TaiKhoanDTO account = null;

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM TAIKHOAN WHERE MAND = @MAND", conn);
          cmd.Parameters.AddWithValue("MAND", id);

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              account = new TaiKhoanDTO
              {
                MaND = reader.GetString(0),
                TenTK = reader.GetString(1),
                MatKhau = reader.GetString(2),
                TrangThai = reader.GetBoolean(3),
              };
            }
          }

          return account;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public bool Active(string id)
    {
      if (id == null)
      {
        throw new ArgumentNullException("User Id cannot be null");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE TAIKHOAN SET TRANGTHAI = 1 WHERE MAND = @MAND", conn);
          cmd.Parameters.AddWithValue("@MAND", id);

          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public bool Deactive(string id)
    {
      if (id == null)
      {
        throw new ArgumentNullException("User Id cannot be null");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE TAIKHOAN SET TRANGTHAI = 0 WHERE MAND = @MAND", conn);
          cmd.Parameters.AddWithValue("@MAND", id);

          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public TaiKhoanDTO Login(string username, string password)
        {
            try
            {
                TaiKhoanDTO account = null;

                using (SqlConnection conn = CreateConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM TAIKHOAN WHERE TENTK = @username AND MATKHAU = @password AND TRANGTHAI = 1", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            account = new TaiKhoanDTO()
                            {
                                MaND = reader.GetString(0),
                                TenTK = reader.GetString(1),
                                MatKhau = reader.GetString(2),
                                TrangThai = reader.GetBoolean(3),
                            };
                        }
                    }
                }

                return account;
            }
            catch (Exception ex)
            {
                throw new DataException("An error occurred while getting data from the database.", ex);
            }
        }

    public List<TaiKhoanDTO> GetAllConnected()
    {
      try
      {
        List<TaiKhoanDTO> accounts = new List<TaiKhoanDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM TAIKHOAN", conn);

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              accounts.Add(new TaiKhoanDTO
              {
                MaND = reader.GetString(0),
                TenTK = reader.GetString(1),
                MatKhau = reader.GetString(2),
                TrangThai = reader.GetBoolean(3),
              });
            }
          }

          return accounts;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }

    public bool Update(TaiKhoanDTO newTk)
    {
      if (newTk == null)
      {
        throw new ArgumentNullException("Account cannot be null");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE TAIKHOAN SET TENTK = @TENTK, MATKHAU = @MATKHAU, TRANGTHAI = @TRANGTHAI WHERE MAND = @MAND", conn);
          cmd.Parameters.AddWithValue("@MAND", newTk.MaND);
          cmd.Parameters.AddWithValue("@TENTK", newTk.TenTK);
          cmd.Parameters.AddWithValue("@MATKHAU", newTk.MatKhau);
          cmd.Parameters.AddWithValue("@TRANGTHAI", newTk.TrangThai);

          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }

    public bool Add(TaiKhoanDTO newTk)
    {
      if (newTk == null)
      {
        throw new ArgumentNullException("Account cannot be null");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("INSERT INTO TAIKHOAN (MAND, TENTK, MATKHAU, TRANGTHAI) VALUES(@MAND, @TENTK, @MATKHAU, @TRANGTHAI)", conn);
          cmd.Parameters.AddWithValue("@MAND", newTk.MaND);
          cmd.Parameters.AddWithValue("@TENTK", newTk.TenTK);
          cmd.Parameters.AddWithValue("@MATKHAU", newTk.MatKhau);
          cmd.Parameters.AddWithValue("@TRANGTHAI", newTk.TrangThai);

          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }

        public List<TaiKhoanDTO> FindByAnyProperty(string searchText)
        {
            try
            {
                List<TaiKhoanDTO> accounts = new List<TaiKhoanDTO>();

                using (SqlConnection conn = CreateConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT * FROM TAIKHOAN 
                        WHERE MAND LIKE @SEARCH_TEXT 
                        OR TENTK LIKE @SEARCH_TEXT
                        ORDER BY TRANGTHAI DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SEARCH_TEXT", $"%{searchText}%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                accounts.Add(new TaiKhoanDTO
                                {
                                    MaND = reader.GetString(0),
                                    TenTK = reader.GetString(1),
                                    MatKhau = reader.GetString(2),
                                    TrangThai = reader.GetBoolean(3),
                                });
                            }
                        }
                    }
                }

                return accounts;
            }
            catch (SqlException ex)
            {
                throw new DataException("Database error occurred while searching for members.", ex);
            }
            catch (Exception ex)
            {
                throw new DataException("An unexpected error occurred while searching for members.", ex);
            }
        }
  }
}

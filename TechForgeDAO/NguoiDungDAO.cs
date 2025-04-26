using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechForgeDTO;
using System.Text.RegularExpressions;

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

    public List<string> GetAllRoles()
    {
      try
      {
        List<string> result = new List<string>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT DISTINCT VAITRO FROM NGUOIDUNG", conn);
          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read()) {
              result.Add(reader.GetString(0));
            }
          }
        }

        return result;
      } catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }

    public NguoiDungDTO GetById(string id)
        {
            try
            {
                NguoiDungDTO user = null;

                using (SqlConnection conn = CreateConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM NGUOIDUNG WHERE MAND = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new NguoiDungDTO()
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
                            };
                        }
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new DataException("An error occurred while getting data from the database.", ex);
            }
        }
    public int Add(NguoiDungDTO newUser)
    {
      if (newUser == null)
      {
        throw new ArgumentNullException("User cannot be null.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("INSERT INTO NGUOIDUNG (MAND, HOTEN, NGSINH, GIOITINH, CCCD, SDT, DCHI, VAITRO, NGVAOLAM) VALUES (@mand, @hoten, @ngsinh, @gioitinh, @cccd, @sdt, @dchi, @vaitro, @ngvaolam)", conn);
          cmd.Parameters.AddWithValue("@mand", newUser.MaND);
          cmd.Parameters.AddWithValue("@hoten", newUser.HoTen);
          cmd.Parameters.AddWithValue("@ngsinh", newUser.NgSinh);
          cmd.Parameters.AddWithValue("@gioitinh", newUser.GioiTinh);
          cmd.Parameters.AddWithValue("@cccd", newUser.Cccd);
          cmd.Parameters.AddWithValue("@sdt", newUser.Sdt);
          cmd.Parameters.AddWithValue("@dchi", newUser.Dchi);
          cmd.Parameters.AddWithValue("@vaitro", newUser.VaiTro);
          cmd.Parameters.AddWithValue("@ngvaolam", newUser.NgVaoLam);
          return cmd.ExecuteNonQuery();
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while adding data to the database.", ex);
      }
    }

    public bool Update(NguoiDungDTO updatedNguoiDung)
    {
      if (updatedNguoiDung == null)
      {
        throw new ArgumentNullException("User cannot be null.");
      }
      try
      {
        using(SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE NGUOIDUNG SET HOTEN = @hoten, NGSINH = @ngsinh, GIOITINH = @gioitinh, CCCD = @cccd, SDT = @sdt, DCHI = @dchi, VAITRO = @vaitro, NGVAOLAM = @ngvaolam WHERE MAND = @mand", conn);
          cmd.Parameters.AddWithValue("@mand", updatedNguoiDung.MaND);
          cmd.Parameters.AddWithValue("@hoten", updatedNguoiDung.HoTen);
          cmd.Parameters.AddWithValue("@ngsinh", updatedNguoiDung.NgSinh);
          cmd.Parameters.AddWithValue("@gioitinh", updatedNguoiDung.GioiTinh);
          cmd.Parameters.AddWithValue("@cccd", updatedNguoiDung.Cccd);
          cmd.Parameters.AddWithValue("@sdt", updatedNguoiDung.Sdt);
          cmd.Parameters.AddWithValue("@dchi", updatedNguoiDung.Dchi);
          cmd.Parameters.AddWithValue("@vaitro", updatedNguoiDung.VaiTro);
          cmd.Parameters.AddWithValue("@ngvaolam", updatedNguoiDung.NgVaoLam);

          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while updating data in the database.", ex);
      }
    }
    public string GetNextId(string vaiTro)
    {
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT TOP(1) MAND FROM NGUOIDUNG WHERE MAND LIKE @VAITRO ORDER BY CAST(SUBSTRING(MAND, 5, LEN(MAND) - 4) AS INT) DESC", conn);
          cmd.Parameters.AddWithValue("@VAITRO", $"{vaiTro}_%");
          object result = cmd.ExecuteScalar();
          if (result != null && result != DBNull.Value)
          {
            String[] id = result.ToString().Split('_');

            return $"{id[0]}_{int.Parse(id[1]) + 1}";
          }
          return null;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting the next ID from the database.", ex);
      }
    }
  }
}

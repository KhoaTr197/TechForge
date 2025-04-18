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
    }
}

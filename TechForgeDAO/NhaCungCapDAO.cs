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
        public List<NhaCungCapDTO> GetAllConnected()
        {
            try
            {
                List<NhaCungCapDTO> providers = new List<NhaCungCapDTO>();

                using (SqlConnection conn = CreateConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM NHACUNGCAP where TRANGTHAI = 1", conn);

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
    }
}

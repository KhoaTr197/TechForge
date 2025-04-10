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
  public class SanPhamDAO : BaseDAO
  {
    public SanPhamDAO(string _connStr) : base(_connStr)
    {
    }
    public List<SanPhamDTO> GetAllConnected()
    {
      try
      {
        List<SanPhamDTO> products = new List<SanPhamDTO>();

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM SANPHAM WHERE TRANGTHAI = 1", conn);

          using (SqlDataReader reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              products.Add(new SanPhamDTO()
              {
                MaSP = reader.GetInt32(0),
                TenSP = reader.GetString(1),
                GiaNhap = reader.GetDecimal(2),
                Gia = reader.GetDecimal(3),
                KhuyenMai = reader.GetDecimal(4),
                MoTa = reader.GetString(5),
                SoLuong = reader.GetInt32(6),
                DanhMuc = reader.GetInt32(7),
                Hsx = reader.GetInt32(8),
                NgSx = reader.GetDateTime(9),
                TrangThai = reader.GetBoolean(10)
              });
            }
          }
        }

        return products;
      }
      catch (Exception ex) {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public DataSet GetAllDisconnected(DataSet ds)
    {
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM SANPHAM WHERE TRANGTHAI = 1", conn);
          adapter.Fill(ds, "SANPHAM");
        }

        return ds;
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public int Add(SanPhamDTO newProduct)
    {
      if (newProduct == null)
      {
        throw new ArgumentNullException("Product cannot be null.");
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("INSERT INTO SANPHAM (TENSP, GIANHAP, GIA, KHUYENMAI, MOTA, SL, DANHMUC, HSX, NGSX, TRANGTHAI) VALUES(@TENSP, @GIANHAP, @GIA, @KHUYENMAI, @MOTA, @SL, @DANHMUC, @HSX, @NGSX, @TRANGTHAI)", conn);
          cmd.Parameters.AddWithValue("@TENSP", newProduct.TenSP);
          cmd.Parameters.AddWithValue("@GIANHAP", newProduct.GiaNhap);
          cmd.Parameters.AddWithValue("@GIA", newProduct.Gia);
          cmd.Parameters.AddWithValue("@KHUYENMAI", newProduct.KhuyenMai);
          cmd.Parameters.AddWithValue("@MOTA", newProduct.MoTa);
          cmd.Parameters.AddWithValue("@SL", newProduct.SoLuong);
          cmd.Parameters.AddWithValue("@DANHMUC", newProduct.DanhMuc);
          cmd.Parameters.AddWithValue("@HSX", newProduct.Hsx);
          cmd.Parameters.AddWithValue("@NGSX", newProduct.NgSx);
          cmd.Parameters.AddWithValue("@TRANGTHAI", newProduct.TrangThai);

          int newId = Convert.ToInt32(cmd.ExecuteScalar());
          newProduct.MaSP = newId;
          return newId;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while adding data to the database.", ex);
      }
    }
    public bool Update(SanPhamDTO updatedProduct)
    {
      if (updatedProduct == null)
      {
        throw new ArgumentNullException("Product cannot be null.");
      }
      if (updatedProduct.MaSP <= 0)
      {
        throw new ArgumentNullException("Product id must be a positive value.");
      }
      try {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE SANPHAM SET TENSP = @TENSP, GIANHAP = @GIANHAP, GIA = @GIA, KHUYENMAI = @KHUYENMAI, MOTA = @MOTA, SL = @SL, DANHMUC = @DANHMUC, HSX = @HSX, NGSX = @NGSX, TRANGTHAI = @TRANGTHAI WHERE MASP = @MASP", conn);
          cmd.Parameters.AddWithValue("@MASP", updatedProduct.MaSP);
          cmd.Parameters.AddWithValue("@TENSP", updatedProduct.TenSP);
          cmd.Parameters.AddWithValue("@GIANHAP", updatedProduct.GiaNhap);
          cmd.Parameters.AddWithValue("@GIA", updatedProduct.Gia);
          cmd.Parameters.AddWithValue("@KHUYENMAI", updatedProduct.KhuyenMai);
          cmd.Parameters.AddWithValue("@MOTA", updatedProduct.MoTa);
          cmd.Parameters.AddWithValue("@SL", updatedProduct.SoLuong);
          cmd.Parameters.AddWithValue("@DANHMUC", updatedProduct.DanhMuc);
          cmd.Parameters.AddWithValue("@HSX", updatedProduct.Hsx);
          cmd.Parameters.AddWithValue("@NGSX", updatedProduct.NgSx);
          cmd.Parameters.AddWithValue("@TRANGTHAI", updatedProduct.TrangThai);

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
        throw new ArgumentException("Product ID must be a positive value.", nameof(id));
      }
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("UPDATE SANPHAM SET TRANGTHAI = 0 WHERE MASP = @MASP", conn);
          cmd.Parameters.AddWithValue("@MASP", id);

          return cmd.ExecuteNonQuery() > 0;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while deleting data from the database.", ex);
      }
    }
  }
}

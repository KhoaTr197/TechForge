using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechForgeDAO;
using TechForgeDTO;
using System.Runtime.InteropServices.ComTypes;
using System.Globalization;

namespace TechForgeDAO
{
  public class DoanhThuDAO : BaseDAO
  {
    public DoanhThuDAO(string _connStr) : base(_connStr)
    {
    }
    public void GetStatisticData(DoanhThuDTO statisticData)
    {
      statisticData.SoNgay = this.GetNumberOfDays(statisticData);
      statisticData.SoHoiVien = this.GetNumberOfMembers();
      statisticData.SoNCC = this.GetNumberOfSuppliers();
      statisticData.SoSanPham = this.GetNumberOfProducts();
      statisticData.SoHoaDon = this.GetNumberOfOrders(statisticData);
      statisticData.DsSPBanChay = this.GetTopProductsList(statisticData);
      statisticData.DsSPTonKho = this.GetUndestockList();
      statisticData.DsDoanhThu = this.GetRevenueList(statisticData);
    }
    public int GetNumberOfDays(DoanhThuDTO doanhThuDTO)
    {
      return (doanhThuDTO.NgKetThuc - doanhThuDTO.NgBatDau).Days;
    }
    public int GetNumberOfMembers()
    {
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT COUNT(MAHV) FROM HOIVIEN WHERE TRANGTHAI = 1", conn);

          return (int)cmd.ExecuteScalar();

        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public int GetNumberOfSuppliers()
    {
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT COUNT(MANCC) FROM NHACUNGCAP WHERE TRANGTHAI = 1", conn);

          return (int)cmd.ExecuteScalar();

        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public int GetNumberOfProducts()
    {
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT COUNT(MASP) FROM SANPHAM WHERE TRANGTHAI = 1", conn);

          return (int)cmd.ExecuteScalar();

        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public int GetNumberOfOrders(DoanhThuDTO doanhThuDTO)
    {
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          string queryStr = "select count(MAHD) from [HOADON] where NGLAPHD between @fromDate and @toDate";
          SqlCommand cmd = new SqlCommand(queryStr, conn);
          cmd.Parameters.Add(new SqlParameter("@fromDate", doanhThuDTO.NgBatDau));
          cmd.Parameters.Add(new SqlParameter("@toDate", doanhThuDTO.NgKetThuc));

          return (int)cmd.ExecuteScalar();
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }


    public List<KeyValuePair<string, int>> GetTopProductsList(DoanhThuDTO doanhThuDTO)
    {
      List<KeyValuePair<string, int>> TopProductsList = new List<KeyValuePair<string, int>>();
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();

          //Get Top 5 products
          string queryStr = @"select top 5 SP.TENSP, sum(CTHD.SL) as SOLUONG
                                        from CTHD
                                        inner join SANPHAM SP on SP.MASP = CTHD.MASP
                                        inner
                                        join [HOADON] HD on HD.MAHD = CTHD.MAHD
                                        where NGLAPHD between @fromDate and @toDate
                                        group by SP.TENSP
                                        order by SOLUONG desc";
          SqlCommand cmd = new SqlCommand(queryStr, conn);

          cmd.Parameters.Add(new SqlParameter("@fromDate", doanhThuDTO.NgBatDau));
          cmd.Parameters.Add(new SqlParameter("@toDate", doanhThuDTO.NgKetThuc));
          SqlDataReader reader = cmd.ExecuteReader();
          while (reader.Read())
          {
            TopProductsList.Add(new KeyValuePair<string, int>(reader[0].ToString(), (int)reader[1]));
          }
          reader.Close();
          return TopProductsList;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }

    public List<KeyValuePair<string, int>> GetUndestockList()
    {
      List<KeyValuePair<string, int>> UnderstockList = new List<KeyValuePair<string, int>>();
      try
      {
        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();

          //Get understock list
          string queryStr = @"select TENSP, SL
                                        from SANPHAM
                                        where SL <= 5 and TRANGTHAI = 1";
          SqlCommand cmd = new SqlCommand(queryStr, conn);

          SqlDataReader reader = cmd.ExecuteReader();
          while (reader.Read())
          {
            UnderstockList.Add(new KeyValuePair<string, int>(reader[0].ToString(), (int)reader[1]));
          }
          reader.Close();
          return UnderstockList;
        }
      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
    public List<DoanhThuTheoTG> GetRevenueList(DoanhThuDTO doanhThuDTO)
    {
      List<DoanhThuTheoTG> DoanhThuList = new List<DoanhThuTheoTG>();
      doanhThuDTO.TongDoanhThu = 0;
      try
      {

        using (SqlConnection conn = CreateConnection())
        {
          conn.Open();
          string queryStr = @"select NGLAPHD, sum(TONGTIEN) as TONGTIEN
                                        from[HOADON]
                                        where NGLAPHD between @fromDate and @toDate
                                        group by NGLAPHD";
          SqlCommand cmd = new SqlCommand(queryStr, conn);
          cmd.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = doanhThuDTO.NgBatDau;
          cmd.Parameters.Add("@toDate", SqlDbType.DateTime).Value = doanhThuDTO.NgKetThuc;

          SqlDataReader reader = cmd.ExecuteReader();
          var resultTable = new List<KeyValuePair<DateTime, decimal>>();
          while (reader.Read())
          {
            resultTable.Add(new KeyValuePair<DateTime, decimal>((DateTime)reader[0], (decimal)reader[1]));
            doanhThuDTO.TongDoanhThu += (decimal)reader[1];
          }
          reader.Close();

          //Group by Hours
          if (doanhThuDTO.SoNgay <= 1)
          {
            DoanhThuList = (from orderList in resultTable
                            group orderList by orderList.Key.ToString("hh tt")
                            into order
                            select new DoanhThuTheoTG
                            {
                              ThoiGian = order.Key,
                              TongTien = order.Sum(amount => amount.Value)
                            }).ToList();
          }
          //Group by Days
          else if (doanhThuDTO.SoNgay <= 30)
          {
            DoanhThuList = (from orderList in resultTable
                            group orderList by orderList.Key.ToString("dd MMM")
                               into order
                            select new DoanhThuTheoTG
                            {
                              ThoiGian = order.Key,
                              TongTien = order.Sum(amount => amount.Value)
                            }).ToList();
          }
          //Group by Weeks
          else if (doanhThuDTO.SoNgay <= 92)
          {
            DoanhThuList = (from orderList in resultTable
                            group orderList by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                orderList.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                               into order
                            select new DoanhThuTheoTG
                            {
                              ThoiGian = "Week " + order.Key.ToString(),
                              TongTien = order.Sum(amount => amount.Value)
                            }).ToList();
          }
          //Group by Months
          else if (doanhThuDTO.SoNgay <= (365 * 2))
          {
            bool isYear = doanhThuDTO.SoNgay <= 365 ? true : false;
            DoanhThuList = (from orderList in resultTable
                            group orderList by orderList.Key.ToString("MMM yyyy")
                               into order
                            select new DoanhThuTheoTG
                            {
                              ThoiGian = isYear ? order.Key.Substring(0, order.Key.IndexOf(" ")) : order.Key,
                              TongTien = order.Sum(amount => amount.Value)
                            }).ToList();
          }
          //Group by Years
          else
          {
            DoanhThuList = (from orderList in resultTable
                            group orderList by orderList.Key.ToString("yyyy")
                               into order
                            select new DoanhThuTheoTG
                            {
                              ThoiGian = order.Key,
                              TongTien = order.Sum(amount => amount.Value)
                            }).ToList();
          }
          return DoanhThuList;
        }

      }
      catch (Exception ex)
      {
        throw new DataException("An error occurred while getting data from the database.", ex);
      }
    }
  }
}

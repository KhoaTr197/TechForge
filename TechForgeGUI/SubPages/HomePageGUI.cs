using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeBUS;
using TechForgeDTO;
using TechForgeGUI.BaseControls;
using TechForgeGUI.BaseForms;

namespace TechForgeGUI.SubPages
{
  public partial class HomePageGUI : Page
  {
    private NguoiDungBUS nguoiDungBUS;
    private HoaDonBUS hoaDonBUS;
    private HoiVienBUS hoiVienBUS;
    private SanPhamBUS sanPhamBUS;
    private LichSuHoatDongBUS lichSuHoatDongBUS;
    private TaiKhoanDTO currentAccount {  get; set; }
    private NguoiDungDTO currentUser { get; set; }
    // Summary cards section
    private SummaryCards summaryCards;
    //private SummaryCards summaryCards;

    public HomePageGUI(TaiKhoanDTO _currentAccount, NguoiDungDTO _currentUser)
    {
      this.currentAccount = _currentAccount;
      this.currentUser = _currentUser;
      InitializeComponent();
      InitializeBUS();
      LoadData();
      LoadActivityLog();
      StartDateTimeTimer();

      summaryCards = new SummaryCards(flpSummary, 4);

      //// Add stats cards
      //summaryCards.Add(new SummaryCard[] {
      //  new SummaryCard("Tổng khách hàng", "0", "users_icon", Color.FromArgb(52, 152, 219)),
      //  new SummaryCard("Tổng sản phẩm", "0", "box_icon", Color.FromArgb(46, 204, 113)),
      //  new SummaryCard("Tổng đơn hàng", "0", "order_icon", Color.FromArgb(155, 89, 182)),
      //  new SummaryCard("Tỷ lệ hoàn thành", "0%", "rate_icon", Color.FromArgb(231, 76, 60)),
      //});

      flpActivityList.Resize += (s, e) =>
      {
        foreach (Control card in flpActivityList.Controls)
        {
          card.Width = flpActivityList.ClientSize.Width - 20;
        }
      };
    }

    private void InitializeBUS()
    {
      nguoiDungBUS = new NguoiDungBUS(connStr);
      hoaDonBUS = new HoaDonBUS(connStr);
      hoiVienBUS = new HoiVienBUS(connStr);
      sanPhamBUS = new SanPhamBUS(connStr);
      lichSuHoatDongBUS = new LichSuHoatDongBUS(connStr);
    }
    private void BtnViewProfile_Click(object sender, EventArgs e)
    {
      // TODO: Show user profile form or dialog
      MessageBox.Show("Chức năng đang được phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    private void LoadActivityLog() {
      List<LichSuHoatDongDTO> logs = lichSuHoatDongBUS.GetRecentAllConnected(currentUser.MaND);

      foreach (LichSuHoatDongDTO log in logs)
      {
        TableLayoutPanel logCard = new TableLayoutPanel
        {
          Width = flpActivityList.ClientSize.Width - 8,
          Height = 80,
          Margin = new Padding(4),
          BorderStyle = BorderStyle.FixedSingle,
          BackColor = Color.White,
          ColumnCount = 1,
          RowCount = 2,
          RowStyles =
          {
            new RowStyle(SizeType.Percent, 50F),
            new RowStyle(SizeType.Percent, 50F),
          }
        };

        Label lblTime = new Label
        {
          Text = log.ThoiGian.ToString("dd/MM/yyyy HH:mm:ss"),
          AutoSize = true,
          Font = new Font("Segoe UI", 10),
          ForeColor = Color.Black,
          TextAlign = ContentAlignment.MiddleLeft
        };

        Label lblDetails = new Label
        {
          Text = log.NoiDung,
          AutoSize = true,
          Font = new Font("Segoe UI", 10, FontStyle.Bold),
          ForeColor = Color.Black,
          TextAlign = ContentAlignment.MiddleLeft
        };

        logCard.Controls.Add(lblTime, 0, 0);
        logCard.Controls.Add(lblDetails, 0, 1);

        flpActivityList.Controls.Add(logCard);
      }
    }
    private void LoadData()
    {
      // Load user info
      if (currentUser != null)
      {
        lblWelcome.Text = $"Chào mừng trở lại, {currentUser.HoTen}!";
        lblUserRole.Text = $"Vai trò: {currentUser.VaiTro}";
        lblAccountName.Text = $"Tên đăng nhập: {currentAccount.TenTK}";
      }

      // Load stats data
      var totalCustomers = hoiVienBUS.GetAllConnected().Count;
      var totalProducts = sanPhamBUS.GetAllConnected().Count;

    }

    private void StartDateTimeTimer()
    {
      Timer timer = new Timer();
      timer.Interval = 1000; // Update every second
      timer.Tick += (s, e) => lblCurrentDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
      timer.Start();
    }
  }
}
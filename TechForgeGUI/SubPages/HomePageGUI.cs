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

    // Main container panels
    private TableLayoutPanel tlpMain;
    private TableLayoutPanel tlpTop;
    private TableLayoutPanel flpLeft;
    private TableLayoutPanel flpRight;
    private FlowLayoutPanel flpActivityList;

    // Welcome section
    private TableLayoutPanel tlpWelcome;
    private Label lblWelcome;
    private Label lblDateTime;

    // Credential info section
    private Panel flpCredential;
    private Label lblUserRole;
    private Label lblAccountName;
    private Button btnViewProfile;

    // Stats cards section
    private FlowLayoutPanel flpSummaryCards;
    private SummaryCards summaryCards;

    public HomePageGUI(TaiKhoanDTO _currentAccount, NguoiDungDTO _currentUser)
    {
      this.currentAccount = _currentAccount;
      this.currentUser = _currentUser;
      InitializeComponent();
      InitializeBUS();
      InitializeLayout();
      LoadData();
      StartDateTimeTimer();
    }

    private void InitializeBUS()
    {
      nguoiDungBUS = new NguoiDungBUS(connStr);
      hoaDonBUS = new HoaDonBUS(connStr);
      hoiVienBUS = new HoiVienBUS(connStr);
      sanPhamBUS = new SanPhamBUS(connStr);
      lichSuHoatDongBUS = new LichSuHoatDongBUS(connStr);
    }

    private void InitializeLayout()
    {
      tlpMain = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        RowCount = 2,
        ColumnStyles = {
          new ColumnStyle(SizeType.Percent, 50F),
          new ColumnStyle(SizeType.Percent, 50F)
        },
        RowStyles =
        {
          new RowStyle(SizeType.Percent, 30F),
          new RowStyle(SizeType.Percent, 70F),
        },
        Padding = new Padding(8),
      };
      this.Controls.Add(tlpMain);

      tlpTop = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        ColumnCount = 1,
        RowCount = 2,
        ColumnStyles = {
          new ColumnStyle(SizeType.Percent, 100F),
        },
        RowStyles =
        {
          new RowStyle(SizeType.Percent, 30F),
          new RowStyle(SizeType.Percent, 70F),
        },
      };
      tlpMain.Controls.Add(tlpTop, 0, 0);
      tlpMain.SetColumnSpan(tlpTop, 2);

      flpLeft = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        RowCount = 2,
        RowStyles =
        {
          new RowStyle(SizeType.Absolute, 48),
          new RowStyle(SizeType.Percent, 100)
        }
      };
      tlpMain.Controls.Add(flpLeft, 0, 1);

      flpRight = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        RowCount = 2,
        RowStyles =
        {
          new RowStyle(SizeType.Absolute, 48),
          new RowStyle(SizeType.Percent, 100)
        }
      };
      tlpMain.Controls.Add(flpRight, 0, 1);

      // Initialize sections
      InitializeWelcomeSection();
      InitializeStatsCards();
      InitializeActivityLog();

      flpLeft.Controls.Add(new Label
      {
        Text = "Đang phát triển",
        Dock = DockStyle.Top,
        Font = new Font(DefaultFontName, 14, FontStyle.Bold),
        Height = 30,
        TextAlign = ContentAlignment.MiddleLeft
      });

      LoadActivityLog();
    }
    private void InitializeWelcomeSection()
    {

      tlpWelcome = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        Padding = new Padding(8),
        BackColor = Color.White,
        ColumnCount = 3,
        RowCount = 1,
        ColumnStyles = {
          new ColumnStyle(SizeType.Percent, 80F),
          new ColumnStyle(SizeType.Percent, 20F),
          new ColumnStyle(SizeType.AutoSize)
        }
      };

      FlowLayoutPanel flpWelcomeInfo = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.LeftToRight,
      };

      flpCredential = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        FlowDirection = FlowDirection.TopDown
      };

      lblWelcome = new Label
      {
        Text = "Chào mừng trở lại!",
        AutoSize = true,
        Font = new Font(DefaultFontName, 16, FontStyle.Bold),
      };

      lblDateTime = new Label
      {
        Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"),
        AutoSize = true,
        Font = new Font(DefaultFontName, 12),
        Margin = new Padding(0, 4, 0, 4)
      };

      lblUserRole = new Label
      {
        Text = "Vai trò: ---",
        AutoSize = true,
        Font = new Font(DefaultFontName, 10),
        ForeColor = Color.Gray,
      };

      lblAccountName = new Label
      {
        Text = "Tên đăng nhập: ---",
        AutoSize = true,
        Font = new Font(DefaultFontName, 10),
        ForeColor = Color.Gray,
      };

      btnViewProfile = new Button
      {
        Text = "Xem thông tin",
        Size = new Size(128, 40),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.FromArgb(52, 152, 219),
        ForeColor = Color.White,
        Cursor = Cursors.Hand,
      };
      btnViewProfile.Click += BtnViewProfile_Click;

      flpWelcomeInfo.Controls.AddRange(new Control[] { lblWelcome, lblDateTime });
      tlpWelcome.Controls.Add(flpWelcomeInfo, 0, 0);


      flpCredential.Controls.AddRange(new Control[] { 
        lblUserRole,
        lblAccountName, 
      });

      tlpWelcome.Controls.Add(flpCredential, 1, 0);
      tlpWelcome.Controls.Add(btnViewProfile, 2, 0);

      tlpTop.Controls.Add(tlpWelcome);
    }
    private void BtnViewProfile_Click(object sender, EventArgs e)
    {
      // TODO: Show user profile form or dialog
      MessageBox.Show("Chức năng đang được phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    private void InitializeStatsCards()
    {
      flpSummaryCards = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.LeftToRight,
        WrapContents = false,
        Margin = new Padding(0)
      };

      summaryCards = new SummaryCards(flpSummaryCards, 4);

      // Add stats cards
      summaryCards.Add(new SummaryCard[] {
        new SummaryCard("Tổng khách hàng", "0", "users_icon", Color.FromArgb(52, 152, 219)),
        new SummaryCard("Tổng sản phẩm", "0", "box_icon", Color.FromArgb(46, 204, 113)),
        new SummaryCard("Tổng đơn hàng", "0", "order_icon", Color.FromArgb(155, 89, 182)),
        new SummaryCard("Tỷ lệ hoàn thành", "0%", "rate_icon", Color.FromArgb(231, 76, 60)),
      });

      tlpTop.Controls.Add(flpSummaryCards);
    }
    private void InitializeActivityLog()
    {

      Label lblActivity = new Label
      {
        Text = "Lịch sử hoạt động",
        Dock = DockStyle.Top,
        Font = new Font(DefaultFontName, 14, FontStyle.Bold),
        Height = 30,
        TextAlign = ContentAlignment.MiddleLeft
      };

      flpActivityList = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.Gray,
        AutoScroll = true,
      };
      flpActivityList.Resize += (s, e) =>
      {
        foreach (Control card in flpActivityList.Controls)
        {
          card.Width = flpActivityList.ClientSize.Width - 20;
        }
      };

      flpRight.Controls.Add(lblActivity, 0, 0);
      flpRight.Controls.Add(flpActivityList, 0, 1);
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

      // Update stats cards
      summaryCards.cards[0].Value = totalCustomers.ToString();
      summaryCards.cards[1].Value = totalProducts.ToString();
      // The other stats would be updated when their functionality is implemented
      summaryCards.Update();
    }

    private void StartDateTimeTimer()
    {
      Timer timer = new Timer();
      timer.Interval = 1000; // Update every second
      timer.Tick += (s, e) => lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
      timer.Start();
    }
  }
}
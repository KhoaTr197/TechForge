using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TechForgeBUS;
using TechForgeDTO;
using TechForgeGUI.BaseForms;

namespace TechForgeGUI.SubPages
{
  public partial class StatisticPageGUI : Page
  {
    private DoanhThuBUS bus { get; set; }
    private DoanhThuDTO StatisticData { get; set; }

    //Controls
    private TableLayoutPanel mainLayoutPanel; // main table layout panel contains all controls
    private TableLayoutPanel panelDateRangePicker; //panel contains date range picker and buttons (Header)

    //Set both From-to date picker as properties to easily access them in other methods
    private DateTimePicker dtpFrom;
    private DateTimePicker dtpTo;

    private TableLayoutPanel tablePanelReportSummary; // table layout panel contains all report summary info like total revenue, receipt.
    private TableLayoutPanel tablePanelStatistic; // table layout panel contains all statistic info like charts, labels, etc.
    private Panel panelGrossRevenueChart; // panel contains gross revenue chart
    private Panel panelTopProductChart; // panel contains top product chart
    private TableLayoutPanel tablePanelStoreSummary; // panel contains all report summary info like total revenue, receipt.

    private DataGridView dgvUnderstock;
    private Button currentBtn;  // selected button of date range picker

    //Properties
    private DateTime cachedFromDate;
    private DateTime cachedToDate;
    public StatisticPageGUI()
    {
      InitializeComponent();

      this.Dock = DockStyle.Fill;

      this.bus = new DoanhThuBUS(connStr);
      this.StatisticData = new DoanhThuDTO();

      InitializeMainLayoutPanel();
      InitializeDateRangePicker();
      InitializeTablePanelStatistic();
      InitializeReportSummary();
      InitializeGrossRevenueChart();
      InitializeTopProductChart();
      InitializeStoreSummary();
      InitializeDgvUnderstock();

      StatisticData.NgBatDau = dtpFrom.Value;
      StatisticData.NgKetThuc = dtpTo.Value;

      this.Load += StatisticFormGUI_Load;
    }
    private void InitializeMainLayoutPanel()
    {
      mainLayoutPanel = new TableLayoutPanel
      {
        Dock = DockStyle.Fill,
        ColumnCount = 1,
        RowCount = 2,
      };
      mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 64));
      mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

      this.Controls.Add(mainLayoutPanel);
    }
    private void InitializeDateRangePicker()
    {
      //Init FlowLayoutPanel Date Range Picker
      panelDateRangePicker = new TableLayoutPanel()
      {
        Dock = DockStyle.Top,
        Height = 64,
        ColumnCount = 2,
        RowCount = 1,
        ColumnStyles =
        {
            new ColumnStyle(SizeType.AutoSize),
            new ColumnStyle(SizeType.Percent, 100F)
        }
      };

      //Init Date Range From-To Picker
      TableLayoutPanel panelDateFromTo = new TableLayoutPanel()
      {
        AutoSize = true,
        BackColor = Color.Transparent,
        ColumnCount = 4,
        RowStyles =
        {
          new RowStyle(SizeType.AutoSize),
        },
      };
      Label lblFromDate = new Label()
      {
        Dock = DockStyle.Fill,
        Font = new Font("Segoe UI", 12),
        Text = "Từ ngày:",
        TextAlign = ContentAlignment.MiddleLeft,
      };
      Label lblToDate = new Label()
      {
        Dock = DockStyle.Fill,
        Font = new Font("Segoe UI", 12),
        Text = "Đến ngày:",
        TextAlign = ContentAlignment.MiddleLeft,
      };
      dtpFrom = new DateTimePicker()
      {
        CalendarFont = new Font("Segoe UI", 10),
        CalendarMonthBackground = Color.White,
        Dock = DockStyle.Fill,
        Enabled = false,
        Font = new Font("Segoe UI", 12),
        Format = DateTimePickerFormat.Custom,
        CustomFormat = "dd/MM/yyyy",
        Size = new Size(160, 32),
        Value = DateTime.Today.AddDays(-30)
      };
      dtpFrom.ValueChanged += dtpFrom_ValueChanged;
      dtpTo = new DateTimePicker()
      {
        CalendarFont = new Font("Segoe UI", 10),
        CalendarMonthBackground = Color.White,
        Dock = DockStyle.Fill,
        Enabled = false,
        Font = new Font("Segoe UI", 12),
        Format = DateTimePickerFormat.Custom,
        CustomFormat = "dd/MM/yyyy",
        Size = new Size(160, 32),
        Value = DateTime.Today
      };
      dtpTo.ValueChanged += dtpTo_ValueChanged;


      //Init Date Range Bar Picker
      FlowLayoutPanel flpNavTab = new FlowLayoutPanel()
      {
        AutoSize = true,
        BackColor = Color.Transparent,
        Dock = DockStyle.Right,
      };
      Button btnThisMonth = new Button()
      {
        Name = "btnThisMonth",
        Text = "Tháng này",
        Font = new Font("Segoe UI", 10),
        FlatAppearance =
        {
          BorderSize = 1,
          BorderColor = Color.Gray,
          MouseOverBackColor = Color.FromArgb(254, 86, 37),
          MouseDownBackColor = Color.FromArgb(254, 86, 37)
        },
        FlatStyle = FlatStyle.Flat,
        Margin = new Padding(0),
        BackColor = this.BackColor,
        ForeColor = Color.Black,
        Size = new Size(112, 32),
      };
      btnThisMonth.Click += btnDateRange_Click;
      Button btnLast30Days = new Button()
      {
        Name = "btnLast30Days",
        Text = "30 ngày trước",
        Font = new Font("Segoe UI", 10),
        FlatAppearance =
        {
          BorderSize = 1,
          BorderColor = Color.Gray,
          MouseOverBackColor = Color.FromArgb(254, 86, 37),
          MouseDownBackColor = Color.FromArgb(254, 86, 37),
          CheckedBackColor = Color.FromArgb(254, 86, 37),
        },
        FlatStyle = FlatStyle.Flat,
        Margin = new Padding(0),
        BackColor = this.BackColor,
        ForeColor = Color.Black,
        Size = new Size(112, 32),
      };
      btnLast30Days.Click += btnDateRange_Click;
      Button btnLast7Days = new Button()
      {
        Name = "btnLast7Days",
        Text = "7 ngày trước",
        Font = new Font("Segoe UI", 10),
        FlatAppearance =
        {
          BorderSize = 1,
          BorderColor = Color.Gray,
          MouseOverBackColor = Color.FromArgb(254, 86, 37),
          MouseDownBackColor = Color.FromArgb(254, 86, 37)
        },
        FlatStyle = FlatStyle.Flat,
        Margin = new Padding(0),
        BackColor = this.BackColor,
        ForeColor = Color.Black,
        Size = new Size(112, 32)
      };
      btnLast7Days.Click += btnDateRange_Click;
      Button btnToday = new Button()
      {
        Name = "btnToday",
        Text = "Hôm nay",
        Font = new Font("Segoe UI", 10),
        FlatAppearance =
        {
          BorderSize = 1,
          BorderColor = Color.Gray,
          MouseOverBackColor = Color.FromArgb(254, 86, 37),
          MouseDownBackColor = Color.FromArgb(254, 86, 37)
        },
        FlatStyle = FlatStyle.Flat,
        Margin = new Padding(0),
        BackColor = this.BackColor,
        ForeColor = Color.Black,
        Size = new Size(112, 32)
      };
      btnToday.Click += btnDateRange_Click;
      Button btnCustomDate = new Button()
      {
        Name = "btnCustomDate",
        Text = "Tùy chỉnh",
        Font = new Font("Segoe UI", 10),
        FlatAppearance =
        {
          BorderSize = 1,
          BorderColor = Color.Gray,
          MouseOverBackColor = Color.FromArgb(254, 86, 37),
          MouseDownBackColor = Color.FromArgb(254, 86, 37)
        },
        FlatStyle = FlatStyle.Flat,
        Margin = new Padding(0),
        BackColor = this.BackColor,
        ForeColor = Color.Black,
        Size = new Size(112, 32)
      };
      btnCustomDate.Click += btnDateRange_Click;

      //Add controls to panels
      panelDateFromTo.Controls.AddRange(new Control[] {
        lblFromDate,
        dtpFrom,
        lblToDate,
        dtpTo,
      });
      flpNavTab.Controls.AddRange(new Control[] {
        btnCustomDate,
        btnToday,
        btnLast7Days,
        btnLast30Days,
        btnThisMonth,
      });
      panelDateRangePicker.Controls.AddRange(new Control[] {
        panelDateFromTo,
        flpNavTab
      });

      this.Controls.Add(panelDateRangePicker);

      panelDateRangePicker.Controls.Add(panelDateFromTo, 0, 0);
      panelDateRangePicker.Controls.Add(flpNavTab, 1, 0);

      mainLayoutPanel.Controls.Add(panelDateRangePicker, 0, 0);

      SetSelectedBtn(btnLast30Days);
    }
    private void InitializeTablePanelStatistic()
    {
      tablePanelStatistic = new TableLayoutPanel()
      {
        Dock = DockStyle.Fill,
        ColumnCount = 3,
        ColumnStyles = {
          new ColumnStyle(SizeType.Percent, 20F),
          new ColumnStyle(SizeType.Percent, 50F),
          new ColumnStyle(SizeType.Percent, 30F),
        },
        RowCount = 3,
        RowStyles = {
          new ColumnStyle(SizeType.Percent, 10F),
          new ColumnStyle(SizeType.Percent, 50F),
          new ColumnStyle(SizeType.Percent, 40F),
        },
      };
      mainLayoutPanel.Controls.Add(tablePanelStatistic, 0, 1);
    }
    private void InitializeReportSummary()
    {
      //Init Panel Report Summary
      tablePanelReportSummary = new TableLayoutPanel()
      {
        Dock = DockStyle.Fill,
        ColumnCount = 2,
        ColumnStyles =
        {
          new ColumnStyle(SizeType.Percent, 50),
          new ColumnStyle(SizeType.Percent, 50),
        },
      };

      //Init Panel Receipt Total
      FlowLayoutPanel flpTotalReceipt = new FlowLayoutPanel()
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        FlowDirection = FlowDirection.TopDown,
        Name = "flpTotalReceipt",
        Padding = new Padding(4),
      };
      Label lblTotalReceiptLabel = new Label()
      {
        AutoSize = true,
        Font = new Font("Segoe UI", 12, FontStyle.Bold),
        ForeColor = Color.Black,
        Text = "Số lượng hóa đơn:",
        TextAlign = ContentAlignment.MiddleLeft,
      };
      Label lblTotalReceipt = new Label()
      {
        Font = new Font("Segoe UI", 12),
        ForeColor = Color.Black,
        Name = "lblTotalReceipt",
        TextAlign = ContentAlignment.MiddleLeft,
      };

      //Init Panel Gross Revenue
      FlowLayoutPanel flpGrossRevenue = new FlowLayoutPanel()
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        FlowDirection = FlowDirection.TopDown,
        Name = "flpGrossRevenue",
        Padding = new Padding(4),
      };
      Label lblGrossRevenueLabel = new Label()
      {
        AutoSize = true,
        Font = new Font("Segoe UI", 12, FontStyle.Bold),
        ForeColor = Color.Black,
        Text = "Tổng doanh thu:",
        TextAlign = ContentAlignment.MiddleLeft,
      };
      Label lblGrossRevenue = new Label()
      {
        Font = new Font("Segoe UI", 12),
        ForeColor = Color.Black,
        TextAlign = ContentAlignment.MiddleLeft,
        Name = "lblGrossRevenue",
      };

      //Add controls to panels
      flpTotalReceipt.Controls.AddRange(new Control[] {
        lblTotalReceiptLabel,
        lblTotalReceipt
      });
      flpGrossRevenue.Controls.AddRange(new Control[] {
        lblGrossRevenueLabel,
        lblGrossRevenue
      });

      tablePanelReportSummary.Controls.Add(flpTotalReceipt, 0, 0);
      tablePanelReportSummary.SetColumnSpan(flpTotalReceipt, 1);

      tablePanelReportSummary.Controls.Add(flpGrossRevenue, 1, 0);
      tablePanelReportSummary.SetColumnSpan(flpGrossRevenue, 1);

      tablePanelStatistic.Controls.Add(tablePanelReportSummary, 0, 0);

      tablePanelStatistic.SetColumnSpan(tablePanelReportSummary, 2);
    }
    private void InitializeGrossRevenueChart()
    {
      //Init Gross Revenue Chart
      panelGrossRevenueChart = new Panel()
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
      };

            Chart chartGrossRevenue = new Chart()
            {
                Dock = DockStyle.Fill,
                Name = "chartGrossRevenue",
                Titles =
                {
                    new Title()
                    {
                        Name = "GrossRevenueTitle",
                        Font = new Font("Segoe UI", 12, FontStyle.Bold),
                        Text = "Doanh thu theo thời gian",
                        Alignment = ContentAlignment.TopLeft,
                    }
                },
                ChartAreas =
                {
                    new ChartArea()
                    {
                        Name = "GrossRevenueChartArea",
                        AxisX = new Axis(){
                            MajorGrid = new Grid()
                            {
                                LineColor = Color.Silver,
                            }
                        },
                        AxisY = new Axis()
                        {
                            LabelStyle = new LabelStyle()
                            {
                                Format = "N0",
                            },
                            MajorGrid = new Grid()
                            {
                                LineColor = Color.LightGray,
                            }
                        }
                    }
                },
                Series =
        {
          new Series()
          {
            Name = "Doanh thu",
            Color = Color.FromArgb(254, 86, 37),
            IsValueShownAsLabel = true,
            Font = new Font("Segoe UI", 10),
            LabelForeColor = Color.Black,
            LabelBackColor = Color.White,
            ChartType = SeriesChartType.SplineArea,
            BackGradientStyle = GradientStyle.DiagonalLeft,
            BackSecondaryColor = Color.FromArgb(255, 128, 128),
            LabelFormat = "N0",
          }
        },
        Legends =
        {
          new Legend()
          {
            Name = "DoanhThuLegend",
            Docking = Docking.Bottom,
            ForeColor = Color.DimGray,
            Font = new Font("Segoe UI", 12),
            IsTextAutoFit = false,
            
          }
        },
      };

      panelGrossRevenueChart.Controls.Add(chartGrossRevenue);

      tablePanelStatistic.Controls.Add(panelGrossRevenueChart, 0, 1);
      tablePanelStatistic.SetColumnSpan(panelGrossRevenueChart, 2);
    }
    private void InitializeTopProductChart()
    {
      //Init Panel Top Product Chart
      panelTopProductChart = new Panel()
      {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
      };

      Chart chartTopProduct = new Chart()
      {
        Dock = DockStyle.Fill,
        Name = "chartTopProduct",
        Titles =
        {
          new Title()
          {
            Name = "TopProductTitle",
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            Text = "Top 5 sản phẩm bán chạy",
            Alignment = ContentAlignment.TopLeft,
          }
        },
        ChartAreas =
        {
          new ChartArea()
          {
            Name = "TopProductChartArea",
          }
        },
        Series =
        {
          new Series()
          {
            Name = "TopProductSeries",
            LabelForeColor = Color.White,
            BackGradientStyle = GradientStyle.DiagonalLeft,
            BackSecondaryColor = Color.FromArgb(255, 192, 255),
            IsValueShownAsLabel = true,
            Font = new Font("Segoe UI", 10),
            ChartType = SeriesChartType.Doughnut,
            CustomProperties = "DoughnutRadius=64",
            BorderWidth = 4,
            BorderColor = Color.White,
          }
        },
        Legends =
        {
          new Legend()
          {
            Name = "TopProductLegend",
            Docking = Docking.Bottom,
            ForeColor = Color.DimGray,
            Font = new Font("Segoe UI", 12),
            IsTextAutoFit = false,
          }
        },
        Palette = ChartColorPalette.None,
        PaletteCustomColors = new Color[] {
          Color.FromArgb(31, 1, 185),
          Color.FromArgb(30, 255, 188),
          Color.FromArgb(222, 77, 134),
          Color.FromArgb(180, 227, 61),
          Color.FromArgb(252, 68, 15)
        }
      };

      panelTopProductChart.Controls.Add(chartTopProduct);

      tablePanelStatistic.Controls.Add(panelTopProductChart, 3, 0);
      tablePanelStatistic.SetRowSpan(panelTopProductChart, 3);
    }
    private void InitializeStoreSummary()
    {
      tablePanelStoreSummary = new TableLayoutPanel()
      {
        Name = "tablePanelStoreSummary",
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        ColumnCount = 1,
        RowCount = 4,
        Padding = new Padding(8),
        AutoSize = true,
        RowStyles = {
          new RowStyle(SizeType.Percent, 10F),
          new RowStyle(SizeType.Percent, 30F),
          new RowStyle(SizeType.Percent, 30F),
          new RowStyle(SizeType.Percent, 30F)
        }
      };

      //Init Store Summary Controls
      Label lblStoreSummaryLabel = new Label()
      {
        AutoSize = true,
        Font = new Font("Segoe UI", 12, FontStyle.Bold),
        Text = "Cửa hàng:",
        TextAlign = ContentAlignment.MiddleLeft,
      };
      FlowLayoutPanel flpTotalMember = new FlowLayoutPanel()
      {
        Name = "flpTotalMember",
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.TopDown,
      };
      Label lblTotalMemberLabel = new Label()
      {
        AutoSize = true,
        Font = new Font("Segoe UI", 12, FontStyle.Bold),
        Text = "Số lượng hội viên:",
        TextAlign = ContentAlignment.MiddleLeft,
        Margin = new Padding(8, 0, 0, 0),
      };
      Label lblTotalMember = new Label()
      {
        Name = "lblTotalMember",
        Font = new Font("Segoe UI", 12),
        TextAlign = ContentAlignment.MiddleLeft,
        Margin = new Padding(8, 0, 0, 16),
      };
      FlowLayoutPanel flpTotalProduct = new FlowLayoutPanel()
      {
        Name = "flpTotalProduct",
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.TopDown,
      };
      Label lblTotalProductLabel = new Label()
      {
        AutoSize = true,
        Font = new Font("Segoe UI", 12, FontStyle.Bold),
        Text = "Số lượng sản phẩm:",
        TextAlign = ContentAlignment.MiddleLeft,
        Margin = new Padding(8, 0, 0, 0),
      };
      Label lblTotalProduct = new Label()
      {
        Name = "lblTotalProduct",
        Font = new Font("Segoe UI", 12),
        TextAlign = ContentAlignment.MiddleLeft,
        Margin = new Padding(8, 0, 0, 16),
      };
      FlowLayoutPanel flpTotalSupplier = new FlowLayoutPanel()
      {
        Name = "flpTotalSupplier",
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.TopDown,
      };
      Label lblTotalSupplierLabel = new Label()
      {
        AutoSize = true,
        Font = new Font("Segoe UI", 12, FontStyle.Bold),
        Text = "Số lượng nhà cung cấp:",
        TextAlign = ContentAlignment.MiddleLeft,
        Margin = new Padding(8, 0, 0, 0),
      };
      Label lblTotalSupplier = new Label()
      {
        Name = "lblTotalSupplier",
        Font = new Font("Segoe UI", 12),
        TextAlign = ContentAlignment.MiddleLeft,
        Margin = new Padding(8, 0, 0, 16),
      };

      flpTotalMember.Controls.AddRange(new Control[] {
        lblTotalMemberLabel,
        lblTotalMember
      });
      flpTotalProduct.Controls.AddRange(new Control[] {
        lblTotalProductLabel,
        lblTotalProduct
      });
      flpTotalSupplier.Controls.AddRange(new Control[] {
        lblTotalSupplierLabel,
        lblTotalSupplier
      });

      tablePanelStoreSummary.Controls.Add(lblStoreSummaryLabel, 0, 0);
      tablePanelStoreSummary.Controls.Add(flpTotalMember, 0, 1);
      tablePanelStoreSummary.Controls.Add(flpTotalProduct, 0, 2);
      tablePanelStoreSummary.Controls.Add(flpTotalSupplier, 0, 3);

      tablePanelStatistic.Controls.Add(tablePanelStoreSummary, 0, 2);
      tablePanelStatistic.SetColumnSpan(tablePanelStoreSummary, 1);
    }
    private void InitializeDgvUnderstock()
    {
      //Init Panel Report Summary
      dgvUnderstock = new DataGridView()
      {
        Dock = DockStyle.Fill,
        Name = "dgvUnderstock",
        BackgroundColor = Color.White,
        BorderStyle = BorderStyle.None,
        CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
        ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
        EnableHeadersVisualStyles = false,
        AutoGenerateColumns = false,
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        ReadOnly = true,
        RowHeadersVisible = false,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
        Columns = {
          new DataGridViewTextBoxColumn()
          {
            Name = "Key",
            DataPropertyName = "Key",
            HeaderText = "Tên sản phẩm",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
          },
          new DataGridViewTextBoxColumn()
          {
            Name = "Value",
            DataPropertyName = "Value",
            HeaderText = "Số lượng",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            Width=100,
          }
        },
        RowsDefaultCellStyle = new DataGridViewCellStyle()
        {
            BackColor = Color.White,
            SelectionBackColor = Color.FromArgb(254, 86, 37),
        },
        ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
        {
            SelectionBackColor = Color.White,
            BackColor = Color.White,
        }
      };
      tablePanelStatistic.Controls.Add(dgvUnderstock, 1, 2);
    }
    private void StatisticFormGUI_Load(object sender, EventArgs e)
    {
      bus.GetStatisticData(StatisticData);
      cachedFromDate = dtpFrom.Value;
      cachedToDate = dtpTo.Value;

      LoadData();
    }
    private void LoadData()
    {
      StatisticData.NgBatDau = dtpFrom.Value;
      StatisticData.NgKetThuc = dtpTo.Value;

      //get Statistic Data
      bus.GetStatisticData(StatisticData);

      //load report summary data
      //ToString("#,##0") + " ₫") -> 123,456,328 ₫
      FlowLayoutPanel panelReceipt = (FlowLayoutPanel)tablePanelReportSummary.Controls["flpTotalReceipt"];
      FlowLayoutPanel panelGrossRevenue = (FlowLayoutPanel)tablePanelReportSummary.Controls["flpGrossRevenue"];

      panelReceipt.Controls["lblTotalReceipt"].Text = StatisticData.SoHoaDon.ToString();
      panelGrossRevenue.Controls["lblGrossRevenue"].Text = StatisticData.TongDoanhThu.ToString("#,##0") + " ₫";

      //load store summary data
      FlowLayoutPanel panelTotalMember = (FlowLayoutPanel)tablePanelStoreSummary.Controls["flpTotalMember"];
      FlowLayoutPanel panelTotalProduct = (FlowLayoutPanel)tablePanelStoreSummary.Controls["flpTotalProduct"];
      FlowLayoutPanel panelTotalSupplier = (FlowLayoutPanel)tablePanelStoreSummary.Controls["flpTotalSupplier"];

      panelTotalMember.Controls["lblTotalMember"].Text = StatisticData.SoHoiVien.ToString();
      panelTotalProduct.Controls["lblTotalProduct"].Text = StatisticData.SoSanPham.ToString();
      panelTotalSupplier.Controls["lblTotalSupplier"].Text = StatisticData.SoNCC.ToString();

      Chart chartGrossRevenue = (Chart)panelGrossRevenueChart.Controls["chartGrossRevenue"];
      Chart chartTopProduct = (Chart)panelTopProductChart.Controls["chartTopProduct"];

            //change to charttype column
            chartGrossRevenue.Series[0].ChartType = StatisticData.DsDoanhThu.Count == 1 ? 
                SeriesChartType.Column : SeriesChartType.SplineArea;

      //binding gross revenue data
      chartGrossRevenue.DataSource = StatisticData.DsDoanhThu;
      chartGrossRevenue.Series[0].XValueMember = "ThoiGian";
      chartGrossRevenue.Series[0].YValueMembers = "TongTien";     
      chartGrossRevenue.DataBind();
            foreach (var point in chartGrossRevenue.Series[0].Points)
            {
                point.LabelBackColor = Color.Transparent;
            }

            //binding top product data
            chartTopProduct.DataSource = StatisticData.DsSPBanChay;
      chartTopProduct.Series[0].XValueMember = "Key";
      chartTopProduct.Series[0].YValueMembers = "Value";
      chartTopProduct.DataBind();

      //binding understock data
      dgvUnderstock.DataSource = StatisticData.DsSPTonKho;
    }
    //event handler when btn date range click
    private void btnDateRange_Click(object sender, EventArgs e)
    {
      Button selectedBtn = (Button)sender;

      //set as today
      dtpFrom.Value = DateTime.Today;
      dtpTo.Value = DateTime.Now;

      //change based on selected button
      if (selectedBtn.Name == "btnLast7Days")
      {
        dtpFrom.Value = DateTime.Today.AddDays(-7);
      }
      else if (selectedBtn.Name == "btnLast30Days")
      {
        dtpFrom.Value = DateTime.Today.AddDays(-30);
      }
      else if (selectedBtn.Name == "btnThisMonth")
      {
        dtpFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
      }
      else if (selectedBtn.Name == "btnCustomDate")
      {
        dtpFrom.Enabled = true;
        dtpTo.Enabled = true;
      }
      LoadData();
      SetSelectedBtn(selectedBtn);
    }
    //set selected button
    private void SetSelectedBtn(Button selectedBtn)
    {
      selectedBtn.BackColor = Color.FromArgb(254, 86, 37);
      selectedBtn.ForeColor = Color.White;
      selectedBtn.FlatAppearance.BorderColor = Color.FromArgb(254, 86, 37);

      if (currentBtn != null && currentBtn != selectedBtn)
      {
        currentBtn.BackColor = this.BackColor;
        currentBtn.ForeColor = Color.Black;
        currentBtn.FlatAppearance.BorderColor = Color.Gray;
      }
      currentBtn = selectedBtn;
    }
    //event handlers for date range picker value changed
    private void dtpFrom_ValueChanged(object sender, EventArgs e)
    {
      if (dtpFrom.Value < cachedFromDate) {
        bus.GetStatisticData(StatisticData);
        MessageBox.Show("Re-queried!");
      }
      LoadData();
    }
    private void dtpTo_ValueChanged(object sender, EventArgs e)
    {
      if (dtpTo.Value > cachedToDate)
      {
        bus.GetStatisticData(StatisticData);
      }
      LoadData();
    }
  }
}

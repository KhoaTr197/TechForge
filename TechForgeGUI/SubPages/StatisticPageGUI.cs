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
    public partial class StatisticPageGUI : Form
    {
        private DoanhThuBUS bus { get; set; }
        private DoanhThuDTO dto { get; set; }

        private Button currentBtn;


        protected readonly string connStr = "Data Source=.;Initial Catalog=TECHFORGE;Integrated Security=True;";

        public StatisticPageGUI()
        {
            InitializeComponent();
            
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
            this.BackColor = Color.FromArgb(244, 244, 248);
            
            //
            dgvUnderstock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //
            this.bus = new DoanhThuBUS(connStr);
            this.dto = new DoanhThuDTO();
            dto.NgBatDau = dtpStart.Value;
            dto.NgKetThuc = dtpEnd.Value;
            //default - last 30 days
            dtpStart.Value = DateTime.Today.AddDays(-30);
            dtpEnd.Value = DateTime.Now;
            SetSelectedBtn(btnLast30days);
            DisableCustomDate();

        }

        private void StatisticFormGUI_Load(object sender, EventArgs e)
        {
            LoadData(dtpStart.Value, dtpEnd.Value);
        }

        private void LoadData(DateTime startDate, DateTime endDate)
        {
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, 59);
            //
            if (startDate != dto.NgBatDau || endDate != dto.NgKetThuc)
            {
                dto.NgBatDau = startDate;
                dto.NgKetThuc = endDate;
                //setup data
                bus.Setup(dto);

                //ToString("#,##0") + " ₫") -> 123,456,328 ₫
                lblGrossRevenue.Text = dto.TongDoanhThu.ToString("#,##0") + " ₫";
                lblTotalMember.Text = dto.SoHoiVien.ToString();
                lblTotalOrder.Text = dto.SoHoaDon.ToString();
                lblTotalProduct.Text = dto.SoSanPham.ToString();
                lblTotalSupplier.Text = dto.SoNCC.ToString();

                chartGrossRevenue.Series[0].ChartType = SeriesChartType.SplineArea;
                //change to charttype column
                if (dto.DoanhThuList.Count == 1)
                {
                    chartGrossRevenue.Series[0].ChartType = SeriesChartType.Column;
                }
                //binding data
                chartGrossRevenue.DataSource = dto.DoanhThuList;
                chartGrossRevenue.Series[0].XValueMember = "ThoiGian";
                chartGrossRevenue.Series[0].YValueMembers = "TongTien";
                chartGrossRevenue.DataBind();
                //
                chartTopProduct.DataSource = dto.SPBanChayList;
                chartTopProduct.Series[0].XValueMember = "Key";
                chartTopProduct.Series[0].YValueMembers = "Value";
                chartTopProduct.DataBind();
                //
                dgvUnderstock.DataSource = dto.SPTonKhoList;
                dgvUnderstock.Columns[0].HeaderText = "Tên Sản Phẩm";
                dgvUnderstock.Columns[1].HeaderText = "Số lượng";
                dgvUnderstock.Columns[1].Width = 100;
            }
        }
        private void DisableCustomDate()
        {
            btnOk.Visible = false;
            dtpStart.Enabled = false;
            dtpEnd.Enabled = false;
        }
        private void btnToday_Click(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Now;
            LoadData(dtpStart.Value, dtpEnd.Value);
            DisableCustomDate();
            SetSelectedBtn(sender);
        }

        private void btnLast7days_Click(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Today.AddDays(-7);
            dtpEnd.Value = DateTime.Now;
            LoadData(dtpStart.Value, dtpEnd.Value);
            DisableCustomDate();
            SetSelectedBtn(sender);
        }

        private void btnLast30days_Click(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Today.AddDays(-30);
            dtpEnd.Value = DateTime.Now;
            LoadData(dtpStart.Value, dtpEnd.Value);
            DisableCustomDate();
            SetSelectedBtn(sender);
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            dtpStart.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEnd.Value = DateTime.Now;
            LoadData(dtpStart.Value, dtpEnd.Value);
            DisableCustomDate();
            SetSelectedBtn(sender);
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            btnOk.Visible = true;
            dtpStart.Enabled = true;
            dtpEnd.Enabled = true;
            SetSelectedBtn(sender);
        }

        private void SetSelectedBtn(object sender)
        {
            Button seletedBtn = (Button)sender;
            seletedBtn.BackColor = Color.FromArgb(255, 192, 192);
            seletedBtn.ForeColor = Color.White;

            if (currentBtn != null && currentBtn != seletedBtn)
            {
                currentBtn.BackColor = this.BackColor;
                currentBtn.ForeColor = Color.DarkGray;
            }
            currentBtn = seletedBtn;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            LoadData(dtpStart.Value, dtpEnd.Value);
        }

    }
}

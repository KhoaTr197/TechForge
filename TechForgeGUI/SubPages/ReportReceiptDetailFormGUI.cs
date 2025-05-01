using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeDTO;
using TechForgeBUS;
using System.Globalization;

namespace TechForgeGUI.SubPages
{
    public partial class ReportReceiptDetailFormGUI : Form
    {
        private HoaDonDTO thongtinhoadon {  get; set; }
        public ReportReceiptDetailFormGUI(HoaDonDTO _thongtinhoadon)
        {
            InitializeComponent();
            this.thongtinhoadon = _thongtinhoadon;
        }

        private void ReportReceiptDetailFormGUI_Load(object sender, EventArgs e)
        {

            rpvReceiptDetail.LocalReport.ReportEmbeddedResource = "TechForgeGUI.Reports.rptReceiptDetail.rdlc";
            if (thongtinhoadon?.Cthd == null || !thongtinhoadon.Cthd.Any())
            {
                MessageBox.Show("Không có chi tiết hóa đơn để hiển thị.");
                return;
            }
            ReportDataSource rpdsCTHD = new ReportDataSource() 
            {
                Name = "dsCTHD",
                Value = thongtinhoadon.Cthd
            };

            ReportDataSource rpdsHoaDon = new ReportDataSource()
            {
                Name = "dsHoaDon",
                Value = new List<HoaDonDTO> { thongtinhoadon }
            };

            rpvReceiptDetail.LocalReport.DataSources.Add(rpdsCTHD);
            rpvReceiptDetail.LocalReport.DataSources.Add(rpdsHoaDon);

            this.rpvReceiptDetail.RefreshReport();
        }
    }
}

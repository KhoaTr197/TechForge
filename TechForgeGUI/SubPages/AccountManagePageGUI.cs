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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TechForgeGUI.SubPages
{
    public partial class AccountManagePageGUI : ManagePage
    {
        private List<TaiKhoanDTO> dsTaiKhoan { get; set; }
        private TaiKhoanBUS taiKhoanBus { get; set; }
        private NguoiDungBUS nguoiDungBus { get; set; }
        private List<NguoiDungDTO> dsNguoiDung { get; set; }

        private List<NguoiDungDTO> dsNDChuaCoTK { get; set; }

        public AccountManagePageGUI()
        {
            InitializeComponent();

            InitializeBUS();
            GetData();
            AddColumns();
            LoadData();
            SetUpFeature();

            dgvMainList.dgvList.CellClick += dgvList_CellClick;

            btnAdd.Click += BtnAdd_Click;

            btnSearch.Click += BtnSearch_Click;
        }

        protected void InitializeBUS()
        {
            taiKhoanBus = new TaiKhoanBUS(this.connStr);
            nguoiDungBus = new NguoiDungBUS(this.connStr);
        }
        private void SetUpFeature()
        {
            summaryCards.Controls.Clear();
            summaryCards.Add(new SummaryCard[]
            {
                new SummaryCard("Số tài khoản", dsTaiKhoan.Count.ToString(), "box_icon", Color.FromArgb(52, 152, 219)),
                new SummaryCard("Số tài khoản kích hoạt", dsTaiKhoan.Where(tk => tk.TrangThai).Count().ToString(), "box_icon", Color.FromArgb(46, 204, 113)),
            });

        }
        protected void GetData()
        {
            dsTaiKhoan = taiKhoanBus.GetAllConnected();
            dsNguoiDung = nguoiDungBus.GetAllConnected();
            dsNDChuaCoTK = dsNguoiDung.Where(nd => dsTaiKhoan.All(tk => tk.MaND != nd.MaND)).ToList();
        }

        protected void LoadData()
        {
            dgvMainList.Binding(dsTaiKhoan);
            dgvMainList.dgvList.AutoGenerateColumns = false;
        }

        protected void AddColumns()
        {
            this.SuspendLayout();

            dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MaND",
                HeaderText = "Mã người dùng",
                DataPropertyName = "MaND",
                FillWeight = 32,
                Visible = true
            });
            dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenTK",
                HeaderText = "Tên tài khoản",
                DataPropertyName = "TenTK",
                Visible = true
            });
            dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MatKhau",
                HeaderText = "Mật khẩu",
                DataPropertyName = "MatKhau",
                Visible = false
            });
            dgvMainList.dgvList.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TrangThai",
                HeaderText = "Trạng Thái",
                DataPropertyName = "TrangThai",
                Visible = true
            });

            // Attach event handler for cell formatting
            dgvMainList.dgvList.CellFormatting += dgvList_CellFormatting;

            this.ResumeLayout();
        }

        protected void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                if (dgvMainList.dgvList.Columns[e.ColumnIndex].Name == "TrangThai")
                {
                    bool status = (bool)e.Value;
                    if (status)
                    {
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.BackColor = Color.Green;
                        e.Value = "Kích hoạt";
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.White;
                        e.CellStyle.BackColor = Color.Red;
                        e.Value = "Huỷ kích hoạt";
                    }
                }
            }
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (dsNDChuaCoTK.Count == 0)
            {
                MessageBox.Show("Tất cả Người dùng đã có Tài khoản, hãy thêm Người dùng trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AccountDetailFormGUI DetailForm = new AccountDetailFormGUI(taiKhoanBus, dsNDChuaCoTK);
            OverlayFormGUI Overlay = new OverlayFormGUI(Form.ActiveForm, DetailForm);

            Overlay.Show(Form.ActiveForm);
            DetailForm.Show(Form.ActiveForm);

            DetailForm.AddSubmit += DetailsForm_AddSubmit;
        }

        private void DetailsForm_AddSubmit(object sender, DetailFormAddSubmitEventArgs e)
        {
            GetData();
            LoadData();

            SetUpFeature();
        }

        private void DetailsForm_EditSubmit(object sender, DetailFormEditSubmitEventArgs e)
        {
            GetData();
            LoadData();

            SetUpFeature();
        }
        private void DetailsForm_DeleteSubmit(object sender, DetailFormDeleteSubmitEventArgs e)
        {
            GetData();
            LoadData();

            SetUpFeature();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            List<TaiKhoanDTO> dsKetQua = taiKhoanBus.FindByAnyProperty(txtSearch.Text.Trim().ToLower());
            if (dsKetQua.Count == 0)
            {
                MessageBox.Show("Không có kết quả phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dsTaiKhoan = dsKetQua;
            LoadData();
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridView dgvMainList = (DataGridView)sender;

                if (dgvMainList.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvMainList.SelectedRows[0];
                    TaiKhoanDTO taiKhoan = dsTaiKhoan.Find(nd => nd.MaND == selectedRow.Cells[0].Value.ToString());

                    AccountDetailFormGUI DetailForm = new AccountDetailFormGUI(taiKhoanBus, dsNDChuaCoTK, taiKhoan);
                    OverlayFormGUI Overlay = new OverlayFormGUI(Form.ActiveForm, DetailForm);

                    Overlay.Show(Form.ActiveForm);
                    DetailForm.Show(Form.ActiveForm);

                    // Assign event handler for submits
                    DetailForm.AddSubmit += DetailsForm_AddSubmit;
                    DetailForm.EditSubmit += DetailsForm_EditSubmit;
                    DetailForm.DeleteSubmit += DetailsForm_DeleteSubmit;
                }
            }
        }

    }
}

namespace TechForgeGUI.SubPages
{
  partial class ImportExportDetailFormGUI
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
      this.dgvDetail = new System.Windows.Forms.DataGridView();
      this.dgvTxtColMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tenSPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.hinhAnhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewImageColumn();
      this.giaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.soLuongDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.thanhTienDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.maLSDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.hoatDongDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.chiTietLichSuKhoDTOBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.tlpInfo = new System.Windows.Forms.TableLayoutPanel();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.dtpThoiGian = new System.Windows.Forms.DateTimePicker();
      this.txtMa = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.nudTongTien = new System.Windows.Forms.NumericUpDown();
      this.label4 = new System.Windows.Forms.Label();
      this.cboNhanVienLap = new System.Windows.Forms.ComboBox();
      this.cboHoatDong = new System.Windows.Forms.ComboBox();
      this.tlpDetailInfo = new System.Windows.Forms.TableLayoutPanel();
      this.txtMaSP = new System.Windows.Forms.TextBox();
      this.label9 = new System.Windows.Forms.Label();
      this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
      this.nudGia = new System.Windows.Forms.NumericUpDown();
      this.txtTenSP = new System.Windows.Forms.TextBox();
      this.label12 = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.txtSearch = new System.Windows.Forms.TextBox();
      this.lstSearchResults = new System.Windows.Forms.ListBox();
      this.label7 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.nudChiTietTongTien = new System.Windows.Forms.NumericUpDown();
      this.tlpDetailAction = new System.Windows.Forms.TableLayoutPanel();
      this.btnChiTietThem = new System.Windows.Forms.Button();
      this.btnChiTietCapNhat = new System.Windows.Forms.Button();
      this.tlpMain.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.chiTietLichSuKhoDTOBindingSource)).BeginInit();
      this.tlpInfo.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudTongTien)).BeginInit();
      this.tlpDetailInfo.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudGia)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChiTietTongTien)).BeginInit();
      this.tlpDetailAction.SuspendLayout();
      this.SuspendLayout();
      // 
      // tlpMain
      // 
      this.tlpMain.ColumnCount = 2;
      this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
      this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
      this.tlpMain.Controls.Add(this.dgvDetail, 0, 1);
      this.tlpMain.Controls.Add(this.tlpInfo, 0, 0);
      this.tlpMain.Controls.Add(this.tlpDetailInfo, 1, 0);
      this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tlpMain.Location = new System.Drawing.Point(0, 0);
      this.tlpMain.Name = "tlpMain";
      this.tlpMain.RowCount = 2;
      this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.81818F));
      this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.18182F));
      this.tlpMain.Size = new System.Drawing.Size(922, 436);
      this.tlpMain.TabIndex = 1;
      // 
      // dgvDetail
      // 
      this.dgvDetail.AllowUserToAddRows = false;
      this.dgvDetail.AllowUserToDeleteRows = false;
      this.dgvDetail.AutoGenerateColumns = false;
      this.dgvDetail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvDetail.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
      this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvTxtColMaSP,
            this.tenSPDataGridViewTextBoxColumn,
            this.hinhAnhDataGridViewTextBoxColumn,
            this.giaDataGridViewTextBoxColumn,
            this.soLuongDataGridViewTextBoxColumn,
            this.thanhTienDataGridViewTextBoxColumn,
            this.maLSDataGridViewTextBoxColumn,
            this.hoatDongDataGridViewCheckBoxColumn});
      this.dgvDetail.DataSource = this.chiTietLichSuKhoDTOBindingSource;
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvDetail.DefaultCellStyle = dataGridViewCellStyle4;
      this.dgvDetail.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvDetail.Location = new System.Drawing.Point(4, 142);
      this.dgvDetail.Margin = new System.Windows.Forms.Padding(4);
      this.dgvDetail.Name = "dgvDetail";
      this.dgvDetail.ReadOnly = true;
      this.dgvDetail.RowHeadersVisible = false;
      this.dgvDetail.RowTemplate.Height = 48;
      this.dgvDetail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgvDetail.Size = new System.Drawing.Size(591, 290);
      this.dgvDetail.TabIndex = 0;
      this.dgvDetail.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDetail_CellFormatting);
      this.dgvDetail.SelectionChanged += new System.EventHandler(this.dgvDetail_SelectionChanged);
      // 
      // dgvTxtColMaSP
      // 
      this.dgvTxtColMaSP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
      this.dgvTxtColMaSP.DataPropertyName = "MaSP";
      this.dgvTxtColMaSP.FillWeight = 120.203F;
      this.dgvTxtColMaSP.HeaderText = "Mã SP";
      this.dgvTxtColMaSP.Name = "dgvTxtColMaSP";
      this.dgvTxtColMaSP.ReadOnly = true;
      this.dgvTxtColMaSP.Width = 73;
      // 
      // tenSPDataGridViewTextBoxColumn
      // 
      this.tenSPDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.tenSPDataGridViewTextBoxColumn.DataPropertyName = "TenSP";
      this.tenSPDataGridViewTextBoxColumn.FillWeight = 61.7467F;
      this.tenSPDataGridViewTextBoxColumn.HeaderText = "Tên SP";
      this.tenSPDataGridViewTextBoxColumn.Name = "tenSPDataGridViewTextBoxColumn";
      this.tenSPDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // hinhAnhDataGridViewTextBoxColumn
      // 
      this.hinhAnhDataGridViewTextBoxColumn.DataPropertyName = "HinhAnh";
      this.hinhAnhDataGridViewTextBoxColumn.FillWeight = 80F;
      this.hinhAnhDataGridViewTextBoxColumn.HeaderText = "Hình Ảnh";
      this.hinhAnhDataGridViewTextBoxColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
      this.hinhAnhDataGridViewTextBoxColumn.Name = "hinhAnhDataGridViewTextBoxColumn";
      this.hinhAnhDataGridViewTextBoxColumn.ReadOnly = true;
      this.hinhAnhDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      // 
      // giaDataGridViewTextBoxColumn
      // 
      this.giaDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
      this.giaDataGridViewTextBoxColumn.DataPropertyName = "Gia";
      this.giaDataGridViewTextBoxColumn.FillWeight = 61.7467F;
      this.giaDataGridViewTextBoxColumn.HeaderText = "Giá";
      this.giaDataGridViewTextBoxColumn.Name = "giaDataGridViewTextBoxColumn";
      this.giaDataGridViewTextBoxColumn.ReadOnly = true;
      this.giaDataGridViewTextBoxColumn.Width = 54;
      // 
      // soLuongDataGridViewTextBoxColumn
      // 
      this.soLuongDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
      this.soLuongDataGridViewTextBoxColumn.DataPropertyName = "SoLuong";
      this.soLuongDataGridViewTextBoxColumn.FillWeight = 61.7467F;
      this.soLuongDataGridViewTextBoxColumn.HeaderText = "Số Lượng";
      this.soLuongDataGridViewTextBoxColumn.Name = "soLuongDataGridViewTextBoxColumn";
      this.soLuongDataGridViewTextBoxColumn.ReadOnly = true;
      this.soLuongDataGridViewTextBoxColumn.Width = 92;
      // 
      // thanhTienDataGridViewTextBoxColumn
      // 
      this.thanhTienDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
      this.thanhTienDataGridViewTextBoxColumn.DataPropertyName = "ThanhTien";
      this.thanhTienDataGridViewTextBoxColumn.FillWeight = 61.7467F;
      this.thanhTienDataGridViewTextBoxColumn.HeaderText = "Thành Tiền";
      this.thanhTienDataGridViewTextBoxColumn.Name = "thanhTienDataGridViewTextBoxColumn";
      this.thanhTienDataGridViewTextBoxColumn.ReadOnly = true;
      this.thanhTienDataGridViewTextBoxColumn.Width = 101;
      // 
      // maLSDataGridViewTextBoxColumn
      // 
      this.maLSDataGridViewTextBoxColumn.DataPropertyName = "MaLS";
      this.maLSDataGridViewTextBoxColumn.HeaderText = "MaLS";
      this.maLSDataGridViewTextBoxColumn.Name = "maLSDataGridViewTextBoxColumn";
      this.maLSDataGridViewTextBoxColumn.ReadOnly = true;
      this.maLSDataGridViewTextBoxColumn.Visible = false;
      // 
      // hoatDongDataGridViewCheckBoxColumn
      // 
      this.hoatDongDataGridViewCheckBoxColumn.DataPropertyName = "HoatDong";
      this.hoatDongDataGridViewCheckBoxColumn.HeaderText = "HoatDong";
      this.hoatDongDataGridViewCheckBoxColumn.Name = "hoatDongDataGridViewCheckBoxColumn";
      this.hoatDongDataGridViewCheckBoxColumn.ReadOnly = true;
      this.hoatDongDataGridViewCheckBoxColumn.Visible = false;
      // 
      // chiTietLichSuKhoDTOBindingSource
      // 
      this.chiTietLichSuKhoDTOBindingSource.DataSource = typeof(TechForgeDTO.ChiTietLichSuKhoDTO);
      // 
      // tlpInfo
      // 
      this.tlpInfo.ColumnCount = 5;
      this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 16F));
      this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpInfo.Controls.Add(this.label2, 0, 1);
      this.tlpInfo.Controls.Add(this.label1, 0, 0);
      this.tlpInfo.Controls.Add(this.label5, 0, 2);
      this.tlpInfo.Controls.Add(this.dtpThoiGian, 1, 1);
      this.tlpInfo.Controls.Add(this.txtMa, 1, 0);
      this.tlpInfo.Controls.Add(this.label3, 3, 0);
      this.tlpInfo.Controls.Add(this.nudTongTien, 4, 0);
      this.tlpInfo.Controls.Add(this.label4, 3, 1);
      this.tlpInfo.Controls.Add(this.cboNhanVienLap, 4, 1);
      this.tlpInfo.Controls.Add(this.cboHoatDong, 1, 2);
      this.tlpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tlpInfo.Location = new System.Drawing.Point(3, 3);
      this.tlpInfo.Name = "tlpInfo";
      this.tlpInfo.RowCount = 4;
      this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpInfo.Size = new System.Drawing.Size(593, 132);
      this.tlpInfo.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(4, 37);
      this.label2.Margin = new System.Windows.Forms.Padding(4);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(70, 25);
      this.label2.TabIndex = 1;
      this.label2.Text = "Thời Gian:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(4, 4);
      this.label1.Margin = new System.Windows.Forms.Padding(4);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(70, 25);
      this.label1.TabIndex = 0;
      this.label1.Text = "Mã:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label5.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(4, 70);
      this.label5.Margin = new System.Windows.Forms.Padding(4);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(70, 25);
      this.label5.TabIndex = 4;
      this.label5.Text = "Phiếu:";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // dtpThoiGian
      // 
      this.dtpThoiGian.CalendarFont = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dtpThoiGian.CustomFormat = "dd/MM/yyyy";
      this.dtpThoiGian.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dtpThoiGian.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.dtpThoiGian.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpThoiGian.Location = new System.Drawing.Point(82, 37);
      this.dtpThoiGian.Margin = new System.Windows.Forms.Padding(4);
      this.dtpThoiGian.Name = "dtpThoiGian";
      this.dtpThoiGian.Size = new System.Drawing.Size(142, 25);
      this.dtpThoiGian.TabIndex = 5;
      // 
      // txtMa
      // 
      this.txtMa.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtMa.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.txtMa.Location = new System.Drawing.Point(82, 4);
      this.txtMa.Margin = new System.Windows.Forms.Padding(4);
      this.txtMa.Name = "txtMa";
      this.txtMa.ReadOnly = true;
      this.txtMa.Size = new System.Drawing.Size(142, 25);
      this.txtMa.TabIndex = 6;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(248, 4);
      this.label3.Margin = new System.Windows.Forms.Padding(4);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(72, 25);
      this.label3.TabIndex = 2;
      this.label3.Text = "Tổng Tiền:";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nudTongTien
      // 
      this.nudTongTien.Dock = System.Windows.Forms.DockStyle.Fill;
      this.nudTongTien.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.nudTongTien.Increment = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nudTongTien.Location = new System.Drawing.Point(328, 4);
      this.nudTongTien.Margin = new System.Windows.Forms.Padding(4);
      this.nudTongTien.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
      this.nudTongTien.Name = "nudTongTien";
      this.nudTongTien.ReadOnly = true;
      this.nudTongTien.Size = new System.Drawing.Size(261, 25);
      this.nudTongTien.TabIndex = 7;
      this.nudTongTien.ThousandsSeparator = true;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(248, 37);
      this.label4.Margin = new System.Windows.Forms.Padding(4);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(72, 25);
      this.label4.TabIndex = 3;
      this.label4.Text = "NV Lập:";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cboNhanVienLap
      // 
      this.cboNhanVienLap.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cboNhanVienLap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboNhanVienLap.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.cboNhanVienLap.FormattingEnabled = true;
      this.cboNhanVienLap.Location = new System.Drawing.Point(328, 37);
      this.cboNhanVienLap.Margin = new System.Windows.Forms.Padding(4);
      this.cboNhanVienLap.Name = "cboNhanVienLap";
      this.cboNhanVienLap.Size = new System.Drawing.Size(261, 25);
      this.cboNhanVienLap.TabIndex = 8;
      // 
      // cboHoatDong
      // 
      this.cboHoatDong.Dock = System.Windows.Forms.DockStyle.Fill;
      this.cboHoatDong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboHoatDong.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.cboHoatDong.FormattingEnabled = true;
      this.cboHoatDong.Location = new System.Drawing.Point(82, 70);
      this.cboHoatDong.Margin = new System.Windows.Forms.Padding(4);
      this.cboHoatDong.Name = "cboHoatDong";
      this.cboHoatDong.Size = new System.Drawing.Size(142, 25);
      this.cboHoatDong.TabIndex = 9;
      // 
      // tlpDetailInfo
      // 
      this.tlpDetailInfo.BackColor = System.Drawing.Color.White;
      this.tlpDetailInfo.ColumnCount = 2;
      this.tlpDetailInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpDetailInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tlpDetailInfo.Controls.Add(this.txtMaSP, 1, 2);
      this.tlpDetailInfo.Controls.Add(this.label9, 0, 2);
      this.tlpDetailInfo.Controls.Add(this.nudSoLuong, 1, 5);
      this.tlpDetailInfo.Controls.Add(this.nudGia, 1, 4);
      this.tlpDetailInfo.Controls.Add(this.txtTenSP, 1, 3);
      this.tlpDetailInfo.Controls.Add(this.label12, 0, 5);
      this.tlpDetailInfo.Controls.Add(this.label6, 0, 3);
      this.tlpDetailInfo.Controls.Add(this.txtSearch, 0, 0);
      this.tlpDetailInfo.Controls.Add(this.lstSearchResults, 0, 1);
      this.tlpDetailInfo.Controls.Add(this.label7, 0, 4);
      this.tlpDetailInfo.Controls.Add(this.label8, 0, 7);
      this.tlpDetailInfo.Controls.Add(this.nudChiTietTongTien, 1, 7);
      this.tlpDetailInfo.Controls.Add(this.tlpDetailAction, 0, 10);
      this.tlpDetailInfo.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tlpDetailInfo.Location = new System.Drawing.Point(603, 4);
      this.tlpDetailInfo.Margin = new System.Windows.Forms.Padding(4);
      this.tlpDetailInfo.Name = "tlpDetailInfo";
      this.tlpDetailInfo.RowCount = 11;
      this.tlpMain.SetRowSpan(this.tlpDetailInfo, 2);
      this.tlpDetailInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpDetailInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpDetailInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpDetailInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpDetailInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpDetailInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpDetailInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpDetailInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpDetailInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpDetailInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tlpDetailInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpDetailInfo.Size = new System.Drawing.Size(315, 428);
      this.tlpDetailInfo.TabIndex = 2;
      // 
      // txtMaSP
      // 
      this.txtMaSP.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtMaSP.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.txtMaSP.Location = new System.Drawing.Point(84, 143);
      this.txtMaSP.Margin = new System.Windows.Forms.Padding(4);
      this.txtMaSP.Name = "txtMaSP";
      this.txtMaSP.ReadOnly = true;
      this.txtMaSP.Size = new System.Drawing.Size(227, 25);
      this.txtMaSP.TabIndex = 16;
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label9.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label9.Location = new System.Drawing.Point(4, 143);
      this.label9.Margin = new System.Windows.Forms.Padding(4);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(72, 25);
      this.label9.TabIndex = 15;
      this.label9.Text = "Mã:";
      this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nudSoLuong
      // 
      this.nudSoLuong.Dock = System.Windows.Forms.DockStyle.Fill;
      this.nudSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.nudSoLuong.Location = new System.Drawing.Point(84, 242);
      this.nudSoLuong.Margin = new System.Windows.Forms.Padding(4);
      this.nudSoLuong.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
      this.nudSoLuong.Name = "nudSoLuong";
      this.nudSoLuong.Size = new System.Drawing.Size(227, 25);
      this.nudSoLuong.TabIndex = 11;
      this.nudSoLuong.ThousandsSeparator = true;
      this.nudSoLuong.ValueChanged += new System.EventHandler(this.nudSoLuong_ValueChanged);
      // 
      // nudGia
      // 
      this.nudGia.Dock = System.Windows.Forms.DockStyle.Fill;
      this.nudGia.Enabled = false;
      this.nudGia.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.nudGia.Location = new System.Drawing.Point(84, 209);
      this.nudGia.Margin = new System.Windows.Forms.Padding(4);
      this.nudGia.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
      this.nudGia.Name = "nudGia";
      this.nudGia.ReadOnly = true;
      this.nudGia.Size = new System.Drawing.Size(227, 25);
      this.nudGia.TabIndex = 10;
      this.nudGia.ThousandsSeparator = true;
      // 
      // txtTenSP
      // 
      this.txtTenSP.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtTenSP.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.txtTenSP.Location = new System.Drawing.Point(84, 176);
      this.txtTenSP.Margin = new System.Windows.Forms.Padding(4);
      this.txtTenSP.Name = "txtTenSP";
      this.txtTenSP.ReadOnly = true;
      this.txtTenSP.Size = new System.Drawing.Size(227, 25);
      this.txtTenSP.TabIndex = 9;
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label12.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label12.Location = new System.Drawing.Point(4, 242);
      this.label12.Margin = new System.Windows.Forms.Padding(4);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(72, 25);
      this.label12.TabIndex = 8;
      this.label12.Text = "Số Lượng:";
      this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label6.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(4, 176);
      this.label6.Margin = new System.Windows.Forms.Padding(4);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(72, 25);
      this.label6.TabIndex = 2;
      this.label6.Text = "Tên SP:";
      this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtSearch
      // 
      this.tlpDetailInfo.SetColumnSpan(this.txtSearch, 2);
      this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.txtSearch.Location = new System.Drawing.Point(4, 4);
      this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
      this.txtSearch.Name = "txtSearch";
      this.txtSearch.Size = new System.Drawing.Size(307, 25);
      this.txtSearch.TabIndex = 0;
      this.txtSearch.Text = "Tìm kiếm sản phẩm ...";
      this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
      // 
      // lstSearchResults
      // 
      this.lstSearchResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.tlpDetailInfo.SetColumnSpan(this.lstSearchResults, 2);
      this.lstSearchResults.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lstSearchResults.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lstSearchResults.FormattingEnabled = true;
      this.lstSearchResults.ItemHeight = 17;
      this.lstSearchResults.Location = new System.Drawing.Point(3, 36);
      this.lstSearchResults.Name = "lstSearchResults";
      this.lstSearchResults.ScrollAlwaysVisible = true;
      this.lstSearchResults.Size = new System.Drawing.Size(309, 100);
      this.lstSearchResults.TabIndex = 1;
      this.lstSearchResults.SelectedIndexChanged += new System.EventHandler(this.lstSearchResults_SelectedIndexChanged);
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(4, 209);
      this.label7.Margin = new System.Windows.Forms.Padding(4);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(72, 25);
      this.label7.TabIndex = 3;
      this.label7.Text = "Giá:";
      this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(4, 275);
      this.label8.Margin = new System.Windows.Forms.Padding(4);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(72, 25);
      this.label8.TabIndex = 4;
      this.label8.Text = "Tổng Tiền:";
      this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // nudChiTietTongTien
      // 
      this.nudChiTietTongTien.Dock = System.Windows.Forms.DockStyle.Fill;
      this.nudChiTietTongTien.Enabled = false;
      this.nudChiTietTongTien.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.nudChiTietTongTien.Location = new System.Drawing.Point(84, 275);
      this.nudChiTietTongTien.Margin = new System.Windows.Forms.Padding(4);
      this.nudChiTietTongTien.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
      this.nudChiTietTongTien.Name = "nudChiTietTongTien";
      this.nudChiTietTongTien.ReadOnly = true;
      this.nudChiTietTongTien.Size = new System.Drawing.Size(227, 25);
      this.nudChiTietTongTien.TabIndex = 12;
      this.nudChiTietTongTien.ThousandsSeparator = true;
      // 
      // tlpDetailAction
      // 
      this.tlpDetailAction.ColumnCount = 2;
      this.tlpDetailInfo.SetColumnSpan(this.tlpDetailAction, 2);
      this.tlpDetailAction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tlpDetailAction.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tlpDetailAction.Controls.Add(this.btnChiTietThem, 0, 0);
      this.tlpDetailAction.Controls.Add(this.btnChiTietCapNhat, 1, 0);
      this.tlpDetailAction.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tlpDetailAction.Location = new System.Drawing.Point(0, 372);
      this.tlpDetailAction.Margin = new System.Windows.Forms.Padding(0);
      this.tlpDetailAction.Name = "tlpDetailAction";
      this.tlpDetailAction.RowCount = 1;
      this.tlpDetailAction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tlpDetailAction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
      this.tlpDetailAction.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
      this.tlpDetailAction.Size = new System.Drawing.Size(315, 56);
      this.tlpDetailAction.TabIndex = 14;
      // 
      // btnChiTietThem
      // 
      this.btnChiTietThem.BackColor = System.Drawing.Color.Gray;
      this.btnChiTietThem.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnChiTietThem.Enabled = false;
      this.btnChiTietThem.FlatAppearance.BorderSize = 0;
      this.btnChiTietThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnChiTietThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnChiTietThem.ForeColor = System.Drawing.Color.White;
      this.btnChiTietThem.Location = new System.Drawing.Point(4, 4);
      this.btnChiTietThem.Margin = new System.Windows.Forms.Padding(4);
      this.btnChiTietThem.Name = "btnChiTietThem";
      this.btnChiTietThem.Size = new System.Drawing.Size(149, 48);
      this.btnChiTietThem.TabIndex = 15;
      this.btnChiTietThem.Text = "Thêm Vào CT";
      this.btnChiTietThem.UseVisualStyleBackColor = false;
      this.btnChiTietThem.Click += new System.EventHandler(this.btnChiTietThem_Click);
      // 
      // btnChiTietCapNhat
      // 
      this.btnChiTietCapNhat.BackColor = System.Drawing.Color.Orange;
      this.btnChiTietCapNhat.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnChiTietCapNhat.FlatAppearance.BorderSize = 0;
      this.btnChiTietCapNhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnChiTietCapNhat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnChiTietCapNhat.ForeColor = System.Drawing.Color.White;
      this.btnChiTietCapNhat.Location = new System.Drawing.Point(161, 4);
      this.btnChiTietCapNhat.Margin = new System.Windows.Forms.Padding(4);
      this.btnChiTietCapNhat.Name = "btnChiTietCapNhat";
      this.btnChiTietCapNhat.Size = new System.Drawing.Size(150, 48);
      this.btnChiTietCapNhat.TabIndex = 16;
      this.btnChiTietCapNhat.Text = "Cập Nhật";
      this.btnChiTietCapNhat.UseVisualStyleBackColor = false;
      this.btnChiTietCapNhat.Click += new System.EventHandler(this.btnChiTietCapNhat_Click);
      // 
      // ImportExportDetailFormGUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(922, 478);
      this.Controls.Add(this.tlpMain);
      this.Name = "ImportExportDetailFormGUI";
      this.Text = "ImportExportDetailFormGUI";
      this.Controls.SetChildIndex(this.tlpMain, 0);
      this.tlpMain.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.chiTietLichSuKhoDTOBindingSource)).EndInit();
      this.tlpInfo.ResumeLayout(false);
      this.tlpInfo.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudTongTien)).EndInit();
      this.tlpDetailInfo.ResumeLayout(false);
      this.tlpDetailInfo.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudGia)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.nudChiTietTongTien)).EndInit();
      this.tlpDetailAction.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tlpMain;
    private System.Windows.Forms.DataGridView dgvDetail;
    private System.Windows.Forms.TableLayoutPanel tlpInfo;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.DateTimePicker dtpThoiGian;
    private System.Windows.Forms.TextBox txtMa;
    private System.Windows.Forms.NumericUpDown nudTongTien;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cboNhanVienLap;
    private System.Windows.Forms.ComboBox cboHoatDong;
    private System.Windows.Forms.BindingSource chiTietLichSuKhoDTOBindingSource;
    private System.Windows.Forms.TableLayoutPanel tlpDetailInfo;
    private System.Windows.Forms.NumericUpDown nudSoLuong;
    private System.Windows.Forms.NumericUpDown nudGia;
    private System.Windows.Forms.TextBox txtTenSP;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TextBox txtSearch;
    private System.Windows.Forms.ListBox lstSearchResults;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.NumericUpDown nudChiTietTongTien;
    private System.Windows.Forms.TableLayoutPanel tlpDetailAction;
    private System.Windows.Forms.Button btnChiTietThem;
    private System.Windows.Forms.Button btnChiTietCapNhat;
    private System.Windows.Forms.TextBox txtMaSP;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.DataGridViewTextBoxColumn dgvTxtColMaSP;
    private System.Windows.Forms.DataGridViewTextBoxColumn tenSPDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewImageColumn hinhAnhDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn giaDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn soLuongDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn thanhTienDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn maLSDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewCheckBoxColumn hoatDongDataGridViewCheckBoxColumn;
  }
}
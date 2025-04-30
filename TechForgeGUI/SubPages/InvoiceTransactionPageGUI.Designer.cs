namespace TechForgeGUI.SubPages
{
  partial class InvoiceTransactionPageGUI
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.maSPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.hinhAnhDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewImageColumn();
      this.tenSPDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.giaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.soLuongDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.khuyenMaiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.thanhTienDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.giaCuoiCungDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.soTienKmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.maHDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.chiTietHoaDonDTOBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.dataGridView2 = new System.Windows.Forms.DataGridView();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.chiTietHoaDonDTOBindingSource)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
      this.flowLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
      this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1368, 558);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // groupBox2
      // 
      this.groupBox2.BackColor = System.Drawing.Color.White;
      this.groupBox2.Controls.Add(this.dataGridView1);
      this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox2.Location = new System.Drawing.Point(3, 226);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(951, 329);
      this.groupBox2.TabIndex = 2;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Sản Phẩm Trong Hóa Đơn";
      // 
      // dataGridView1
      // 
      this.dataGridView1.AutoGenerateColumns = false;
      this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.maSPDataGridViewTextBoxColumn,
            this.hinhAnhDataGridViewTextBoxColumn,
            this.tenSPDataGridViewTextBoxColumn,
            this.giaDataGridViewTextBoxColumn,
            this.soLuongDataGridViewTextBoxColumn,
            this.khuyenMaiDataGridViewTextBoxColumn,
            this.thanhTienDataGridViewTextBoxColumn,
            this.giaCuoiCungDataGridViewTextBoxColumn,
            this.soTienKmDataGridViewTextBoxColumn,
            this.maHDDataGridViewTextBoxColumn});
      this.dataGridView1.DataSource = this.chiTietHoaDonDTOBindingSource;
      this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGridView1.Location = new System.Drawing.Point(3, 28);
      this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.RowHeadersVisible = false;
      this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView1.Size = new System.Drawing.Size(945, 298);
      this.dataGridView1.TabIndex = 0;
      // 
      // maSPDataGridViewTextBoxColumn
      // 
      this.maSPDataGridViewTextBoxColumn.DataPropertyName = "MaSP";
      this.maSPDataGridViewTextBoxColumn.HeaderText = "Mã SP";
      this.maSPDataGridViewTextBoxColumn.Name = "maSPDataGridViewTextBoxColumn";
      // 
      // hinhAnhDataGridViewTextBoxColumn
      // 
      this.hinhAnhDataGridViewTextBoxColumn.DataPropertyName = "HinhAnh";
      this.hinhAnhDataGridViewTextBoxColumn.HeaderText = "Hình Ảnh";
      this.hinhAnhDataGridViewTextBoxColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
      this.hinhAnhDataGridViewTextBoxColumn.Name = "hinhAnhDataGridViewTextBoxColumn";
      this.hinhAnhDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
      this.hinhAnhDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
      // 
      // tenSPDataGridViewTextBoxColumn
      // 
      this.tenSPDataGridViewTextBoxColumn.DataPropertyName = "TenSP";
      this.tenSPDataGridViewTextBoxColumn.HeaderText = "Tên SP";
      this.tenSPDataGridViewTextBoxColumn.Name = "tenSPDataGridViewTextBoxColumn";
      // 
      // giaDataGridViewTextBoxColumn
      // 
      this.giaDataGridViewTextBoxColumn.DataPropertyName = "Gia";
      this.giaDataGridViewTextBoxColumn.HeaderText = "Giá";
      this.giaDataGridViewTextBoxColumn.Name = "giaDataGridViewTextBoxColumn";
      // 
      // soLuongDataGridViewTextBoxColumn
      // 
      this.soLuongDataGridViewTextBoxColumn.DataPropertyName = "SoLuong";
      this.soLuongDataGridViewTextBoxColumn.HeaderText = "Số Lượng";
      this.soLuongDataGridViewTextBoxColumn.Name = "soLuongDataGridViewTextBoxColumn";
      // 
      // khuyenMaiDataGridViewTextBoxColumn
      // 
      this.khuyenMaiDataGridViewTextBoxColumn.DataPropertyName = "KhuyenMai";
      this.khuyenMaiDataGridViewTextBoxColumn.HeaderText = "Khuyến Mãi (%)";
      this.khuyenMaiDataGridViewTextBoxColumn.Name = "khuyenMaiDataGridViewTextBoxColumn";
      // 
      // thanhTienDataGridViewTextBoxColumn
      // 
      this.thanhTienDataGridViewTextBoxColumn.DataPropertyName = "ThanhTien";
      this.thanhTienDataGridViewTextBoxColumn.HeaderText = "Thành Tiền";
      this.thanhTienDataGridViewTextBoxColumn.Name = "thanhTienDataGridViewTextBoxColumn";
      // 
      // giaCuoiCungDataGridViewTextBoxColumn
      // 
      this.giaCuoiCungDataGridViewTextBoxColumn.DataPropertyName = "GiaCuoiCung";
      this.giaCuoiCungDataGridViewTextBoxColumn.HeaderText = "GiaCuoiCung";
      this.giaCuoiCungDataGridViewTextBoxColumn.Name = "giaCuoiCungDataGridViewTextBoxColumn";
      this.giaCuoiCungDataGridViewTextBoxColumn.Visible = false;
      // 
      // soTienKmDataGridViewTextBoxColumn
      // 
      this.soTienKmDataGridViewTextBoxColumn.DataPropertyName = "SoTienKm";
      this.soTienKmDataGridViewTextBoxColumn.HeaderText = "SoTienKm";
      this.soTienKmDataGridViewTextBoxColumn.Name = "soTienKmDataGridViewTextBoxColumn";
      this.soTienKmDataGridViewTextBoxColumn.Visible = false;
      // 
      // maHDDataGridViewTextBoxColumn
      // 
      this.maHDDataGridViewTextBoxColumn.DataPropertyName = "MaHD";
      this.maHDDataGridViewTextBoxColumn.HeaderText = "MaHD";
      this.maHDDataGridViewTextBoxColumn.Name = "maHDDataGridViewTextBoxColumn";
      this.maHDDataGridViewTextBoxColumn.Visible = false;
      // 
      // chiTietHoaDonDTOBindingSource
      // 
      this.chiTietHoaDonDTOBindingSource.DataSource = typeof(TechForgeDTO.ChiTietHoaDonDTO);
      // 
      // groupBox1
      // 
      this.groupBox1.BackColor = System.Drawing.Color.White;
      this.groupBox1.Controls.Add(this.tableLayoutPanel2);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox1.Location = new System.Drawing.Point(3, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(951, 217);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Tìm Kiếm Sản Phẩm";
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 1;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Controls.Add(this.dataGridView2, 0, 1);
      this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel1, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.button2, 0, 2);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 28);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 3;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel2.Size = new System.Drawing.Size(945, 186);
      this.tableLayoutPanel2.TabIndex = 0;
      // 
      // dataGridView2
      // 
      this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGridView2.Location = new System.Drawing.Point(3, 51);
      this.dataGridView2.Name = "dataGridView2";
      this.dataGridView2.Size = new System.Drawing.Size(939, 94);
      this.dataGridView2.TabIndex = 0;
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Controls.Add(this.textBox1);
      this.flowLayoutPanel1.Controls.Add(this.button1);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(945, 48);
      this.flowLayoutPanel1.TabIndex = 1;
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(3, 3);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(415, 32);
      this.textBox1.TabIndex = 0;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(424, 3);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(82, 32);
      this.button1.TabIndex = 1;
      this.button1.Text = "button1";
      this.button1.UseVisualStyleBackColor = true;
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(3, 151);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(82, 32);
      this.button2.TabIndex = 2;
      this.button2.Text = "button2";
      this.button2.UseVisualStyleBackColor = true;
      // 
      // InvoiceTransactionPageGUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "InvoiceTransactionPageGUI";
      this.Size = new System.Drawing.Size(1368, 558);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.chiTietHoaDonDTOBindingSource)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.tableLayoutPanel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
      this.flowLayoutPanel1.ResumeLayout(false);
      this.flowLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.DataGridViewTextBoxColumn maSPDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewImageColumn hinhAnhDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn tenSPDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn giaDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn soLuongDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn khuyenMaiDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn thanhTienDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn giaCuoiCungDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn soTienKmDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn maHDDataGridViewTextBoxColumn;
    private System.Windows.Forms.BindingSource chiTietHoaDonDTOBindingSource;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.DataGridView dataGridView2;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
  }
}

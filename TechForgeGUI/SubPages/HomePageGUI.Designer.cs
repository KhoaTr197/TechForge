namespace TechForgeGUI.SubPages
{
  partial class HomePageGUI
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
      this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
      this.lblUserRole = new System.Windows.Forms.Label();
      this.lblAccountName = new System.Windows.Forms.Label();
      this.lblWelcome = new System.Windows.Forms.Label();
      this.lblCurrentDate = new System.Windows.Forms.Label();
      this.button1 = new System.Windows.Forms.Button();
      this.panel1 = new System.Windows.Forms.Panel();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.flpActivityList = new System.Windows.Forms.FlowLayoutPanel();
      this.flpSummary = new System.Windows.Forms.FlowLayoutPanel();
      this.tlpMain.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.flowLayoutPanel2.SuspendLayout();
      this.panel1.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tlpMain
      // 
      this.tlpMain.ColumnCount = 2;
      this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.tlpMain.Controls.Add(this.tableLayoutPanel2, 0, 0);
      this.tlpMain.Controls.Add(this.panel1, 0, 2);
      this.tlpMain.Controls.Add(this.flpSummary, 0, 1);
      this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tlpMain.Location = new System.Drawing.Point(0, 0);
      this.tlpMain.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
      this.tlpMain.Name = "tlpMain";
      this.tlpMain.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
      this.tlpMain.RowCount = 3;
      this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
      this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
      this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
      this.tlpMain.Size = new System.Drawing.Size(1368, 641);
      this.tlpMain.TabIndex = 0;
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 5;
      this.tlpMain.SetColumnSpan(this.tableLayoutPanel2, 2);
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel2, 3, 0);
      this.tableLayoutPanel2.Controls.Add(this.lblWelcome, 0, 0);
      this.tableLayoutPanel2.Controls.Add(this.lblCurrentDate, 1, 0);
      this.tableLayoutPanel2.Controls.Add(this.button1, 4, 0);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 0);
      this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 1;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(1352, 64);
      this.tableLayoutPanel2.TabIndex = 0;
      // 
      // flowLayoutPanel2
      // 
      this.flowLayoutPanel2.Controls.Add(this.lblUserRole);
      this.flowLayoutPanel2.Controls.Add(this.lblAccountName);
      this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.flowLayoutPanel2.Location = new System.Drawing.Point(1026, 0);
      this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
      this.flowLayoutPanel2.Name = "flowLayoutPanel2";
      this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(8);
      this.flowLayoutPanel2.Size = new System.Drawing.Size(200, 64);
      this.flowLayoutPanel2.TabIndex = 3;
      // 
      // lblUserRole
      // 
      this.lblUserRole.AutoSize = true;
      this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.lblUserRole.Location = new System.Drawing.Point(8, 8);
      this.lblUserRole.Margin = new System.Windows.Forms.Padding(0);
      this.lblUserRole.Name = "lblUserRole";
      this.lblUserRole.Size = new System.Drawing.Size(74, 19);
      this.lblUserRole.TabIndex = 2;
      this.lblUserRole.Text = "Vai trò: xxx";
      // 
      // lblAccountName
      // 
      this.lblAccountName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.lblAccountName.AutoSize = true;
      this.lblAccountName.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.lblAccountName.Location = new System.Drawing.Point(8, 27);
      this.lblAccountName.Margin = new System.Windows.Forms.Padding(0);
      this.lblAccountName.Name = "lblAccountName";
      this.lblAccountName.Size = new System.Drawing.Size(125, 19);
      this.lblAccountName.TabIndex = 3;
      this.lblAccountName.Text = "Tên đăng nhập: xxx";
      // 
      // lblWelcome
      // 
      this.lblWelcome.AutoSize = true;
      this.lblWelcome.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
      this.lblWelcome.Location = new System.Drawing.Point(3, 0);
      this.lblWelcome.Name = "lblWelcome";
      this.lblWelcome.Size = new System.Drawing.Size(390, 64);
      this.lblWelcome.TabIndex = 0;
      this.lblWelcome.Text = "Chào mừng trở lại, Tên Người Dùng!";
      this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // lblCurrentDate
      // 
      this.lblCurrentDate.AutoSize = true;
      this.lblCurrentDate.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lblCurrentDate.Font = new System.Drawing.Font("Segoe UI", 12F);
      this.lblCurrentDate.Location = new System.Drawing.Point(399, 0);
      this.lblCurrentDate.Name = "lblCurrentDate";
      this.lblCurrentDate.Size = new System.Drawing.Size(258, 64);
      this.lblCurrentDate.TabIndex = 1;
      this.lblCurrentDate.Text = "Thứ tư, ngày XX tháng YY năm ZZZZ";
      this.lblCurrentDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
      this.button1.BackColor = System.Drawing.Color.DodgerBlue;
      this.button1.FlatAppearance.BorderSize = 0;
      this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button1.ForeColor = System.Drawing.Color.White;
      this.button1.Location = new System.Drawing.Point(1229, 3);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(120, 58);
      this.button1.TabIndex = 4;
      this.button1.Text = "Xem thông tin";
      this.button1.UseVisualStyleBackColor = false;
      // 
      // panel1
      // 
      this.tlpMain.SetColumnSpan(this.panel1, 2);
      this.panel1.Controls.Add(this.groupBox1);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(11, 227);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1346, 411);
      this.panel1.TabIndex = 1;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.flpActivityList);
      this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox1.Location = new System.Drawing.Point(0, 0);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(1346, 411);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Lịch Sử Hoạt Động";
      // 
      // flpActivityList
      // 
      this.flpActivityList.AutoScroll = true;
      this.flpActivityList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flpActivityList.Location = new System.Drawing.Point(3, 32);
      this.flpActivityList.Name = "flpActivityList";
      this.flpActivityList.Size = new System.Drawing.Size(1340, 376);
      this.flpActivityList.TabIndex = 0;
      // 
      // flpSummary
      // 
      this.tlpMain.SetColumnSpan(this.flpSummary, 2);
      this.flpSummary.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flpSummary.Location = new System.Drawing.Point(11, 67);
      this.flpSummary.Name = "flpSummary";
      this.flpSummary.Size = new System.Drawing.Size(1346, 154);
      this.flpSummary.TabIndex = 2;
      // 
      // HomePageGUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tlpMain);
      this.Name = "HomePageGUI";
      this.Size = new System.Drawing.Size(1368, 641);
      this.tlpMain.ResumeLayout(false);
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      this.flowLayoutPanel2.ResumeLayout(false);
      this.flowLayoutPanel2.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tlpMain;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    private System.Windows.Forms.Label lblUserRole;
    private System.Windows.Forms.Label lblAccountName;
    private System.Windows.Forms.Label lblWelcome;
    private System.Windows.Forms.Label lblCurrentDate;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.FlowLayoutPanel flpSummary;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.FlowLayoutPanel flpActivityList;
  }
}

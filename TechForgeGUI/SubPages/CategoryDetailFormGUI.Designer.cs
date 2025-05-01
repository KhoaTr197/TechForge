namespace TechForgeGUI.SubPages
{
  partial class CategoryDetailFormGUI
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
      this.label1 = new System.Windows.Forms.Label();
      this.txtMaDM = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtTenDM = new System.Windows.Forms.TextBox();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.tableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(4, 20);
      this.label1.Margin = new System.Windows.Forms.Padding(4);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(105, 25);
      this.label1.TabIndex = 1;
      this.label1.Text = "Mã Danh Mục:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtMaDM
      // 
      this.txtMaDM.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtMaDM.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtMaDM.Location = new System.Drawing.Point(117, 20);
      this.txtMaDM.Margin = new System.Windows.Forms.Padding(4);
      this.txtMaDM.Name = "txtMaDM";
      this.txtMaDM.Size = new System.Drawing.Size(278, 25);
      this.txtMaDM.TabIndex = 2;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(4, 53);
      this.label2.Margin = new System.Windows.Forms.Padding(4);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(105, 25);
      this.label2.TabIndex = 3;
      this.label2.Text = "Tên Danh Mục:";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtTenDM
      // 
      this.txtTenDM.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtTenDM.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtTenDM.Location = new System.Drawing.Point(117, 53);
      this.txtTenDM.Margin = new System.Windows.Forms.Padding(4);
      this.txtTenDM.Name = "txtTenDM";
      this.txtTenDM.Size = new System.Drawing.Size(278, 25);
      this.txtTenDM.TabIndex = 4;
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.3208F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.6792F));
      this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.txtTenDM, 1, 1);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.txtMaDM, 1, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(399, 296);
      this.tableLayoutPanel1.TabIndex = 5;
      // 
      // CategoryDetailFormGUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(399, 338);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "CategoryDetailFormGUI";
      this.Text = "CategoryDetailFormGUI";
      this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtMaDM;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtTenDM;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
  }
}
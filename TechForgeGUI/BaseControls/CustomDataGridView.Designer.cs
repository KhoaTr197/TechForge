namespace TechForgeGUI.BaseControls
{
  partial class CustomDataGridView
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      this.flpPagination = new System.Windows.Forms.FlowLayoutPanel();
      this.btnNext = new System.Windows.Forms.Button();
      this.lblCurentPage = new System.Windows.Forms.Label();
      this.btnPrev = new System.Windows.Forms.Button();
      this.dgvList = new System.Windows.Forms.DataGridView();
      this.flpPagination.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
      this.SuspendLayout();
      // 
      // flpPagination
      // 
      this.flpPagination.AutoSize = true;
      this.flpPagination.BackColor = System.Drawing.Color.White;
      this.flpPagination.Controls.Add(this.btnNext);
      this.flpPagination.Controls.Add(this.lblCurentPage);
      this.flpPagination.Controls.Add(this.btnPrev);
      this.flpPagination.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.flpPagination.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
      this.flpPagination.Location = new System.Drawing.Point(0, 452);
      this.flpPagination.Name = "flpPagination";
      this.flpPagination.Size = new System.Drawing.Size(949, 38);
      this.flpPagination.TabIndex = 1;
      // 
      // btnNext
      // 
      this.btnNext.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnNext.Location = new System.Drawing.Point(882, 3);
      this.btnNext.Name = "btnNext";
      this.btnNext.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.btnNext.Size = new System.Drawing.Size(64, 32);
      this.btnNext.TabIndex = 0;
      this.btnNext.Text = "Next";
      this.btnNext.UseVisualStyleBackColor = true;
      // 
      // lblCurentPage
      // 
      this.lblCurentPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
      this.lblCurentPage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblCurentPage.Location = new System.Drawing.Point(796, 0);
      this.lblCurentPage.Name = "lblCurentPage";
      this.lblCurentPage.Size = new System.Drawing.Size(80, 38);
      this.lblCurentPage.TabIndex = 2;
      this.lblCurentPage.Text = "Page N";
      this.lblCurentPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // btnPrev
      // 
      this.btnPrev.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnPrev.Location = new System.Drawing.Point(726, 3);
      this.btnPrev.Name = "btnPrev";
      this.btnPrev.Size = new System.Drawing.Size(64, 32);
      this.btnPrev.TabIndex = 1;
      this.btnPrev.Text = "Prev";
      this.btnPrev.UseVisualStyleBackColor = true;
      // 
      // dgvList
      // 
      this.dgvList.AllowUserToAddRows = false;
      this.dgvList.AllowUserToDeleteRows = false;
      this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvList.DefaultCellStyle = dataGridViewCellStyle2;
      this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvList.Location = new System.Drawing.Point(0, 0);
      this.dgvList.Margin = new System.Windows.Forms.Padding(0);
      this.dgvList.MinimumSize = new System.Drawing.Size(400, 400);
      this.dgvList.Name = "dgvList";
      this.dgvList.ReadOnly = true;
      this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgvList.Size = new System.Drawing.Size(949, 452);
      this.dgvList.TabIndex = 2;
      this.dgvList.VirtualMode = true;
      // 
      // CustomDataGridView
      // 
      this.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.Controls.Add(this.dgvList);
      this.Controls.Add(this.flpPagination);
      this.Margin = new System.Windows.Forms.Padding(0);
      this.Name = "CustomDataGridView";
      this.Size = new System.Drawing.Size(949, 490);
      this.DockChanged += new System.EventHandler(this.CustomDataGridView_DockChanged);
      this.flpPagination.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    protected System.Windows.Forms.Button btnNext;
    protected System.Windows.Forms.FlowLayoutPanel flpPagination;
    protected System.Windows.Forms.Button btnPrev;
    protected System.Windows.Forms.Label lblCurentPage;
    public System.Windows.Forms.DataGridView dgvList;
  }
}

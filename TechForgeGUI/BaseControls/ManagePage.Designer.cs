namespace TechForgeGUI.BaseForms
{
  partial class ManagePage
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
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.dgvMainList = new TechForgeGUI.BaseControls.CustomDataGridView();
      this.flpSummary = new System.Windows.Forms.FlowLayoutPanel();
      this.tlpSearchFilter = new System.Windows.Forms.TableLayoutPanel();
      this.label1 = new System.Windows.Forms.Label();
      this.btnSearch = new System.Windows.Forms.Button();
      this.txtSearch = new System.Windows.Forms.TextBox();
      this.flpActions = new System.Windows.Forms.FlowLayoutPanel();
      this.btnAdd = new System.Windows.Forms.Button();
      this.tableLayoutPanel1.SuspendLayout();
      this.tlpSearchFilter.SuspendLayout();
      this.flpActions.SuspendLayout();
      this.SuspendLayout();
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
      this.tableLayoutPanel1.Controls.Add(this.dgvMainList, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.flpSummary, 0, 0);
      this.tableLayoutPanel1.Controls.Add(this.tlpSearchFilter, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.flpActions, 1, 1);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 3;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(1203, 646);
      this.tableLayoutPanel1.TabIndex = 0;
      // 
      // dgvMainList
      // 
      this.dgvMainList.BackColor = System.Drawing.SystemColors.AppWorkspace;
      this.tableLayoutPanel1.SetColumnSpan(this.dgvMainList, 2);
      this.dgvMainList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvMainList.Location = new System.Drawing.Point(0, 225);
      this.dgvMainList.Margin = new System.Windows.Forms.Padding(0);
      this.dgvMainList.Name = "dgvMainList";
      this.dgvMainList.Size = new System.Drawing.Size(1203, 421);
      this.dgvMainList.TabIndex = 0;
      // 
      // flpSummary
      // 
      this.tableLayoutPanel1.SetColumnSpan(this.flpSummary, 2);
      this.flpSummary.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flpSummary.Location = new System.Drawing.Point(0, 0);
      this.flpSummary.Margin = new System.Windows.Forms.Padding(0);
      this.flpSummary.Name = "flpSummary";
      this.flpSummary.Size = new System.Drawing.Size(1203, 120);
      this.flpSummary.TabIndex = 1;
      // 
      // tlpSearchFilter
      // 
      this.tlpSearchFilter.ColumnCount = 4;
      this.tlpSearchFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpSearchFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
      this.tlpSearchFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tlpSearchFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
      this.tlpSearchFilter.Controls.Add(this.label1, 0, 0);
      this.tlpSearchFilter.Controls.Add(this.btnSearch, 2, 0);
      this.tlpSearchFilter.Controls.Add(this.txtSearch, 1, 0);
      this.tlpSearchFilter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tlpSearchFilter.Location = new System.Drawing.Point(3, 123);
      this.tlpSearchFilter.Name = "tlpSearchFilter";
      this.tlpSearchFilter.RowCount = 2;
      this.tlpSearchFilter.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tlpSearchFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tlpSearchFilter.Size = new System.Drawing.Size(836, 99);
      this.tlpSearchFilter.TabIndex = 2;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(3, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(68, 35);
      this.label1.TabIndex = 0;
      this.label1.Text = "Tìm Kiếm:";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // btnSearch
      // 
      this.btnSearch.BackColor = System.Drawing.Color.DodgerBlue;
      this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
      this.btnSearch.FlatAppearance.BorderSize = 0;
      this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnSearch.ForeColor = System.Drawing.Color.White;
      this.btnSearch.Location = new System.Drawing.Point(531, 3);
      this.btnSearch.Name = "btnSearch";
      this.btnSearch.Size = new System.Drawing.Size(75, 29);
      this.btnSearch.TabIndex = 4;
      this.btnSearch.Text = "Tìm";
      this.btnSearch.UseVisualStyleBackColor = false;
      // 
      // txtSearch
      // 
      this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtSearch.Location = new System.Drawing.Point(77, 3);
      this.txtSearch.Name = "txtSearch";
      this.txtSearch.Size = new System.Drawing.Size(448, 29);
      this.txtSearch.TabIndex = 3;
      // 
      // flpActions
      // 
      this.flpActions.Controls.Add(this.btnAdd);
      this.flpActions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flpActions.Location = new System.Drawing.Point(845, 123);
      this.flpActions.Name = "flpActions";
      this.flpActions.Size = new System.Drawing.Size(355, 99);
      this.flpActions.TabIndex = 3;
      // 
      // btnAdd
      // 
      this.btnAdd.BackColor = System.Drawing.Color.ForestGreen;
      this.btnAdd.FlatAppearance.BorderSize = 0;
      this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnAdd.ForeColor = System.Drawing.Color.White;
      this.btnAdd.Location = new System.Drawing.Point(3, 3);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(80, 32);
      this.btnAdd.TabIndex = 0;
      this.btnAdd.Text = "Thêm";
      this.btnAdd.UseVisualStyleBackColor = false;
      // 
      // ManagePage
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tableLayoutPanel1);
      this.Name = "ManagePage";
      this.Size = new System.Drawing.Size(1203, 646);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tlpSearchFilter.ResumeLayout(false);
      this.tlpSearchFilter.PerformLayout();
      this.flpActions.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.FlowLayoutPanel flpSummary;
    private System.Windows.Forms.TableLayoutPanel tlpSearchFilter;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.FlowLayoutPanel flpActions;
    protected System.Windows.Forms.Button btnAdd;
    public BaseControls.CustomDataGridView dgvMainList;
    public System.Windows.Forms.Button btnSearch;
    public System.Windows.Forms.TextBox txtSearch;
  }
}
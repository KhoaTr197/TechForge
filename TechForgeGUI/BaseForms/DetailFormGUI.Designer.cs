namespace TechForgeGUI.BaseForms
{
  partial class DetailFormGUI
  {
    public System.ComponentModel.IContainer components = null;
    public System.Windows.Forms.FlowLayoutPanel flpActionsPanel;
    public System.Windows.Forms.Button btnDelete;
    public System.Windows.Forms.Button btnEdit;
    public System.Windows.Forms.Button btnAdd;

    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.flpActionsPanel = new System.Windows.Forms.FlowLayoutPanel();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnEdit = new System.Windows.Forms.Button();
      this.btnAdd = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // flpActionsPanel
      // 
      this.flpActionsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(86)))), ((int)(((byte)(37)))));
      this.flpActionsPanel.AutoSize = true;
      this.flpActionsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.flpActionsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
      this.flpActionsPanel.Controls.Add(this.btnDelete);
      this.flpActionsPanel.Controls.Add(this.btnEdit);
      this.flpActionsPanel.Controls.Add(this.btnAdd);
      this.flpActionsPanel.Location = new System.Drawing.Point(0, 400);
      this.flpActionsPanel.Name = "flpActionsPanel";
      this.flpActionsPanel.Size = new System.Drawing.Size(800, 50);
      this.flpActionsPanel.TabIndex = 0;
      // 
      // btnDelete
      // 
      this.btnDelete.BackColor = System.Drawing.Color.White;
      this.btnDelete.Text = "Xóa";
      this.btnDelete.AutoSize = true;
      this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(75, 30);
      this.btnDelete.TabIndex = 1;
      // 
      // btnEdit
      // 
      this.btnEdit.BackColor = System.Drawing.Color.White;
      this.btnEdit.Text = "Sửa";
      this.btnEdit.AutoSize = true;
      this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.btnEdit.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
      this.btnEdit.Name = "btnEdit";
      this.btnEdit.Size = new System.Drawing.Size(75, 30);
      this.btnEdit.TabIndex = 2;
      // 
      // btnAdd
      // 
      this.btnAdd.BackColor = System.Drawing.Color.White;
      this.btnAdd.Text = "Thêm";
      this.btnAdd.AutoSize = true;
      this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
      this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(75, 30);
      this.btnAdd.TabIndex = 3;
      // 
      // DetailFormGUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.flpActionsPanel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "DetailFormGUI";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "DetailFormGUI";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
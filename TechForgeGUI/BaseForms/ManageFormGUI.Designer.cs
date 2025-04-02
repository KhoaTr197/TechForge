namespace TechForgeGUI.BaseForms
{
  partial class ManageFormGUI
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.btnAdd = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.button4 = new System.Windows.Forms.Button();
      this.txtSearchbar = new System.Windows.Forms.TextBox();
      this.dgvMainList = new TechForgeGUI.BaseControls.CustomDataGridView();
      this.panel1.SuspendLayout();
      this.flowLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.flowLayoutPanel1);
      this.panel1.Controls.Add(this.txtSearchbar);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Margin = new System.Windows.Forms.Padding(0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1203, 159);
      this.panel1.TabIndex = 1;
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.Controls.Add(this.btnAdd);
      this.flowLayoutPanel1.Controls.Add(this.button2);
      this.flowLayoutPanel1.Controls.Add(this.button3);
      this.flowLayoutPanel1.Controls.Add(this.button4);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(864, 0);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(339, 159);
      this.flowLayoutPanel1.TabIndex = 1;
      // 
      // btnAdd
      // 
      this.btnAdd.Location = new System.Drawing.Point(3, 3);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(80, 48);
      this.btnAdd.TabIndex = 0;
      this.btnAdd.Text = "btnAdd";
      this.btnAdd.UseVisualStyleBackColor = true;
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(89, 3);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(80, 48);
      this.button2.TabIndex = 1;
      this.button2.Text = "button2";
      this.button2.UseVisualStyleBackColor = true;
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(175, 3);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(80, 48);
      this.button3.TabIndex = 2;
      this.button3.Text = "button3";
      this.button3.UseVisualStyleBackColor = true;
      // 
      // button4
      // 
      this.button4.Location = new System.Drawing.Point(3, 57);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(80, 48);
      this.button4.TabIndex = 3;
      this.button4.Text = "button4";
      this.button4.UseVisualStyleBackColor = true;
      // 
      // txtSearchbar
      // 
      this.txtSearchbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtSearchbar.Location = new System.Drawing.Point(8, 8);
      this.txtSearchbar.Margin = new System.Windows.Forms.Padding(8);
      this.txtSearchbar.Name = "txtSearchbar";
      this.txtSearchbar.Size = new System.Drawing.Size(413, 32);
      this.txtSearchbar.TabIndex = 0;
      // 
      // dgvMainList
      // 
      this.dgvMainList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvMainList.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.dgvMainList.Location = new System.Drawing.Point(0, 159);
      this.dgvMainList.Margin = new System.Windows.Forms.Padding(0);
      this.dgvMainList.Name = "dgvMainList";
      this.dgvMainList.Size = new System.Drawing.Size(1203, 487);
      this.dgvMainList.TabIndex = 2;
      // 
      // ManageFormGUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1203, 646);
      this.Controls.Add(this.dgvMainList);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "ManageFormGUI";
      this.Text = "InteractiveListForm";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.flowLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.TextBox txtSearchbar;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button4;
    private BaseControls.CustomDataGridView dgvMainList;
  }
}
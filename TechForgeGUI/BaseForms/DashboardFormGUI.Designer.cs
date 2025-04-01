namespace TechForgeGUI.BaseForm
{
  partial class DashboardFormGUI
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
      this.IconList = new System.Windows.Forms.ImageList(this.components);
      this.sideBar1 = new TechForgeGUI.BaseControls.Sidebar();
      this.panelMain = new System.Windows.Forms.Panel();
      this.SuspendLayout();
      // 
      // IconList
      // 
      this.IconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.IconList.ImageSize = new System.Drawing.Size(16, 16);
      this.IconList.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // sideBar1
      // 
      this.sideBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
      this.sideBar1.Dock = System.Windows.Forms.DockStyle.Left;
      this.sideBar1.Location = new System.Drawing.Point(0, 0);
      this.sideBar1.Name = "sideBar1";
      this.sideBar1.Size = new System.Drawing.Size(160, 729);
      this.sideBar1.TabIndex = 1;
      // 
      // panelMain
      // 
      this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelMain.Location = new System.Drawing.Point(160, 0);
      this.panelMain.Margin = new System.Windows.Forms.Padding(0);
      this.panelMain.Name = "panelMain";
      this.panelMain.Size = new System.Drawing.Size(1190, 729);
      this.panelMain.TabIndex = 5;
      // 
      // DashboardForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1350, 729);
      this.Controls.Add(this.panelMain);
      this.Controls.Add(this.sideBar1);
      this.Name = "DashboardForm";
      this.Text = "DashboardForm";
      this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.ImageList IconList;
    private BaseControls.Sidebar sideBar1;
    private System.Windows.Forms.Panel panelMain;
  }
}
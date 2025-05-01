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
      components = new System.ComponentModel.Container();
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.tlpMain);
      this.Name = "HomePageGUI";
      this.Size = new System.Drawing.Size(1368, 641);
      this.tlpMain.ResumeLayout(false);
      this.tlpWelcome.ResumeLayout(false);
      this.tlpWelcome.PerformLayout();
      this.flowLayoutPanel2.ResumeLayout(false);
      this.flowLayoutPanel2.PerformLayout();
      this.panel1.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tlpMain;
    private System.Windows.Forms.TableLayoutPanel tlpWelcome;
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

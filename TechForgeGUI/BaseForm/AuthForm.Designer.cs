namespace TechForgeGUI
{
  partial class AuthForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthForm));
      this.panelMain = new System.Windows.Forms.Panel();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.imgListIcons = new System.Windows.Forms.ImageList(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // panelMain
      // 
      this.panelMain.Dock = System.Windows.Forms.DockStyle.Right;
      this.panelMain.Location = new System.Drawing.Point(484, 0);
      this.panelMain.Name = "panelMain";
      this.panelMain.Size = new System.Drawing.Size(400, 461);
      this.panelMain.TabIndex = 0;
      // 
      // pictureBox1
      // 
      this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pictureBox1.ErrorImage = null;
      this.pictureBox1.Image = global::TechForgeGUI.Properties.Resources.PlaceholderImg;
      this.pictureBox1.Location = new System.Drawing.Point(0, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(484, 461);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.pictureBox1.TabIndex = 1;
      this.pictureBox1.TabStop = false;
      // 
      // imgListIcons
      // 
      this.imgListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListIcons.ImageStream")));
      this.imgListIcons.TransparentColor = System.Drawing.Color.Transparent;
      this.imgListIcons.Images.SetKeyName(0, "key_icon.jpg");
      this.imgListIcons.Images.SetKeyName(1, "user_icon.jpg");
      // 
      // AuthForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(884, 461);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.panelMain);
      this.Name = "AuthForm";
      this.Text = "AuthForm";
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panelMain;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.ImageList imgListIcons;
  }
}
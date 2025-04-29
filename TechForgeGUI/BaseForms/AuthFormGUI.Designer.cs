namespace TechForgeGUI
{
  partial class AuthFormGUI
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthFormGUI));
      this.panelMain = new System.Windows.Forms.Panel();
      this.picFormImg = new System.Windows.Forms.PictureBox();
      this.imgListIcons = new System.Windows.Forms.ImageList(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.picFormImg)).BeginInit();
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
      // picFormImg
      // 
      this.picFormImg.Dock = System.Windows.Forms.DockStyle.Fill;
      this.picFormImg.ErrorImage = null;
      this.picFormImg.Location = new System.Drawing.Point(0, 0);
      this.picFormImg.Name = "picFormImg";
      this.picFormImg.Size = new System.Drawing.Size(484, 461);
      this.picFormImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
      this.picFormImg.TabIndex = 1;
      this.picFormImg.TabStop = false;
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
      this.Controls.Add(this.picFormImg);
      this.Controls.Add(this.panelMain);
      this.Name = "AuthForm";
      this.Text = "AuthForm";
      ((System.ComponentModel.ISupportInitialize)(this.picFormImg)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panelMain;
    private System.Windows.Forms.PictureBox picFormImg;
    private System.Windows.Forms.ImageList imgListIcons;
  }
}
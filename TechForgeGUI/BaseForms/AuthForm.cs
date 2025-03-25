using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechForgeGUI
{
  public partial class AuthForm : Form
  {
    protected Panel panelForm;
    protected Label lblTitle;
    protected Label lblUsername;
    protected Label lblPassword;
    protected TextBox txtUsername;
    protected TextBox txtPassword;
    protected Button btnSubmit;
    protected string DefaultFontName = "Segoe UI";
    public AuthForm()
    {
      InitializeComponent();
      InitializeBaseControls();

      this.StartPosition = FormStartPosition.CenterScreen;

      this.Icon = Properties.Resources.AppIcon;
    }
    // Init some Base Controls
    private void InitializeBaseControls()
    {
      //Init Panel
      panelForm = new Panel
      {
        AutoSize = true,
        AutoSizeMode = AutoSizeMode.GrowAndShrink,
      };

      //Init Auth Form (not the WinForm) Title
      lblTitle = new Label
      {
        Text = "Authentication",
        TextAlign = ContentAlignment.MiddleCenter,
        Font = new Font(DefaultFontName, 16), 
        Size = new Size(200, 30),
        Margin = new Padding(500),
        Location = new Point((panelMain.ClientSize.Width - 200) / 2, 20)
      };

      //Init Username Label & Textbox
      lblUsername = new Label
      {
        AutoSize = true,
        Text = "Username:",
        Font = new Font(DefaultFontName, 10),
        Location = new Point(40, 90)
      };
      txtUsername = new TextBox
      {
        Font = new Font(DefaultFontName, 10),
        Size = new Size(200, 20),
        Location = new Point(150, 90)
      };

      //Init Password Label & Textbox
      lblPassword = new Label
      {
        Text = "Password:",
        Font = new Font(DefaultFontName, 10),
        Location = new Point(40, 130)
      };
      txtPassword = new TextBox
      {
        Font = new Font(DefaultFontName, 10),
        Size = new Size(200, 20),
        Location = new Point(150, 130),
        UseSystemPasswordChar = true
      };

      //Init Submit Button
      btnSubmit = new Button
      {
        Text = "Submit",
        Size = new Size(100, 30),
        Location = new Point((panelMain.ClientSize.Width - 100) / 2, 180)
      };

      panelForm.Controls.AddRange(new Control[] { 
        lblTitle,
        lblUsername,
        txtUsername,
        lblPassword,
        txtPassword,
        btnSubmit
      });

      panelForm.Location = new Point(0, panelForm.ClientSize.Height);

      panelMain.Controls.Add(panelForm);
    }
  }
}

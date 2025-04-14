using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeDTO;
using TechForgeBUS;
using System.Linq.Expressions;

namespace TechForgeGUI
{
  public partial class AuthFormGUI : Form
  {
    protected Panel panelForm;
    protected Label lblTitle;
    protected Label lblUsername;
    protected Label lblPassword;
    protected TextBox txtUsername;
    protected TextBox txtPassword;
    protected Button btnSubmit;
    protected string DefaultFontName = "Segoe UI";
        protected readonly string connStr = "Data Source=.;Initial Catalog=TECHFORGE;Integrated Security=True;";
        protected TaiKhoanDTO taiKhoanDto;
        protected TaiKhoanBUS taiKhoanBus;
        protected NguoiDungBUS nguoiDungBus;
        protected NguoiDungDTO nguoiDungDto;
        protected Form frmMain;
    public AuthFormGUI()
    {
      InitializeComponent();
      InitializeBaseControls();

      this.StartPosition = FormStartPosition.CenterScreen;
      this.AcceptButton = btnSubmit;
      this.Icon = Properties.Resources.AppIcon;
            //Init bus
            taiKhoanBus = new TaiKhoanBUS(this.connStr);
            nguoiDungBus = new NguoiDungBUS(this.connStr);
            //Init DTO
            taiKhoanDto = new TaiKhoanDTO();
            nguoiDungDto = new NguoiDungDTO();
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
            btnSubmit.Click += BtnSubmit_Click;

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
        public bool ValidateAccount(string username, string password, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrEmpty(username))
            {
                message = "Tên tài khoản không được để trống!";
                return false;
            }
            if(string.IsNullOrEmpty(password))
            {
                message = "Mật khẩu không được để trống!";
                return false;
            }
            if (password.Length < 6)
            {
                message = "Mật khẩu phải có ít nhất 6 ký tự.";
                return false;
            }
            if (username.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                message = "Tên đăng nhập chỉ được chứa chữ cái và số.";
                return false;
            }

            return true;
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            if(ValidateAccount(username, password, out string message))
            {
                taiKhoanDto = taiKhoanBus.Login(username, password);
                if(taiKhoanDto == null)
                {
                    MessageBox.Show("khong co tai khoan");
                }
                else
                {
                    nguoiDungDto = nguoiDungBus.GetByID(taiKhoanDto.MaND);
                    
                    switch (nguoiDungDto.VaiTro)
                    {
                        case "ADMIN":
                            {
                                frmMain = new ManagerFormGUI(taiKhoanDto, nguoiDungDto);
                                frmMain.Show();
                                break;
                            }
                        case "Quản Lý Kho":
                            {
                                MessageBox.Show("Chua co!");
                                this.Close();
                                break;
                            }
                        case "Thu Ngân":
                            {
                                frmMain = new CashierFormGUI(taiKhoanDto, nguoiDungDto);
                                frmMain.Show();
                                break;
                            }
                    }
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show(message);
            }
        }
    }
}

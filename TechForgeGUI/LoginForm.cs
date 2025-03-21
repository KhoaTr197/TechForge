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
  public partial class LoginForm : AuthForm
  {
    public LoginForm()
    {
      InitializeComponent();

      this.lblTitle.Text = "Đăng Nhập";
      this.lblUsername.Text = "Tên Tài Khoản:";
      this.lblPassword.Text = "Mật Khẩu:";
      this.btnSubmit.Text = "Đăng Nhập";
    }
  }
}

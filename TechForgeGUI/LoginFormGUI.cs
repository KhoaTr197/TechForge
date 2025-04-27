using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeBUS;
using TechForgeDTO;

namespace TechForgeGUI
{
    public partial class LoginFormGUI : AuthFormGUI
    {
    public string UserRole { get; set; }
    public NguoiDungDTO UserInfo { get; set; }
    public TaiKhoanDTO UserCredential { get; set; }
    public LoginFormGUI()
    {
      InitializeComponent();

      this.lblTitle.Text = "Đăng Nhập";
      this.lblUsername.Text = "Tên Tài Khoản:";
      this.lblPassword.Text = "Mật Khẩu:";
      this.btnSubmit.Text = "Đăng Nhập";

      btnSubmit.Click += BtnSubmit_Click;
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
      string username = txtUsername.Text.Trim();
      string password = txtPassword.Text.Trim();
      if (ValidateAccount(username, password, out string message))
      {
        taiKhoanDto = taiKhoanBus.Login(username, password);
        if (taiKhoanDto == null)
        {
          MessageBox.Show("khong co tai khoan");
        }
        else
        {
          nguoiDungDto = nguoiDungBus.GetByID(taiKhoanDto.MaND);

          this.DialogResult = DialogResult.OK;
          this.UserRole = nguoiDungDto.VaiTro;
          this.UserInfo = nguoiDungDto;
          this.UserCredential = taiKhoanDto;

          logBUS.Add(new LichSuHoatDongDTO()
          {
            MaND = nguoiDungDto.MaND,
            ThoiGian = DateTime.Now,
            NoiDung = "Đã đăng nhập vào hệ thống",
            VaiTro = nguoiDungDto.VaiTro,
          });

          this.Close();
        }
      }
      else
      {
        MessageBox.Show(message);
      }
    }

  }
}

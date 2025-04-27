using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeDTO;
using TechForgeGUI.BaseForm;
using TechForgeGUI.Utils;

namespace TechForgeGUI
{
  internal static class Program
  {
    /// <summary>  
    /// The main entry point for the application.  
    /// </summary>  
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      GlobalStatics.SetUp();

      using (LoginFormGUI loginForm = new LoginFormGUI())
      {
        if (loginForm.ShowDialog() == DialogResult.OK)
        {
          Form frmMain = null;
          if (loginForm.UserRole == "ADMIN")
          {
            frmMain = new ManagerFormGUI(loginForm.UserCredential, loginForm.UserInfo);
          }
          else if (loginForm.UserRole == "Quản Lý Kho")
          {
            frmMain = new WarehouseStaffFormGUI(loginForm.UserCredential, loginForm.UserInfo);
          }
          else if (loginForm.UserRole == "Thu Ngân")
          {
            frmMain = new CashierFormGUI(loginForm.UserCredential, loginForm.UserInfo);
          }

          if (frmMain != null)
          {
            Application.Run(frmMain);
          }
          else
          {
            throw new InvalidOperationException("Unknown Rule");
          }
        }
      }
    }
  }
}

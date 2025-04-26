using System;
using TechForgeDTO;
using TechForgeGUI.BaseForm;

namespace TechForgeGUI
{
  public partial class CashierFormGUI : DashboardFormGUI
  {
    public CashierFormGUI(TaiKhoanDTO _currentAccount, NguoiDungDTO _currentUser)
    {
      this.Text = "TechForge - Cashier";
      this.SetUpForm("Cashier", _currentAccount, _currentUser);
      this.SetUpSidebar();
    }
  }
}

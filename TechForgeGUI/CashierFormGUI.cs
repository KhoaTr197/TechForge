using System;
using TechForgeGUI.BaseForm;

namespace TechForgeGUI
{
  public partial class CashierFormGUI : DashboardFormGUI
  {
    public CashierFormGUI()
    {
      this.Text = "TechForge - Cashier";
      this.SetUpForm("Cashier");
      this.SetUpSidebar();
    }
  }
}

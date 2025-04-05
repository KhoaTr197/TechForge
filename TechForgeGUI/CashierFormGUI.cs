using System;
using TechForgeGUI.BaseForm;

namespace TechForgeGUI
{
  public partial class CashierFormGUI : DashboardFormGUI
  {
    public CashierFormGUI()
    {
      SetupSidebar();
      this.Text = "TechForge - Cashier";
      this.SetUpSidebar("Cashier");
    }
  }
}

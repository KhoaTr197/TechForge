using TechForgeGUI.BaseForm;

namespace TechForgeGUI
{
  public partial class ManagerFormGUI : DashboardFormGUI
  {
    public ManagerFormGUI()
    {
      this.Text = "TechForge - Manager";
      this.SetUpForm("Manager");
      this.SetUpSidebar();
    }
  }
}

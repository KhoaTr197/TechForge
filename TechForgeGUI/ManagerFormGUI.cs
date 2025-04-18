using TechForgeBUS;
using TechForgeDTO;
using TechForgeGUI.BaseForm;

namespace TechForgeGUI
{
  public partial class ManagerFormGUI : DashboardFormGUI
  {
    public ManagerFormGUI(TaiKhoanDTO _currentAccount, NguoiDungDTO _currentUser)
        {
            this.Text = "TechForge - Manager";
            this.SetUpForm("Manager", _currentAccount, _currentUser);
            this.SetUpSidebar();
        }
    }
    
}

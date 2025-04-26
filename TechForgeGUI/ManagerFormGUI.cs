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

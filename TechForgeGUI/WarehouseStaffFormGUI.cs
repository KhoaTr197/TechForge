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
  public partial class WarehouseStaffFormGUI : DashboardFormGUI
  {
    public WarehouseStaffFormGUI(TaiKhoanDTO _currentAccount, NguoiDungDTO _currentUser)
    {
      this.Text = "TechForge - Warehouse Staff";
      this.SetUpForm("WarehouseStaff", _currentAccount, _currentUser);
      this.SetUpSidebar();
    }
  }
}

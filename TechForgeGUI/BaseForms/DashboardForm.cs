using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeGUI.BaseControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace TechForgeGUI.BaseForm
{
  public partial class DashboardForm : Form
  {
    public DashboardForm()
    {
      InitializeComponent();

      List<SidebarTabItem> tabs = new List<SidebarTabItem> {
        new SidebarTabItem{ Id="Homepage", Text="Trang Chủ" },
        new SidebarTabItem{ Id="Product", Text="Sản Phẩm" },
        new SidebarTabItem{ Id="Invoice", Text="Đơn Hàng" },
        new SidebarTabItem{ Id="Customer", Text="Khách Hàng" },
        new SidebarTabItem{ Id="Logout", Text="Đăng Xuất" },
      };

      sideBar1.Init(tabs);
      sideBar1.SelectedTabChanged += Hello;

    }
    private void Hello(object sender, SidebarTabChangedEventArgs e) {
      label1.Text = sideBar1.SelectedTab.Text.ToString();
    }

  }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Svg;
using TechForgeGUI.BaseControls;
using TechForgeGUI.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace TechForgeGUI.BaseForm
{
  public partial class DashboardForm : Form
  {
    public DashboardForm()
    {
      InitializeComponent();

      SVGIconHandler iconHandler = new SVGIconHandler("Resources", new Size(16, 16));

      Dictionary<string, Bitmap> icons = iconHandler.ConvertToBitmap();

      foreach (KeyValuePair<string, Bitmap> icon in icons)
      {
        string name = icon.Key;
        Bitmap bitmap = icon.Value;
        IconList.Images.Add(name, bitmap);
      }

      List<SidebarTabItem> tabs = new List<SidebarTabItem> {
        new SidebarTabItem{ Id="Homepage", ImageList=IconList, ImageKey="homepage_icon", Text="Trang Chủ" },
        new SidebarTabItem{ Id="Product", ImageList=IconList, ImageKey="box_icon", Text="Sản Phẩm" },
        new SidebarTabItem{ Id="Invoice", ImageList=IconList, ImageKey="receipt_icon", Text="Đơn Hàng" },
        new SidebarTabItem{ Id="Customer", ImageList=IconList, ImageKey="users_icon", Text="Khách Hàng" },
        new SidebarTabItem{ Id="Logout", ImageList=IconList, ImageKey="logout_icon", Text="Đăng Xuất" },
      };

      sideBar1.Init(tabs);
      sideBar1.SelectedTabChanged += Hello;

    }
    private void Hello(object sender, SidebarSelectedTabChangedEventArgs e) {
      label1.Text = sideBar1.SelectedTab.Text.ToString();
    }

  }
}

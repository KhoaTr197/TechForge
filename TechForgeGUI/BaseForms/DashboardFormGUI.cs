using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Svg;
using TechForgeGUI.BaseControls;
using TechForgeGUI.BaseForms;
using TechForgeGUI.SubForms;
using TechForgeGUI.Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace TechForgeGUI.BaseForm
{
  public partial class DashboardFormGUI : Form
  {
    private ManagePage currentPage;
    public DashboardFormGUI()
    {
      InitializeComponent();
    }
    public void SetUpSidebar(string job = "Cashier")
    {
      List<SidebarTabItem> tabs = null;
      switch (job)
      {
        case "Cashier":
          tabs = new List<SidebarTabItem>() {
            new SidebarTabItem { Id="Homepage", ImageList=GlobalStatics.iconList, ImageKey="homepage_icon", Text="Trang Chủ" },
            new SidebarTabItem {
              Id="Product", ImageList=GlobalStatics.iconList, ImageKey="box_icon", Text="Sản Phẩm",
              SubSidebarItems = new List<SidebarTabItem>
              {
                new SidebarTabItem{ Id="Manufacturer", ImageList=GlobalStatics.iconList, ImageKey="box_icon", Text="Hãng" },
                new SidebarTabItem{ Id="Category", ImageList=GlobalStatics.iconList, ImageKey="box_icon", Text="Danh Mục" },
              }
            },
            new SidebarTabItem{ Id="Invoice", ImageList=GlobalStatics.iconList, ImageKey="receipt_icon", Text="Đơn Hàng" },
            new SidebarTabItem{ Id="Users", ImageList=GlobalStatics.iconList, ImageKey="users_icon", Text="Người Dùng" },
            new SidebarTabItem{ Id="Customer", ImageList=GlobalStatics.iconList, ImageKey="users_icon", Text="Khách Hàng" },
            new SidebarTabItem{ Id="Logout", ImageList=GlobalStatics.iconList, ImageKey="logout_icon", Text="Đăng Xuất" },
          };
          break;
        case "Manager":
          tabs = new List<SidebarTabItem>
          {
            new SidebarTabItem { Id = "Homepage", ImageList = GlobalStatics.iconList, ImageKey = "homepage_icon", Text = "Trang Chủ" },
            new SidebarTabItem { Id = "Product", ImageList = GlobalStatics.iconList, ImageKey = "box_icon", Text = "Sản Phẩm" },
            new SidebarTabItem { Id = "Supplier", ImageList = GlobalStatics.iconList, ImageKey = "supplier_icon", Text = "Nhà cung cấp" },
            new SidebarTabItem { Id="Logout", ImageList=GlobalStatics.iconList, ImageKey="logout_icon", Text="Đăng Xuất" }
          };
          break;
        case "WarehouseStaff":
          tabs = new List<SidebarTabItem>
          {
            new SidebarTabItem { Id = "Homepage", ImageList = GlobalStatics.iconList, ImageKey = "homepage_icon", Text = "Trang Chủ" },
            new SidebarTabItem { Id = "Product", ImageList = GlobalStatics.iconList, ImageKey = "box_icon", Text = "Sản Phẩm" },
            new SidebarTabItem { Id = "Warehouse", ImageList = GlobalStatics.iconList, ImageKey = "warehouse_icon", Text = "Kho" },
          };
          break;
      }

      //Set up sidebar
      sideBar1.Init(tabs);
      sideBar1.SelectedTabChanged += OpenSubForm;
    }
    protected void OpenSubForm(object sender, SidebarSelectedTabChangedEventArgs e) {
      if (currentPage != null)
        panelMain.Controls.Remove(currentPage);
      //sideBar1.Init(tabs);
      //sideBar1.SelectedTabChanged += Hello;

    }

    public void SetupSidebar(string job = "Cashier")
        {
            List<SidebarTabItem> tabs = null;
            switch (job)
            {
                case "Cashier":
                    {
                        tabs = new List<SidebarTabItem> {
                            new SidebarTabItem{ Id="Homepage", ImageList=GlobalStatics.iconList, ImageKey="homepage_icon", Text="Trang Chủ" },
                            new SidebarTabItem{ Id="Product", ImageList=GlobalStatics.iconList, ImageKey="box_icon", Text="Sản Phẩm" },
                            new SidebarTabItem{ Id="Invoice", ImageList=GlobalStatics.iconList, ImageKey="receipt_icon", Text="Đơn Hàng" },
                            new SidebarTabItem{ Id="Users", ImageList=GlobalStatics.iconList, ImageKey="users_icon", Text="Người Dùng" },
                            new SidebarTabItem{ Id="Customer", ImageList=GlobalStatics.iconList, ImageKey="users_icon", Text="Khách Hàng" },
                            new SidebarTabItem{ Id="Logout", ImageList=GlobalStatics.iconList, ImageKey="logout_icon", Text="Đăng Xuất" },
                        };
                    }
                    break;
                case "Manager":
                    {
                        tabs = new List<SidebarTabItem> {
                            new SidebarTabItem{ Id="Homepage", ImageList=GlobalStatics.iconList, ImageKey="homepage_icon", Text="Trang Chủ" },
                            new SidebarTabItem{ Id="Product", ImageList=GlobalStatics.iconList, ImageKey="box_icon", Text="Sản Phẩm" },
                            //new SidebarTabItem{ Id="Invoice", ImageList=GlobalStatics.iconList, ImageKey="receipt_icon", Text="Đơn Hàng" },
                            new SidebarTabItem{ Id="Provider", ImageList=GlobalStatics.iconList, ImageKey="provider_icon", Text="Nhà cung cấp" },
                            //new SidebarTabItem{ Id="Users", ImageList=GlobalStatics.iconList, ImageKey="users_icon", Text="Người Dùng" },
                            new SidebarTabItem{ Id="Logout", ImageList=GlobalStatics.iconList, ImageKey="logout_icon", Text="Đăng Xuất" },
                        };
                    }
                    break;
                case "WarehouseStaff":
                    {
                        tabs = new List<SidebarTabItem> {
                            new SidebarTabItem{ Id="Homepage", ImageList=GlobalStatics.iconList, ImageKey="homepage_icon", Text="Trang Chủ" },
                            new SidebarTabItem{ Id="Product", ImageList=GlobalStatics.iconList, ImageKey="box_icon", Text="Sản Phẩm" },
                            new SidebarTabItem{ Id="Warehouse", ImageList=GlobalStatics.iconList, ImageKey="warehouse_icon", Text="Kho" },
                            new SidebarTabItem{ Id="Provider", ImageList=GlobalStatics.iconList, ImageKey="provider_icon", Text="Nhà cung cấp" },
                            new SidebarTabItem{ Id="Logout", ImageList=GlobalStatics.iconList, ImageKey="logout_icon", Text="Đăng Xuất" },
                        };
                    } break;
            }
            sideBar1.Init(tabs);
            sideBar1.SelectedTabChanged += Hello;
        }
    protected void Hello(object sender, SidebarSelectedTabChangedEventArgs e) {
      if (currentForm != null)
        currentForm.Close();

      switch (sideBar1.SelectedTab.Id)
      {
        case "Product":
          {
            currentPage = new ProductManagePageGUI();
            panelMain.Controls.Add(currentPage);
            break;
          }
        case "Users":
          {
            currentPage = new UserManagePageGUI();
            panelMain.Controls.Add(currentPage);
            break;
          }
        case "Supplier":
          {
            currentPage = new SupplierManagePageGUI();
            panelMain.Controls.Add(currentPage);
            currentPage.Show();
            break;
          }
        case "Provider":
            {
                currentForm = new ProviderManageFormGUI();
                panelMain.Controls.Add(currentForm);
                currentForm.Show();
                break;
            }
        default:
          break;
      }
    }

  }
}

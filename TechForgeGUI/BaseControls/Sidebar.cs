using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechForgeGUI.BaseControls
{
  public class SidebarTabItem : Button
  {
    public string Id { get; set; }

    public SidebarTabItem(string id=null)
    {
      Id = id;
    }
  }
  public class SidebarTabChangedEventArgs : EventArgs
  {
    public SidebarTabChangedEventArgs()
    {
    }
  }
  public partial class Sidebar : UserControl
  {
    //Base Controls
    protected Panel panelLogo;
    protected Panel flpTabs;
    //Properties
    protected List<SidebarTabItem> Tabs;
    public SidebarTabItem SelectedTab;
    protected Color BgColor { get; set; } = Color.FromArgb(34, 34, 34);
    protected Color TabBgColor { get; set; } = Color.Transparent;
    protected Color TabTextColor { get; set; } = Color.White;
    protected Color TabHoverColor { get; set; } = Color.FromArgb(254, 86, 37);
    protected string DefaultFontName = "Segoe UI";
    public event EventHandler<SidebarTabChangedEventArgs> SelectedTabChanged;
    public Sidebar()
    {
      InitializeComponent();

      this.BackColor = BgColor;
      this.Tabs = new List<SidebarTabItem>();
      this.Size = new Size(160, 480);

      this.panelLogo = new Panel() {
        BackColor = TabBgColor,
        Size = new Size(160, 48),
        Location = new Point(0, 0),
      };
      this.flpTabs = new FlowLayoutPanel() {
        Location = new Point(0, panelLogo.Size.Height),
        FlowDirection = FlowDirection.TopDown,
        AutoSize = true,
        AutoSizeMode = AutoSizeMode.GrowOnly,
      };

      this.SelectedTab = null;
      panelLogo.BackgroundImage = null;

      this.Controls.Add(panelLogo);
      this.Controls.Add(flpTabs);
    }
    public void Init(List<SidebarTabItem> tabs)
    {
      this.Tabs = tabs;

      this.CreateTabItems();
    }
    private void CreateTabItems()
    {

      int n = this.Tabs.Count;
      int tabHeight = (int)Math.Round(this.Size.Width / 3.33333);

      for (int i = 0; i < n; i++)
      {
          SidebarTabItem tabItem = new SidebarTabItem
          {
            Id = Tabs[i].Id,
            Text = Tabs[i].Text,
            Font = new Font(DefaultFontName, 10, FontStyle.Bold),
            FlatStyle = FlatStyle.Flat,
            ForeColor = TabTextColor,
            BackColor = TabBgColor,
            Size = new Size(this.Size.Width, tabHeight),
            Margin = new Padding(0),
          };
          tabItem.FlatAppearance.BorderSize = 0;
          tabItem.FlatAppearance.MouseOverBackColor = TabHoverColor;

          tabItem.MouseEnter += TabItem_MouseEnter;
          tabItem.MouseLeave += TabItem_MouseLeave;
          tabItem.Click += TabItem_Click;
          
          //if (i == 0)
          //{
          //  SelectTabItem(tabItem);
          //}

          flpTabs.Controls.Add(tabItem);
      }
    }
    private void SelectTabItem(SidebarTabItem tabItem)
    {
      SelectedTab = tabItem;
      SelectedTab.BackColor = TabHoverColor;
    }
    private void TabItem_Click(object sender, EventArgs e)
    {
      if (SelectedTab != null)
      {
        SelectedTab.BackColor = TabBgColor;
      }
      SelectTabItem((SidebarTabItem)sender);
      if(SelectedTabChanged != null)
        SelectedTabChanged.Invoke(this, new SidebarTabChangedEventArgs());
    }
    private void TabItem_MouseEnter(object sender, EventArgs e)
    {
      SidebarTabItem tabItem = (SidebarTabItem)sender;
      if (SelectedTab != null && tabItem != SelectedTab)
      {
        tabItem.BackColor = this.TabHoverColor;
      }
    }
    private void TabItem_MouseLeave(object sender, EventArgs e)
    {
      SidebarTabItem tabItem = (SidebarTabItem)sender;
      if (SelectedTab != null && tabItem != SelectedTab)
      {
        tabItem.BackColor = this.TabBgColor;
      }
    }
  }
}

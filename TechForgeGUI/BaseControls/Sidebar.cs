using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechForgeGUI.BaseControls
{
  //Sidebar Tab Item
  public class SidebarTabItem : Button
  {
    public string Id { get; set; }
    public SidebarTabItem ParentSidebarItem { get; set; }
    public List<SidebarTabItem> SubSidebarItems { get; set; }
    public SidebarTabItem(string id=null, SidebarTabItem tab=null, List<SidebarTabItem> tabs = null)
    {
      Id = id;
      ParentSidebarItem = tab;
      SubSidebarItems = tabs;

      FlatStyle = FlatStyle.Flat;
      FlatAppearance.BorderSize = 0;
      Margin = new Padding(0);
    }
  }
  //Sidebar Tab Style
  public class SidebarStyle
  {
    public Color BgColor { get; set; }
    public Color TextColor { get; set; }
    public Color HoverColor { get; set; }
    public SidebarStyle(Color bgColor = default, Color textColor = default, Color hoverColor = default)
    {
      BgColor = bgColor == default ? Color.Transparent : bgColor;
      TextColor = textColor == default ? Color.White : textColor;
      HoverColor = hoverColor == default ? Color.FromArgb(254, 86, 37) : hoverColor;
    }
  }
  //Custom Event Classes
  public class SidebarTabItemsChangedEventArgs : EventArgs
  {
    public SidebarTabItemsChangedEventArgs()
    {
    }
  }
  public class SidebarSelectedTabChangedEventArgs : EventArgs
  {
    public SidebarSelectedTabChangedEventArgs()
    {
    }
  }
  public partial class Sidebar : UserControl
  {
    //Base Controls
    protected Panel panelLogo;
    protected Panel flpTabs;
    private Panel spacer;
    //Properties
    protected List<SidebarTabItem> Tabs;
    public SidebarTabItem SelectedTab;
    protected SidebarStyle Style { get; set; }
    protected string DefaultFontName = "Segoe UI";
    //Custom Events
    public event EventHandler<SidebarTabItemsChangedEventArgs> TabItemsChanged;
    public event EventHandler<SidebarSelectedTabChangedEventArgs> SelectedTabChanged;
    public Sidebar()
    {
      //Init Component (default)
      InitializeComponent();

      //Init Sidebar Control and add some styles
      this.Tabs = new List<SidebarTabItem>();
      this.Style = new SidebarStyle();
      this.BackColor = this.Style.BgColor;
      this.Size = new Size(160, 1000);
      this.DockChanged += Sidebar_DockChanged;
      this.TabItemsChanged += Sidebar_ItemsChanged;

      //Init a Panel for Logo 
      this.panelLogo = new Panel()
      {
        BackgroundImage = null,
        BackColor = this.Style.BgColor,
        Size = new Size(160, 64),
        Location = new Point(0, 0),
      };
      //Init a Flow Panel Layout for Tab Items
      this.flpTabs = new FlowLayoutPanel()
      {
        Location = new Point(0, panelLogo.Size.Height),
        FlowDirection = FlowDirection.TopDown,
        Size = new Size(this.Width, this.Size.Height - panelLogo.Size.Height),
      };
      //Init a Panel for Spacing
      this.spacer = new Panel()
      {
        Dock = DockStyle.Fill,
      };

      //Init Selected Tab as null
      this.SelectedTab = null;

      //Add 2 Panels into this Sidebar control
      this.Controls.Add(panelLogo);
      this.Controls.Add(flpTabs);
    }
    public void Init(List<SidebarTabItem> tabs)
    {
      this.Tabs = tabs;

      this.CreateTabItems();

      TabItemsChanged.Invoke(this, new SidebarTabItemsChangedEventArgs());
    }
    private void CreateTabItems()
    {
      int n = this.Tabs.Count;
      int tabHeight = (int)Math.Round(this.Size.Width / 3.33333);

      for (int i = 0; i < n; i++)
      {
        SidebarTabItem tab = this.Tabs[i];
        tab.Text = $"   {Tabs[i].Text}";
        tab.Font = new Font(DefaultFontName, 10, FontStyle.Bold);
        tab.ImageAlign = ContentAlignment.MiddleLeft;
        tab.ImageList = this.Tabs[i].ImageList;
        tab.ImageKey = Tabs[i].ImageKey;
        tab.TextImageRelation = TextImageRelation.ImageBeforeText;
        tab.ForeColor = this.Style.TextColor;
        tab.BackColor = this.Style.BgColor;
        tab.Size = new Size(this.Size.Width, tabHeight);
        tab.Padding = new Padding(8, 0, 8, 0);
        tab.FlatAppearance.MouseOverBackColor = this.Style.HoverColor;

        tab.MouseEnter += TabItem_MouseEnter;
        tab.MouseLeave += TabItem_MouseLeave;
        tab.Click += TabItem_Click;

        if (i == 0)
        {
          SelectTabItem(tab);
        }

        flpTabs.Controls.Add(tab);

        if (tab.SubSidebarItems != null && tab.SubSidebarItems.Count > 0)
        {
          CreateSubTabItems(tab, tabHeight);
        }

        if (i == n - 1)
        {
          flpTabs.Controls.Add(spacer);
        }
      }
    }

    private void CreateSubTabItems(SidebarTabItem tab, int tabHeight)
    {
      foreach (var subTab in tab.SubSidebarItems)
      {
        subTab.Text = $"    {subTab.Text}";
        subTab.Font = new Font(DefaultFontName, 9);
        subTab.ImageAlign = ContentAlignment.MiddleLeft;
        subTab.ImageList = subTab.ImageList;
        subTab.ImageKey = subTab.ImageKey;
        subTab.TextImageRelation = TextImageRelation.ImageBeforeText;
        subTab.ForeColor = this.Style.TextColor;
        subTab.BackColor = this.Style.BgColor;
        subTab.Size = new Size(this.Size.Width, tabHeight);
        subTab.Padding = new Padding(16, 0, 8, 0);
        subTab.FlatAppearance.MouseOverBackColor = this.Style.HoverColor;
        subTab.ParentSidebarItem = tab;

        subTab.MouseEnter += TabItem_MouseEnter;
        subTab.MouseLeave += TabItem_MouseLeave;
        subTab.Click += TabItem_Click;

        subTab.Visible = false;

        flpTabs.Controls.Add(subTab);
      }
    }
    //Handle Select Tab Item
    private void SelectTabItem(SidebarTabItem tabItem)
    {
      //Set Selected Tab
      SelectedTab = tabItem;
      SelectedTab.BackColor = this.Style.HoverColor;

      //Invoke the SelectedTabChanged Event
      if (SelectedTabChanged != null)
        SelectedTabChanged.Invoke(this, new SidebarSelectedTabChangedEventArgs());
    }
    private void TabItem_Click(object sender, EventArgs e)
    {
      SidebarTabItem clickedTab = (SidebarTabItem)sender;

      //Check Selected Tab isnt null and reset their style, hide sub tabs
      if (SelectedTab != null)
      {
        SelectedTab.BackColor = this.Style.BgColor;

        // Hide sub-tabs of previously selected tab
        if (SelectedTab.SubSidebarItems != null)
        {
          foreach (var subTab in SelectedTab.SubSidebarItems)
          {
            subTab.Visible = false;
          }
        }
        else if (SelectedTab.ParentSidebarItem != null)  // SelectedTab is a sub-tab
        {
          foreach (var subTab in SelectedTab.ParentSidebarItem.SubSidebarItems)
          {
            subTab.Visible = false;
          }
        }
      }
      //Call Select Tab Handler
      SelectTabItem(clickedTab);

      if (SelectedTab.SubSidebarItems != null)
      {
        foreach (var subTab in SelectedTab.SubSidebarItems)
        {
          subTab.Visible = true;
        }
      }
      // If clicked tab is a sub-tab, show all sibling sub-tabs
      else if (clickedTab.ParentSidebarItem != null)
      {
        if (clickedTab.ParentSidebarItem.SubSidebarItems != null)
        {
          foreach (var subTab in clickedTab.ParentSidebarItem.SubSidebarItems)
          {
            subTab.Visible = true;
          }
        }
      }
    }
    //Handle effect when mouse enter Tab Item
    private void TabItem_MouseEnter(object sender, EventArgs e)
    {
      SidebarTabItem tabItem = (SidebarTabItem)sender;
      if (tabItem != SelectedTab)
      {
        tabItem.BackColor = this.Style.HoverColor;
      }
    }
    //Handle effect when mouse leave Tab Item
    private void TabItem_MouseLeave(object sender, EventArgs e)
    {
      SidebarTabItem tabItem = (SidebarTabItem)sender;
      if (tabItem != SelectedTab)
      {
        tabItem.BackColor = this.Style.BgColor;
      }
    }
    //Event Handlers when Qty Item changed
    private void Sidebar_ItemsChanged(object sender, EventArgs e)
    {
      if (Tabs.Count > 0)
        spacer.Height = this.Height - this.panelLogo.Height - (this.Tabs[0].Size.Height * this.Tabs.Count + 1);
    }
    //Event Handlers when Dock changed
    private void Sidebar_DockChanged(object sender, EventArgs e)
    {
      if (Tabs.Count > 0)
        spacer.Height = this.Height - this.panelLogo.Height - (this.Tabs[0].Height * this.Tabs.Count + 1);
    }
  }
}

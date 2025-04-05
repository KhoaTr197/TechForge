using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using TechForgeBUS;
using TechForgeGUI.BaseControls;
using TechForgeGUI.Utils;

namespace TechForgeGUI.BaseForms
{
  public partial class ManagePage : UserControl
  {
    public Form FormParent { get; set; }
    protected readonly string connStr = "Data Source=.;Initial Catalog=TECHFORGE;Integrated Security=True;";
    protected CustomDataGridView dgvMainListRef;
    protected string DefaultFontName = "Segoe UI";
    public ManagePage()
    {
      InitializeComponent();
      InitializeButtons();

      this.Font = new Font(DefaultFontName, 10);
      this.Dock = DockStyle.Fill;

      dgvMainListRef = dgvMainList;
    }
    private void InitializeButtons()
    {
      btnAdd.ImageList = GlobalStatics.iconList;
      btnAdd.ImageKey = "add_icon";
      btnAdd.TextImageRelation = TextImageRelation.ImageBeforeText;
      btnAdd.Font = new Font(DefaultFontName, 10);
    }
    private void dgvMainList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
      //When data binding is completed, do sth here!
    }
    protected virtual void InitializeBUS()
    {
      //Initialize BUS method here!
    }
    protected virtual void LoadData() {
      //Use BUS method to load data here!
    }
  }
}

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

namespace TechForgeGUI.BaseForms
{
  public partial class ManageFormGUI : Form
  {
    protected readonly string connStr = "Data Source=DESKTOP-PEL4G3N;Initial Catalog=TECHFORGE;Integrated Security=True;";
    protected DataGridView dgvMainListRef;
    protected string DefaultFontName = "Segoe UI";
    public ManageFormGUI()
    {
      InitializeComponent();
      InitializeDataGridView();

      this.Font = new Font(DefaultFontName, 10);
      this.TopLevel = false;
      this.Dock = DockStyle.Fill;
      this.FormBorderStyle = FormBorderStyle.None;
      this.StartPosition = FormStartPosition.Manual;
      this.ControlBox = false;
    }
    private void InitializeDataGridView() 
    {
      dgvMainList.AutoGenerateColumns = true;
      dgvMainList.DataSource = null;
      dgvMainList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      dgvMainList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
      dgvMainList.ScrollBars = ScrollBars.Both;

      dgvMainList.DataBindingComplete += dgvMainList_DataBindingComplete;

      dgvMainListRef = dgvMainList;
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

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
  public partial class Page : UserControl
  {
    protected readonly string connStr = "Data Source=.;Initial Catalog=TECHFORGE;Integrated Security=True;";
    public Page()
    {
      InitializeComponent();

      this.Dock = DockStyle.Fill;
    }
  }
}

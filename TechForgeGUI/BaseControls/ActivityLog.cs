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
  public class ActivityLogEntry
  {
    public DateTime Time { get; set; }
    public string User { get; set; }
    public string Action { get; set; }
    public string Details { get; set; }
  }
  public partial class ActivityLog : UserControl
  {
    public ActivityLog()
    {
      InitializeComponent();
    }
  }
}

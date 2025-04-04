using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechForgeGUI.BaseForms
{
  public partial class OverlayFormGUI : Form
  {
    public OverlayFormGUI()
    {
      InitializeComponent();

      TopMost = true;
      FormBorderStyle = FormBorderStyle.None;
      StartPosition = FormStartPosition.Manual;
      BackColor = Color.Black;
      Opacity = 0.5;
    }
  }
}

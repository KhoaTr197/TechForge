using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechForgeGUI.BaseForms
{
  public partial class OverlayFormGUI : Form
  {
    // Property to hold the modal form
    public Form modal { get; set; }

    // Constructor to initialize the overlay form
    public OverlayFormGUI(Form _modal = null)
    {
      InitializeComponent();

      // Set the modal form
      modal = _modal;

      // Set form properties
      TopMost = true;
      ShowInTaskbar = false;
      FormBorderStyle = FormBorderStyle.None;
      StartPosition = FormStartPosition.Manual;
      BackColor = Color.Black;
      Opacity = 0.5;
      Size = Form.ActiveForm.ClientSize;
      Location = Form.ActiveForm.PointToScreen(new Point(0, 0));

      // Add mouse down event handler
      this.MouseDown += OverlayFormGUI_Click;
    }

    // Event handler to close the modal and overlay form on mouse click
    private void OverlayFormGUI_Click(object sender, EventArgs e)
    {
      if (modal == null)
        return;

      modal.Close();
      this.Close();
    }
  }
}

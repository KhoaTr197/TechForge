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
    public new Form ParentForm { get; set; }
    public new Form Modal { get; set; }

    // Constructor to initialize the overlay form
    public OverlayFormGUI(Form _ParentForm, Form _Modal = null)
    {
      InitializeComponent();

      // Set the parent, modal form
      this.ParentForm = _ParentForm;
      this.Modal = _Modal;

      this.SuspendLayout();

      // Set form properties
      ShowInTaskbar = false;
      FormBorderStyle = FormBorderStyle.None;
      BackColor = Color.Black;
      Opacity = 0.5;
      Size = ParentForm.ClientSize;
      Location = ParentForm.PointToScreen(Point.Empty);

      this.ResumeLayout(false);

      // Add mouse down event handler
      this.MouseDown += OverlayFormGUI_Click;

      this.Load += OverlayFormGUI_Load;
      this.ParentForm.Resize += OverlayFormGUI_Resize;
      this.ParentForm.LocationChanged += OverlayFormGUI_LocationChanged;
    }
    // Event handler to set the overlay form's size and location when it loads
    private void OverlayFormGUI_Load(object sender, EventArgs e)
    {
      this.Size = ParentForm.ClientSize;
      this.Location = ParentForm.PointToScreen(Point.Empty);
    }
    // Event handler to resize the overlay form when the parent form is resized
    private void OverlayFormGUI_Resize(object sender, EventArgs e)
    {
      this.Size = ParentForm.ClientSize;
    }
    // Event handler to reposition the overlay form when the parent form's location changes
    private void OverlayFormGUI_LocationChanged(object sender, EventArgs e)
    {
      this.Location = ParentForm.PointToScreen(Point.Empty);
    }
    // Event handler to close the modal and overlay form on mouse click
    private void OverlayFormGUI_Click(object sender, EventArgs e)
    {
      if (Modal == null)
        return;

      Modal.Close();
      Modal.Dispose();
      Modal = null;

      this.Close();
    }
  }
}

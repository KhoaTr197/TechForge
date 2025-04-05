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
  public partial class DetailFormGUI : Form
  {
    // Overlay form to create a transparent background effect
    protected OverlayFormGUI overlay;
    // Panel to hold action buttons
    private FlowLayoutPanel flpActionsPanel;
    // Button to edit details
    public Button btnEdit;
    // Button to delete details
    public Button btnDelete;
    // Default font name for the form
    protected string DefaultFontName = "Segoe UI";
    // Event triggered when edit is submitted
    public event EventHandler<DetailFormEditSubmitEventArgs> EditSubmit;
    // Event triggered when delete is submitted
    public event EventHandler<DetailFormDeleteSubmitEventArgs> OnDeletedSubmit;

    public DetailFormGUI()
    {
      InitializeComponent();

      // Set form properties
      TopMost = true;
      FormBorderStyle = FormBorderStyle.None;
      MinimumSize = new Size(980, 500);
      StartPosition = FormStartPosition.Manual;
      Location = Form.ActiveForm != null ? Form.ActiveForm.PointToScreen(new Point((Form.ActiveForm.Width - this.Width) / 2, (Form.ActiveForm.Height - this.Height) / 2)) : new Point(0, 0);

      // Set the form's background color to transparent
      overlay = new OverlayFormGUI(this);
      overlay.Show(Form.ActiveForm);

      // Initialize action panel
      flpActionsPanel = new FlowLayoutPanel
      {
        BackColor = Color.FromArgb(254, 86, 37),
        AutoSize = true,
        Dock = DockStyle.Bottom,
        FlowDirection = FlowDirection.RightToLeft,
      };

      // Initialize edit button
      btnEdit = new Button
      {
        BackColor = Color.White,
        Text = "Sửa",
        AutoSize = true,
        Font = new Font(DefaultFontName, 10),
        Dock = DockStyle.Right,
        Margin = new Padding(4),
      };

      // Initialize delete button
      btnDelete = new Button
      {
        BackColor = Color.White,
        Text = "Xóa",
        AutoSize = true,
        Font = new Font(DefaultFontName, 10),
        Dock = DockStyle.Right,
        Margin = new Padding(4),
      };

      // Add buttons to action panel
      flpActionsPanel.Controls.Add(btnDelete);
      flpActionsPanel.Controls.Add(btnEdit);
      // Add action panel to form
      this.Controls.Add(flpActionsPanel);
    }

    // Method to get a control by its name
    protected Control GetControlByName(Control root, string name)
    {
      foreach (Control panel in root.Controls)
      {
        foreach (Control control in panel.Controls)
        {
          if (control.Name == name)
          {
            return control;
          }
        }
      }
      return null;
    }

    // Method to trigger the EditSubmit event
    protected virtual void OnEditSubmit(DetailFormEditSubmitEventArgs e)
    {
      EditSubmit.Invoke(this, e);
    }
  }
  // Event arguments for edit submit event
  public class DetailFormEditSubmitEventArgs : EventArgs
  {
    public DetailFormEditSubmitEventArgs()
    {
    }
  }
  // Event arguments for delete submit event
  public class DetailFormDeleteSubmitEventArgs : EventArgs
  {
    public DetailFormDeleteSubmitEventArgs()
    {
    }
  }
}

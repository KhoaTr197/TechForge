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

    // Controls
    private FlowLayoutPanel flpActionsPanel;
    protected Button btnAdd;
    protected Button btnEdit;
    protected Button btnDelete;

    protected string DefaultFontName = "Segoe UI";

    // Custom Events for button clicks
    public event EventHandler<DetailFormAddSubmitEventArgs> AddSubmit;
    public event EventHandler<DetailFormEditSubmitEventArgs> EditSubmit;
    public event EventHandler<DetailFormDeleteSubmitEventArgs> DeleteSubmit;

    public DetailFormGUI()
    {
      InitializeComponent();

      // Set form properties
      this.TopMost = true;
      this.ControlBox = false;
      FormBorderStyle = FormBorderStyle.FixedDialog;
      MinimumSize = new Size(Form.ActiveForm.Width / 100 * 70, Form.ActiveForm.Height / 100 * 80);
      StartPosition = FormStartPosition.Manual;
      Location = Form.ActiveForm != null ? Form.ActiveForm.PointToScreen(new Point((Form.ActiveForm.Width - this.Width) / 2, (Form.ActiveForm.Height - this.Height) / 2)) : new Point(0, 0);

      Form.ActiveForm.SizeChanged += ParentForm_SizeChanged;
      Form.ActiveForm.LocationChanged += ParentForm_LocationChanged;

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
      btnAdd = new Button
      {
        BackColor = Color.White,
        Text = "Thêm",
        AutoSize = true,
        Font = new Font(DefaultFontName, 10),
        Dock = DockStyle.Right,
        Margin = new Padding(4),
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
      flpActionsPanel.Controls.Add(btnAdd);
      // Add action panel to form
      this.Controls.Add(flpActionsPanel);
    }
        // Method to get a control by its name
        /*protected Control GetControlByName(Control root, string name)
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
        }*/
        protected Control GetControlByName(Control container, string name)
        {
            if (container == null || string.IsNullOrEmpty(name))
                return null;

            foreach (Control control in container.Controls)
            {
                if (control.Name == name)
                    return control;
                Control found = GetControlByName(control, name);
                if (found != null)
                    return found;
            }
            return null;
        }

        // Method to trigger the EditSubmit event
        protected virtual void OnAddSubmit(DetailFormAddSubmitEventArgs e)
    {
      AddSubmit.Invoke(this, e);
    }
    protected virtual void OnEditSubmit(DetailFormEditSubmitEventArgs e)
    {
      EditSubmit.Invoke(this, e);
    }
    protected virtual void OnDeleteSubmit(DetailFormDeleteSubmitEventArgs e)
    {
      DeleteSubmit.Invoke(this, e);
    }
    private void ParentForm_SizeChanged(object sender, EventArgs e)
    {

      overlay.Size = Form.ActiveForm.ClientSize;
      this.Size = new Size(Form.ActiveForm.Width / 100 * 70, Form.ActiveForm.Height / 100 * 70);
      this.Location = Form.ActiveForm.PointToScreen(new Point((Form.ActiveForm.Width - this.Width) / 2, (Form.ActiveForm.Height - this.Height) / 2));
    }
    private void ParentForm_LocationChanged(object sender, EventArgs e)
    {
      overlay.Location = Form.ActiveForm.PointToScreen(new Point(0, 0));
      this.Location = Form.ActiveForm.PointToScreen(new Point((Form.ActiveForm.Width - this.Width) / 2, (Form.ActiveForm.Height - this.Height) / 2));
    }
  }
  // Event arguments for edit submit event
  public class DetailFormAddSubmitEventArgs : EventArgs
  {
    public DetailFormAddSubmitEventArgs()
    {
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

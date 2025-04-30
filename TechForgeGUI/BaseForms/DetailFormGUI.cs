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
    protected string Type; //add or detail
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
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      if (Form.ActiveForm != null) this.MinimumSize = new Size(Form.ActiveForm.Width / 100 * 70, Form.ActiveForm.Height / 100 * 80);
      this.StartPosition = FormStartPosition.Manual;
      Location = Form.ActiveForm != null ? Form.ActiveForm.PointToScreen(new Point((Form.ActiveForm.Width - this.Width) / 2, (Form.ActiveForm.Height - this.Height) / 2)) : new Point(0, 0);
    }

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
  }
  // Event arguments for edit submit event
  public class DetailFormAddSubmitEventArgs : EventArgs
  {
    public DetailFormGUI Modal;
    public DetailFormAddSubmitEventArgs()
    {
      Modal = null;
    }
    public DetailFormAddSubmitEventArgs(DetailFormGUI _modal)
    {
      Modal = _modal;
    }
  }
  // Event arguments for edit submit event
  public class DetailFormEditSubmitEventArgs : EventArgs
  {
    public DetailFormGUI Modal;
    public DetailFormEditSubmitEventArgs()
    {
      Modal = null;
    }
    public DetailFormEditSubmitEventArgs(DetailFormGUI _modal)
    {
      Modal = _modal;
    }
  }
  // Event arguments for delete submit event
  public class DetailFormDeleteSubmitEventArgs : EventArgs
  {
    public DetailFormGUI Modal;
    public DetailFormDeleteSubmitEventArgs()
    {
      Modal = null;
    }
    public DetailFormDeleteSubmitEventArgs(DetailFormGUI _modal)
    {
      Modal = _modal;
    }
  }
}

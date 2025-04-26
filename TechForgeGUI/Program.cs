using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeGUI.BaseForm;
using TechForgeGUI.Utils;

namespace TechForgeGUI
{
  internal static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      GlobalStatics.SetUp();

      LoginFormGUI loginForm = new LoginFormGUI();

      Application.Run(loginForm);
    }
    private static void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      GlobalStatics.iconList.Dispose();

      foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
      {
        form.Close();
      }
    }
  }
}

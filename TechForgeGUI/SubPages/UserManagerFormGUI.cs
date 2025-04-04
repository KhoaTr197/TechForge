using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeBUS;
using TechForgeDTO;
using TechForgeGUI.BaseForms;

namespace TechForgeGUI.SubForms
{
  public partial class UserManagerFormGUI : ManagePage
  {
    private NguoiDungBUS bus { get; set; }
    public UserManagerFormGUI()
    {
      InitializeComponent();
      InitializeBUS();
      LoadData();
    }
    protected override void InitializeBUS()
    {
      bus = new NguoiDungBUS(this.connStr);
    }
    protected override void LoadData()
    {
      dgvMainListRef.BindingData(bus.GetAllConnected().Cast<object>().ToList());
    }
  }
}

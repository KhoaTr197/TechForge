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
using TechForgeGUI.BaseForms;

namespace TechForgeGUI.SubPages
{
  public partial class InvoiceManagePageGUI : ManagePage
  {
    private SanPhamBUS sanPhamBus { get; set; }
    public InvoiceManagePageGUI()
    {
      InitializeComponent();
    }
  }
}

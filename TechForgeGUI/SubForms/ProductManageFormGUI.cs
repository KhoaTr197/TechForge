using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechForgeGUI.BaseForms;
using TechForgeBUS;

namespace TechForgeGUI
{
  public partial class ProductManageFormGUI : ManageFormGUI
  {
    private SanPhamBUS bus { get; set; }
    public ProductManageFormGUI()
    {
      InitializeComponent();
      InitializeBUS();
      LoadData();
    }
    sealed protected override void InitializeBUS()
    {
      bus = new SanPhamBUS(this.connStr);
    }
    sealed protected override void LoadData()
    {
      dgvMainListRef.BindingData(bus.GetAllDisconnected().Tables["SANPHAM"]);
    }
  }
}

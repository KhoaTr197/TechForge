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

namespace TechForgeGUI.SubForms
{
    public partial class ProviderManageFormGUI : ManageFormGUI
    {
        private NhaCungCapBUS bus {  get; set; }
        public ProviderManageFormGUI()
        {
            InitializeComponent();
            InitializeBUS();
            LoadData();
        }
        sealed protected override void InitializeBUS()
        {
            bus = new NhaCungCapBUS(this.connStr);
        }
        sealed protected override void LoadData()
        {
            dgvMainListRef.BindingData(bus.GetAllConnected().Cast<object>().ToList());

            var columnMappings = new Dictionary<string, (string, bool)>{
                { "MaNCC", ("Mã Nhà Cung Cấp", true) },
                { "TenNCC", ("Tên Nhà Cung Cấp", true) },
                { "Ndd", ("Tên Người Đại Diện", true) },
                { "Sdt", ("Số Điện Thoại", true) },
                { "TrangThai",  ("Trạng thái", false)},
            };
            dgvMainListRef.SetColumnNames(columnMappings);
        }
    }
}

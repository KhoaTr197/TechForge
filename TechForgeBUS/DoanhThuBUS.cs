using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechForgeDAO;
using TechForgeDTO;

namespace TechForgeBUS
{
    public class DoanhThuBUS
    {
        private readonly DoanhThuDAO DAO;

        public DoanhThuBUS(string _connStr)
        {
            this.DAO = new DoanhThuDAO(_connStr);
        }

        public void Setup(DoanhThuDTO doanhThuDTO)
        {
            this.DAO.Setup(doanhThuDTO);
        }
    }
}

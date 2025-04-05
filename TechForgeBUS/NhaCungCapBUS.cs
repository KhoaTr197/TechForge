using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechForgeDAO;
using TechForgeDTO;

namespace TechForgeBUS
{
    public class NhaCungCapBUS
    {
        private readonly NhaCungCapDAO DAO;
        public NhaCungCapBUS(string _connStr)
        {
            this.DAO = new NhaCungCapDAO(_connStr);
        }
        public List<NhaCungCapDTO> GetAllConnected()
        {
            return this.DAO.GetAllConnected();
        }
    }
}

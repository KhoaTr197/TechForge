using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechForgeDTO;
using TechForgeDAO;

namespace TechForgeBUS
{
    public class TaiKhoanBUS
    {
        private readonly TaiKhoanDAO DAO;

        public TaiKhoanBUS(string _connStr)
        {
            this.DAO = new TaiKhoanDAO(_connStr);
        }
        public TaiKhoanDTO Login(string username, string password)
        {
            return DAO.Login(username, password);
        }
    }
}

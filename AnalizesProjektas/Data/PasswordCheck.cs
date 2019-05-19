using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Data
{
    public class PasswordCheck : IPasswordCheck
    {
        public bool CheckPassword(string password)
        {
            if (password.Length > 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoviApp.Server.RequestModels
{
    /// <summary>
    /// Ответ сервера при авторизации работника.
    /// /api/auth/login.employee
    /// </summary>
    public class ResponseAuth
    {
        public string token;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomoviApp.Models;

namespace DomoviApp.Server.Requests
{
    /// <summary>
    /// Ответ сервера при получении пользователя.
    /// /api/users/me
    /// /api/users/{number}
    /// </summary>
    public class RequestAuth
    {
        public string login;
        public string password;
    }
}

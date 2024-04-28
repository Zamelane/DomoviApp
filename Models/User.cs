using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoviApp.Models
{
    /// <summary>
    /// Ответ сервера при получении пользователя.
    /// /api/users/me
    /// /api/users/{number}
    /// </summary>
    internal class User
    {
        /// <summary>
        /// Сервер не отправляет. Используется только для редактирования пользователя.
        /// </summary>
        public string password = null;
        public string login;
        public string phone;
        public string first_name;
        public string last_name;
        public string patronymic;
        public bool is_passed_moderation;
        public bool is_banned;
    }
}

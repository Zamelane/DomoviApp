using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomoviApp.Controllers
{
    internal class AuthController : IDataErrorInfo
    {
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public string this[string columnName]
        {
            get
            {
                var error = String.Empty;
                switch (columnName)
                {
                    case "Login":
                        if (Login == String.Empty)
                        {
                            error = "Введите логин";
                        } else if (Login.Length > 45)
                        {
                            error = "Логин не может быть длиннее 45 символов";
                        }
                        break;
                    case "Password":
                        if (Password == String.Empty)
                        {
                            error = "Пароль должен быть минимум 3 символа";
                        } else if (Password.Length > 50)
                        {
                            error = "Вы ввели слишком большой пароль. Введите не более 50 символов";
                        }
                        break;
                }
                return error;
            }
        }
        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}

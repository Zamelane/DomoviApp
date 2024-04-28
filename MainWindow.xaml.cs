using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DomoviApp.Controllers;
using DomoviApp.Models;
using DomoviApp.Server.RequestClient;
using DomoviApp.Server.RequestModels;
using DomoviApp.Server.Requests;
using Newtonsoft.Json;

namespace DomoviApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : AdonisUI.Controls.AdonisWindow
    {
        AuthController authController;
        public MainWindow()
        {
            InitializeComponent();
            authController = new AuthController();
            this.DataContext = authController;
        }

        private async void ButtonAuth_Click(object sender, RoutedEventArgs e)
        {
            var response = await Client.Post<ResponseAuth>(new Uri("http://domovi.ru/api/auth/login.employee"), new RequestAuth()
            {
                login = "chernov-nm",
                password = "V28TxZs9r9"
            });

            string token = response.Deserialize<ResponseAuth>().token;

            var responseUser = await Client.Get<User>(new Uri("http://domovi.ru/api/users/me"), token);

            MessageBox.Show(response.IsSuccefful() ? responseUser.Deserialize<User>().first_name : "Ошибка");
        }
    }
}

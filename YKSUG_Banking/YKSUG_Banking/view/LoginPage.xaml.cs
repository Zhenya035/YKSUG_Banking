using System;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.entity.Request;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Authentication(object sender, EventArgs e)
        {
            var request = new AuthenticationRequest();

            UserNameField.IsEnabled = false;
            PasswordField.IsEnabled = false;

            LoginButton.IsEnabled = false;

            request.Username = UserNameField.Text.ToString().Trim();
            request.Password = PasswordField.Text.ToString().Trim();

            MainPage.authResponse = await Requests.SendLogin(request);

            if (MainPage.authResponse.Token.Contains("false"))
            {
                await DisplayAlert("ERROR", "Неверные данные", "ОК");
                LoginButton.IsEnabled = true;
                UserNameField.IsEnabled = true;
                PasswordField.IsEnabled = true;
            }
            else
            {
                await AuthRequestHandler.SaveAuthData(request);
                if (MainPage.authResponse.Role == "USER")
                {
                    MainPage.account = await Requests.GetAccount(UserNameField.Text,
                        MainPage.authResponse.Token);

                    var tmpCardNumber = new StringBuilder("");

                    for (var i = 0; i < 4; i++)
                    {
                        for (var j = 0; j < 4; j++) tmpCardNumber.Append(MainPage.account.Card.CardNumber[4 * i + j]);

                        tmpCardNumber.Append(" ");
                    }

                    MainPage.prettyCardNumber = tmpCardNumber.ToString();

                    await Shell.Current.GoToAsync("//UserMain/MainPage");

                    Shell.SetTabBarIsVisible(this, true);
                }
                else if (MainPage.authResponse.Role == "ADMIN")
                {
                    await Shell.Current.GoToAsync("//AdminMain/Users");

                    Shell.SetTabBarIsVisible(this, true);
                }
            }
        }
        protected override async void OnAppearing()
        {
            Shell.SetTabBarIsVisible(this, false);
            LoginButton.IsEnabled = true;
            var request = await AuthRequestHandler.LoadAuthData();
            if (request != null)
            {
                UserNameField.Text = request.Username;
                PasswordField.Text = request.Password;
            }
            

            base.OnAppearing();
        }
    }
}
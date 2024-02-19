using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.entity.Request;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
    {
        AppShell.SetTabBarIsVisible(this, false);
        InitializeComponent();
    }
    
    private async void Authentication(object sender, EventArgs e)
    {
        AuthenticationRequest request = new AuthenticationRequest();

        request.Username = UserNameField.Text;
        request.Password = PasswordField.Text;
            
        MainPage.authResponse = await Requests.SendLogin(request);
            
        if (MainPage.authResponse.Token.Contains("false"))
        {
            ErrorField.Text = "Invalid credentials";
        }
        else
        {
            await AuthRequestHandler.SaveAuthData(request);
            if (MainPage.authResponse.Role == "USER")
            {

                MainPage.account = await Requests.GetAccount(MainPage.account, UserNameField.Text, MainPage.authResponse.Token);

                StringBuilder tmpCardNumber = new StringBuilder("");

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        tmpCardNumber.Append(MainPage.account.Card.CardNumber[4 * i + j]);
                    }

                    tmpCardNumber.Append(" ");
                }

                MainPage.prettyCardNumber = tmpCardNumber.ToString();

                ErrorField.Text = "";

                await AppShell.Current.GoToAsync("//UserMain");

                AppShell.SetTabBarIsVisible(this, true);
            }
            else if(MainPage.authResponse.Role == "ADMIN")
            {
                await AppShell.Current.GoToAsync("//AdminMain");

                AppShell.SetTabBarIsVisible(this, true);
            }
        }
    }
    
    private async void AutoAuthentication(AuthenticationRequest request)
    {
        MainPage.authResponse = await Requests.SendLogin(request);
            
        if (MainPage.authResponse.Token.Contains("false"))
        {
            ErrorField.Text = "Invalid credentials";
        }
        else
        {
            if (MainPage.authResponse.Role == "USER")
            {

                MainPage.account = await Requests.GetAccount(MainPage.account, request.Username, MainPage.authResponse.Token);

                StringBuilder tmpCardNumber = new StringBuilder("");

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        tmpCardNumber.Append(MainPage.account.Card.CardNumber[4 * i + j]);
                    }

                    tmpCardNumber.Append(" ");
                }

                MainPage.prettyCardNumber = tmpCardNumber.ToString();

                ErrorField.Text = "";

                await AppShell.Current.GoToAsync("//UserMain");

                AppShell.SetTabBarIsVisible(this, true);
            }
            else
            {

            }
        }
    }

    protected override async void OnAppearing()
    {
        AuthenticationRequest request = await AuthRequestHandler.LoadAuthData();
        if (request != null)
        {
            UserNameField.Text = request.Username;
            PasswordField.Text = request.Password;
            
            AutoAuthentication(request);
        }
        base.OnAppearing();
    }
    }
}
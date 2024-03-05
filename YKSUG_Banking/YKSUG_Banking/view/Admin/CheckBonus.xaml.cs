using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.entity.Request;
using YKSUG_Banking.scripts.entity.Response;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckBonus : ContentPage
    {
        public CheckBonus()
        {
            InitializeComponent();
        }

        private async void Check(object sender, EventArgs e)
        {
            if (TokenEntry.Text == "" ||  NameEntry.Text == "")
            {
                await DisplayAlert("ERROR","Неверные данные", "OK");
                return;
            }

            AdminCheckBonusTokenRequest request = new AdminCheckBonusTokenRequest();
            request.Username = NameEntry.Text;
            request.Token = TokenEntry.Text;

            AdminResponse response = await Requests.CheckTokenPostRequest(request);

            if (response.State.ToLower().Contains("true"))
            {
                await DisplayAlert("Успешно", "Бонус найден", "ОК");
            }
            else
            {
                await DisplayAlert("Неудача", "Бонус не найден", "ОК");
            }
        }
    }
}
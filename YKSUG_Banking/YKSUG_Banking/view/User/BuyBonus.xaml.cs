using System;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.entity;
using YKSUG_Banking.scripts.entity.Request;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyBonus : ContentPage
    {
        private readonly BonusMainData bonus;

        public BuyBonus(BonusMainData bonus)
        {
            this.bonus = bonus;
            InitializeComponent();
        }

        private async void Buy(object sender, EventArgs e)
        {
            var buingBonus = new BuyBonusRequest();
            buingBonus.BonusName = bonus.name;
            buingBonus.Username = MainPage.account.Username;

            var response = await Requests.BuyBonusPostRequest(buingBonus);

            if (response.Token.Contains("false"))
            {
                var error = new StringBuilder("");
                for (var i = 7; i < response.Token.Length - 1; ++i) error.Append(response.Token[i]);

                await DisplayAlert("ERROR", error.ToString(), "OK");
            }
            else
            {
                await DisplayAlert("Success", "Bonus was bought", "OK");
            }

            await Navigation.PopAsync();
        }

        protected override async void OnAppearing()
        {
            name.Text = bonus.name;
            description.Text = bonus.description;
            amountBonuses.Text = bonus.amount.ToString();
            price.Text = bonus.price.ToString();

            if (MainPage.account != null)
            {
                MainPage.account = await Requests.GetAccount(MainPage.account.Username,
                    MainPage.authResponse.Token);

                username.Text = MainPage.account.Username;
                cardNumber.Text = MainPage.prettyCardNumber;
                amount.Text = MainPage.account.Card.Amount.ToString();
            }

            Shell.SetTabBarIsVisible(this, false);

            base.OnAppearing();
        }
    }
}
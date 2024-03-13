using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.entity;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Bonuses : ContentPage
    {
        public Bonuses()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            MainPage.account = await Requests.GetAccount(MainPage.account, MainPage.account.Username,
                MainPage.authResponse.Token);

            MainPage.account.Bonus.Reverse();
            BoughtBonuses.ItemsSource = MainPage.account.Bonus;
            BonusesTemplate.ItemsSource = await Requests.ShowAllBonuses();

            if (MainPage.account.Bonus.Count != 0)
                Bought.IsVisible = true;
            else if (Bought.IsVisible) Bought.IsVisible = false;
            Shell.SetTabBarIsVisible(this, true);

            base.OnAppearing();
        }

        private async void BuyBonus(object sender, SelectionChangedEventArgs e)
        {
            var bonus = e.CurrentSelection[0] as BonusMainData;

            await Navigation.PushAsync(new BuyBonus(bonus));
        }
    }
}
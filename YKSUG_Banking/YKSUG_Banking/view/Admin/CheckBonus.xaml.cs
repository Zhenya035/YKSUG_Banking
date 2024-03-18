using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.entity;
using YKSUG_Banking.scripts.entity.Request;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckBonus : ContentPage
    {
        private readonly AccountMainInfo account;

        public CheckBonus(AccountMainInfo account)
        {
            this.account = account;

            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            UserBonuses.ItemsSource = account.Bonus;
            base.OnAppearing();
        }

        private async void Check(object sender, SelectionChangedEventArgs e)
        {
            var result = await DisplayAlert("Подтвердить действие", "Вы хотите использовать бонус?", "Да", "Нет");

            if (result)
            {
                var bonus = e.CurrentSelection[0] as BoughtBonusInfo;

                var request = new AdminCheckBonusTokenRequest();
                request.Username = account.Username;
                request.Token = bonus.Token;

                var response = await Requests.CheckTokenPostRequest(request);

                if (response.State.ToLower().Contains("true"))
                {
                    await DisplayAlert("Успешно", "Бонус использован", "ОК");
                    await Navigation.PopToRootAsync();
                }
                else
                    await DisplayAlert("Неудача", "Бонус не найден", "ОК");
            }
        }
    }
}
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.entity;
using YKSUG_Banking.scripts.entity.Request;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GiveAndTakeTransactions : ContentPage
    {
        private readonly AccountMainInfo accounnt;

        public GiveAndTakeTransactions(AccountMainInfo accounnt)
        {
            this.accounnt = accounnt;
            InitializeComponent();
        }

        private async void GiveTransaction(object sender, EventArgs e)
        {
            if (MoneyEntry.Text == "" || DescriptionEntry.Text == "" || Convert.ToInt64(MoneyEntry.Text) < 0)
            {
                await DisplayAlert("Error", "Неверные данные", "ОК");
                return;
            }

            var request = new AdminTransactionRequest();
            request.cardNumber = accounnt.Card.CardNumber;
            request.description = DescriptionEntry.Text;
            request.amount = Convert.ToInt64(MoneyEntry.Text);

            var response = await Requests.GiveTransactionPostRequest(request);

            if (response.State.ToLower().Contains("true"))
            {
                await DisplayAlert("Успешно", "Транзакция прошла успешно", "ОК");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Неудача", "Транзакция не прошла", "ОК");
            }
        }

        private async void TakeTransaction(object sender, EventArgs e)
        {
            if (MoneyEntry.Text == "" || DescriptionEntry.Text == "" || Convert.ToInt64(MoneyEntry.Text) < 0)
            {
                await DisplayAlert("Error", "Неверные данные", "ОК");
                return;
            }

            var request = new AdminTransactionRequest();
            request.cardNumber = accounnt.Card.CardNumber;
            request.description = DescriptionEntry.Text;
            request.amount = Convert.ToInt64(MoneyEntry.Text);

            var response = await Requests.TakeTransactionPostRequest(request);

            if (response.State.ToLower().Contains("true"))
            {
                await DisplayAlert("Успешно", "Транзакция прошла успешно", "ОК");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Неудача", "Транзакция не прошла", "ОК");
            }
        }

        protected override void OnAppearing()
        {
            NameLabel.Text = accounnt.Username;
            CardLabel.Text = accounnt.Card.CardNumber;
            MoneyLabel.Text = accounnt.Card.Amount.ToString();

            base.OnAppearing();
        }
    }
}
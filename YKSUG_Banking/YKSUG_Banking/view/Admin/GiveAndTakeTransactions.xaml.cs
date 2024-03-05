using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.entity;
using YKSUG_Banking.scripts.entity.Request;
using YKSUG_Banking.scripts.entity.Response;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GiveAndTakeTransactions : ContentPage
    {
        private AccountMainInfo accounnt;
        
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
            
            AdminTransactionRequest request = new AdminTransactionRequest();
            request.cardNumber = accounnt.Card.CardNumber;
            request.description = DescriptionEntry.Text;
            request.amount = Convert.ToInt64(MoneyEntry.Text);

            AdminResponse response = await Requests.GiveTransactionPostRequest(request);

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
            
            AdminTransactionRequest request = new AdminTransactionRequest();
            request.cardNumber = accounnt.Card.CardNumber;
            request.description = DescriptionEntry.Text;
            request.amount = Convert.ToInt64(MoneyEntry.Text);

            AdminResponse response = await Requests.TakeTransactionPostRequest(request);

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
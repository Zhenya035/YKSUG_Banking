using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.entity.Request;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Transaction : ContentPage
    {
        public Transaction()
        {
            InitializeComponent();
        }
        
        private async void SendTransaction(object sender, EventArgs e)
        {
            if (AmountEntry.Text == "" || Convert.ToInt64(AmountEntry.Text) == 0)
            {
                await DisplayAlert("State","You are dolboeb", "ok");//Todo change dolboeb
                return;
            }
        
            TransactionRequest request = new TransactionRequest();

            request.fromCard = MainPage.account.Card.CardNumber;
            request.toCard = CardNumberEntry.Text;
            request.amount = Convert.ToInt64(AmountEntry.Text);
            request.description = DescriptionEditor.Text;
        
            if(request.fromCard.Equals(request.toCard))
            {
                await DisplayAlert("State","You are dolboeb", "ok");//Todo change dolboeb
                return;
            }
            string state = await Requests.SendTransaction(request);

            if (state.Contains("false"))
            {
                await DisplayAlert("State","You are dolboeb", "ok");//Todo change dolboeb
                return;
            }
            else
            {
                await DisplayAlert("State","Successfully", "ok");
                CardNumberEntry.Text = "";
                AmountEntry.Text = "";
                DescriptionEditor.Text = "";
            }

            MainPage.account = await Requests.GetAccount(MainPage.account, MainPage.account.Username, MainPage.authResponse.Token);
            amount.Text = MainPage.account.Card.Amount.ToString();
        }

        protected override async void OnAppearing()
        {
            if (MainPage.account != null)
            {
                MainPage.account = await Requests.GetAccount(MainPage.account, MainPage.account.Username, MainPage.authResponse.Token);
            
                username.Text = MainPage.account.Username;
                cardNumber.Text = MainPage.prettyCardNumber; 
                amount.Text = MainPage.account.Card.Amount.ToString();
            }

            base.OnAppearing();
        }
        
    }
}
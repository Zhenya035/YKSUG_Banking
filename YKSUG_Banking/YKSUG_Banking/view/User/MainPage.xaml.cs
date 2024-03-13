using Xamarin.Forms;
using YKSUG_Banking.scripts.entity;
using YKSUG_Banking.scripts.entity.Response;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public static AuthenticationResponse authResponse { get; set; }
        public static AccountMainInfo account { get; set; }
        public static string prettyCardNumber { get; set; }

        protected override async void OnAppearing()
        {
            if (account != null)
            {
                account = await Requests.GetAccount(account.Username, authResponse.Token);

                username.Text = account.Username;
                cardNumber.Text = prettyCardNumber;
                amount.Text = account.Card.Amount.ToString();

                ServicesTemplate.ItemsSource = await Requests.ShowLastServices();
            }

            base.OnAppearing();
        }
    }
}
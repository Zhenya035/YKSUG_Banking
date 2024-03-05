using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using YKSUG_Banking.scripts.entity;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view.Admin
{
    public partial class AllUsers : ContentPage
    {
        private Task<List<AccountMainInfo>> accounts;
        
        public AllUsers()
        {
            InitializeComponent();
        }

        private async void CreateTransaction(object sender, SelectionChangedEventArgs e)
        {
            AccountMainInfo account = e.CurrentSelection[0] as AccountMainInfo;

            await Navigation.PushAsync(new GiveAndTakeTransactions(account));
        }

        protected override async void OnAppearing()
        {
            Users.ItemsSource = await Requests.ShowAllAccounts();
            
            base.OnAppearing();
        }
    }
}
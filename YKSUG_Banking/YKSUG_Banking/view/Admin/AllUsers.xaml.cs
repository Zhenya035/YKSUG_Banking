using System.Collections.Generic;
using Xamarin.Forms;
using YKSUG_Banking.scripts.entity;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view.Admin
{
    public partial class AllUsers : ContentPage
    {
        private List<AccountMainInfo> accounts; 
        
        public AllUsers()
        {
            InitializeComponent();
        }

        private async void CreateTransaction(object sender, SelectionChangedEventArgs e)
        {
            var account = e.CurrentSelection[0] as AccountMainInfo;
            await Navigation.PushAsync(new GiveAndTakeTransactions(account));
        }

        protected override async void OnAppearing()
        {
            accounts = await Requests.ShowAllAccounts();
            Users.ItemsSource = accounts;

            base.OnAppearing();
        }

        private List<AccountMainInfo> Find(string str)
        {
            List<AccountMainInfo> findingList = new List<AccountMainInfo>();

            for (var i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Username.ToLower().Contains(str.ToLower()))
                {
                    findingList.Add(accounts[i]);
                }
            }
            
            return findingList;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            Users.ItemsSource = Find(searchBar.Text);
        }
    }
}
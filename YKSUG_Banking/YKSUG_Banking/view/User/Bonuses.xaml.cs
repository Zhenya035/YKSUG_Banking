using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.entity;
using YKSUG_Banking.scripts.entity.Request;
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

        private List<BonusMainData> bonuses;
        
        protected override async void OnAppearing()
        {
            bonuses = await Requests.ShowAllBonuses();
            if (bonuses.Count == 0)
            {
                NoBonusLabel.IsVisible = true;
                SearchBonusBar.IsVisible = false;
            }
            else
            {
                SearchBonusBar.IsVisible = true;
                NoBonusLabel.IsVisible = false;
            }
            
            BonusesTemplate.ItemsSource = bonuses;

            base.OnAppearing();
        }

        private async void BuyBonus(object sender, SelectionChangedEventArgs e)
        {
            var responce = await DisplayAlert("Покупка", "Хотите купить этот бонус?", "Да", "Нет");

            if (!responce)
            {
                return;
            }
            
            var bonus = e.CurrentSelection[0] as BonusMainData;

            var buingBonus = new BuyBonusRequest();
            buingBonus.BonusName = bonus.name;
            buingBonus.Username = MainPage.account.Username;

            var response = await Requests.BuyBonusPostRequest(buingBonus);

            if (response.Token.Contains("false"))
            {
                var error = new StringBuilder("");
                for (var i = 7; i < response.Token.Length - 1; ++i) error.Append(response.Token[i]);

                await DisplayAlert("Ошибка", error.ToString(), "Ок");
            }
            else
            {
                await DisplayAlert("Успешно", "Бонус куплен", "Ок");
                OnAppearing();
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            BonusesTemplate.ItemsSource = Find(searchBar.Text);
        }
        
        private List<BonusMainData> Find(string str)
        {
            List<BonusMainData> findingList = new List<BonusMainData>();

            for (var i = 0; i < bonuses.Count; i++)
            {
                if (bonuses[i].name.ToLower().Contains(str.ToLower()))
                {
                    findingList.Add(bonuses[i]);
                }
            }
            
            return findingList;
        }
    }
}
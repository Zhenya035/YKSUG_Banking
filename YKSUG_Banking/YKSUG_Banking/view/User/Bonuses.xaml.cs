using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        private async void BuyBonus(BonusMainData bonus, Frame frame)
        {
            var responce = await DisplayAlert("Покупка", "Хотите купить этот бонус?", "Да", "Нет");
            
            if (!responce)
            {
                
                return;
            }

            var buingBonus = new BuyBonusRequest();
            buingBonus.BonusName = bonus.name;
            buingBonus.Username = MainPage.account.Username;

            var response = await Requests.BuyBonusPostRequest(buingBonus);

            if (response.Token.Contains("false"))
            {
                var error = new StringBuilder("");
                for (var i = 7; i < response.Token.Length - 1; ++i) error.Append(response.Token[i]);

                await DisplayAlert("Ошибка", error.ToString(), "Ок");
                BonusesTemplate.ItemsSource = bonuses;
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

        private async void OnFrameTapped(object sender, EventArgs e)
        {
            Frame frame = (Frame)sender;
            frame.Scale = 0.95;
            BonusMainData bonus = (BonusMainData)frame.BindingContext;
            BuyBonus(bonus, frame);
            await Task.Delay(100);
            frame.Scale = 1;
        }
    }
}
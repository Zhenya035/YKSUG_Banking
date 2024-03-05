using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.entity;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllBonuses : ContentPage
    {
        public AllBonuses()
        {
            InitializeComponent();
        }

        private async void AddBonus(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateBonus());
        }

        private async void EditBonus(object sender, SelectionChangedEventArgs e)
        {
            BonusMainData bonus = e.CurrentSelection[0] as BonusMainData;

            await Navigation.PushAsync(new EditBonus(bonus));
        }
        
        protected override async void OnAppearing()
        {
            BonusesTemplate.ItemsSource = await Requests.ShowAllBonuses();
			
            base.OnAppearing();
        }
    }
}
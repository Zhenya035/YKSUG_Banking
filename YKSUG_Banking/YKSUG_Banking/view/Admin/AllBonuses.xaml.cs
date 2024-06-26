﻿using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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

        protected override async void OnAppearing()
        {
            BonusesTemplate.ItemsSource = await Requests.ShowAllBonuses();

            base.OnAppearing();
        }
    }
}
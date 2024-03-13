using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.entity;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditBonus : ContentPage
    {
        private readonly BonusMainData bonus;

        public EditBonus(BonusMainData bonus)
        {
            this.bonus = bonus;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            NameEntry.Text = bonus.name;
            DescriptionEntry.Text = bonus.description;
            PriceEntry.Text = bonus.price.ToString();
            AmountEntry.Text = bonus.amount.ToString();
            base.OnAppearing();
        }

        private async void Save(object sender, EventArgs e)
        {
            bonus.name = NameEntry.Text;
            bonus.description = DescriptionEntry.Text;
            bonus.price = Convert.ToInt64(PriceEntry.Text);
            bonus.amount = Convert.ToInt64(AmountEntry.Text);

            var response = await Requests.EditBonusPutRequest(bonus);

            if (response.State.ToLower().Contains("true"))
            {
                await DisplayAlert("Успешно", "Бонус изменён", "Ок");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Неудача", "Бонус не изменён", "Ок");
            }
        }

        private async void Delete(object sender, EventArgs e)
        {
            var response = await Requests.DeleteBonusPostRequest(bonus);

            if (response.State.ToLower().Contains("true"))
            {
                await DisplayAlert("Успешно", "Бонус удалён", "Ок");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Неудача", "Бонус не найден", "Ок");
            }
        }
    }
}
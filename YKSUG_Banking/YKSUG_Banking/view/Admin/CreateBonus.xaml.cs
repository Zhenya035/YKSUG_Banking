using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.entity;
using YKSUG_Banking.scripts.entity.Response;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view.Admin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateBonus : ContentPage
    {
        public CreateBonus()
        {
            InitializeComponent();
        }

        private async void Create(object sender, EventArgs e)
        {
            if (NameEntry.Text == "" || DescriptionEntry.Text == "" || PriceEntry.Text == "" ||
                AmountEntry.Text == "" || Convert.ToInt64(PriceEntry.Text) < 0 || Convert.ToInt64(AmountEntry.Text) < 0)
            {
                await DisplayAlert("ERROR", "Неверные данные", "Ок");
                return;
            }

            BonusMainData request = new BonusMainData();
            request.name = NameEntry.Text;
            request.description = DescriptionEntry.Text;
            request.price = Convert.ToInt64(PriceEntry.Text);
            request.amount = Convert.ToInt64(AmountEntry.Text);
            
            AdminResponse response = await Requests.CreateBonusPostRequest(request);

            if (response.State.ToLower().Contains("true"))
            {
                await DisplayAlert("Успешно", "Бонус создан", "Ок");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Неудача", "Бонус не создан", "Ок");
            }
        }
    }
}
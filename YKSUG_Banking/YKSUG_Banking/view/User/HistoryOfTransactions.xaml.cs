using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YKSUG_Banking.scripts.servises;

namespace YKSUG_Banking.view.User
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryOfTransactions : ContentPage
    {
        public HistoryOfTransactions()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            ServicesTemplate.ItemsSource = await Requests.ShowLastServices();
            base.OnAppearing();
        }
    }
}
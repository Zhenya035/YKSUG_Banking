using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YKSUG_Banking.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }
    }
}
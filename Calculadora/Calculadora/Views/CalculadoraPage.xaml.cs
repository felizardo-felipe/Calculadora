using Calculadora.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculadora.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculadoraPage : ContentPage
    {
        public CalculadoraPage()
        {
            InitializeComponent();
            BindingContext = new CalculadoraViewModel();
        }
    }
}
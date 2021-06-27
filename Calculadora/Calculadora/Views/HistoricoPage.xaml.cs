using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculadora.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoricoPage : ContentPage
    {
        public HistoricoPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = await App.Database.GetCalculosAsync();
        }
    }
}
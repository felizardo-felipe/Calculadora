using Calculadora.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculadora.ViewModels
{
    public class HistoricoViewModel : INotifyPropertyChanged
    {
        public ICommand OnAppearingCommand { get; }
        public List<Calculo> Calculos;

        public event PropertyChangedEventHandler PropertyChanged;
        public HistoricoViewModel()
        {
            OnAppearingCommand = new Command(PageAppearingAsync);
        }

        private async void PageAppearingAsync()
        {
            Calculos = await App.Database.GetCalculosAsync().ConfigureAwait(false);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

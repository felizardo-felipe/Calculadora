using Calculadora.ViewModels;
using Calculadora.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Calculadora
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CalculadoraPage), typeof(CalculadoraPage));
            Routing.RegisterRoute(nameof(HistoricoPage), typeof(HistoricoPage));
        }
    }
}

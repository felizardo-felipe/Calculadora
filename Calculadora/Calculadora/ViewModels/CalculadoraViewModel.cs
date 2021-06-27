using Calculadora.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculadora.ViewModels
{
    public class CalculadoraViewModel : INotifyPropertyChanged
	{
		private string _display;
		private string _expressao;
		private string _expressaoDisplay;
		private string _digitos;
		private string _resultado;
		private string _ultimoNumero;
		private bool _novoCalculo = false;

		public CalculadoraViewModel()
		{
			CancelaCommand = new Command(DoCancelaCommand);
			TotalCommand = new Command(DoTotalCommand);
			OperandoCommand = new Command(DoOperandoCommand);
			DigitoCommand = new Command(DoDigitoCommand);

			Display = "0";
		}

		public string Display
		{
			get => _display;
			set
			{
				_display = value;
				OnPropertyChanged();
			}
		}

		public string ExpressaoDisplay
		{
			get => _expressaoDisplay;
			set
			{
				_expressaoDisplay = value;
				OnPropertyChanged();
			}
		}

		public ICommand CancelaCommand { get; }
		public ICommand TotalCommand { get; }
		public ICommand OperandoCommand { get; }
		public ICommand DigitoCommand { get; }

		public event PropertyChangedEventHandler PropertyChanged;

		private void DoDigitoCommand(object obj)
		{
			if (_novoCalculo)
            {
				ExpressaoDisplay = obj.ToString();
				_novoCalculo = false;
			}		
			else
				ExpressaoDisplay += obj.ToString();
					
			_digitos += obj.ToString();
			Display = _digitos;
		}

		private void DoOperandoCommand(object obj)
		{
			if (!string.IsNullOrEmpty(ExpressaoDisplay))
            {
				if (!int.TryParse(ExpressaoDisplay[ExpressaoDisplay.Length - 1].ToString(), out _))
					return;

				ExpressaoDisplay += obj.ToString();
				if (obj.ToString() == "%")
				{
					if (_ultimoNumero != null)
						_digitos = Convert.ToDouble(new DataTable().Compute($"({_digitos} / 100) * {_ultimoNumero}", null)).ToString();
					else
						Reset();
				}
				else
				{
					_ultimoNumero = _digitos;
					_expressao += _ultimoNumero + obj.ToString();
					_digitos = null;
				}
			}
		}

		private async void DoTotalCommand(object obj)
		{
			if (!string.IsNullOrEmpty(_expressao))
            {
				if (_digitos != null)
					_expressao += _digitos;
				else
					_expressao += _ultimoNumero;
				ExpressaoDisplay += obj.ToString();
				_resultado = Convert.ToDouble(new DataTable().Compute(_expressao, null)).ToString();
				Display = _resultado;
				_digitos = _expressao = _ultimoNumero = null;
				_novoCalculo = true;
			}

			await App.Database.SalvaCalculoAsync(new Calculo
			{
				Expressao = ExpressaoDisplay,
				Resultado = Display
			}); ;
		}

		private void DoCancelaCommand(object obj)
		{
			Reset();
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private void Reset()
        {
			Display = "0";
			ExpressaoDisplay = _digitos = _expressao = _ultimoNumero = null;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace _422_Dubkova_Kochetkova_pr1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
            SetTooltips();
        }

        private void SetTooltips()
		{
			xTextBox.ToolTip = "Введите значение x (число)";
			yTextBox.ToolTip = "Введите значение y (число)";
			shRadioButton.ToolTip = "Выбрать функцию sh(x)";
			x2RadioButton.ToolTip = "Выбрать функцию x²";
			exRadioButton.ToolTip = "Выбрать функцию e^x";
			calculateButton.ToolTip = "Вычислить результат";
			clearButton.ToolTip = "Очистить все поля";
			resultTextBox.ToolTip = "Результат вычисления";

		}

		private double CalculateF(double x, string function)
		{
			switch (function)
			{
				case "sh(x)": return Math.Sinh(x);
				case "x²": return x * x;
				case "e^x": return Math.Exp(x);
				default: return 0; // не должно произойти
			}
		}

		private void CalculateButton_Click(object sender, RoutedEventArgs e)
		{
            if (!ValidateInput()) return;

            try
			{
				double x = double.Parse(xTextBox.Text);
				double y = double.Parse(yTextBox.Text);
				string selectedFunction = "";

				if (shRadioButton.IsChecked == true) selectedFunction = "sh(x)";
				else if (x2RadioButton.IsChecked == true) selectedFunction = "x²";
				else if (exRadioButton.IsChecked == true) selectedFunction = "e^x";

				double fx = CalculateF(x, selectedFunction);
				double xy = x * y;
				double alpha;

				if (xy > 0)
					alpha = Math.Pow(fx + y, 2) - Math.Sqrt(fx * y);
				else if (xy < 0)
					alpha = Math.Pow(fx + y, 2) + Math.Sqrt(fx * Math.Abs(y));
				else
					alpha = Math.Pow(fx + y, 2) + 1;

				resultTextBox.Text = $"α = {alpha}";
			}
			catch (Exception ex)
			{
				resultTextBox.Text = "Ошибка: " + ex.Message;
			}
		}

        private bool ValidateInput()
		{
			string xValue = xTextBox.Text;
			string yValue = yTextBox.Text;


			if (string.IsNullOrEmpty(xValue) || string.IsNullOrEmpty(yValue))
			{
				MessageBox.Show("Пожалуйста, заполните все поля ввода.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}


			if (!double.TryParse(xValue, out _) || !double.TryParse(yValue, out _))
			{
				MessageBox.Show("Некорректный формат числа. Введите действительное число.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
				return false;
			}
			return true;

		}

		private void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			xTextBox.Clear();
			yTextBox.Clear();
			resultTextBox.Clear();
			shRadioButton.IsChecked = true; 
		}
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение выхода", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.No)
			{
				e.Cancel = true; // отмена закрытия окна
			}
		}
	}
}


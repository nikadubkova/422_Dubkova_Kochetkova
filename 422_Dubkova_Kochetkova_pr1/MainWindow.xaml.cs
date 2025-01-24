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
		}

		private double CalculateF(double x, string function)
		{
			switch (function)
			{
				case "sh(x)": return Math.Sinh(x);
				case "x²": return x * x;
				case "e^x": return Math.Exp(x);
				default: return 0; // Should not happen
			}
		}

		private void CalculateButton_Click(object sender, RoutedEventArgs e)
		{
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

		private void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			xTextBox.Clear();
			yTextBox.Clear();
			resultTextBox.Clear();
			shRadioButton.IsChecked = true; // Reset to default function
		}
	}
}
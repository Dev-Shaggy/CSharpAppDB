using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ERPSerwis
{
	/// <summary>
	/// Interaction logic for AddSubjectWindow.xaml
	/// </summary>
	public partial class AddSubjectWindow : Window
	{
		private string name;
		public AddSubjectWindow()
		{
			InitializeComponent();
		}

		private void Cancel(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}

		private void Confirm(object sender, RoutedEventArgs e)
		{
			name = subject_name.Text;
			this.DialogResult = true;
		}
		public string getName()
		{
			return name;
		}
	}
}

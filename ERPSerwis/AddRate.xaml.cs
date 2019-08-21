using ERPSerwis.Model;
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
	/// Interaction logic for AddRate.xaml
	/// </summary>
	public partial class AddRate : Window
	{
		private Student stud;
		private Rate subject_rate;
		private List<Subject> subjects;
		public AddRate(List<Subject> subjects, Student student)
		{
			InitializeComponent();
			this.subjects = subjects;
			stud = student;
			cb_subjects.ItemsSource = this.subjects;

		}

		private void Cancel(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}

		private void Confirm(object sender, RoutedEventArgs e)
		{
			subject_rate = new Rate() { Student = stud, Subject = subjects[cb_subjects.SelectedIndex], Value = Double.Parse(rate.Text) };

			this.DialogResult = true;
		}
		public Rate getRate()
		{
			return subject_rate;
		}
	}
}

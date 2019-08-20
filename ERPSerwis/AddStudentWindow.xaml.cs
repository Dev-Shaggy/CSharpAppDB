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
	/// Interaction logic for AddStudentWindow.xaml
	/// </summary>
	public partial class AddStudentWindow : Window
	{
		private Student student;
		public AddStudentWindow(List<Subject> subjects)
		{
			InitializeComponent();
			student = new Student();
			student.Subjects = subjects;
		}

		public AddStudentWindow(Student s)
		{
			InitializeComponent();
			student = new Student();
			student.Subjects = s.Subjects;

			index.Text = s.Index;
			lname.Text = s.LastName;
			fname.Text = s.FirstName;
			date.SelectedDate = s.BirthDate;
		}


		private void AddStudent(object sender, RoutedEventArgs e)
		{
			student.Index = index.Text;
			student.LastName = lname.Text;
			student.FirstName = fname.Text;
			student.BirthDate = date.SelectedDate.Value;
			this.DialogResult = true;
		}

		private void Cancel(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}

		public Student getStudent()
		{
			return student;
		}
	}
}

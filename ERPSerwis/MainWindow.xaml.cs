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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ERPSerwis
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			using(var db = new DBModel())
			{
				db.Database.Exists();

				//var student = new Student() { FirstName = "first name", LastName = "last name", Index = "123456", BirthDate = new DateTime(2000,01,01) };
				//var subject = new Subject() { Name = "subject"};
				//student.Subjects.Add(subject);
				//subject.Students.Add(student);

				//var rate = new Rate() { Student = student, Subject = subject ,Value = 5.0};

				//db.Rates.Add(rate);

				//db.SaveChanges();
			}
		}
	}
}

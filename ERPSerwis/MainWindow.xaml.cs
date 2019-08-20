using ERPSerwis.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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



/*
 * TODO:
 * 
 * przycisk do edycji studenta
 * wyświetlanie ocen studenta
 * usuwanie studenta
 * dodawanie ocen
 * szukanie po dacie urodzenia
 * 
 * 
 * 
 * Zrobione:
 * wyszukiwanie po imieniu, nazwisku i numerze indeksu
 * dodawanie studenta
 * wyswietlanie listy studentow
 * łaczenie z bazą danych
 * wyswietlanie danych studenta po kliknięciu na studenta
 */

namespace ERPSerwis
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public List<Student> students;

		public MainWindow()
		{
			InitializeComponent();

			using(var db = new DBModel())
			{
				if (!db.Database.Exists())
				{
					MessageBox.Show("Nie udało się pobrać danych. Brak połączenia z bazą.");
				}

				students = db.Students.ToList();

				student_list.ItemsSource = students;

		//var student = new Student() { FirstName = "first name", LastName = "last name", Index = "123456", BirthDate = new DateTime(2000,01,01) };
		//var subject = new Subject() { Name = "subject"};
		//student.Subjects.Add(subject);
		//subject.Students.Add(student);

		//var rate = new Rate() { Student = student, Subject = subject ,Value = 5.0};

		//db.Rates.Add(rate);

		//db.SaveChanges();
		}
}

		private void AddNewStudent(object sender, RoutedEventArgs e)
		{
			using (var db = new DBModel())
			{
				if (!db.Database.Exists())
				{
					MessageBox.Show("Nie udało się pobrać danych. Brak połączenia z bazą.");
				}

				AddStudentWindow window = new AddStudentWindow(db.Subjects.ToList());
				//AddStudentWindow window = new AddStudentWindow(db.Students.Find(1));
				if (window.ShowDialog() == true)
				{
					db.Students.Add(window.getStudent());
					db.SaveChanges();
				}

				var students = db.Students.ToList();

				student_list.ItemsSource = students;
			}
		}

		private void SearchAction(object sender, RoutedEventArgs e)
		{
			using (var db = new DBModel())
			{
				if (!db.Database.Exists())
				{
					MessageBox.Show("Nie udało się pobrać danych. Brak połączenia z bazą.");
				}
				
				if (search_box.Text.Length >0 )
				{
					students = db.Students.Where(x => x.FirstName.ToLower().Contains(search_box.Text.ToLower())
					||  x.LastName.ToLower().Contains(search_box.Text.ToLower())
					||  x.Index.ToLower().Contains(search_box.Text.ToLower())
					).ToList();
				}
				else
				{
					students = db.Students.ToList();
				}

				student_list.ItemsSource = students;
			}
		}

		private void Student_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				Student s = students[student_list.SelectedIndex];

				tb_date.Content = s.BirthDate.ToString();
				tb_index.Content = s.Index;
				tb_name.Content = s.LastName + " " + s.FirstName;

			}catch
			{
			
			}
			
		}
	}
}

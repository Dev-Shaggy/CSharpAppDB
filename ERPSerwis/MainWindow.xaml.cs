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




namespace ERPSerwis
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public List<Student> students;
		private Student currStudent;

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

				Student s = new Student();

				s.Index = tb_index.Text;
				s.LastName = tb_lname.Text;
				s.FirstName = tb_fname.Text;
				s.BirthDate = dp_birthDate.SelectedDate.Value;

				db.Students.Add(s);
				db.SaveChanges();

				students = db.Students.ToList();

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
				
				if (search_box.Text.Length > 0 )
				{
					DateTime dt;
					if(DateTime.TryParse(search_box.Text, out dt))
					{
						dt = DateTime.Parse(search_box.Text);
					}
					students = db.Students.Where(x => x.FirstName.ToLower().Contains(search_box.Text.ToLower())
					|| x.LastName.ToLower().Contains(search_box.Text.ToLower())
					|| x.Index.ToLower().Contains(search_box.Text.ToLower())
					|| (x.BirthDate.Year.Equals(dt.Year)
					&& x.BirthDate.Year.Equals(dt.Year)
					&& x.BirthDate.Day.Equals(dt.Day))
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
				currStudent = students[student_list.SelectedIndex];

				//Informacje o studencie
				dp_birthDate.SelectedDate = currStudent.BirthDate;
				tb_index.Text = currStudent.Index;
				tb_fname.Text = currStudent.FirstName;
				tb_lname.Text = currStudent.LastName;


				//Oceny studenta
				using (var db = new DBModel())
				{
					var s_subjects = db.Subjects.ToList();

					var s_rates = db.Rates.Where(x => x.Student.Id == currStudent.Id).ToList();


					List<SubjectRates> list = new List<SubjectRates>() ;


					foreach(var sub in s_rates)
					{
						list.Add(new SubjectRates() { Subject_Name = sub.Subject.Name, Subject_Rates = sub.Value});
					}

					student_Rates.ItemsSource = list;
				}

			}catch
			{
			
			}
			
		}

		private void DeleteStudent(object sender, RoutedEventArgs e)
		{
			using (var db = new DBModel())
			{
				foreach(var rate in db.Rates.Where(x=> x.Student.Id == currStudent.Id).ToList())
				{
					db.Rates.Remove(rate);
				}

				db.Students.Remove(db.Students.Find(currStudent.Id));
				db.SaveChanges();
				students = db.Students.ToList();
				student_list.ItemsSource = students;
			}
		}

		private void SaveStudent(object sender, RoutedEventArgs e)
		{
			using (var db = new DBModel())
			{

				currStudent.Index = tb_index.Text;
				currStudent.LastName = tb_lname.Text;
				currStudent.FirstName = tb_fname.Text;
				currStudent.BirthDate = dp_birthDate.SelectedDate.Value;

				db.Students.Find(currStudent.Id).Index = currStudent.Index;
				db.Students.Find(currStudent.Id).LastName = currStudent.LastName;
				db.Students.Find(currStudent.Id).FirstName = currStudent.FirstName;
				db.Students.Find(currStudent.Id).BirthDate = currStudent.BirthDate;



				db.SaveChanges();
				students = db.Students.ToList();
				student_list.ItemsSource = students;
			}
		}

		private void AddRate(object sender, RoutedEventArgs e)
		{
			using (var db = new DBModel())
			{
				var window = new AddRate(db.Subjects.ToList(),currStudent);

				if (window.ShowDialog() == true)
				{
					Rate rate = window.getRate();
					Console.WriteLine();
					db.Students.Find(currStudent.Id).Subjects.Add(rate.Subject);
					db.Rates.Add(new Rate() { Subject=rate.Subject,Student=db.Students.Find(currStudent.Id),Value=rate.Value });
					db.SaveChanges();
				}


				var s_rates = db.Rates.Where(x => x.Student.Id == currStudent.Id).ToList();


				List<SubjectRates> list = new List<SubjectRates>();


				foreach (var sub in s_rates)
				{
					list.Add(new SubjectRates() { Subject_Name = sub.Subject.Name, Subject_Rates = sub.Value });
				}

				student_Rates.ItemsSource = list;


			}
		}

		private void CreateSubject(object sender, RoutedEventArgs e)
		{
			var window = new AddSubjectWindow();

			if (window.ShowDialog() == true)
			{
				using (var db = new DBModel())
				{
					db.Subjects.Add(new Subject() { Name=window.getName()});
					db.SaveChanges();
				}
			}
		}
	}
}

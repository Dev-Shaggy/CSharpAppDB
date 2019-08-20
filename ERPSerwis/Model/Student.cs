using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSerwis.Model
{
	[Table("Students")]
	public class Student
	{
		[Key]
		public int Id { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Index { get; set; }
		public DateTime BirthDate { get; set; }

		public virtual ICollection<Subject> Subjects { get; set; }

		public virtual ICollection<Rate> Rates { get; set; }

		public Student()
		{
			this.Subjects = new HashSet<Subject>();
			this.Rates = new HashSet<Rate>();
		}
	}
}

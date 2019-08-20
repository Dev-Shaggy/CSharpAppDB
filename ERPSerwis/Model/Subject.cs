using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSerwis.Model
{
	[Table("Subjects")]
	public class Subject
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public virtual ICollection<Student> Students { get; set; }

		public virtual ICollection<Rate> Rates { get; set; }

		public Subject()
		{
			this.Students = new HashSet<Student>();
			this.Rates = new HashSet<Rate>();
		}
	}
}

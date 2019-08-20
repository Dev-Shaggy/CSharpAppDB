using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPSerwis.Model
{
	[Table("Rate")]
	public class Rate
	{
		[Key]
		public int Id{ get; set; }

		public virtual Student Student{ get; set; }
		public virtual Subject Subject { get; set; }

		public double Value { get; set; }

	}
}

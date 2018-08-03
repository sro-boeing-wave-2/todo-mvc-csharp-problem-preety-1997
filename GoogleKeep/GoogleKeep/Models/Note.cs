using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleKeep.Models
{
	public class Note
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public Boolean CanbePinned { get; set; }
		public List<Checklist> ChecklistList { get; set; }
		public List<Labels> LabelsList { get; set; }
	}
	public class Checklist
	{
		[Key]
		public int Id { get; set; }
		public string Value { get; set; }
		public Boolean IsChecked { get; set; }

	}

	public class Labels
	{
		[Key]
		public int Id { get; set; }
		public string Value { get; set; }

	}
}

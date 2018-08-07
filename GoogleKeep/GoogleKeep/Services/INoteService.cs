using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleKeep.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoogleKeep.Services
{
	public interface INoteService
	{
		Task<IEnumerable<Note>> GetNotes([FromQuery] string title, [FromQuery] string label, [FromQuery] bool? pinned);
		Task<Note> GetNotes([FromRoute] int id);
		Task<Note> PutNotes([FromRoute] int id, [FromBody] Note notes);
		Task<Note> PostNotes([FromBody] Note notes);
		Task<Note> DeleteNotes([FromRoute] int id);
		Task<IEnumerable<Note>> DeleteNotes([FromQuery] string title);
		bool NotesExists(int id);
	}
}

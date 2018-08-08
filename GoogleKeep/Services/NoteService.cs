using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleKeep.Models;
using GoogleKeep.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoogleKeep.Data;

namespace GoogleKeep.Services
{
	public class NoteService : INoteService

	{
		private readonly NotesContext _context;

		public NoteService(NotesContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<Note>> GetNotes([FromQuery] string title, [FromQuery] string label, [FromQuery] bool? pinned)
		{
			var result = await _context.Note.Include(n => n.ChecklistList).Include(n => n.LabelsList)
			.Where(x => ((title == null || x.Title == title) && (label == null || x.LabelsList.Any(y => y.Value == label)) && (pinned == null || x.CanbePinned == pinned))).ToListAsync();
			return result;
		}

		public async Task<Note> GetNotes([FromRoute] int id)
		{
			var notes = await _context.Note.Include(n => n.LabelsList).Include(n => n.ChecklistList).SingleOrDefaultAsync(x => x.Id == id);
			return notes;
		}

		public async Task<Note> PutNotes([FromRoute] int id, [FromBody] Note notes)
		{
			_context.Note.Update(notes);
			await _context.SaveChangesAsync();
			return await Task.FromResult(notes);
		}

		public async Task<Note> PostNotes([FromBody] Note notes)
		{
			_context.Note.Add(notes);
			await _context.SaveChangesAsync();
			return await Task.FromResult(notes);
		}

		public async Task<Note> DeleteNotes([FromRoute] int id)
		{
			var notes = await _context.Note.Include(x => x.ChecklistList).Include(x => x.LabelsList).SingleOrDefaultAsync(x => (x.Id == id));
			if (notes == null)
			{
				return await Task.FromResult<Note>(null);
			}
			_context.Note.Remove(notes);
			await _context.SaveChangesAsync();
			return notes;
		}


		public async Task<IEnumerable<Note>> DeleteNotes([FromQuery] string title)
		{
			var notes = await _context.Note.Include(x => x.ChecklistList).Include(x => x.LabelsList).Where(x => (x.Title == title)).ToListAsync();
			if (notes == null)
			{
				return await Task.FromResult<IEnumerable<Note>>(null);
			}
			_context.Note.RemoveRange(notes);
			await _context.SaveChangesAsync();

			return notes;
		}

		public bool NotesExists(int id)
		{
			var result = _context.Note.Any(e => e.Id == id);
			return result;
		}
	}
}

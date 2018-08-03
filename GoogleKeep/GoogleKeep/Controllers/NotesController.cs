﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoogleKeep.Models;

namespace GoogleKeep.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotesController : ControllerBase
	{
		private readonly NotesContext _context;

		public NotesController(NotesContext context)
		{
			_context = context;
		}

		// GET: api/Notes
		[HttpGet]
		public IEnumerable<Note> GetNote()
		{
			return _context.Note.Include(n => n.ChecklistList).Include(n => n.LabelsList);
		}

		// GET: api/Notes/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetNote([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var note = await _context.Note.Include(n => n.ChecklistList).Include(n => n.LabelsList).FirstOrDefaultAsync(m => m.Id == id);

			if (note == null)
			{
				return NotFound();
			}

			return Ok(note);
		}

		// PUT: api/Notes/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutNote([FromRoute] int id, [FromBody] Note note)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != note.Id)
			{
				return BadRequest();
			}

			_context.Entry(note).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!NoteExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Notes
		[HttpPost]
		public async Task<IActionResult> PostNote([FromBody] Note note)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.Note.Add(note);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetNote", new { id = note.Id }, note);
		}

		// DELETE: api/Notes/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteNote([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var note = await _context.Note.FindAsync(id);
			if (note == null)
			{
				return NotFound();
			}

			_context.Note.Remove(note);
			await _context.SaveChangesAsync();

			return Ok(note);
		}

		private bool NoteExists(int id)
		{
			return _context.Note.Any(e => e.Id == id);
		}
	}
}
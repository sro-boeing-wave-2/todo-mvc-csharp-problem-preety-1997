using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoogleKeep.Models;
using GoogleKeep.Services;


namespace GoogleKeep.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotesController : ControllerBase
	{
		INoteService _NotesServices;
		public NotesController(INoteService notesService)
		{
			_NotesServices = notesService;
		}

		//GET: api/Notes or GET: api/Notes?{query}
		[HttpGet]
		public async Task<IActionResult> GetNotes([FromQuery] string title, [FromQuery] string label, [FromQuery] bool? pinned)
		{
			var result = await _NotesServices.GetNotes(title, label, pinned);
			return Ok(result);
		}

		// GET: api/Notes/5
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetNotesById([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var notes = await _NotesServices.GetNotes(id);

			if (notes == null)
			{
				return NotFound();
			}

			return Ok(notes);
		}

		// PUT: api/Notes/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutNotes([FromRoute] int id, [FromBody] Note notes)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != notes.Id)
			{
				return BadRequest();
			}
			try
			{
				await _NotesServices.PutNotes(id, notes);
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!NotesExists(id))
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
		public async Task<IActionResult> PostNotes([FromBody] Note notes)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			await _NotesServices.PostNotes(notes);

			return CreatedAtAction("GetNotesById", new { id = notes.Id }, notes);
		}

		// DELETE: api/Notes/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteNotes([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var notes = await _NotesServices.DeleteNotes(id);
			if (notes == null)
			{
				return NotFound();
			}

			return Ok(notes);
		}

		// DELETE: api/Notes/?{query}
		[HttpDelete]
		public async Task<IActionResult> DeleteNotes([FromQuery] string title)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var notes = await _NotesServices.DeleteNotes(title);
			if (notes == null)
			{
				return NotFound();
			}

			return Ok(notes);
		}

		public bool NotesExists(int id)
		{
			var result = _NotesServices.NotesExists(id);
			return result;
		}
	}


}



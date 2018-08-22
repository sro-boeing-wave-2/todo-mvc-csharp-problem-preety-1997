using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoogleKeep.Models;
using GoogleKeep.Services;
using MongoDB.Bson;

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
		public  IActionResult GetNotes()
		{
			var result =  _NotesServices.GetNotes();
			return Ok(result);
		}

		// GET: api/Notes/5
		[HttpGet("{id:length(24)}")]
		public  IActionResult GetNoteById([FromRoute] string id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var notes =  _NotesServices.GetNote(new ObjectId(id));

			if (notes == null)
			{
				return NotFound();
			}

			return Ok(notes);
		}
		[HttpGet]
		[Route("query")]
		public IActionResult GetNotesByQuery([FromQuery] bool? Ispinned = null, [FromQuery]string title = "", [FromQuery] string labelName = "")
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var note = _NotesServices.GetNotesByQuery(Ispinned, title, labelName);
			if (note == null)
			{
				return NotFound();
			}
			return Ok(note);
		}

		[HttpPut("{id:length(24)}")]
		public IActionResult Put(string id, [FromBody]Note p)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var recId = new ObjectId(id);
			var note = _NotesServices.GetNote(recId);
			if (note == null)
			{
				return NotFound();
			}

			_NotesServices.Update(recId, p);
			return new OkResult();
		}

		// POST: api/Notes
		[HttpPost]
		public IActionResult Post([FromBody]Note p)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_NotesServices.Create(p);
			return Ok(p);
		}

		[HttpDelete("{id:length(24)}")]
		public IActionResult Delete(string id)
		{
			var note = _NotesServices.GetNote(new ObjectId(id));
			if (note == null)
			{
				return NotFound();
			}

			_NotesServices.Remove(note.Id);
			return Ok();
		}
	}


}



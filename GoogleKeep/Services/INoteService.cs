using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleKeep.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace GoogleKeep.Services
{
	public interface INoteService
	{
		IEnumerable<Note> GetNotes();
		IEnumerable<Note> GetNotesByQuery(bool? Ispinned =null , string title ="" , string labelName ="");
		Note GetNote(ObjectId id);
		Note Create(Note note);
		void Update(ObjectId id, Note note);
		void Remove(ObjectId id);
	}
}

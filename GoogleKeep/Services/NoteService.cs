using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleKeep.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace GoogleKeep.Services
{
	public class NoteService :INoteService

	{
		MongoClient _client;
		MongoServer _server;
		MongoDatabase _db;

		public NoteService()
		{
			_client = new MongoClient("mongodb://localhost:27017");
			_server = _client.GetServer();
			_db = _server.GetDatabase("NotesDb");
		}

		public Note Create(Note note)
		{
			_db.GetCollection<Note>("Notes").Save(note);
			return note;
		}

		public Note GetNote(ObjectId id)
		{
			var res = Query<Note>.EQ(p => p.Id, id);
			return _db.GetCollection<Note>("Notes").FindOne(res);
		}

		public IEnumerable<Note> GetNotes()
		{
			return _db.GetCollection<Note>("Notes").FindAll();
		}

		public IEnumerable<Note> GetNotesByQuery(bool? Ispinned =null , string title="" , string labelName ="")
		{
			return _db.GetCollection<Note>("Notes").FindAll().Where(
				m => ((title == "") || (m.Title == title)) && ((!Ispinned.HasValue) || (m.IsPinned == Ispinned)) && ((labelName == "") || (m.Labels).Any(b => b.LabelName == labelName)));
		}

		public void Update(ObjectId id, Note note)
		{
			note.Id = id;

			var res = Query<Note>.EQ(pd => pd.Id, id);

			var operation = Update<Note>.Replace(note);

			_db.GetCollection<Note>("Notes").Update(res, operation);

		}
		public void Remove(ObjectId id)
		{
			var res = Query<Note>.EQ(e => e.Id, id);
			var operation = _db.GetCollection<Note>("Notes").Remove(res);
		}
	}
}

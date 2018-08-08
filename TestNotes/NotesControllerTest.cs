using System;
using Xunit;
using GoogleKeep.Controllers;
using GoogleKeep.Models;
using GoogleKeep.Data;
using GoogleKeep.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TestNotes
{
    public class NotesControllerTest
    {
		

		public NotesController GetNotesController()
		{
			var optionBuilder = new DbContextOptionsBuilder<NotesContext>();
			optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			NotesContext todoContext = new NotesContext(optionBuilder.Options);
			CreateData(optionBuilder.Options);
			return new NotesController(new NoteService(todoContext));
			
			//_controller = new NotesController(new NoteService(todoContext));
			//CreateData(todoContext);
		}
		
		public void CreateData(DbContextOptions<NotesContext> options)
		{
			using (var notecontext = new NotesContext(options))
			{
				var note = new List<Note>() {
				new Note
			{   Id =1,
				Title = "preety",
				Text = "kumari",
				LabelsList = new List<Labels>
				{
					new Labels { Value = "black"},
					new Labels { Value = "green"}
				},
				ChecklistList = new List<Checklist>
				{
					new Checklist { Value = "coke",IsChecked =true},
					new Checklist { Value = "pepsi",IsChecked = false}
				},
				CanbePinned = true
			},
				new Note
			{
				Id =2,
				Title = "bicky",
				Text = "anand",
				LabelsList = new List<Labels>
				{
					new Labels { Value = "blue"},
					new Labels { Value = "white"}
				},
				ChecklistList = new List<Checklist>
				{
					new Checklist {Value = "pepsi",IsChecked= true}
				},
			   CanbePinned = false
			}
			};
				notecontext.Note.AddRange(note);
				var CountOfEntitYBeingTracked = notecontext.ChangeTracker.Entries().Count();
				notecontext.SaveChanges();
			}
		}

		[Fact]
		public async void TestGetAll()
		{
			var _controller = GetNotesController();
			var result = await _controller.GetNotes(null, null, null);
			var okObjectResult = result as OkObjectResult;
			var notes = okObjectResult.Value as List<Note>;
			Assert.Equal(2, notes.Count);
		}

		[Fact]
		public async void TestGetByTitle()
		{
			var _controller = GetNotesController();
			var result = await _controller.GetNotes("preety", null, null);
			var okObjectResult = result as OkObjectResult;
			var notes = okObjectResult.Value as List<Note>;
			Assert.Single(notes);
		}

		[Fact]
		public async void TestGetByPinned()
		{
			var _controller = GetNotesController();
			var result = await _controller.GetNotes(null, null, true);
			var okObjectResult = result as OkObjectResult;
			var notes = okObjectResult.Value as List<Note>;
			Assert.Single(notes);
		}

		[Fact]
		public async void TestGetByLabel()
		{
			var _controller = GetNotesController();
			var result = await _controller.GetNotes(null, "blue", null);
			var okObjectResult = result as OkObjectResult;
			var notes = okObjectResult.Value as List<Note>;
			Assert.Single(notes);
		}
		[Fact]
		public async void TestPost()
		{
			var _controller = GetNotesController();
			var note = new Note
			{   Id =3,
				Title = "chunkey",
				Text = "pandey",
				LabelsList = new List<Labels>
				{
					new Labels { Value ="houseful1"},
				},
				ChecklistList = new List<Checklist>
				{
					new Checklist { Value= "houseful2",IsChecked= true}

				},
				CanbePinned = true
			};
			var result = await _controller.PostNotes(note);
			var okObjectResult = result as CreatedAtActionResult;
			var notes = okObjectResult.Value as Note;
			Console.WriteLine(notes.Id);
			Assert.Equal(note, notes);
		}

		[Fact]
		public async void TestPut()
		{
			var _controller = GetNotesController();
			var note = new Note
			{
				Id = 1,
				Title = "chunkey",
				Text = "pandey",
				LabelsList = new List<Labels>
				{
					new Labels { Value ="houseful1"},
				},
				ChecklistList = new List<Checklist>
				{
					new Checklist { Value= "houseful2",IsChecked= true}

				},
				CanbePinned = true
			};
			var result = await _controller.PutNotes(1, note);
			var okObjectResult = result as OkObjectResult;
			var notes = okObjectResult.Value as Note;
			Assert.Equal(note,notes);
		}
		[Fact]
		public  void DeleteTest()
		{
			var _controller = GetNotesController();
			var result =  _controller.DeleteNotes(1);
			Assert.True(result.IsCompletedSuccessfully);
			
		}
		[Fact]
		public  void DeleteTestByTitle()
		{
			var _controller = GetNotesController();
			var result = _controller.DeleteNotes("bicky");
			Assert.True(result.IsCompletedSuccessfully);

		}


	}
}

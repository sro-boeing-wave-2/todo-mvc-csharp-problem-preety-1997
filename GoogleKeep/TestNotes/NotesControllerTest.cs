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

namespace TestNotes
{
    public class NotesControllerTest
    {
		private readonly NotesController _controller;

		public NotesControllerTest()
		{
			var optionBuilder = new DbContextOptionsBuilder<NotesContext>();
			optionBuilder.UseInMemoryDatabase<NotesContext>(Guid.NewGuid().ToString());
			var todoContext = new NotesContext(optionBuilder.Options);
			_controller = new NotesController(new NoteService(todoContext));
			CreateData(todoContext);
		}

		public void CreateData(NotesContext todoContext)
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
			todoContext.Note.AddRange(note);
			todoContext.SaveChanges();
		}

		[Fact]
		public async void TestGetAll()
		{
			var result = await _controller.GetNotes(null, null, null);
			var okObjectResult = result as OkObjectResult;
			var notes = okObjectResult.Value as List<Note>;
			Assert.Equal(2, notes.Count);
		}

		[Fact]
		public async void TestGetByTitle()
		{
			var result = await _controller.GetNotes("preety", null, null);
			var okObjectResult = result as OkObjectResult;
			var notes = okObjectResult.Value as List<Note>;
			Assert.Single(notes);
		}

		[Fact]
		public async void TestGetByPinned()
		{
			var result = await _controller.GetNotes(null, null, true);
			var okObjectResult = result as OkObjectResult;
			var notes = okObjectResult.Value as List<Note>;
			Assert.Single(notes);
		}

		[Fact]
		public async void TestGetByLabel()
		{
			var result = await _controller.GetNotes(null, "blue", null);
			var okObjectResult = result as OkObjectResult;
			var notes = okObjectResult.Value as List<Note>;
			Assert.Single(notes);
		}
		[Fact]
		public async void TestPost()
		{
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
			Console.WriteLine(notes);
			Assert.Equal("chunkey",notes.Title);
		}

		
	}
}

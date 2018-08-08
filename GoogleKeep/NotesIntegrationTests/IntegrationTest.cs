using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using FluentAssertions;
using System.Net.Http.Headers;
using GoogleKeep.Models;
using GoogleKeep.Data;
using GoogleKeep.Services;
using GoogleKeep.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using GoogleKeep;
using Microsoft.EntityFrameworkCore;


namespace NotesIntegrationTests
{
    public class IntegrationTest
    {
		private HttpClient _client;
		private NotesContext _context;
		public IntegrationTest()
		{
			var _server = new TestServer(new WebHostBuilder()
				.UseEnvironment("Testing")
				.UseStartup<Startup>());
			_client = _server.CreateClient();
			_context = _server.Host.Services.GetService(typeof(NotesContext)) as NotesContext;
			List<Note> NoteGet1 = new List<Note> { new Note()
		{

			Title = "title1",
			Text = "Message1",
			ChecklistList = new List<Checklist>()
						{
							new Checklist(){ Value = "checklist1", IsChecked = true},
							new Checklist(){ Value = "checklist2", IsChecked = false}
						},
			LabelsList = new List<Labels>()
						{
							new Labels(){Value = "label1"},
							new Labels(){ Value = "Label2"}
						},
			CanbePinned = false
		}
			};
			_context.Note.AddRange(NoteGet1);
			_context.Note.AddRange(NoteDelete);
			_context.SaveChanges();
		}
		Note NoteGet = new Note()
		{

			Title = " deleted title",
			Text = "deleted Message",
			ChecklistList = new List<Checklist>()
						{
							new Checklist(){ Value = "deleted checklist1", IsChecked = true},
							new Checklist(){ Value = "deleted checklist2", IsChecked = false}
						},
			LabelsList = new List<Labels>()
						{
							new Labels(){Value = "deleted label1"},
							new Labels(){ Value = "deleted Label2"}
						},
			CanbePinned = false
		};
		Note NotePut = new Note()
		{
			Id = 1,
			Title = " deleted title",
			Text = "deleted Message",
			ChecklistList = new List<Checklist>()
						{
							new Checklist(){ Value = "deleted checklist1", IsChecked = true},
							new Checklist(){ Value = "deleted checklist2", IsChecked = false}
						},
			LabelsList = new List<Labels>()
						{
							new Labels(){Value = "deleted label1"},
							new Labels(){ Value = "deleted Label2"}
						},
			CanbePinned = false
		};
		Note NotePost = new Note()
		{

			Title = "creating title",
			Text = "creating Message",
			ChecklistList = new List<Checklist>()
						{
							new Checklist(){ Value = "creating checklist1", IsChecked = true},
							new Checklist(){ Value = "creating checklist2", IsChecked = false}
						},
			LabelsList = new List<Labels>()
						{
							new Labels(){Value = "creating label1"},
							new Labels(){ Value = "creating Label2"}
						},
			CanbePinned = false
		};
		Note NoteDelete = new Note()
		{

			Title = " deleted title",
			Text = "deleted Message",
			ChecklistList = new List<Checklist>()
						{
							new Checklist(){ Value = "deleted checklist1", IsChecked = true},
							new Checklist(){ Value = "deleted checklist2", IsChecked = false}
						},
			LabelsList = new List<Labels>()
						{
							new Labels(){Value = "deleted label1"},
							new Labels(){ Value = "deleted Label2"}
						},
			CanbePinned = false
		};
		[Fact]
		public async Task Notes_Post()
		{
			var content = JsonConvert.SerializeObject(NotePost);
			var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("/api/Notes", stringContent);
			var responseString = await response.Content.ReadAsStringAsync();
			var note = JsonConvert.DeserializeObject<Note>(responseString);
			Console.WriteLine(note.Title);
			Assert.Equal(HttpStatusCode.Created, response.StatusCode);			
		}
	}
}

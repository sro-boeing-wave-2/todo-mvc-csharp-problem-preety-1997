using System;
using Xunit;
using GoogleKeep.Controllers;
using GoogleKeep.Models;
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
			
			return new NotesController(new NoteService());
			
			
		}
		
		public void CreateData()
		{
		
				var note = new List<Note>() {
				new Note
			{   Id ='riehtg948550',
				Title = "preety",
				Text = "kumari",
				Labels = new List<Label>
				{
					new Label { LabelName = "black"},
					new Label { LabelName = "green"}
				},
				Checklist = new List<ChecklistItem>
				{
					new ChecklistItem { ChecklistItemName = "coke",IsChecked =true},
					new ChecklistItem { ChecklistItemName = "pepsi",IsChecked = false}
				},
				IsPinned = true
			},
				new Note
			{
				Id ='fghgftvhfty67576',
				Title = "bicky",
				Text = "anand",
				Labels = new List<Label>
				{
					new Label { LabelName = "blue"},
					new Label { LabelName = "white"}
				},
				Checklist = new List<ChecklistItem>
				{
					new ChecklistItem {ChecklistItemName = "pepsi",IsChecked= true}
				},
			   IsPinned = false
			}
			};
			}
		}

	

		


	}
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleKeep.Models
{    [BsonIgnoreExtraElements]
	public class Note
	{
		public  ObjectId Id { get; set; }
		[BsonElement("Title")]
		public string Title { get; set; }
		[BsonElement("Text")]
		public string Text { get; set; }
		[BsonElement("IsPinned")]
		public Boolean IsPinned { get; set; }
		[BsonElement("Checklist")]
		public List<ChecklistItem> Checklist { get; set; }
		[BsonElement("Labels")]
		public List<Label> Labels { get; set; }
	}
	[BsonIgnoreExtraElements]
	public class ChecklistItem
	{
		
		public int Id { get; set; }
		[BsonElement("ChecklistItemName")]
		public string ChecklistItemName { get; set; }
		[BsonElement("IsChecked")]
		public Boolean IsChecked { get; set; }

	}
	[BsonIgnoreExtraElements]
	public class Label
	{
		
		public int Id { get; set; }
		[BsonElement("LabelName")]
		public string LabelName { get; set; }

	}
}

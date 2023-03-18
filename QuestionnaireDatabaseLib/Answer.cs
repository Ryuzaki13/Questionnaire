using System;
using System.Text.Json.Nodes;

namespace QuestionnaireDatabaseLib {
	public class Answer : CacheObject {
		public int ID { get; set; }
		public string Student { get; set; }
		public int Question { get; set; }
		public JsonNode Content { get; set; }
		public DateTime Date { get; set; }

		public Account StudentReference { get; set; }
		public Question QuestionReference { get; set; }
	}
}

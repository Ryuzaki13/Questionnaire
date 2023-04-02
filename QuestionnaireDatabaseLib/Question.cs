using System.Text.Json.Serialization;

namespace QuestionnaireDatabaseLib {
	[PostgresTable]
	public class Question : CacheObject {
		[PostgresField]
		public int ID { get; set; }
		[PostgresField]
		public string Type { get; set; }
		[PostgresField]
		public string Text { get; set; }
		[JsonRequired]
		[PostgresField]
		public QuestionContent Content { get; set; }
		[PostgresField]
		public int Form { get; set; }
		[PostgresField]
		public int Position { get; set; }

		public QuestionType TypeReference { get; set; }
		public Form FormReference { get; set; }
	}
}

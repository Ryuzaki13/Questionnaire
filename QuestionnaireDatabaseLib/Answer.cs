using System;
using System.Text.Json.Serialization;

namespace QuestionnaireDatabaseLib {
	[PostgresTable]
	public class Answer : CacheObject {
		[PostgresField]
		public int ID { get; set; }

		[PostgresField]
		public string Student { get; set; }

		[PostgresField]
		public int Question { get; set; }

		[JsonRequired]
		[PostgresField]
		public QuestionContent Content { get; set; }

		[PostgresField]
		public DateTime Date { get; set; }

		public Account StudentReference { get; set; }
		public Question QuestionReference { get; set; }
	}
}

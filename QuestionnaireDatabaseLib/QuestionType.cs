namespace QuestionnaireDatabaseLib {
	[PostgresTable]
	public class QuestionType : CacheObject {
		[PostgresField]
		public string Name { get; set; }
		[PostgresField]
		public string Description { get; set; }
	}
}

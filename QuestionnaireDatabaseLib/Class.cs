namespace QuestionnaireDatabaseLib {
	[PostgresTable]
	public class Class : CacheObject {
		[PostgresField]
		public string Name { get; set; }
	}
}

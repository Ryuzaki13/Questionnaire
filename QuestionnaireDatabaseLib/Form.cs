namespace QuestionnaireDatabaseLib {
	[PostgresTable]
	public class Form : CacheObject {
		[PostgresField]
		public int ID { get; set; }
		[PostgresField]
		public string Name { get; set; }
		[PostgresField]
		public string Teacher { get; set; }		

		public Account TeacherReference { get; set; }		
	}
}

namespace QuestionnaireDatabaseLib {

	[PostgresTable]
	public class Role : CacheObject {
		[PostgresField]
		public string Name { get; set; }

		public static readonly string Admin = "Admin";
		public static readonly string Teacher = "Teacher";
		public static readonly string Student = "Student";
	}
}

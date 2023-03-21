namespace QuestionnaireDatabaseLib {
	[PostgresTable]
	public class Account : CacheObject {
		[PostgresField]
		public string Login { get; set; }
		[PostgresField]
		public string Password { get; set; }
		[PostgresField]
		public string FirstName { get; set; }
		[PostgresField]
		public string LastName { get; set; }
		[PostgresField]
		public string Patronymic { get; set; }
		[PostgresField]
		public string Role { get; set; }
		[PostgresField]
		public string Class { get; set; }

		public Role RoleReference { get; set; }
		public Class ClassReference { get; set; }
	}
}

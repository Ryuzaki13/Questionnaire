namespace QuestionnaireDatabaseLib {
	public class Account : CacheObject {
		public string Login { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Patronymic { get; set; }
		public string Role { get; set; }
		public string Class { get; set; }

		//public Role RoleReference { get; set; }
		//public Class ClassReference { get; set; }
	}
}

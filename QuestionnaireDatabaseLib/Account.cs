using System.Collections.ObjectModel;

namespace QuestionnaireDatabaseLib {
	[PostgresTable]
	public class Account : CacheObject {
		public Account() {
			Forms = new ObservableCollection<Form>();
		}

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
		public ObservableCollection<Form> Forms { get; set; }
	}
}

using System.Collections.ObjectModel;

namespace QuestionnaireDatabaseLib {
	[PostgresTable]
	public class Form : CacheObject {
		public Form() {
			Questions = new ObservableCollection<Question>();
		}

		[PostgresField]
		public int ID { get; set; }
		[PostgresField]
		public string Name { get; set; }
		[PostgresField]
		public string Teacher { get; set; }

		public Account TeacherReference { get; set; }
		public ObservableCollection<Question> Questions { get; set; }
	}
}

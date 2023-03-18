using System.Text.Json.Serialization;

namespace QuestionnaireDatabaseLib {
	public class Question : CacheObject {
		public int ID { get; set; }
		public string Type { get; set; }

		[JsonRequired]
		public QuestionContent Content { get; set; }
		public int Form { get; set; }
		public int Position { get; set; }

		//public QuestionType TypeReference { get; set; }
		//public Form FormReference { get; set; }
	}
}

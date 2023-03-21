using System;

namespace QuestionnaireDatabaseLib {
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class PostgresTableAttribute : Attribute {

	}
}

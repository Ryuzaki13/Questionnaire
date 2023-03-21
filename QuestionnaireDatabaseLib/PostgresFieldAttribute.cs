using System;

namespace QuestionnaireDatabaseLib {
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class PostgresFieldAttribute : Attribute {

	}
}

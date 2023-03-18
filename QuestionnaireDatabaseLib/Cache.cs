using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace QuestionnaireDatabaseLib {
	public class Cache {
		private static Connection connection;
		private static ObservableCollection<Role> roles { get; set; }
		private static ObservableCollection<Class> classes { get; set; }
		private static ObservableCollection<Account> accounts { get; set; }
		private static ObservableCollection<QuestionType> questionTypes { get; set; }
		private static ObservableCollection<Question> questions { get; set; }
		private static ObservableCollection<Form> forms { get; set; }
		private static ObservableCollection<Answer> answers { get; set; }

		public static Connection Connection {
			set {
				connection = value;
			}
		}

		public static ObservableCollection<Role> Roles {
			get {
				if (roles == null) {
					loadRoles();
				}
				return roles;
			}
		}
		public static ObservableCollection<Class> Classes {
			get {
				if (classes == null) {
					loadClasses();
				}
				return classes;
			}
		}
		public static ObservableCollection<Account> Accounts {
			get {
				if (accounts == null) {
					loadAccounts();
				}
				return accounts;
			}
		}
		public static ObservableCollection<QuestionType> QuestionTypes {
			get {
				if (questionTypes == null) {
					loadQuestionTypes();
				}
				return questionTypes;
			}
		}
		public static ObservableCollection<Question> Questions {
			get {
				if (questions == null) {
					loadQuestions();
				}
				return questions;
			}
		}
		public static ObservableCollection<Form> Forms {
			get {
				if (forms == null) {
					loadForms();
				}
				return forms;
			}
		}

		private static void loadRoles() {
			if (connection == null)
				return;

			if (!connection.IsConnected()) {
				return;
			}

			roles = new ObservableCollection<Role>(
				connection.Query<Role>("SELECT * FROM \"Role\" ORDER BY \"Name\"")
			);
		}
		private static void loadClasses() {
			if (connection == null)
				return;

			if (!connection.IsConnected()) {
				return;
			}

			classes = new ObservableCollection<Class>(
				connection.Query<Class>("SELECT * FROM \"Class\" ORDER BY \"Name\"")
			);
		}
		private static void loadAccounts() {
			if (connection == null)
				return;

			if (!connection.IsConnected()) {
				return;
			}

			accounts = new ObservableCollection<Account>(
				connection.Query<Account>("SELECT * FROM \"Account\" ORDER BY \"LastName\", \"FirstName\", \"Patronymic\"")
			);
		}
		private static void loadQuestionTypes() {
			if (connection == null)
				return;

			if (!connection.IsConnected()) {
				return;
			}

			questionTypes = new ObservableCollection<QuestionType>(
				connection.Query<QuestionType>("SELECT * FROM \"QuestionType\" ORDER BY \"Name\"")
			);
		}
		private static void loadQuestions() {
			if (connection == null)
				return;

			if (!connection.IsConnected()) {
				return;
			}

			questions = new ObservableCollection<Question>(
				connection.Query<Question>("SELECT * FROM \"Question\" ORDER BY \"Position\"")
			);
		}
		private static void loadForms() {
			if (connection == null)
				return;

			if (!connection.IsConnected()) {
				return;
			}

			forms = new ObservableCollection<Form>(
				connection.Query<Form>("SELECT * FROM \"Form\" ORDER BY \"Teacher\", \"Name\"")
			);
		}

		public static T Add<T>(T entity) where T : new() {
			PropertyInfo[] properties = typeof(T).GetProperties();
			List<object> parameters = new List<object>();
			string sql = "insert into \"" + typeof(T).Name + "\" (";
			string sqlParameters = "(";
			string sqlReturning = "";
			int i = 1;
			foreach (PropertyInfo property in properties) {
				if (property.Name == "ID") {
					sqlReturning = " returning \"ID\"";
					continue;
				}
				
				sql += "\"" + property.Name + "\",";

				if (property.GetCustomAttribute<JsonRequiredAttribute>() != null) {
					parameters.Add(JsonSerializer.Serialize(property.GetValue(entity)));
					sqlParameters += "$" + (i++) + "::jsonb,";
				} else {
					parameters.Add(property.GetValue(entity));
					sqlParameters += "$" + (i++) + ",";
				}
			}

			sql = sql.Substring(0, sql.Length - 1) + ") values " + sqlParameters.Substring(0, sqlParameters.Length - 1) + ")" + sqlReturning;

			List<T> ts = connection.Query<T>(sql, parameters.ToArray());
			if (ts != null) {
				if (ts.Count > 0) {
					return ts[0];
				}
			}

			return default;
		}
	}
}


// https://github.com/kurerid/DBControllLib

// *быстрый доступ*
// расписание на сегодня
// оценки на этой недели
//
// зачетная книжка
// 
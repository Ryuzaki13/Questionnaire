using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace QuestionnaireDatabaseLib {
	public class Cache {
		private static Dictionary<string, Account> accountMap = new Dictionary<string, Account>();
		private static Dictionary<int, Form> formMap = new Dictionary<int, Form>();
		private static Dictionary<int, Question> questionMap = new Dictionary<int, Question>();

		private static Connection connection;
		private static ObservableCollection<Role> roles { get; set; }
		private static ObservableCollection<Class> classes { get; set; }
		private static ObservableCollection<Account> accounts { get; set; }
		private static ObservableCollection<QuestionType> questionTypes { get; set; }
		private static ObservableCollection<Question> questions { get; set; }
		private static ObservableCollection<Form> forms { get; set; }
		private static ObservableCollection<Answer> answers { get; set; }

		public static ObservableCollection<Role> Roles { get { return roles; } }
		public static ObservableCollection<Class> Classes { get { return classes; } }
		public static ObservableCollection<Account> Accounts { get { return accounts; } }
		public static ObservableCollection<QuestionType> QuestionTypes { get { return questionTypes; } }
		public static ObservableCollection<Question> Questions { get { return questions; } }
		public static ObservableCollection<Form> Forms { get { return forms; } }
		public static ObservableCollection<Answer> Answers { get { return answers; } }

		public static Connection Connection {
			set {
				connection = value;

				load();
			}
		}

		private static void load() {
			loadRoles();
			loadClasses();
			loadAccounts();
			loadForms();
			loadQuestionTypes();
			loadQuestions();
			loadAnswers();
		}

		private static void loadRoles() {
			if (connection == null) {
				return;
			}
			if (!connection.IsConnected()) {
				return;
			}

			roles = connection.Query<Role>("SELECT * FROM \"Role\" ORDER BY \"Name\"");
			if (roles == null) {
				roles = new ObservableCollection<Role>();
			}
		}
		private static void loadClasses() {
			if (connection == null) {
				return;
			}
			if (!connection.IsConnected()) {
				return;
			}

			classes = connection.Query<Class>("SELECT * FROM \"Class\" ORDER BY \"Name\"");
			if (classes == null) {
				classes = new ObservableCollection<Class>();
			}
		}
		private static void loadAccounts() {
			if (connection == null) {
				return;
			}
			if (!connection.IsConnected()) {
				return;
			}

			accounts = connection.Query<Account>("SELECT * FROM \"Account\" ORDER BY \"LastName\", \"FirstName\", \"Patronymic\"");
			if (accounts == null) {
				accounts = new ObservableCollection<Account>();
				return;
			}
			foreach (Account account in accounts) {
				accountMap.Add(account.Login, account);

				foreach (Role role in roles) {
					if (role.Name == account.Role) {
						account.RoleReference = role;
						break;
					}
				}

				if (account.Class == null) {
					continue;
				}

				foreach (Class _class in classes) {
					if (_class.Name == account.Class) {
						account.ClassReference = _class;
						break;
					}
				}
			}
		}
		private static void loadForms() {
			if (connection == null) {
				return;
			}
			if (!connection.IsConnected()) {
				return;
			}

			forms = connection.Query<Form>("SELECT * FROM \"Form\" ORDER BY \"Teacher\", \"Name\"");
			if (forms == null) {
				forms = new ObservableCollection<Form>();
				return;
			}
			foreach (Form form in forms) {
				formMap.Add(form.ID, form);

				if (accountMap.ContainsKey(form.Teacher)) {
					Account account = accountMap[form.Teacher];
					if (account != null) {
						form.TeacherReference = account;
					}					
				}
			}
		}
		private static void loadQuestionTypes() {
			if (connection == null) {
				return;
			}
			if (!connection.IsConnected()) {
				return;
			}

			questionTypes = connection.Query<QuestionType>("SELECT * FROM \"QuestionType\" ORDER BY \"Name\"");
			if (questionTypes == null) {
				questionTypes = new ObservableCollection<QuestionType>();
			}
		}
		private static void loadQuestions() {
			if (connection == null) {
				return;
			}
			if (!connection.IsConnected()) {
				return;
			}

			questions = connection.Query<Question>("SELECT * FROM \"Question\" ORDER BY \"Position\"");
			if (questions == null) {
				questions = new ObservableCollection<Question>();
				return;
			}
			foreach (Question question in questions) {
				questionMap.Add(question.ID, question);

				if (formMap.ContainsKey(question.Form)) {
					Form form = formMap[question.Form];
					if (form != null) {
						question.FormReference = form;
					}
				}
				
				foreach (QuestionType questionType in questionTypes) {
					if (questionType.Name == question.Type) {
						question.TypeReference = questionType;
						break;
					}
				}
			}
		}
		private static void loadAnswers() {
			if (connection == null) {
				return;
			}
			if (!connection.IsConnected()) {
				return;
			}

			answers = connection.Query<Answer>("SELECT * FROM \"Answer\" ORDER BY \"Question\"");
			if (answers == null) {
				answers = new ObservableCollection<Answer>();
				return;
			}
			foreach (Answer answer in answers) {
				if (accountMap.ContainsKey(answer.Student)) {
					Account account = accountMap[answer.Student];
					if (account != null) {
						answer.StudentReference = account;
					}
				}

				if (questionMap.ContainsKey(answer.Question)) {
					Question question = questionMap[answer.Question];
					if (question != null) {
						answer.QuestionReference = question;
					}
				}
			}
		}

		public static Account GetAccount(string login) {
			if (accountMap.ContainsKey(login)) {
				return accountMap[login];
			}
			return null;
		}

		public static Form AddForm(Form form) {
			Form responseForm = Add(form);
			if (responseForm != null) {
				form.ID = responseForm.ID;
			}
			
			forms.Add(form);
			formMap.Add(form.ID, form);

			Account account = GetAccount(form.Teacher);
			if (account != null) {
				form.TeacherReference = account;
			}

			return form;
		}

		public static T Add<T>(T entity) where T : new() {
			if (typeof(T).GetCustomAttribute<PostgresTableAttribute>() == null) {
				return default;
			}

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

				if (property.GetCustomAttribute<PostgresFieldAttribute>() != null) {
					sql += "\"" + property.Name + "\",";

					if (property.GetCustomAttribute<JsonRequiredAttribute>() != null) {
						parameters.Add(JsonSerializer.Serialize(property.GetValue(entity)));
						sqlParameters += "$" + (i++) + "::jsonb,";
					} else {
						parameters.Add(property.GetValue(entity));
						sqlParameters += "$" + (i++) + ",";
					}
				}
			}

			sql = sql.Substring(0, sql.Length - 1) + ") values " + sqlParameters.Substring(0, sqlParameters.Length - 1) + ")" + sqlReturning;

			try {
				ObservableCollection<T> ts = connection.Query<T>(sql, parameters.ToArray());
				if (ts != null) {
					if (ts.Count > 0) {
						return ts[0];
					}
				}
			}
			catch (Npgsql.PostgresException ex) {
				return default;
			}

			return entity;
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
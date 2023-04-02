using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.View {
	public class Pages {
		private static RegistrationPage registration;
		private static AuthorizationPage authorization;
		private static TeacherPage teacher;
		private static StudentPage student;
		private static QuestionConstructorPage questionConstructor;
		private static QuestionnairePage questionnaire;

		public static RegistrationPage Registration {
			get {
				if (registration == null) {
					registration = new RegistrationPage();
				}
				return registration;
			}
		}

		public static AuthorizationPage Authorization {
			get {
				if (authorization == null) {
					authorization = new AuthorizationPage();
				}
				return authorization;
			}
		}

		public static TeacherPage Teacher {
			get {
				if (teacher == null) {
					teacher = new TeacherPage();
				}
				return teacher;
			}
		}

		public static StudentPage Student {
			get {
				if (student == null) {
					student = new StudentPage();
				}
				return student;
			}
		}

		public static QuestionConstructorPage QuestionConstructor {
			get {
				if (questionConstructor == null) {
					questionConstructor = new QuestionConstructorPage();
				}
				return questionConstructor;
			}
		}

		public static QuestionnairePage Questionnaire {
			get {
				if (questionnaire == null) {
					questionnaire = new QuestionnairePage();
				}
				return questionnaire;
			}
		}

		public static void Reset() {
			registration = null;
			authorization = null;
			teacher = null;
			student = null;
			questionConstructor = null;
			questionnaire = null;
		}
	}
}

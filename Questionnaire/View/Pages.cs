using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.View {
	public class Pages {
		private static AuthorizationPage authorization;
		private static TeacherPage teacher;
		private static QuestionConstructorPage questionConstructor;

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

		public static QuestionConstructorPage QuestionConstructor {
			get {
				if (questionConstructor == null) {
					questionConstructor = new QuestionConstructorPage();
				}
				return questionConstructor;
			}
		}
	}
}

using QuestionnaireDatabaseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Questionnaire.View {
	public partial class QuestionnairePage : Page {
		private Form form;

		public QuestionnairePage() {
			InitializeComponent();
		}

		public void SetForm(Form form) {
			this.form = form;

			buildQuestions();
		}

		private void buildQuestions() {
			foreach (var question in form.Questions) {
				if (question.Type == typeof(TextBox).Name) {
					wpQuestions.Children.Add(new UserControls.QuestionTextBox() {
						Position = question.Position,
						QuestionText = question.Content.Text,
						ID = question.ID,
					});
					continue;
				}
				if (question.Type == typeof(ComboBox).Name) {
					wpQuestions.Children.Add(new UserControls.QuestionOneVariant(question.Content.Variants) {
						Position = question.Position,
						QuestionText = question.Content.Text,
						ID = question.ID,
					});
					continue;
				}
				if (question.Type == typeof(CheckBox).Name) {
					wpQuestions.Children.Add(new UserControls.QuestionManyVariant(question.Content.Variants) {
						Position = question.Position,
						QuestionText = question.Content.Text,
						ID = question.ID,
					});
					continue;
				}
				if (question.Type == typeof(DatePicker).Name) {
					wpQuestions.Children.Add(new UserControls.QuestionDate() {
						Position = question.Position,
						QuestionText = question.Content.Text,
						ID = question.ID,
					});
					continue;
				}
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			foreach (UIElement element in wpQuestions.Children) {
				UserControls.IQuestionElement questionElement = (UserControls.IQuestionElement)element;
				if (questionElement != null) {
					var answers = questionElement.GetAnswers();
					if (answers.Length != 0) {
						Console.Write(questionElement.GetID());
						Console.Write(": ");
						Console.WriteLine(string.Join("; ", answers));
					} else {
						Console.Write("Вопрос ");
						Console.Write(questionElement.GetID());
						Console.WriteLine(" без ответа");
					}
				}
			}
		}
	}
}

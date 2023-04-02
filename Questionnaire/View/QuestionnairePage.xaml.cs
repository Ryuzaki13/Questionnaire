using QuestionnaireDatabaseLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
						QuestionText = question.Text,
						ID = question.ID,
					});
					continue;
				}
				if (question.Type == typeof(ComboBox).Name) {
					wpQuestions.Children.Add(new UserControls.QuestionOneVariant(question.Content.Variants) {
						Position = question.Position,
						QuestionText = question.Text,
						ID = question.ID,
					});
					continue;
				}
				if (question.Type == typeof(CheckBox).Name) {
					wpQuestions.Children.Add(new UserControls.QuestionManyVariant(question.Content.Variants) {
						Position = question.Position,
						QuestionText = question.Text,
						ID = question.ID,
					});
					continue;
				}
				if (question.Type == typeof(DatePicker).Name) {
					wpQuestions.Children.Add(new UserControls.QuestionDate() {
						Position = question.Position,
						QuestionText = question.Text,
						ID = question.ID,
					});
					continue;
				}
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			string accountLogin = Pages.Authorization.GetAccount().Login;
			DateTime date = DateTime.Now;
			ObservableCollection<Answer> answers = new ObservableCollection<Answer>();
			foreach (UIElement element in wpQuestions.Children) {
				UserControls.IQuestionElement questionElement = (UserControls.IQuestionElement)element;
				if (questionElement != null) {
					var questionAnswers = questionElement.GetAnswers();
					if (questionAnswers.Length != 0) {
						answers.Add(new Answer() {
							Question = questionElement.GetID(),
							Student = accountLogin,
							Content = new QuestionContent() { Variants = questionAnswers },
							Date = date
						});
					} else {
						MainWindow.MessageShow(string.Format("Вопрос {0} без ответа", questionElement.GetID()));
						return;
					}
				}
			}

			foreach (var answer in answers) {
				Cache.AddAnswer(answer);
			}

			MainWindow.MessageShow("Анкета успешно сохранена");

			Pages.Student.UpdateFilter();
			NavigationService.Navigate(Pages.Student);
		}
	}
}

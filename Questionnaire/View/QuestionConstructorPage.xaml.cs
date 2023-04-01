using QuestionnaireDatabaseLib;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Questionnaire.View {
	public partial class QuestionConstructorPage : Page {
		public ObservableCollection<string> Variants { get; set; }
		public ObservableCollection<Question> Quesions { get; set; }
		public QuestionType SelectedQuestionType { get; set; }

		public QuestionConstructorPage() {
			InitializeComponent();

			Quesions = new ObservableCollection<Question>();
			Variants = new ObservableCollection<string>();

			cbQuestionType.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = Cache.QuestionTypes });

			DataContext = this;
		}

		private void onCreateQuestion(object sender, RoutedEventArgs e) {
			string text = tbQuestionText.Text.Trim();
			if (text.Length == 0 || SelectedQuestionType == null) {
				return;
			}

			if (isQuestionIncludeVariants() && Variants.Count == 0) {
				MainWindow.MessageShow("необходимо заполнить варианты ответа", true);
				return;
			}

			Question question = new Question();
			question.Type = SelectedQuestionType.Name;
			question.Content = new QuestionContent() {
				Text = text
			};

			if (isQuestionIncludeVariants()) {
				question.Content.Variants = Variants.ToArray();
			} else {
				question.Content.Variants = null;
			}

			question.Position = Quesions.Count + 1;
			Quesions.Add(question);

			Variants.Clear();
			tbQuestionText.Clear();
		}

		private void onChangeQuestionType(object sender, SelectionChangedEventArgs e) {
			variantBlock.IsEnabled = isQuestionIncludeVariants();
		}

		private bool isQuestionIncludeVariants() {
			if (SelectedQuestionType == null) {
				return false;
			}
			return SelectedQuestionType.Name == typeof(ComboBox).Name ||
				SelectedQuestionType.Name == typeof(CheckBox).Name;
		}

		private void onCreateVariant(object sender, RoutedEventArgs e) {
			string text = tbVariantText.Text.Trim();
			if (text.Length == 0) {
				return;
			}

			Variants.Add(text);
			tbVariantText.Clear();
		}

		private void onCreateForm(object sender, RoutedEventArgs e) {
			string formName = tbFormName.Text.Trim();
			if (formName.Length == 0) {
				MainWindow.MessageShow("необходимо ввести название анкеты", true);
				return;
			}

			Form form = Cache.AddForm(new Form() { Name = formName, Teacher = Pages.Authorization.GetAccount().Login });
			if (form == null) {
				MainWindow.MessageShow("анкета не была добавлена 😥", true);
				return;
			}

			foreach (Question question in Quesions) {
				question.Form = form.ID;
				Cache.AddQuestion(question);
			}

			Quesions.Clear();
			tbFormName.Clear();

			MainWindow.MessageShow("анкета успешно добавлена 😉");
			NavigationService.Navigate(Pages.Teacher);
		}
	}
}

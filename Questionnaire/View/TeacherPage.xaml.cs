using QuestionnaireDatabaseLib;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Questionnaire.View {
	public partial class TeacherPage : Page {

		public TeacherPage() {
			InitializeComponent();

			lvForms.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = Cache.Forms });
			lvQuestions.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = Cache.Questions });

			ApplyFormsFilter();
			ApplyQuestionsFilter(0);
		}

		private void ApplyQuestionsFilter(int formId) {
			var view = CollectionViewSource.GetDefaultView(lvQuestions.ItemsSource);
			if (view == null) {
				return;
			}

			view.Filter = new Predicate<object>(any => {
				Question question = any as Question;
				if (question != null) {
					return question.Form == formId;
				}
				return false;
			});
		}

		private void ApplyFormsFilter() {
			var view = CollectionViewSource.GetDefaultView(lvForms.ItemsSource);
			if (view == null) {
				return;
			}

			string accountLogin = Pages.Authorization.GetAccount().Login;

			view.Filter = new Predicate<object>(any => {
				Form form = any as Form;
				if (form != null) {
					return form.Teacher == accountLogin;
				}
				return false;
			});
		}

		private void onCreateNewForm(object sender, RoutedEventArgs e) {
			NavigationService.Navigate(Pages.QuestionConstructor);
		}

		private void onChangeForm(object sender, SelectionChangedEventArgs e) {
			ListView listView = sender as ListView;
			if (listView == null) {
				return;
			}

			Form selectedForm = listView.SelectedItem as Form;
			if (selectedForm == null) {
				return;
			}

			ApplyQuestionsFilter(selectedForm.ID);
		}
	}
}

using QuestionnaireDatabaseLib;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Questionnaire.View {
	public partial class TeacherPage : Page {
		public ObservableCollection<Form> Forms { get; set; }

		public TeacherPage() {
			InitializeComponent();

			Forms = new ObservableCollection<Form>();

			DataContext = this;
		}

		private void onCreateNewForm(object sender, RoutedEventArgs e) {
			NavigationService.Navigate(Pages.QuestionConstructor);
		}

		public void UpdateFormList() {
			Forms.Clear();

			foreach (Form question in Cache.Forms) {
				// TODO тут что-то хотел написать...
			}
		}

	}
}

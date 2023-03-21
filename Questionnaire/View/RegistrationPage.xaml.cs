using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using QuestionnaireDatabaseLib;

namespace Questionnaire.View {

	public partial class RegistrationPage : Page {
		private Account account;

		public RegistrationPage() {
			InitializeComponent();

			account = new Account() { Role = "Teacher" };
			SetBinding(DataContextProperty, new Binding() { Source = account });
		}

		private void onRegister(object sender, RoutedEventArgs e) {
			account.Password = tbPassword.Password.Trim();

			account = Cache.Add(account);
			if (account != null) {
				MainWindow.MessageShow("Аккаунт зарегистрирован");
			} else {
				MainWindow.MessageShow("Ошибка регистрации", true);
			}
		}
	}
}

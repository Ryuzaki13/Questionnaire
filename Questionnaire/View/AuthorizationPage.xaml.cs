using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using QuestionnaireDatabaseLib;

namespace Questionnaire.View {
	public partial class AuthorizationPage : Page {
		private Account account;

		public AuthorizationPage() {
			InitializeComponent();
		}

		public Account GetAccount() {
			return account;
		}

		private void onLogin(object sender, RoutedEventArgs e) {
			string login = tbLogin.Text.Trim();
			string password = tbPassword.Password.Trim();

			if (login.Length == 0 || password.Length == 0) {
				MainWindow.MessageShow("Введите логин/пароль", true);
				return;
			}

			account = Cache.GetAccount(login);
			if (account == null) {
				MainWindow.MessageShow("Неверный логин/пароль", true);
				return;
			}

			if (account.Password != password) {
				MainWindow.MessageShow("Неверный логин/пароль", true);
				return;
			}

			if (account.Role == Role.Teacher) {
				NavigationService.Navigate(Pages.Teacher);
				return;
			}

			if (account.Role == Role.Student) {

				return;
			}

			if (account.Role == Role.Admin) {
				NavigationService.Navigate(Pages.Teacher);
				return;
			}
		}
	}
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using QuestionnaireDatabaseLib;

namespace Questionnaire {

	public partial class MainWindow : Window {
		private static Button buttonLogout;
		private static DispatcherTimer timer = new DispatcherTimer();
		private static Border border;
		private static TextBlock textBlock;
		private static SolidColorBrush errorColor = new SolidColorBrush(Color.FromArgb(150, 150, 0, 0));
		private static SolidColorBrush noticeColor = new SolidColorBrush(Color.FromArgb(150, 0, 150, 0));

		public MainWindow() {
			InitializeComponent();

			timer.Interval = new TimeSpan(0, 0, 0, 5);
			timer.Tick += Timer_Tick;
			border = Message;   // Имя элемента <Border>
			textBlock = MessageText; // Имя элемента <TextBlock>
			buttonLogout = bLogout;

			ConnectionParameters parameters = ConnectionParameters.Load("connection_string.json");
			if (parameters == null) {
				parameters = new ConnectionParameters("127.0.0.1", 5432, "postgres", "1234", "Questionnaire");
				parameters.Save();
			}

			Connection connection = new Connection(new ConnectionParameters() {
				Database = "Questionnaire",
				Host = "localhost",
				Port = 5432,
				Password = "1234",
				Username = "postgres"
			});

			Cache.Connection = connection;

			//AppFrame.Navigate(View.Pages.Registration);
			AppFrame.Navigate(View.Pages.Authorization);
		}

		public static void MessageShow(string message, bool isError = false) {
			textBlock.Text = message;
			if (isError) {
				border.Background = errorColor;
			} else {
				border.Background = noticeColor;
			}
			border.Visibility = Visibility.Visible;
			timer.Start();
		}

		private void Timer_Tick(object sender, EventArgs e) {
			MessageText.Text = "";
			Message.Visibility = Visibility.Collapsed;
			timer.Stop();
		}

		private void onLogout(object sender, RoutedEventArgs e) {
			bLogout.Visibility = Visibility.Hidden;

			View.Pages.Reset();
			Cache.Reload();

			AppFrame.Navigate(View.Pages.Authorization);
		}

		public static void OnLogin() {
			buttonLogout.Visibility = Visibility.Visible;
		}
	}
}

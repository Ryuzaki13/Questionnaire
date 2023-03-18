using System.Windows;
using System.Text.Json;
using QuestionnaireDatabaseLib;
using System.Text.Json.Nodes;
using System.Windows.Controls;
using System;

namespace Questionnaire {

	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();

			ConnectionParameters parameters = ConnectionParameters.Load("connection_string.json");
			if (parameters == null ) {
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

			AppFrame.Navigate(View.Pages.Authorization);
		}
	}
}

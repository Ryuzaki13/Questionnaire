using System.IO;
using System.Text;
using System.Text.Json;

namespace QuestionnaireDatabaseLib {
	public class ConnectionParameters {
		public string Host { get; set; }
		public ushort Port { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Database { get; set; }

		public ConnectionParameters() {
			Host = "";
			Port = 0;
			Username = "";
			Password = "";
			Database = "";
		}

		public ConnectionParameters(string host, ushort port, string username, string password, string database) {
			Host = host;
			Port = port;
			Username = username;
			Password = password;
			Database = database;
		}

		public string GetConnectionString() {
			return string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4};",
				Host, Port, Database, Username, Password);
		}

		public bool Save(string filename = "connection_string.json") {
			try {
				FileStream file = new FileStream(filename, FileMode.Create);
				byte[] bytes = Encoding.Default.GetBytes(JsonSerializer.Serialize(this));
				file.Write(bytes, 0, bytes.Length);
				file.Close();
			} catch {
				return false;
			}
			return true;
		}

		public static ConnectionParameters Load(string filename) {
			try {
				FileStream file = new FileStream(filename, FileMode.Open);
				byte[] bytes = new byte[file.Length + 1];
				file.Read(bytes, 0, bytes.Length);
				return JsonSerializer.Deserialize<ConnectionParameters>(bytes);
			} catch {
				return null;
			}
		}
	}
}

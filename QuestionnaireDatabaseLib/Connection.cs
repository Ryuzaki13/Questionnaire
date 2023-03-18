﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace QuestionnaireDatabaseLib {

	public class QuestionnaireContent {
		public string Text { get; set; }
		public string[] Items { get; set; }
	}

	public class Connection {
		private NpgsqlConnection connection;

		public Connection(ConnectionParameters parameters) {
			connection = new NpgsqlConnection(parameters.GetConnectionString());
			connection.Open();
		}

		public bool IsConnected() {
			return connection.State == System.Data.ConnectionState.Open;
		}

		public T QueryRow<T>(string sql, object[] parameters = null) where T : new() {
			var response = Query<T>(sql, parameters);
			if (response.Count > 0) {
				return response[0];
			}
			return default;
		}

		public List<T> Query<T>(string sql, object[] parameters = null) where T : new() {
			NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
			if (parameters != null) {
				foreach (var parameter in parameters) {
					cmd.Parameters.Add(new NpgsqlParameter() { Value = parameter });
				}
			}

			if (sql.IndexOf("select", StringComparison.OrdinalIgnoreCase) == -1 &&
				sql.IndexOf("returning", StringComparison.OrdinalIgnoreCase) == -1) {
				cmd.ExecuteNonQuery();
			} else {
				NpgsqlDataReader reader = cmd.ExecuteReader();

				if (!reader.HasRows) {
					reader.Close();
					return null;
				}

				List<T> response = new List<T>();

				while (reader.Read()) {
					T entity = new T();

					for (int i = 0; i < reader.FieldCount; i++) {
						string fieldName = reader.GetName(i);

						PropertyInfo property = typeof(T).GetProperty(fieldName);
						if (property == null)
							continue;

						object value;
						if (reader.GetPostgresType(i).Name == "jsonb") {
							value = JsonSerializer.Deserialize<QuestionContent>(reader.GetString(i));
						} else {
							value = reader.GetValue(i);
						}

						property.SetValue(entity, value);
					}
					response.Add(entity);
				}

				reader.Close();
				return response;
			}

			return null;
		}
	}
}
using System.Windows;
using System.Windows.Controls;

namespace Questionnaire.UserControls {
	public partial class QuestionTextBox : UserControl, IQuestionElement {
		public QuestionTextBox() {
			InitializeComponent();
			DataContext = this;
		}

		static QuestionTextBox() {
			QuestionTextProperty = DependencyProperty.Register("QuestionText", typeof(string), typeof(QuestionTextBox));
			AnswerTextProperty = DependencyProperty.Register("AnswerText", typeof(string), typeof(QuestionTextBox));
			PositionProperty = DependencyProperty.Register("Position", typeof(int), typeof(QuestionTextBox));
		}

		public static DependencyProperty QuestionTextProperty;
		public static DependencyProperty AnswerTextProperty;
		public static DependencyProperty PositionProperty;

		public string QuestionText {
			get { return (string)GetValue(QuestionTextProperty); }
			set { SetValue(QuestionTextProperty, value); }
		}

		public string AnswerText {
			get { return (string)GetValue(AnswerTextProperty); }
			set { SetValue(AnswerTextProperty, value); }
		}

		public int Position {
			get { return (int)GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
		}

		public int ID { get; set; }

		public string GetTypeName() {
			return typeof(TextBox).Name;
		}

		public int GetID() {
			return ID;
		}

		public string[] GetAnswers() {
			if (AnswerText != null) {
				string answer = AnswerText.Trim();
				if (answer.Length != 0) {
					return new string[] { answer };
				}
			}
			return new string[] { };
		}
	}
}

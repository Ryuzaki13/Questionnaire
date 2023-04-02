using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Questionnaire.UserControls {
	public partial class QuestionDate : UserControl, IQuestionElement {
		public QuestionDate() {
			InitializeComponent();

			AnswerText = null;

			DataContext = this;
		}

		static QuestionDate() {
			QuestionTextProperty = DependencyProperty.Register("QuestionText", typeof(string), typeof(QuestionDate));
			AnswerTextProperty = DependencyProperty.Register("AnswerText", typeof(DateTime?), typeof(QuestionDate));
			PositionProperty = DependencyProperty.Register("Position", typeof(int), typeof(QuestionDate));
		}

		public static DependencyProperty QuestionTextProperty;
		public static DependencyProperty AnswerTextProperty;
		public static DependencyProperty PositionProperty;

		public string QuestionText {
			get { return (string)GetValue(QuestionTextProperty); }
			set { SetValue(QuestionTextProperty, value); }
		}

		public DateTime? AnswerText {
			get { return (DateTime?)GetValue(AnswerTextProperty); }
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
			if (AnswerText == null) {
				MainWindow.MessageShow("дата не выбрана в вопросе № " + Position);
				return new string[] { };
			}
			DateTime dateTime = AnswerText.Value;
			return new string[] { dateTime.ToString("s") };
		}
	}
}

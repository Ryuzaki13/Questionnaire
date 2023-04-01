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
	public partial class QuestionOneVariant : UserControl, IQuestionElement {
		private static int questionCounter = 0;
		private string selectedVariant;

		public QuestionOneVariant(string[] variants) {
			InitializeComponent();
			buildVariants(variants);
			DataContext = this;
		}

		private void buildVariants(string[] variants) {
			string groupName = "variant" + questionCounter++;
			foreach (string variant in variants) {
				RadioButton radioButton = new RadioButton();
				radioButton.Content = variant;
				radioButton.GroupName = groupName;
				radioButton.Checked += RadioButtonOnChecked;
				spRadioButtons.Children.Add(radioButton);
			}
		}

		private void RadioButtonOnChecked(object sender, RoutedEventArgs e) {
			RadioButton radioButton = sender as RadioButton;
			selectedVariant = radioButton.Content.ToString();
		}

		static QuestionOneVariant() {
			QuestionTextProperty = DependencyProperty.Register("QuestionText", typeof(string), typeof(QuestionOneVariant));
			PositionProperty = DependencyProperty.Register("Position", typeof(int), typeof(QuestionOneVariant));
		}

		public static DependencyProperty QuestionTextProperty;
		public static DependencyProperty PositionProperty;

		public string QuestionText {
			get { return (string)GetValue(QuestionTextProperty); }
			set { SetValue(QuestionTextProperty, value); }
		}

		public int Position {
			get { return (int)GetValue(PositionProperty); }
			set { SetValue(PositionProperty, value); }
		}

		public int ID { get; set; }

		public string GetTypeName() {
			return typeof(ComboBox).Name;
		}

		public int GetID() {
			return ID;
		}

		public string[] GetAnswers() {
			return new string[] { selectedVariant };
		}
	}
}

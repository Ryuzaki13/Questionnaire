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
	public partial class QuestionManyVariant : UserControl, IQuestionElement {
		public QuestionManyVariant(string[] variants) {
			InitializeComponent();
			buildVariants(variants);
			DataContext = this;
		}

		private void buildVariants(string[] variants) {
			foreach (string variant in variants) {
				CheckBox checkBox = new CheckBox();
				checkBox.Content = variant;
				spRadioButtons.Children.Add(checkBox);
			}
		}

		static QuestionManyVariant() {
			QuestionTextProperty = DependencyProperty.Register("QuestionText", typeof(string), typeof(QuestionManyVariant));
			PositionProperty = DependencyProperty.Register("Position", typeof(int), typeof(QuestionManyVariant));
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
			List<string> selectedVariants = new List<string>();
			foreach(UIElement element in spRadioButtons.Children) {
				CheckBox checkBox = element as CheckBox;
				if (checkBox != null) {
					if (checkBox.IsChecked == true) {
						selectedVariants.Add(checkBox.Content.ToString());
					}
				}
			}
			return selectedVariants.ToArray();
		}
	}
}

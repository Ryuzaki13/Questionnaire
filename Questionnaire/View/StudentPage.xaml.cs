using QuestionnaireDatabaseLib;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace Questionnaire.View {
	public partial class StudentPage : Page {
		public StudentPage() {
			InitializeComponent();

			lvForms.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = Cache.Forms });
			lvPassedForms.SetBinding(ItemsControl.ItemsSourceProperty, new Binding() { Source = new ObservableCollection<Form>(Cache.Forms) });			

			ApplyFormsFilter();
			ApplyPassedFormsFilter();
		}

		private void ApplyFormsFilter() {
			var view = CollectionViewSource.GetDefaultView(lvForms.ItemsSource);
			if (view == null) {
				return;
			}

			view.Filter = new Predicate<object>(any => {
				return true;
			});
		}

		private void ApplyPassedFormsFilter() {
			var view = CollectionViewSource.GetDefaultView(lvPassedForms.ItemsSource);
			if (view == null) {
				return;
			}

			bool isSkip = false;

			Account account = Pages.Authorization.GetAccount();
			if (account == null) {
				isSkip = true;
			}

			var formIdCollection = Cache.GetPassedForms(account.Login);
			if (formIdCollection == null) {
				isSkip = true;
			}

			view.Filter = new Predicate<object>(any => {
				if (isSkip == true)
					return false;

				Form form = any as Form;
				if (form != null) {
					return Array.IndexOf(formIdCollection, form.ID) != -1;
				}
				return false;
			});
		}

		private void onOpenForm(object sender, System.Windows.RoutedEventArgs e) {
			Form form = lvForms.SelectedItem as Form;
			if (form == null) {
				return;
			}

			Pages.Questionnaire.SetForm(form);
			NavigationService.Navigate(Pages.Questionnaire);
		}

		private void onChangeForm(object sender, SelectionChangedEventArgs e) {
			bOpenForm.IsEnabled = lvForms.SelectedItem != null;
		}
	}
}

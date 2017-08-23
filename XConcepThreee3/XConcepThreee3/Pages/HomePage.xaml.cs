using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XConcepThreee3.Classes;

namespace XConcepThreee3.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            Padding = Device.OnPlatform(
                new Thickness(10,20,10,10),
                new Thickness(10),
                new Thickness(10)
            );

            employeesListView.ItemTemplate = new DataTemplate(typeof(EmployeeCell));
            employeesListView.RowHeight = 70;//70 pixeles 

            addButton.Clicked += AddButton_Clicked;
            //creamos evento cuando hagamos click en la PG
            employeesListView.ItemSelected += EmployeesListView_ItemSelected;
        }

        private async void EmployeesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new EditPage((Employee)(e.SelectedItem)));
        }

        private async void AddButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(firstNameEntry.Text))
            {
                await DisplayAlert("Error", "You must enter a first name", "Acept");
                firstNameEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(lastNameEntry.Text))
            {
                await DisplayAlert("Error", "You must enter a last name", "Acept");
                lastNameEntry.Focus();
                return;
            }

            if (string.IsNullOrEmpty(salaryEntry.Text))
            {
                await DisplayAlert("Error", "You must enter a salary", "Acept");
                salaryEntry.Focus();
                return;
            }

            InsertEmployee();

        }

        private async void InsertEmployee()
        {
            var employee = new Employee
            {
                Active = activeSwitch.IsToggled,
                ContractDate = contractDateDatePicker.Date,
                FirstName = firstNameEntry.Text,
                LastName = lastNameEntry.Text,
                Salary = decimal.Parse(salaryEntry.Text)
            };

            using (var db=new DataAccess()) {
                db.Insert(employee);
                employeesListView.ItemsSource = db.GetList<Employee>();
            }

            await DisplayAlert("Message","The employe was created ok","Acept");

            activeSwitch.IsToggled = true;
            contractDateDatePicker.Date = DateTime.Today;
            firstNameEntry.Text=string.Empty;
            lastNameEntry.Text=string.Empty;
            salaryEntry.Text = string.Empty;
        }



        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (var db=new DataAccess())
            {
                employeesListView.ItemsSource = db.GetList<Employee>();
            };
        }
    }
}
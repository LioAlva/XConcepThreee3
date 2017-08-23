using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  Q LA PAGINA SE HAGA CUANDO HAGO UN  TAG 
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XConcepThreee3.Classes;

namespace XConcepThreee3.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        private Employee employee;

        public EditPage(Employee employee)
        {
            InitializeComponent();
            Padding = Device.OnPlatform(
            new Thickness(10, 20, 10, 10),
            new Thickness(10),
            new Thickness(10));

             this.employee = employee;

            firstNameEntry.Text = employee.FirstName;
            lastNameEntry.Text = employee.LastName;
            salaryEntry.Text = employee.Salary.ToString();
            activeSwitch.IsToggled = employee.Active;
            contractDateDatePicker.Date = employee.ContractDate;

            updateButton.Clicked += UpdateButton_Clicked;
            deleteButton.Clicked += DeleteButton_Clicked;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Confirm", "Are you sure to delete the record ?", "Yes", "No");
            if (!response)
            {
                return;
            }
            using (var db= new DataAccess()) 
            {
                db.Delete(employee);
            }
            await DisplayAlert("Message", "The record was deleted", "Acept");
            await Navigation.PopAsync();

        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
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

            employee.FirstName = firstNameEntry.Text;
            employee.LastName = lastNameEntry.Text;
            employee.Salary = decimal.Parse(salaryEntry.Text);
            employee.ContractDate = contractDateDatePicker.Date;
            employee.Active = activeSwitch.IsToggled;

            using (var db = new DataAccess())
            {
                db.Update(employee);
            }
            await DisplayAlert("Message","The record was Update","Acept");
            await Navigation.PopAsync();//es como una pila para que vuelva a llamar a la Pagina
            //Al metodo home page, se recarga la Pagina, se ejecuta el home apiring y se vuelve a ejecutar los cambios
        }
    }
}
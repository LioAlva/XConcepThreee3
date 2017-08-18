using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XConcepThreee3.Classes
{
    public class DataAccess:IDisposable
    {//nunca dejar coneecciones habiertas
        private SQLiteConnection connection;

        public DataAccess() {
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(config.Platform,
               System.IO.Path.Combine(config.DirectoryDB,"Employees.db3") 
                );
            connection.CreateTable<Employee>();
           // connection.CreateTable<Product>();
        }

       /* public void InsertEmployee(Employee employee)
        {
            connection.Insert(employee);
        }

        public void InsertProduct(Product product)
        {
            connection.Insert(product);
        }*/

        public void Insert<T> (T model)
        {//inserte un nobjeto de la clase T, t que es un modelo , entonces insert el modelo
            connection.Insert(model);
        } 

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}

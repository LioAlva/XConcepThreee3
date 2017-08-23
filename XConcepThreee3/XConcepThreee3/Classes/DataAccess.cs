using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace XConcepThreee3.Classes
{
    public class DataAccess:IDisposable
    {//nunca dejar coneecciones habiertas
        private SQLiteConnection connection;

        public DataAccess() {
            var config = DependencyService.Get<IConfig>();
            var conplat = config.Platform;
            var pathCombine = System.IO.Path.Combine(config.DirectoryDB, "Employees.db");
            connection = new SQLiteConnection(conplat,pathCombine);
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
        public void Update<T>(T model)
        {
            connection.Update(model);
        }
        public void Delete<T>(T model)
        {
            connection.Delete(model);
        }
        //ASI SERIA
       /* public  Employee FindEmployee(int employeId)
        {
            return connection.Table<Employee>().FirstOrDefault(e=>e.GetHashCode()==employeId);
        }*/

        public T Find<T>(int id) where T :class
        {
            return connection.Table<T>().FirstOrDefault(model=>model.GetHashCode()==id);
        }

        public T First<T>() where T:class
        {
            return connection.Table<T>().FirstOrDefault();
        }

        public List<T> GetList<T>() where T : class
        {
            return connection.Table<T>().ToList();
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}

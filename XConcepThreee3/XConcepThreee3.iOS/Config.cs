using SQLite.Net.Interop;
using System;
using Xamarin.Forms;
using XConcepThreee3.Classes;

//else dependeci serveci no sera el rumtime de la plataform asino de xamarin forms
//esto es para Permite cargar esa clase dinamica mente en tiempo de compilacion
[assembly: Dependency(typeof(XConcepThreee3.iOS.Config))]
namespace XConcepThreee3.iOS
{
    public class Config : IConfig
    {
        private string directoryDB;  
        
        private ISQLitePlatform platform;

        public string DirectoryDB
        {
            get {
                if (string.IsNullOrEmpty(directoryDB)) {
                    var directorio = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    directoryDB = System.IO.Path.Combine(directorio,"..","Library");
                }
                return directoryDB;
            }   
        }

        public ISQLitePlatform Platform
        {
            get {
                    if (platform==null) {
                        platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
                    }
                    return platform;
                }
        }

        
    }
}

using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Practica9
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuDatos : ContentPage
    {
        public ObservableCollection<todoitem> Items { get; set; }
        public static MobileServiceClient Cliente;
        public static IMobileServiceTable<todoitem> Tabla;

        //SQLiteConnection database;

        public MenuDatos()
        {
            InitializeComponent();
            //Cliente = new MobileServiceClient(AzureConnection.AzureURL);
            //Tabla = Cliente.GetTable<todoitem>();
            //Items = new ObservableCollection<todoitem>(database.Table<todoitem>());
            //BindingContext = this;

            Cliente = new MobileServiceClient(AzureConnection.AzureURL);
            Tabla = Cliente.GetTable<todoitem>();
            
               
            LeerTabla();            
        }

        private async void LeerTabla()
        {
            //comvierte la lista de datos a una lista enumerable
            IEnumerable<todoitem> elementos = await Tabla.ToEnumerableAsync();
            Items = new ObservableCollection<todoitem>(elementos);
            
            //Tabla.CreateQuery("select*from toditem where deleted=true;");                      
            BindingContext = this;
        }

        private void InsertarRegistros_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new RegistrosIUD());
        }
        private void RecuperarRegistros_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new unDelete());
        }

        async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            await Navigation.PushModalAsync(new SelectPage(e.SelectedItem as todoitem));


        }
    }
}
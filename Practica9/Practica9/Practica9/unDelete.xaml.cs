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
using Microsoft.WindowsAzure.MobileServices;
namespace Practica9
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class unDelete : ContentPage
    {

        public ObservableCollection<todoitem> Items { get; set; }
        public static MobileServiceClient Cliente;
        public static IMobileServiceTable<todoitem> Tabla;

        public unDelete()
        {
            InitializeComponent();
            Cliente = new MobileServiceClient(AzureConnection.AzureURL);
            Tabla = Cliente.GetTable<todoitem>();

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            
            bool del = false;
            var datos = new todoitem
            {                
                Deleted=del
            };

            await Tabla.UndeleteAsync(datos);
            //await SelectPage.Tabla.UpdateAsync(datos);

        }
    }
}
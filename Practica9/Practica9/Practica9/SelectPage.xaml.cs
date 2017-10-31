﻿using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Practica9
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPage : ContentPage
    {
        public static MobileServiceClient Cliente;
        public static IMobileServiceTable<todoitem> Tabla;
        string datoPiker, datopikersem;
        public SelectPage(object selectedItem)
        {

            var dato = selectedItem as todoitem;
            BindingContext = dato;
            InitializeComponent();
            Cliente = new MobileServiceClient(AzureConnection.AzureURL);
            Tabla = Cliente.GetTable<todoitem>();
            
            //BindingContext = this;
        }


        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                datoPiker = (string)picker.ItemsSource[selectedIndex];
            }
        }

        private void PickerSem_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                datopikersem = (string)picker.ItemsSource[selectedIndex];
            }
        }

        private async void Actualizar_Clicked(object sender, EventArgs e)
        {
            //    var datos = new TESHDatos
            //    {
            //        Matricula = Convert.ToInt64(Entry_ID.Text),
            //        Nombre = Entry_Name.Text,
            //        Apellidos = Entry_Ap.Text,
            //        Direccion = Entry_Dir.Text,
            //        Telefono = Entry_Tel.Text,
            //        Carrera = datoPiker,
            //        Semestre = datopikersem,
            //        Correo = Entry_Cor.Text,
            //        Github = Entry_Git.Text
            //    };
            //    database.Update(datos);
            //    Navigation.PushModalAsync(new MenuDatos());
            //    DisplayAlert("", "Regsitro Actualizado", "OK");
            //}
            int t = 10;
            try
            {

                var datos = new todoitem
                {
                    Id = Entry_ID.Text,
                    Matricula=Convert.ToInt64(Entry_Mat.Text),
                    Nombre = Entry_Name.Text,
                    Apellidos = Entry_Ap.Text,

                    Direccion = Entry_Dir.Text,
                    Telefono = Convert.ToInt64(Entry_Tel.Text),
                    //Carrera=Entry_Car.Text,
                    Carrera = datoPiker,
                    //Semestre = Entry_Sem.Text,
                    Semestre = datopikersem,
                    Correo = Entry_Cor.Text,
                    Github = Entry_Git.Text
                };
            
            if (Entry_ID.Text == "")
            {
                await DisplayAlert("", "Ingresar Matricula", "ok");
            }
            else if (Entry_Name.Text == "")
            {
                await DisplayAlert("", "Ingrese un Nombre", "ok");
            }
            else if (Entry_Ap.Text == "")
            {
                await DisplayAlert("", "Ingresar Apellidos", "ok");
            }
            else if (Entry_Dir.Text == "")
            {
                await DisplayAlert("", "Ingresar una Direccion", "ok");
            }
            else if (Entry_Tel.Text == "")
            {
                await DisplayAlert("", "Ingresar Telefono", "ok");
            }
            else if (datoPiker == "")
            {
                await DisplayAlert("", "Ingresa una Carrera", "ok");
            }
            else if (datopikersem == "")
            {
                await DisplayAlert("", "Ingresar un Semestre", "ok");
            }
            else if (Entry_Cor.Text == "")
            {
                await DisplayAlert("", "Ingresar un Correo", "ok");
            }
            else if (Entry_Git.Text == "")
            {
                await DisplayAlert("", "Ingresar una cuenta de GitHub", "ok");
            }
            else if (Entry_Tel.Text.Length != t)
            {
                await DisplayAlert("", "El Numero Telefonico debe contener 10 Digitos", "ok");
            }
            else
            {
                try
                {
                    await SelectPage.Tabla.UpdateAsync(datos);
                    await DisplayAlert("", "Regsitro Actualizado", "OK");
                    await Navigation.PushModalAsync(new MenuDatos());
                }
                catch (SQLite.SQLiteException ex1)
                {
                    await DisplayAlert("", "Error Matricula Existente SQLITE ", "ok1");
                }
            }
        }
        catch (System.FormatException ex2)
        {
            await DisplayAlert("", "Ingresar Unicamente Numeros Valide Matricula u Telefono", "ok2");
        }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MenuDatos());
        }
        private async void RecuperarDatos_Clicked(object sender, EventArgs e)
        {
            bool del = false;
            var datos = new todoitem
            {

                Deleted = del
            };

            await SelectPage.Tabla.UndeleteAsync(datos);
        }
        
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            var datos = new todoitem
            {
                Id = Entry_ID.Text,
                /* Matricula = Convert.ToInt64(Entry_Mat.Text),
                 Nombre = Entry_Name.Text,
                 Apellidos = Entry_Ap.Text,
                 Direccion = Entry_Dir.Text,
                 Telefono = Convert.ToInt64(Entry_Tel.Text),
                 Carrera = datoPiker,
                 Semestre = datopikersem,
                 Correo = Entry_Cor.Text,
                 Github = Entry_Git.Text*/
            };
            /*database.Delete(datos);
            Navigation.PushModalAsync(new MenuDatos());*/
            await SelectPage.Tabla.DeleteAsync(datos);
            await DisplayAlert("", "Regsitro Eliminado", "OK");
            await Navigation.PushModalAsync(new MenuDatos());
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Geolocator;
using System.IO;
using SQLite;
using EXLuisMontes.Modelos;

namespace EXLuisMontes
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public async void GetLocation()
        {
            Location Location = await Geolocation.GetLastKnownLocationAsync();

            if (Location == null)
            {
                Location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                }); ;
            }

            Longitud.Text = Location.Longitude.ToString();
            Latitud.Text = Location.Latitude.ToString();


        }

        private void BtnLocation_Clicked(object sender, EventArgs e)
        {
            GetLocation();
        }

        private void BtnSalvar_Clicked(object sender, EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PapuFornite.db");
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<Datos>();

            var item = new Datos()
            {
                Longitud = Longitud.Text,
                Latitud = Latitud.Text,
                UbicacionLarga = ularga.Text,
                UbicacionCorta = ucorta.Text
            };

            db.Insert(item);
            Device.BeginInvokeOnMainThread(async () =>
            {

                var result = await this.DisplayAlert("Felicidades", "la direccion se registro Satisfactoriamente", "Si", "Cancelar");
                var page2 = new UbicacionesGuardadas();
                page2.BindingContext = item;
                await Navigation.PushAsync(page2);

            });
        }

     

        private async void BtnVer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync( new UbicacionesGuardadas());
        }
    }
}

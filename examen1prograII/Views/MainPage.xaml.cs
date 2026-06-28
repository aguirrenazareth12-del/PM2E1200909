using examen1prograII.Models;
using System;
using System.IO;

namespace examen1prograII.Views
{
    public partial class MainPage : ContentPage
    {
        string rutaFoto = "";
        double latitud;
        double longitud;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void TomarFoto(object sender, EventArgs e)
        {
            try
            {
                FileResult foto = await MediaPicker.CapturePhotoAsync();

                if (foto == null)
                    return;

                rutaFoto = Path.Combine(FileSystem.CacheDirectory, foto.FileName);

                using Stream origen = await foto.OpenReadAsync();
                using FileStream destino = File.OpenWrite(rutaFoto);

                await origen.CopyToAsync(destino);

                imgSitio.Source = ImageSource.FromFile(rutaFoto);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private async void SeleccionarDeGaleria(object sender, EventArgs e)
        {
            try
            {
                FileResult foto = await MediaPicker.PickPhotoAsync();

                if (foto == null)
                    return; 

                
                rutaFoto = Path.Combine(FileSystem.CacheDirectory, foto.FileName);

                using Stream origen = await foto.OpenReadAsync();
                using FileStream destino = File.OpenWrite(rutaFoto);

                await origen.CopyToAsync(destino);

                
                imgSitio.Source = ImageSource.FromFile(rutaFoto);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error Galería", ex.Message, "OK");
            }
        }

        private async void ObtenerUbicacion(object sender, EventArgs e)
        {
            try
            {
                Location location = await Geolocation.Default.GetLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("GPS", "No se pudo obtener ubicación", "OK");
                    return;
                }

                latitud = location.Latitude;
                longitud = location.Longitude;

                lblLatitud.Text = $"Latitud: {latitud}";
                lblLongitud.Text = $"Longitud: {longitud}";
            }
            catch
            {
                await DisplayAlert("GPS", "GPS desactivado o sin permisos", "OK");
            }
        }

        private async void GuardarSitio(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                await DisplayAlert("Error", "Ingrese descripción", "OK");
                return;
            }

            if (string.IsNullOrEmpty(rutaFoto))
            {
                await DisplayAlert("Error", "Debe tomar una foto", "OK");
                return;
            }

            Sitio sitio = new()
            {
                Descripcion = txtDescripcion.Text,
                Latitud = latitud,
                Longitud = longitud,
                Imagen = rutaFoto
            };

            await App.Database.GuardarSitio(sitio);

            await DisplayAlert("Correcto", "Sitio guardado exitosamente", "OK");

            txtDescripcion.Text = "";
            imgSitio.Source = null;
            lblLatitud.Text = "Latitud:";
            lblLongitud.Text = "Longitud:";
            rutaFoto = "";
        }

        private async void IrLista(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ListaPage));
        }
    }
}
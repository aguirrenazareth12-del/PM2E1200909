using examen1prograII.Models;
using System;

namespace examen1prograII.Views
{
    public partial class SitioMapaPage : ContentPage
    {
        private Sitio _sitioSeleccionado;

        public SitioMapaPage(Sitio sitio)
        {
            InitializeComponent();
            _sitioSeleccionado = sitio;

            if (_sitioSeleccionado != null)
            {
                lblDesc.Text = _sitioSeleccionado.Descripcion;
                lblCoords.Text = $"Lat: {_sitioSeleccionado.Latitud} | Lon: {_sitioSeleccionado.Longitud}";

                AbrirUbicacionExterna();
            }
        }

        private async void AbrirUbicacionExterna()
        {
            try
            {
                var ubicacion = new Location(_sitioSeleccionado.Latitud, _sitioSeleccionado.Longitud);
                var opciones = new MapLaunchOptions { Name = _sitioSeleccionado.Descripcion };
                await Microsoft.Maui.ApplicationModel.Map.OpenAsync(ubicacion, opciones);
            }
            catch (Exception)
            {
                string uri = $"geo:{_sitioSeleccionado.Latitud},{_sitioSeleccionado.Longitud}?q={_sitioSeleccionado.Latitud},{_sitioSeleccionado.Longitud}({Uri.EscapeDataString(_sitioSeleccionado.Descripcion)})";
                await Launcher.Default.OpenAsync(new Uri(uri));
            }
        }

        private async void OnCompartirClicked(object sender, EventArgs e)
        {
            if (_sitioSeleccionado != null && !string.IsNullOrEmpty(_sitioSeleccionado.Imagen))
            {
                try
                {
                    await Share.Default.RequestAsync(new ShareFileRequest
                    {
                        Title = "Compartir Ubicación",
                        File = new ShareFile(_sitioSeleccionado.Imagen)
                    });
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Compartir", $"Error: {ex.Message}", "OK");
                }
            }
        }
    }
}
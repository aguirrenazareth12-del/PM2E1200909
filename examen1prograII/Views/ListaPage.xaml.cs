using examen1prograII.Models;
using System;

namespace examen1prograII.Views
{
    public partial class ListaPage : ContentPage
    {
        public ListaPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listaSitios.ItemsSource = await App.Database.ObtenerSitios();
        }

        private async void EliminarSitio(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Sitio sitio = (Sitio)btn.CommandParameter;

            bool respuesta = await DisplayAlert(
                "Eliminar",
                $"¿Eliminar {sitio.Descripcion}?",
                "Sí",
                "No");

            if (!respuesta)
                return;

            await App.Database.EliminarSitio(sitio);
            listaSitios.ItemsSource = await App.Database.ObtenerSitios();
        }

        private async void EditarSitio(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Sitio sitio = (Sitio)btn.CommandParameter;

            string nuevaDescripcion = await DisplayPromptAsync(
                "Actualizar",
                "Nueva descripción",
                initialValue: sitio.Descripcion);

            if (string.IsNullOrWhiteSpace(nuevaDescripcion))
                return;

            sitio.Descripcion = nuevaDescripcion;
            await App.Database.ActualizarSitio(sitio);
            listaSitios.ItemsSource = await App.Database.ObtenerSitios();
        }

        private async void VerMapa(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                Sitio sitio = (Sitio)btn.CommandParameter;

                if (sitio != null)
                {
                    
                    await Navigation.PushAsync(new SitioMapaPage(sitio));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error de Navegación", ex.Message, "OK");
            }
        }
    }
}
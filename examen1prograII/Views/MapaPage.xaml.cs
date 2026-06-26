using examen1prograII.Models;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace examen1prograII.Views;

[QueryProperty(nameof(SitioId), "id")]
public partial class MapaPage : ContentPage
{
    public string SitioId { get; set; }

    Sitio sitioSeleccionado;
    private object mapa;

    public MapaPage()
    {
        InitializeComponent();
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await DisplayAlert("Prueba", "Entró al mapa", "OK");
        

        var lista = await App.Database.ObtenerSitios();

        sitioSeleccionado = lista.FirstOrDefault(
            x => x.Id == Convert.ToInt32(SitioId));

        if (sitioSeleccionado == null)
        {
            await DisplayAlert("Error", "No encontró el sitio", "OK");
            return;
        }

        await DisplayAlert(
            "Correcto",
            sitioSeleccionado.Descripcion,
            "OK");

        mapa.Pins.Clear();

    }


    private async void CompartirImagen(
        object sender,
        EventArgs e)
    {
        if (sitioSeleccionado == null)
            return;

        await Share.Default.RequestAsync(
            new ShareFileRequest
            {
                Title = "Compartir Sitio",
                File = new ShareFile(
                    sitioSeleccionado.Imagen)

            });
    }   
}
    
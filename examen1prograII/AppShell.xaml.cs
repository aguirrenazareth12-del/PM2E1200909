using examen1prograII.Views;

namespace examen1prograII;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(
            nameof(ListaPage),
            typeof(ListaPage));

        Routing.RegisterRoute(
            nameof(MapaPage),
            typeof(MapaPage));
    }
}
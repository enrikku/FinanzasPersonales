using FinanzasPersonales.Pages.Transacciones;

namespace FinanzasPersonales.Pages.Menu;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
	}

    private async void OnEliminarTransaccionClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Alerta", "En construccion", "OK");
    }

    private async void OnAgregarCategoriaClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Alerta", "En construccion", "OK");
    }

    private async void OnVerTransaccionesClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Alerta", "En construccion", "OK");
    }

    private async void OnAgregarTransaccionClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AgregarTransaccionPage());
    }
}
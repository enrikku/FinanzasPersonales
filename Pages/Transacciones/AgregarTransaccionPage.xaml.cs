using System.Collections.ObjectModel;
using System.Transactions;
using FinanzasPersonales.Models;

namespace FinanzasPersonales.Pages.Transacciones;

public partial class AgregarTransaccionPage : ContentPage
{
    public ObservableCollection<string> TiposTransaccion { get; set; }
    public DateTime Now { get; set; }
    public string SelectedTipoTransaccion { get; set; }


    public Command AgregarTransaccionCommand { get; set; }
    public Command Volver { get; set; }

    public AgregarTransaccionPage()
	{
		InitializeComponent();

        Now = DateTime.Now;

        TiposTransaccion = new ObservableCollection<string>(EnumHelper.ObtenerTiposTransaccion());

        AgregarTransaccionCommand = new Command(AgregarTransaccion);
        Volver = new Command(VolverPage);

        BindingContext = this;
	}

    protected override void OnAppearing()
    {
        pickerCategoria.SelectedIndex = 0;
        txtDescripcion.Text = "";
        txtMonto.Text = "";
        switchEsIngreso.IsToggled = false;
        dpFecha.Date = Now;
    }

    private async void AgregarTransaccion()
    {
        try
        {
            // Validar los campos
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text) ||
                string.IsNullOrWhiteSpace(txtMonto.Text) ||
                pickerCategoria.SelectedItem == null)
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            decimal monto;
            if (!decimal.TryParse(txtMonto.Text, out monto))
            {
                await DisplayAlert("Error", "El monto debe ser un n�mero v�lido.", "OK");
                return; // Salir del m�todo si la conversi�n falla
            }

            Transaccion transaccion = new Transaccion
            {
                EsIngreso = switchEsIngreso.IsToggled,
                Categoria = pickerCategoria.SelectedItem.ToString(),
                Fecha = dpFecha.Date,
                Monto = switchEsIngreso.IsToggled ? monto : monto * -1m,
                Descripcion = txtDescripcion.Text
            };



            // Guardar la transacci�n
            if (await VariablesGlobales.db.SaveItemAsync(transaccion) == 1)
            {
                // Mostrar toast
                await DisplayAlert("Transacci�n Agregada", "La transacci�n se ha agregado correctamente.", "OK");
                await Shell.Current.GoToAsync("//MainPage");
            }
        }
        catch (FormatException)
        {
            await DisplayAlert("Error", "El monto debe ser un n�mero v�lido.", "OK");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            await DisplayAlert("Error", "Ocurri� un error al agregar la transacci�n.", "OK");
        }
    }


    private async void VolverPage()
    {
        try
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
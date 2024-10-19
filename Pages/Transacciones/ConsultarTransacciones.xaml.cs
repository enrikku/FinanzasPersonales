using System.Collections.ObjectModel;
using FinanzasPersonales.Models;
using Microsoft.Maui.Controls;

namespace FinanzasPersonales.Pages.Transacciones;

public partial class ConsultarTransacciones : ContentPage
{
    public ObservableCollection<Transaccion> Transacciones { get; set; }
    private ViewCell m_LastSelectViewCell = null;


    public ConsultarTransacciones()
	{
		InitializeComponent();

        Transacciones = new ObservableCollection<Transaccion>();

        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadTransacciones();
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        //lvTransacciones.ItemsSource = null;
        Transacciones.Clear();
    }

    private async Task LoadTransacciones()
    {
        try
        {
            if (Transacciones.Count == 0)
            { 
                var transacciones = await VariablesGlobales.db.GetItemsAsync<Transaccion>();

                transacciones = transacciones
                    .OrderByDescending(t => t.Fecha)
                    .ToList();

                if (transacciones != null)
                {
                    foreach (var transaccion in transacciones)
                    {
                        Transacciones.Add(transaccion);
                    }
                }
            }
        }
        catch (Exception ex) 
        { 
            Console.WriteLine(ex.ToString());
        }
    }

    private async void EliminarTransaccion(object sender, EventArgs e)
    {
        try
        {
            var miTransaccion = lvTransacciones.SelectedItem as Transaccion;

            if(miTransaccion == null)
            {
                return;
            }

            await VariablesGlobales.db.DeleteItemAsync(miTransaccion);
            Transacciones.Remove(miTransaccion);
        }
        catch (Exception ex) 
        { 
            Console.WriteLine(ex.ToString());
        }
        
    }

    private async void EditarTransaccion(object sender, EventArgs e)
    {
        try
        {
            //var miTransaccion = (Transaccion)((MenuItem)sender).BindingContext;

            //if (miTransaccion == null)
            //{
            //    return;
            //}

            //await VariablesGlobales.db.DeleteItemAsync(miTransaccion);
            //Transacciones.Remove(miTransaccion);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private async void OnTappedListItem(object sender, EventArgs e)
    {
        if (m_LastSelectViewCell != null)
            m_LastSelectViewCell.View.BackgroundColor = Colors.Transparent;

        var viewCell = sender as ViewCell;
        viewCell.View.BackgroundColor = Colors.LightGray;

        m_LastSelectViewCell = viewCell;
    }
}
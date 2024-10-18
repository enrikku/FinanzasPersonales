using System.Collections.ObjectModel;
using FinanzasPersonales.Models;
using Microcharts;
using SkiaSharp;

namespace FinanzasPersonales
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Transaccion> UltimasTransacciones { get; set; }
        public decimal TotalIngresos { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal Balance { get; set; }

        public MainPage()
        {
            InitializeComponent();

            UltimasTransacciones = new ObservableCollection<Transaccion>();

            BindingContext = this;

            //Task.Run(async () => await CargarDatosAsync());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //await VariablesGlobales.db.DeleteAllItemsAsync<Transaccion>();
            await CargarDatosAsync();
            await CargarChartGastosCategoria();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            TotalGastos = 0;
            TotalIngresos = 0;
            Balance = 0;
        }

        private async Task CargarChartGastosCategoria()
        {
            try
            {
                var transacciones = await VariablesGlobales.db.GetItemsAsync<Transaccion>();

                if (transacciones == null || !transacciones.Any())
                {
                    return;
                }

                var gastosPorCategoria = transacciones
                    .Where(t => !t.EsIngreso)
                    .GroupBy(t => t.Categoria)
                    .Select(g => new
                    {
                        Categoria = g.Key,
                        TotalGasto = g.Sum(t => t.Monto)
                    })
                    .ToList();

                if (!gastosPorCategoria.Any())
                {
                    return;
                }

                var entries = gastosPorCategoria.Select(g => new Microcharts.ChartEntry((float)g.TotalGasto)
                {
                    Label = g.Categoria,
                    ValueLabel = g.TotalGasto.ToString("C") + "€",
                    Color = GetColorByCategory(g.Categoria),
                    ValueLabelColor = SKColors.White
                }).ToList();

                // Asignar las nuevas entradas al ChartView
                myChart.Chart = new Microcharts.BarChart
                {
                    Entries = entries,
                    BackgroundColor = SKColors.Transparent,
                    LabelColor = SKColors.White,
                    LabelOrientation = Orientation.Horizontal,
                    LabelTextSize = 40
                };

                myChart.InvalidateSurface(); // Asegurarse de que el gráfico se actualice
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private SKColor GetColorByCategory(string categoria)
        {
            switch (categoria)
            {
                case "Alimentación":
                    return SKColor.Parse("#FF5733"); // Rojo
                case "Transporte":
                    return SKColor.Parse("#33FF57"); // Verde
                case "Entretenimiento":
                    return SKColor.Parse("#3357FF"); // Azul
                case "Salud":
                    return SKColor.Parse("#F1C40F"); // Amarillo
                                                     // Agrega más categorías y colores según necesites
                default:
                    return SKColor.Parse("#7F7F7F"); // Gris para categorías desconocidas
            }
        }

        private async Task CargarDatosAsync()
        {
            try
            {
                var transacciones = await VariablesGlobales.db.GetItemsAsync<Transaccion>();

                foreach (var item in transacciones) 
                {
                    if (item.EsIngreso) TotalIngresos += item.Monto;
                    else TotalGastos += item.Monto;
                }

                lblIngresos.Text = TotalIngresos.ToString() + "€";
                lblGastos.Text = TotalGastos.ToString() + "€";

                decimal balance = TotalIngresos - TotalGastos * -1;
                if (balance > 0) lblBalance.TextColor = Colors.Green;
                else if (balance < 0) lblBalance.TextColor = Colors.Red;

                lblBalance.Text = balance.ToString() + "€";
              

                var ultimas = transacciones
                    .OrderByDescending(t => t.Fecha)
                    .Take(5)
                    .ToList();

                UltimasTransacciones.Clear();
                foreach (var transaccion in ultimas)
                {
                    UltimasTransacciones.Add(transaccion);
                }

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        private async void btnNuevaTransaccion_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("//AgregarTransaccionPage");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

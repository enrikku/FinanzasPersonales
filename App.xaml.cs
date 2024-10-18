using FinanzasPersonales.DB;

namespace FinanzasPersonales
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Establece AppShell como la página principal
            MainPage = new AppShell();
            Task.Run(async () => await InitializeMainPageAsync());
        }

        private async Task InitializeMainPageAsync()
        {
            await IniciarDBAsync();
            await Shell.Current.GoToAsync("MainPage");
        }

        private async Task IniciarDBAsync()
        {
            try
            {
                if (VariablesGlobales.db == null)
                {
                    VariablesGlobales.db = new DatabaseService(VariablesGlobales.RUTA_DB);
                    await VariablesGlobales.db.InitializeDatabaseAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

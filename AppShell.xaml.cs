namespace FinanzasPersonales
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnAgregarTransaccionClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///AgregarTransaccionPage");
        }

        private async void OnConsultarTransaccionesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///ConsultarTransacciones");
        }
    }
}

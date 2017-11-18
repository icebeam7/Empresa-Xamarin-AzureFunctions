using System;
using Empresa_Xamarin_AzureFunctions.Servicios;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Empresa_Xamarin_AzureFunctions.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaListaEmpleados : ContentPage
    {
        public PaginaListaEmpleados()
        {
            InitializeComponent();
        }

        private async void btnObtenerEmpleados_Clicked(object sender, EventArgs e)
        {
            var origen = ((Button)sender).Text.Contains("Google") ? "Google" : "Table";
            lsvEmpleados.ItemsSource = await ServicioEmpresaXamarinAzureFunctions.ObtenerEmpleados(origen);
        }

        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaginaAgregarEmpleado());
        }
    }
}
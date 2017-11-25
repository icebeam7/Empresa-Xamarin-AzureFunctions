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

        void MostrarActividad(bool mostrar)
        {
            indicador.IsVisible = mostrar;
            indicador.IsRunning = mostrar;
            indicador.IsEnabled = mostrar;
        }

        private async void btnObtenerEmpleados_Clicked(object sender, EventArgs e)
        {
            MostrarActividad(true);

            var origen = ((Button)sender).Text.Contains("Google") ? "Google" : "Table";
            var depto = txtBusqueda.Text;
            lsvEmpleados.ItemsSource = await ServicioEmpresaXamarinAzureFunctions.ObtenerEmpleados(origen, depto);

            MostrarActividad(false);
        }

        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PaginaAgregarEmpleado());
        }

        private async void btnInsertarGoogle_Clicked(object sender, EventArgs e)
        {
            MostrarActividad(true);

            var resultado = await ServicioEmpresaXamarinAzureFunctions.Insertar_DeGoogle_ATable();
            await DisplayAlert("Inserción", resultado, "OK");

            MostrarActividad(false);
        }
    }
}
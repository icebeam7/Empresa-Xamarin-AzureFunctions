using System;
using Empresa_Xamarin_AzureFunctions.Modelos;
using Empresa_Xamarin_AzureFunctions.Servicios;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Empresa_Xamarin_AzureFunctions.Paginas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaginaAgregarEmpleado : ContentPage
    {
        public PaginaAgregarEmpleado()
        {
            InitializeComponent();
        }

        private async void btnAgregarEmpleado_Clicked(object sender, EventArgs e)
        {
            var destino = ((Button)sender).Text.Contains("Google") ? "Google" : "Table";

            var empleado = new Empleado()
            {
                ID = txtID.Text,
                Nombre = txtNombre.Text,
                Departamento = txtDepartamento.Text
            };

            lblResultado.Text = await ServicioEmpresaXamarinAzureFunctions.AgregarEmpleado(empleado, destino);
        }
    }
}
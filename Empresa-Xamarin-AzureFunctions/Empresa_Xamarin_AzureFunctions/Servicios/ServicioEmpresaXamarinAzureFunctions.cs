using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Empresa_Xamarin_AzureFunctions.Modelos;
using Empresa_Xamarin_AzureFunctions.Helpers;
using Newtonsoft.Json;

namespace Empresa_Xamarin_AzureFunctions.Servicios
{
    public static class ServicioEmpresaXamarinAzureFunctions
    {
        public async static Task<string> AgregarEmpleado(Empleado empleado, string destino)
        {
            try
            {
                var url = (destino == "Google")
                    ? Constantes.FunctionAgregarEmpleadoGoogleDrive
                    : Constantes.FunctionAgregarEmpleadoTableStorage;

                using (var cliente = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(empleado);
                    var content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var post = await cliente.PostAsync(url, content);
                    return await post.Content.ReadAsStringAsync();
                }
            }
            catch(Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async static Task<List<Empleado>> ObtenerEmpleados(string origen, string depto)
        {
            try
            {
                var url = (origen == "Google")
                    ? (string.IsNullOrWhiteSpace(depto))
                        ? Constantes.FunctionObtenerEmpleadosGoogleDrive
                        : Constantes.FunctionObtenerEmpleadosDeptoGoogleDrive + depto
                    : (string.IsNullOrWhiteSpace(depto))
                        ? Constantes.FunctionObtenerEmpleadosTableStorage
                        : Constantes.FunctionObtenerEmpleadosDeptoTableStorage + depto;

                /*var url = "";

                if (origen == "Google")
                {
                    if (string.IsNullOrWhiteSpace(depto))
                        url = Constantes.FunctionObtenerEmpleadosGoogleDrive;
                    else
                        url = Constantes.FunctionObtenerEmpleadosDeptoGoogleDrive + depto;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(depto))
                        url = Constantes.FunctionObtenerEmpleadosTableStorage;
                    else
                        url = Constantes.FunctionObtenerEmpleadosDeptoTableStorage + depto;
                }*/

                using (var cliente = new HttpClient())
                {
                    var json = await cliente.GetStringAsync(url);
                    return JsonConvert.DeserializeObject<List<Empleado>>(json);
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async static Task<string> Insertar_DeGoogle_ATable()
        {
            try
            {
                var url = Constantes.FunctionInsertarEmpleados_DeGoogle_ATable;

                using (var cliente = new HttpClient())
                {
                    var post = await cliente.PostAsync(url, null);
                    return await post.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
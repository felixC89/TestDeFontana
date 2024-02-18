using Nancy.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace ClienteVentas.Helpers
{
    public class ApiConsumer
    {
        public enum MethodType
        {
            GET = 0,
            POST,
            PUT,
            DELETE,
        }

        public static async Task<T> SendAsync<T>(string Domain, string Method, MethodType MethodType, object Parameter = null, Dictionary<string, string> Headers = null)
        {

            T? _Result = default(T);

            HttpResponseMessage _Response = new HttpResponseMessage();
            StringContent _StringContent = new StringContent("", Encoding.UTF8, "application/json");
            Uri _BaseAddress = new Uri($"{Domain}/{Method}");

            try
            {
                using (HttpClient _httpClient = new HttpClient())
                {


                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Agregando Headers
                    if (Headers != null)
                    {
                        foreach (var item in Headers)
                        {
                            _httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                        }
                    }

                    //Serializamos el objeto en formato json la data a enviar
                    if (Parameter != null)
                    {
                        _StringContent = new StringContent((new JavaScriptSerializer()).Serialize(Parameter), Encoding.UTF8, "application/json");
                    }

                    //Estableciendo el tipo de peticion
                    switch (MethodType)
                    {
                        case MethodType.GET:
                            _Response = await _httpClient.GetAsync(_BaseAddress);
                            break;
                        case MethodType.POST:
                            _Response = await _httpClient.PostAsync(_BaseAddress, _StringContent);
                            break;
                        case MethodType.PUT:
                            _Response = await _httpClient.PutAsync(_BaseAddress, _StringContent);
                            break;
                        case MethodType.DELETE:
                            _Response = await _httpClient.DeleteAsync(_BaseAddress);
                            break;
                        default:
                            break;
                    }

                }

                //Obteniendo la respuesta
                if (_Response.StatusCode == HttpStatusCode.OK)
                {
                    string json = await _Response.Content.ReadAsStringAsync();
                    _Result = (new JavaScriptSerializer()).Deserialize<T>(json);
                }

                if (_Response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string json = await _Response.Content.ReadAsStringAsync();
                    json = json.Replace("\"", "");

                    var tipo = typeof(T);

                    T instance = (T)Activator.CreateInstance(tipo);

                    PropertyInfo propertyInfo = instance.GetType().GetProperty("Message");

                    if (propertyInfo != null)
                    {
                        propertyInfo.SetValue(instance, Convert.ChangeType(json, propertyInfo.PropertyType), null);

                        _Result = instance;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.Message);
            }

            return _Result;

        }
    }
}

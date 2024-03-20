using System.Text;
using System.Text.Json;
using System.Timers;

//Orden de definciones
//1 Atributos privados(private readonly xxx xxx)
//2 CTOR
//3 propiedades publicas
//4. métodos públicos
//5 metodos privados

namespace Orders.Frontend.Repositories
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpclient; //read only son propiedades que solo se pueden usar en el CTOR y ya no se modifican

        private JsonSerializerOptions _jsonDefaultOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, //Para que me pueda mapear el Json independientemente que venga en mayuscula o minuscula.
        };

        public Repository(HttpClient httpclient) //Inyecto el servicio del backend que fue configurado en Program.cs
        {
            _httpclient = httpclient;
        }

        public async Task<HttpResponseWrapper<T>> GetAsync<T>(string url)
        {
            var responseHttp = await _httpclient.GetAsync(url);
            if (responseHttp.IsSuccessStatusCode)
            {
                //tengo que desearelizar la respuesta
                var response = await UnserializeAnswserAsync<T>(responseHttp); //esto es un metodo propio.
                return new HttpResponseWrapper<T>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, true, responseHttp);
            }
        }

        //El orden por convencion es primero metodos publicos depues los privados
        public async Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model) // Post que NO devuelve respuesta
        {
            <//Como al POSt llega un model entonces lo tenemos que serializar(transformar el obj en json)
            var messageJson = JsonSerializer.Serialize(model);
            //despues hay que serializar por ejemplo hay palabras que tienen tildes je. por el UTF8
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");

            var responseHttp = await _httpclient.PostAsync(url, messageContent);

            //como no necesito leer la respuesta entonce no se requiere nada de serializar y de una lo retornamos
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp); //USamos ! porque lo estamos negando
>
        }

        public async Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model) // Post que envia un T y devuelve TActionRespuesta
        {
            //Como al POSt llega un model entonces lo tenemos que serializar(transformar el obj en json)
            var messageJson = JsonSerializer.Serialize(model);
            //despues hay que serializar por ejemplo hay palabras que tienen tildes je. por el UTF8
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");

            var responseHttp = await _httpclient.PostAsync(url, messageContent);

            if (responseHttp.IsSuccessStatusCode)
            {
                //tengo que desearelizar la respuesta
                var response = await UnserializeAnswserAsync<TActionResponse>(responseHttp); //esto es un metodo propio.
                return new HttpResponseWrapper<TActionResponse>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<TActionResponse>(default, true, responseHttp); //Como no hay obj then enviamos default
            }
        }

        public async Task<HttpResponseWrapper<object>> DeleteAsync<T>(string url, T model) // me baso en el POST que no recibe respuesta, pero no se requiere enviar
        {
            var responseHttp = await _httpclient.DeleteAsync(url);

            //como no necesito leer la respuesta entonce no se requiere nada de serializar y de una lo retornamos
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp); //null = no devuelvo respuesta en el delete, IsSuccessStatusCode = si hubo o no error, responseHttp=detalle de la respuesta http
        }

        public async Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T model)
        {
            var messageJson = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");

            var responseHttp = await _httpclient.PutAsync(url, messageContent);

            //como no necesito leer la respuesta entonce no se requiere nada de serializar y de una lo retornamos
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp); //USamos ! porque lo estamos negando
        }

        public async Task<HttpResponseWrapper<TActionResponse>> PutAsync<T, TActionResponse>(string url, T model)
        {
            var messageJson = JsonSerializer.Serialize(model);
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");

            var responseHttp = await _httpclient.PutAsync(url, messageContent);

            if (responseHttp.IsSuccessStatusCode)
            {
                //tengo que desearelizar la respuesta
                var response = await UnserializeAnswserAsync<TActionResponse>(responseHttp); //esto es un metodo propio.
                return new HttpResponseWrapper<TActionResponse>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<TActionResponse>(default, true, responseHttp); //Como no hay obj then enviamos default
            }
        }

        private async Task<T> UnserializeAnswserAsync<T>(HttpResponseMessage responseHttp)  // lo defino al final por el orden que debe llevar readme line 5
        {
            var response = await responseHttp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(response, _jsonDefaultOptions)!;//! significa que estamos concientes que pude venir en null.
            throw new NotImplementedException();
        }
    }
}
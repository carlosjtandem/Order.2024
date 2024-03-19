using System.Net;
using System.Net.Http;

namespace Orders.Frontend.Repositories
{
    public class HttpResponseWrapper<T> //Definimos un envoltorio de todas las respuestas HTTP (GENERICO)
    {

        //CTOR
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Error = error;
            HttpActionResponseMessage = httpResponseMessage;
        }

        //PROPIEDADES
        public T? Response { get; }
        public bool Error { get; }
        public HttpResponseMessage HttpActionResponseMessage { get; }

        //METODOS
        public async Task<string?> GetErrorMessageAsync()
        {
            if (!Error)
            {
                return null;
            }
            var statusCode = HttpActionResponseMessage.StatusCode; //Esto es una enumeracion que devuelve los estados de la respuesta

            if (statusCode == HttpStatusCode.NotFound)
            {
                return "Recurso no encontrado"; // Mensaje claro para el usuario
            }
            if (statusCode == HttpStatusCode.BadRequest)
            {
                return await HttpActionResponseMessage.Content.ReadAsStringAsync();
            }
            if (statusCode == HttpStatusCode.Unauthorized)
            {
                return "Tienes que estar logueado para ejecutar esta operación"; // Mensaje claro para el usuario
            }
            if (statusCode == HttpStatusCode.Forbidden)
            {
                return "No tienes permismo para ejecutar esta operación"; // Mensaje claro para el usuario
            }

            return "Ha ocurrido un error inesperado";

        }
    }

}

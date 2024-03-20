namespace Orders.Frontend.Repositories
{
    public interface IRepository // Tambien lo usaremos con genericos ya que nos va a serviro para varias entidades
    {
        Task<HttpResponseWrapper<T>> GetAsync<T>(string url); //La url es del controlador. Ejemplo se puede traer un envoltorio de paises, envoltorio de productos


        //Tenemos 2 post y lo estamos sobrecargando. 1 que devuelve respuesta y otro que no devuelve respuesta(solo devuelve ok)
        Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model);

        Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model);

        Task<HttpResponseWrapper<object>> DeleteAsync<T>(string url, T model);

        Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T model);

        Task<HttpResponseWrapper<TActionResponse>> PutAsync<T, TActionResponse>(string url, T model); //Sobrecrgamos el method porque este si va a devolver un modelo


    }
}
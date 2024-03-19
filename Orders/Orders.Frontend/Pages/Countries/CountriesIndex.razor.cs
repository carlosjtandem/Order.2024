using Microsoft.AspNetCore.Components;
using Orders.Frontend.Repositories;
using Orders.Shared.Entities;

namespace Orders.Frontend.Pages.Countries
{
    public partial class CountriesIndex //Defino partial para poder trabajar por partes
    {
        [Inject] private IRepository repository { get; set; } = null;  //Estamos creando un repositorio

        public List<Country>? Countries { get; set; } //? es nulleable porque puede pasar que este vacio

        //parte del ciclo de vida
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            var responseHttp = await repository.GetAsync<List<Country>>("api/countries");

            Countries = responseHttp.Response;  // aqui devuelve la lista de paises
        }


    }
}

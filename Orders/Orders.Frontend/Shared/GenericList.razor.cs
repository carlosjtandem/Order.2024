using Microsoft.AspNetCore.Components;

namespace Orders.Frontend.Shared
{
    //Va a permitir listar cualquier objecto
    public partial class GenericList<Titem>
    {
        [Parameter] public RenderFragment? Loading { get; set; } //Puede ser null
        [Parameter] public RenderFragment? NoRecords { get; set; } //campo cuando no hay registros
        [EditorRequired, Parameter] public RenderFragment? Body { get; set; } = null!; // Primer param obligatorio No Puede ser null  
        [EditorRequired, Parameter] public List<Titem>  MyList { get; set; } = null!; // Segundo param obligatorio .. Es el listado
    }
}

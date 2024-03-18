using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Shared.Entities
{
    public class Country
    {
        public int Id { get; set; } //al darle el nombre Id ya sabe que sera un autoincremental

        //Data annotation define que {0} es el nombre de la prop
        [Display(Name = "País")]  //{0} = Name será mostrado en español. es decir País ..
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Name { get; set; } = null!;  //al = null significa que no puede ser null
    }
}
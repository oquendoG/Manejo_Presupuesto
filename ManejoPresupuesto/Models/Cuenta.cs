using ManjeoPresupuesto.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ManjeoPresupuesto.Models
{
    public class Cuenta
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(maximumLength: 50)]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Display(Name = "Tipo cuenta")]
        public int TipoCuentaId { get; set; }

        public string TipoCuenta { get; set; }

        public decimal Balance { get; set; }

        [StringLength(maximumLength: 1000)]
        public string Descripcion { get; set; }

    }
}

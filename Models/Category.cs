using System.ComponentModel.DataAnnotations;

namespace ProjetoH_WebApi_DD_7194.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter de 3 a 60 caracteres!")]
        [MinLength(3, ErrorMessage = "Este campo deve conter de 3 a 60 caracteres!")]
        public string Title { get; set; }
    }
}

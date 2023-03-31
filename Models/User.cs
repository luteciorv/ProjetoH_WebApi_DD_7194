﻿using System.ComponentModel.DataAnnotations;

namespace ProjetoH_WebApi_DD_7194.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter de 3 a 20 caracteres!")]
        [MinLength(3, ErrorMessage = "Este campo deve conter de 3 a 20 caracteres!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório!")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter de 3 a 20 caracteres!")]
        [MinLength(3, ErrorMessage = "Este campo deve conter de 3 a 20 caracteres!")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class User
    {
        
        [ForeignKey("UserDetail")]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public virtual UserDetail UserDetail { get; set; }
    }
}

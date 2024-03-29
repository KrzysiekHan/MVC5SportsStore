﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Proszę podać nazwę użytkownika")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Proszę podać hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
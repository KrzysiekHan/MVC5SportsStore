using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Models
{
    public class RegisterViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; set; }

        [Display(Name = "Nazwa użytkownika")]
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana")]
        public string Username { get; set; }

        [Display(Name = "Imię")]
        [Required(ErrorMessage = "Proszę podać imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Proszę podać nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Adres Email")]
        [Required(ErrorMessage = "Proszę podać adres email")]
        public string EmailAdress { get; set; }

        [Display(Name = "Numer telefonu")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Ulica")]
        [Required(ErrorMessage = "Proszę podać ulicę")]
        public string Street { get; set; }

        [Display(Name = "NrBudynku")]
        [Required(ErrorMessage = "Proszę podać nr budynku")]
        public string BuildingNr { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Proszę podać nazwę miasta")]
        public string City { get; set; }

        [Display(Name = "Województwo")]
        [Required(ErrorMessage = "Podaj nazwę województwa")]
        public string State { get; set; }

        [Display(Name = "Kod pocztowy")]
        public string Zip { get; set; }

        [Display(Name = "Kraj")]
        [Required(ErrorMessage = "Proszę podać nazwę kraju")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Potwierdź hasło")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
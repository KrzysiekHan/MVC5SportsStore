using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class UserDetail
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int ID { get; set; }

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

        public virtual User User { get; set; }
    }
}

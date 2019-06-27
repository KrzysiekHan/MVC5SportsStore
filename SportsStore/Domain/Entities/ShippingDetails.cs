using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Proszę podać nazwisko")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Proszę podać pierwszy wiersz adresu")]
        [Display(Name="wiersz 1")]
        public string Line1 { get; set; }
        [Display(Name="Wiersz 2")]
        public string Line2 { get; set; }
        [Display(Name="Wiersz 3")]
        public string Line3 { get; set; }

        [Display(Name="Miasto")]
        [Required(ErrorMessage ="Proszę podać nazwę miasta")]
        public string City { get; set; }

        [Display(Name="Województwo")]
        [Required(ErrorMessage ="Podaj nazwę województwa")]
        public string State { get; set; }

        [Display(Name="Kod pocztowy")]
        public string Zip { get; set; }

        [Display(Name="Kraj")]
        [Required(ErrorMessage = "Proszę podać nazwę kraju")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }

    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web_ManagementHouseRentals.Data.Entities
{
    public class User : IdentityUser
    {

        [Display(Name = "First Name")]
        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters lenght.")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(50, ErrorMessage = "The field {0} can only contain  {1} characters lenght.")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "CC")]
        [Required]
        [Range(10000000, 99999999, ErrorMessage = "The field {0} have to contain 9 numbers.")]
        public int CC { get; set; }

        [Required]
        [Display(Name = "NIF")]
        [Range(100000000, 999999999, ErrorMessage = "The field {0} have to contain 9 numbers.")]
        public int NIF { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^\d{4}(-\d{3})?$", ErrorMessage = "Incorrect Zip Code fomart. Ex: 2084-321")]
        public string ZipCode { get; set; }

        public string Locality { get; set; }

        public string Municipality { get; set; }

        public string District { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "The field {0} can contain between {2} and {1} characters.")]
        public string Address { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Customer")]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }

        [Display(Name = "Is Landlord?")]
        public bool IsLandlord { get; set; }
        //se tem um anuncio para alugar=> gravar IsLandlord=true;
    }
}

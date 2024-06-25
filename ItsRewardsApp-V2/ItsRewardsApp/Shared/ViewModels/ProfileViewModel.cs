using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.ViewModels
{
    public class ProfileViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        //[StringLength(5, MinimumLength = 5, ErrorMessage = "Zip Code 5 number allowed")]
        //[Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public string CellPhone { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Required]
        [Display(Name = "Email Address")]
        public string EMail { get; set; }
        public DateTime? BirthDate { get; set; }
        public int UserProfileID { get; set; }
    }
}

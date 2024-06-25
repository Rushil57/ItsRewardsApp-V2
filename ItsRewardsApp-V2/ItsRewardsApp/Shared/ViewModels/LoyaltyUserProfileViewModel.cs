using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItsRewardsApp.Shared.ViewModels
{
    public class LoyaltyUserProfileViewModel
    {
        public int UserProfileID { get; set; }

        public int? RevenueCenterID { get; set; } = 0;

        public string isDelete { get; set; } = "";

        public char isActive { get; set; } 
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = "";
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = "";

        public string Title { get; set; } = "";

        [Required]
        [Display(Name = "Address")]
        public string Address1 { get; set; } = "";

        public string Address2 { get; set; } = "";

        public string City { get; set; } = "";

        public string State { get; set; } = "";
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Zip Code 5 number allowed")]
        //[Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; } = "";

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Required]
        [Display(Name ="Email Address")]
        public string EMail { get; set; } = "";

        public string HomePhone { get; set; } = "";
        [StringLength(10, MinimumLength = 10, ErrorMessage = "only 10 number allowed")]
        [Required]
        [Display(Name = "Cell Number")]
        public string CellPhone { get; set; } = "";

        public int? Points { get; set; } = 0;

        public int? CustomerGroup { get; set; } = 0;

        public int? CustomerStatus { get; set; } = 0;

        public DateTime? BirthDate { get; set; } = null;

        public int? PriceLevel { get; set; } = 0;

        public string CustomerNumber { get; set; } = "";

        public string AgeVerified { get; set; } = "";

        public string AppVerified { get; set; } = "";

        public string? UserPass { get; set; }
        //public bool EmailPromotions { get; set; } = false;
        //public bool TextPromotions { get; set; } = false;
        public string? StoreId { get; set; }
        public string? StoreName { get; set; }
        public string? PropertyName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string? ConfirmPassword { get; set; }
        public string? Pinnumber { get; set; }
        public string? UserSignature { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.ViewModels
{
    public class LoginViewModel
    {
        [StringLength(10, MinimumLength = 10, ErrorMessage = "only 10 number allowed")]
        [Required(ErrorMessage = "Cell Phone is required")]
        [Display(Name = "Cell Phone")]
        public string? CellPhone { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; } = "";
        public bool isValid { get; set; } = false;
        public bool RememberMe { get; set; } = false;
        public char isActive { get; set; } 
    }
}

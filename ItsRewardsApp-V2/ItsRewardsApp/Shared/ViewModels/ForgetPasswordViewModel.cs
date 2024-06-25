using Plivo.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ItsRewardsApp.Shared.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [StringLength(10, MinimumLength = 10, ErrorMessage = "only 10 number allowed")]
        [Required(ErrorMessage = "Cell Phone is required")]
        [Display(Name = "Cell Phone")]
        public string? CellPhone { get; set; } = "";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.ViewModels
{
    public class StoreRetailChoiceDataViewModel
    {
        public string SessionID { get; set; } = "";
        public string? FirstName { get; set; } = "";
        public string? LastName { get; set; } = "";
        public CurrentAddressViewModel CurrentAddress { get; set; } = null;
        public DateTime? DateOfBirth { get; set; } = null;
        public string? EmailAddress { get; set; } = "";
        public string? CertificationCD { get; set; } = "";
        public SurveyDataViewModel SurveyDataArray { get; set; } = null;
        public string? LoyaltyId { get; set; } = "";
        public Int32? StoreID { get; set; } = 0;
        public string? StoreName { get; set; } = "";
        public string? Tier { get; set; } = "";

        public string? AltriaAccountNumber { get; set; } = "";
        public string? StatusCode { get; set; } = "";
        public string? ResponsePhares { get; set; } = "";
    }

    public class CurrentAddressViewModel
    {
        public string? AddressLine1 { get; set; } = "";
        public string? City { get; set; } = "";
        public string? state { get; set; } = "";
        public string? Zip { get; set; } = "";
    }
    public class SurveyDataViewModel
    {
        public string? SurveyQuestionID { get; set; } = "";

        public List<SurveyDataResponseViewModel> SurveyResponseData { get; set; } = null;
    }

    public class SurveyDataResponseViewModel
    {
        public string? ResponseID { get; set; } = "";
    }
}

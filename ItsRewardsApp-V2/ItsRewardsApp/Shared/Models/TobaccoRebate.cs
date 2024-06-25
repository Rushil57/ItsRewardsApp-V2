using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.Models
{
	[Table("tblTobaccoRebateProgramType")]
	public class TobaccoRebate
    {
		[Key]
		public int RebateTypeID { get; set; }

		public int? DataBaseID { get; set; }

		public int StoreID { get; set; }

		public string? RJRAccountNumber { get; set; }

		//[System.ComponentModel.DataAnnotations.Key]
		public string? AltriaAccountNumber { get; set; }

		public string? AltriaUSSTC { get; set; }

		public string? AltriaAllOtherManCig { get; set; }

		public string? AltriaPMUSA { get; set; }

		public string? RJRUserName { get; set; }

		public string? RJRPassword { get; set; }

		public string? RJRURL { get; set; }

		public string? AltriaUserName { get; set; }

		public string? AltriaPassword { get; set; }

		public string? AltriaURL { get; set; }

		public DateTime? LastLateSalesEMailDate { get; set; }

		public DateTime? LastRJREMailDate { get; set; }

		public DateTime? LastAltriaEMailDate { get; set; }

		public DateTime? LastRJRFileSent { get; set; }

		public DateTime? LastAltriaFileSent { get; set; }

		public string? IsAltriaChain { get; set; }

		public string? DayClosed { get; set; }

		public string? AltriaChainNumber { get; set; }

		public string? Active { get; set; }

		public DateTime CreateDate { get; set; }

		public string? ChainName { get; set; }

		public int? OutletSequence { get; set; }

		public int? RegisterID { get; set; }

		public string? IsAltriaApproved { get; set; }

		public string? IsRJRApproved { get; set; }

		public string? AltriaFileName { get; set; }

		public DateTime? rjrsubmitiondate { get; set; }

		public string? RJRFileName { get; set; }

		public string? AltriaSubmission { get; set; }

		public string? RJRSubmission { get; set; }

		public string? RJRPortalUserName { get; set; }

		public string? RJRPortalPassword { get; set; }

		public string? PromotionUpdateScript { get; set; }

		public string? marlboromultipack { get; set; }

		public string? iriemail { get; set; }

		public string? msaemail { get; set; }

		public string? coupons { get; set; }

		public string? loyalty { get; set; }

		public string? promotiontext { get; set; }

		public string? RJRStoreAccountNumber { get; set; }

		public string? SendEmail { get; set; }

		public string? RCN { get; set; }

		public string? Altria_Client_Id { get; set; }

		public string? Altria_Client_Secret { get; set; }

		public int? AltriaBatchType { get; set; }

		public string? Tier { get; set; }

		public string? Cigs { get; set; }

		public string? Smokeless { get; set; }

		public string? Cigar { get; set; }

		public string? EVape { get; set; }
	}
}

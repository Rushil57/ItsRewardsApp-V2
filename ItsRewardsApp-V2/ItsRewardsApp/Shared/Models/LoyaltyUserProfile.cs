using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItsRewardsApp.Shared.Models
{
    public class LoyaltyUserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserProfileID { get; set; }

        public int? RevenueCenterID { get; set; } = 0;

        public string? isDelete { get; set; } = "";

        public char isActive { get; set; }
        //[Required]
        public string? FirstName { get; set; } = "";
        //[Required]
        public string? LastName { get; set; } = "";

        public string? Title { get; set; } = "";

        public string? Address1 { get; set; } = "";

        public string? Address2 { get; set; } = "";

        public string? City { get; set; } = "";

        public string? State { get; set; } = "";
        [StringLength(maximumLength:6)]
        public string? ZipCode { get; set; } = "";

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string? EMail { get; set; } = "";

        public string? HomePhone { get; set; } = "";
        //[Required]
        [StringLength(maximumLength:10)]
        public string CellPhone { get; set; } = "";

        public int? Points { get; set; } = 0;

        public int? CustomerGroup { get; set; } = 0;

        public int? CustomerStatus { get; set; } = 0;

        public DateTime? BirthDate { get; set; } = null;

        public int? PriceLevel { get; set; } = 0;

        public string? CustomerNumber { get; set; } = "";

        public string? AgeVerified { get; set; } = "";

        public string? AppVerified { get; set; } = "";

        public byte[]? UserPass { get; set; }
        public string? UserSignature { get; set; }
        //public bool EmailPromotions { get; set; } = false;
        //public bool TextPromotions { get; set; } = false;
         public string? Pinnumber { get; set; }

        //public virtual ICollection<UserProductPurchases> Purchases { get; set; } = new List<UserProductPurchases>();

        //public virtual ICollection<UserLoyaltyStoreMappings> Mappings { get; set; }= new List<UserLoyaltyStoreMappings>();
    }
    public class UserProductPurchases
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int UserProfileID { get; set; }

        public string? TobaccoMember { get; set; }

        public string? Cigar { get; set; }

        public string? Mst { get; set; }

        public string? OTDN { get; set; }

        public string? Cigarette { get; set; }

    }

    public class UserLoyaltyStoreMappings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int UserProfileID { get; set; }

        public int? StoreID { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? LastPurchase { get; set; }
        public DateTime? CreateData { get; set; }

    }
}

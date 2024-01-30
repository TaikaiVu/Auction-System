using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assignment1web.Models.Data

{
    public class Item
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        [Column(TypeName = "varchar(50)")]

        public string? ProductName { get; set; }
        public int MinimumBid { get; set; }
        public DateTime AuctionStartDate { get; set; }
        public DateTime AuctionEndDate { get; set; }
        public string? Category { get; set; }
        public string? Productdescription { get; set; }
        public string? AssetCondition { get; set; }
        [NotMapped]
        public IFormFile? ItemImage { get; set; }
        public string? ImageUrl { get; set; }
        public string? BidderName { get; set; }
        public int Amount { get; set; }


    }
}

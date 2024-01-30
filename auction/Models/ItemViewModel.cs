using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace assignment1web.Models
{
    public class ItemViewModel
    {
        [Key]
        public int ItemId { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Please enter the product name")]
        public string? ProductName { get; set; }

        [DisplayName("Minimum Bid")]
        [Required(ErrorMessage = "Please enter a valid minimum bid")]
        public int MinimumBid { get; set; }

        [DisplayName("Auction Start Date")]
        [Required(ErrorMessage = "Please enter an auction start date")]
        public DateTime AuctionStartDate { get; set; }

        [DisplayName("Auction End Date")]
        [Required(ErrorMessage = "Please enter an auction end date")]
        public DateTime AuctionEndDate { get; set; }

        [DisplayName("Category")]
        [Required(ErrorMessage = "Please enter the product's category")]
        public string? Category { get; set; }

        [DisplayName("Product Description")]
        [Required(ErrorMessage = "Please enter the product description")]
        public string? Productdescription { get; set; }

        [DisplayName("Asset Condition")]
        [Required(ErrorMessage = "Please enter the condition of the item")]
        public string? AssetCondition { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Upload File (jpg files only)")]
        [Required(ErrorMessage = "Please choose an image to upload.")]
        [NotMapped]
        public IFormFile? ItemImage { get; set; }


        public string? ImageUrl { get; set; }

        [DisplayName("Bidder Name")]
        [Required(ErrorMessage = "Please enter the name of the bidder")]
        public string BidderName { get; set; }

        [DisplayName("Bidding Amount")]
        [Required(ErrorMessage = "Please enter the amount of your bidding")]
        public int Amount { get; set; }
    }
}
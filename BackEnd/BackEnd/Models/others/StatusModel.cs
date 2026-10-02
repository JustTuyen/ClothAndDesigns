using BackEnd.Models.productions;
using BackEnd.Models.productions.attributes;
using BackEnd.Models.shopping;
using BackEnd.Models.users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models.others
{
    [Table("Status")]
    [Index(nameof(Type), nameof(CreatedAt))]
    public class StatusModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<PaymentMethodModel> PaymentMethods { get; set; } = new List<PaymentMethodModel>();
        public virtual ICollection<AddressModel> Addresses { get; set; } = new List<AddressModel>();
        public virtual ICollection<OrderModel> Orders { get; set; } = new List<OrderModel>();
        public virtual ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
        public virtual ICollection<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        public virtual ICollection<SubCategoriesModel> SubCategories { get; set; } = new List<SubCategoriesModel>();
        public virtual ICollection<BannerModel> Banners { get; set; } = new List<BannerModel>();
        public virtual ICollection<DiscountModel> Discounts { get; set; } = new List<DiscountModel>();
        public virtual ICollection<UserModel> Users { get; set; } = new List<UserModel>();
        public virtual ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();
        public virtual ICollection<VariationModel> Variations { get; set; } = new List<VariationModel>();
    }
}

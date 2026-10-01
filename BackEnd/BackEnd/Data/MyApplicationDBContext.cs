using BackEnd.Models.others;
using BackEnd.Models.productions;
using BackEnd.Models.productions.attributes;
using BackEnd.Models.productions.tags;
using BackEnd.Models.shopping;
using BackEnd.Models.users;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class MyApplicationDBContext : DbContext
    {
        public MyApplicationDBContext(DbContextOptions<MyApplicationDBContext> options) : base(options) { }
        #region
        //users
        public DbSet<UserModel> Users { get; set; }
        //address
        public DbSet<AddressModel> Addresses { get; set; }
        public DbSet<CityModel> Cities { get; set; }
        public DbSet<DistrictModel> Districts { get; set; }
        public DbSet<WardModel> Wards { get; set; }
        //Cart
        public DbSet<CartModel> Carts { get; set; }
        public DbSet<CartItemModel> CartItems { get; set; }
        //Order
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }
        //invoice
        public DbSet<InvoiceModel> Invoices { get; set; }
        public DbSet<InvoiceItemModel> InvoiceItems { get; set; }
        //images
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<ImageGallaryModel> ImageGallaries { get; set; }
        //others
        public DbSet<BannerModel> Banners { get; set; }
        public DbSet<StatusModel> Statuses { get; set; }
        public DbSet<ActivityLogModel> ActivityLogs { get; set; }
        //Products
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<SubCategoriesMdel> SubCategories { get; set; }
        //tag
        public DbSet<ProductTagModel> productTags { get; set; }
        public DbSet<TagModel> Tags { get; set; }
        //varied
        public DbSet<VariationModel> Variations { get; set; }
        public DbSet<VariationValueModel> variationValues { get; set; }
        public DbSet<AttributeValueModel> AttributeValues { get; set; }
        public DbSet<AttributeModel> Attributes { get; set; }
        //pay
        public DbSet<PaymentMethodModel> paymentMethods { get; set; }
        //activites
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<FavoriteModel> Favorites { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}

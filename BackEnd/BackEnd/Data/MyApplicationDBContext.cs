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
        //discount
        public DbSet<DiscountModel> Discounts { get; set; }
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
        public DbSet<SubCategoriesModel> SubCategories { get; set; }
        //tag
        public DbSet<ProductTagModel> productTags { get; set; }
        public DbSet<TagModel> Tags { get; set; }
        //varied
        public DbSet<VariationModel> Variations { get; set; }
        public DbSet<VariationValueModel> VariationValues { get; set; }
        public DbSet<AttributeValueModel> AttributeValues { get; set; }
        public DbSet<AttributeModel> Attributes { get; set; }
        //pay
        public DbSet<PaymentMethodModel> PaymentMethods { get; set; }
        //activites
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<FavoriteModel> Favorites { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //// Quan hệ 1: Restrict (không cho xóa User nếu còn log)
            //// Quan hệ 2: Cascade (xóa Product thì xóa luôn gallery)
            ///
            //address
            modelBuilder.Entity<AddressModel>()
                .HasOne(f => f.User)
                .WithMany(f => f.Addresses)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AddressModel>()
               .HasOne(f => f.City)
               .WithMany(f => f.Addresses)
               .HasForeignKey(f => f.CityId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AddressModel>()
               .HasOne(f => f.District)
               .WithMany(f => f.Addresses)
               .HasForeignKey(f => f.DistrictId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AddressModel>()
               .HasOne(f => f.Ward)
               .WithMany(f => f.Addresses)
               .HasForeignKey(f => f.WardId)
               .OnDelete(DeleteBehavior.Restrict);

            //district
            modelBuilder.Entity<DistrictModel>()
                .HasOne(f => f.City)
                .WithMany(f => f.Districts)
                .HasForeignKey(f => f.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            //ward
            modelBuilder.Entity<WardModel>()
                .HasOne(f => f.District)
                .WithMany(f => f.Wards)
                .HasForeignKey(f => f.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            //image gallery
            modelBuilder.Entity<ImageGallaryModel>()
                .HasOne(f => f.Product)
                .WithMany(f => f.ImageGallaries)
                .HasForeignKey(f => f.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ImageGallaryModel>()
                .HasOne(f => f.Variation)
                .WithMany()
                .HasForeignKey(g => new { g.VariationId, g.ProductId })
                .HasPrincipalKey(v => new { v.Id, v.ProductId })
                .OnDelete(DeleteBehavior.Cascade);

            //product tag
            modelBuilder.Entity<ProductTagModel>()
                .HasKey(pt => new { pt.ProductId, pt.TagId });

            modelBuilder.Entity<ProductTagModel>()
               .HasOne(f => f.Product)
               .WithMany(p => p.ProductTags)
               .HasForeignKey(g => g.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductTagModel>()
               .HasOne(f => f.Tag)
               .WithMany(p => p.ProductTags)
               .HasForeignKey(g => g.TagId )
               .OnDelete(DeleteBehavior.Restrict);

            //favorites
            modelBuilder.Entity<FavoriteModel>()
               .HasKey(f => new { f.ProductId, f.UserId });

            modelBuilder.Entity<FavoriteModel>()
               .HasOne(f => f.User)
               .WithMany(p => p.Favorites)
               .HasForeignKey(g => g.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FavoriteModel>()
               .HasOne(f => f.Product)
               .WithMany(p => p.Favorites)
               .HasForeignKey(g => g.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

            //comments
            modelBuilder.Entity<CommentModel>()
               .HasKey(f => new { f.ProductId, f.UserId });

            modelBuilder.Entity<CommentModel>()
               .HasOne(f => f.User)
               .WithMany(p => p.Comments)
               .HasForeignKey(g => g.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentModel>()
               .HasOne(f => f.Product)
               .WithMany(p => p.Comments)
               .HasForeignKey(g => g.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CommentModel>()
               .HasOne(f => f.Image)
               .WithOne(f => f.Comment)
               .HasForeignKey<CommentModel>(g => g.ImageId)
               .OnDelete(DeleteBehavior.Cascade);

            //discount
            modelBuilder.Entity<DiscountModel>()
               .HasOne(f => f.Image)
               .WithOne(f => f.Discount)
               .HasForeignKey<DiscountModel>(g => g.ImageId)
               .OnDelete(DeleteBehavior.Cascade);

            //category
            modelBuilder.Entity<CategoryModel>()
               .HasOne(f => f.Image)
               .WithOne(f => f.Category)
               .HasForeignKey<CategoryModel>(g => g.ImageId)
               .OnDelete(DeleteBehavior.Cascade);

            //sub category
            modelBuilder.Entity<SubCategoriesModel>()
                .HasOne(f => f.Category)
                .WithMany(f => f.SubCategories)
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //product
            modelBuilder.Entity<ProductModel>()
                .HasOne(f => f.SubCategory)
                .WithMany(f => f.Products)
                .HasForeignKey(f => f.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductModel>()
              .HasOne(f => f.Discount)
              .WithMany(f => f.Products)
              .HasForeignKey(g => g.DiscountId)
              .OnDelete(DeleteBehavior.SetNull);


            //banners
            modelBuilder.Entity<BannerModel>()
               .HasOne(f => f.Image)
               .WithOne(f => f.Banner)
               .HasForeignKey<BannerModel>(g => g.ImageId)
               .OnDelete(DeleteBehavior.Cascade);

            //variations
            modelBuilder.Entity<VariationModel>()
              .HasOne(f => f.Product)
              .WithMany(f => f.Variations)
              .HasForeignKey(g => g.ProductId)
              .OnDelete(DeleteBehavior.Cascade);

            //cart
            modelBuilder.Entity<CartModel>()
              .HasOne(f => f.User)
              .WithOne(f => f.Cart)
              .HasForeignKey<CartModel>(g => g.UserId)
              .OnDelete(DeleteBehavior.Cascade);

            //cart items
            modelBuilder.Entity<CartItemModel>()
              .HasOne(f => f.Variation)
              .WithMany(f => f.CartItems)
              .HasForeignKey(g => g.VariationId)
              .OnDelete(DeleteBehavior.Restrict);

            //order
            modelBuilder.Entity<OrderModel>()
             .HasOne(f => f.User)
             .WithMany(f => f.Orders)
             .HasForeignKey(g => g.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderModel>()
               .HasOne(f => f.PaymentMethod)
               .WithMany(f => f.Orders)
               .HasForeignKey(g => g.PaymentMethodId)
               .OnDelete(DeleteBehavior.Restrict);

            //order items
            modelBuilder.Entity<OrderItemModel>()
              .HasOne(f => f.Variation)
              .WithMany(f => f.OrderItems)
              .HasForeignKey(g => g.VariationId)
              .OnDelete(DeleteBehavior.Restrict);

            //invoice
            modelBuilder.Entity<InvoiceModel>()
             .HasOne(f => f.User)
             .WithMany(f => f.Invoices)
             .HasForeignKey(g => g.UserId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InvoiceModel>()
               .HasOne(f => f.PaymentMethod)
               .WithMany(f => f.Invoices)
               .HasForeignKey(g => g.PaymentMethodId)
               .OnDelete(DeleteBehavior.Restrict);

            //invoice items
            modelBuilder.Entity<InvoiceItemModel>()
              .HasOne(f => f.Variation)
              .WithMany(f => f.InvoiceItems)
              .HasForeignKey(g => g.VariationId)
              .OnDelete(DeleteBehavior.Restrict);

        }

    }
}

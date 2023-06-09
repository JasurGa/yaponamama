﻿using System;
using Atlas.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Atlas.Domain;
using Atlas.Persistence.EntityTypeConfigurations;

namespace Atlas.Persistence
{
    public class AtlasDbContext : DbContext, IAtlasDbContext
    {
        public DbSet<AddressToClient> AddressToClients { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<CardInfoToClient> CardInfoToClients { get; set; }

        public DbSet<ChatMessage> ChatMessages { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Consignment> Consignments { get; set; }

        public DbSet<Correction> Corrections { get; set; }

        public DbSet<Courier> Couriers { get; set; }

        public DbSet<DebitCreditStatistics> DebitCreditStatistics { get; set; }

        public DbSet<DisposeToConsignment> DisposeToConsignments { get; set; }

        public DbSet<FavoriteGood> FavoriteGoods { get; set; }

        public DbSet<ForgotPasswordCode> ForgotPasswordCodes { get; set; }

        public DbSet<Good> Goods { get; set; }

        public DbSet<GoodToCart> GoodToCarts { get; set; }

        public DbSet<GoodToOrder> GoodToOrders { get; set; }

        public DbSet<HeadRecruiter> HeadRecruiters { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<NotificationAccess> NotificationAccesses { get; set; }

        public DbSet<NotificationType> NotificationTypes { get; set; }

        public DbSet<OfficialRole> OfficialRoles { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderChat> OrderChats { get; set; }

        public DbSet<OrderComment> OrderComments { get; set; }

        public DbSet<OrderFeedback> OrderFeedbacks { get; set; }

        public DbSet<PageVisit> PageVisits { get; set; }

        public DbSet<Promo> Promos { get; set; }

        public DbSet<PromoAdvertise> PromoAdvertises { get; set; }

        public DbSet<PromoAdvertiseGood> PromoAdvertiseGoods { get; set; }

        public DbSet<PromoAdvertisePage> PromoAdvertisePages { get; set; }

        public DbSet<PromoCategory> PromoCategories { get; set; }

        public DbSet<PromoToGood> PromoToGoods { get; set; }

        public DbSet<PromoCategoryToGood> PromoCategoryToGoods { get; set; }

        public DbSet<PromoUsage> PromoUsages { get; set; }

        public DbSet<Provider> Providers { get; set; }

        public DbSet<ProviderPhoneNumber> ProviderPhoneNumbers { get; set; }

        public DbSet<PhotoToGood> PhotoToGoods { get; set; }

        public DbSet<PushNotification> PushNotifications { get; set; }

        public DbSet<PushNotificationRead> PushNotificationReads { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<ReceiptItem> ReceiptItems { get; set; }

        public DbSet<Recommendation> Recommendations { get; set; }

        public DbSet<RecommendationType> RecommendationTypes { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<StoreToGood> StoreToGoods { get; set; }

        public DbSet<SupplyManager> SupplyManagers { get; set; }

        public DbSet<Support> Supports { get; set; }

        public DbSet<SupportCall> SupportCalls { get; set; }

        public DbSet<SupportNote> SupportNotes { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleType> VehicleTypes { get; set; }

        public DbSet<VerifyCode> VerifyCodes { get; set; }

        public DbSet<VerificationRequest> VerificationRequests { get; set; }

        public AtlasDbContext(DbContextOptions<AtlasDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressToClientConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new CardInfoToClientConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new ConsignmentConfiguration());
            modelBuilder.ApplyConfiguration(new CorrectionConfiguration());
            modelBuilder.ApplyConfiguration(new CourierConfiguration());
            modelBuilder.ApplyConfiguration(new DebitCreditStatisticsConfiguration());
            modelBuilder.ApplyConfiguration(new DisposeToConsignmentConfiguration());
            modelBuilder.ApplyConfiguration(new ForgotPasswordCodeConfiguration());
            modelBuilder.ApplyConfiguration(new GoodConfiguration());
            modelBuilder.ApplyConfiguration(new GoodToCartConfiguration());
            modelBuilder.ApplyConfiguration(new GoodToOrderConfiguration());
            modelBuilder.ApplyConfiguration(new HeadRecruiterConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationAccessConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OfficialRoleConfiguration());
            modelBuilder.ApplyConfiguration(new OrderChatConfiguration());
            modelBuilder.ApplyConfiguration(new OrderCommentConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderFeedbackConfiguration());
            modelBuilder.ApplyConfiguration(new PageVisitConfiguration());
            modelBuilder.ApplyConfiguration(new PromoConfiguration());
            modelBuilder.ApplyConfiguration(new PromoToGoodConfiguration());
            modelBuilder.ApplyConfiguration(new PromoAdvertiseConfiguration());
            modelBuilder.ApplyConfiguration(new PromoAdvertiseGoodConfiguration());
            modelBuilder.ApplyConfiguration(new PromoAdvertisePageConfiguration());
            modelBuilder.ApplyConfiguration(new PromoCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PromoCategoryToGoodConfiguration());
            modelBuilder.ApplyConfiguration(new PromoUsageConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderPhoneNumberConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoToGoodConfiguration());
            modelBuilder.ApplyConfiguration(new PushNotificationConfiguration());
            modelBuilder.ApplyConfiguration(new PushNotificationReadConfiguration());
            modelBuilder.ApplyConfiguration(new ReceiptTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReceiptItemTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RecommendationConfiguration());
            modelBuilder.ApplyConfiguration(new RecommendationTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            modelBuilder.ApplyConfiguration(new StoreToGoodConfiguration());
            modelBuilder.ApplyConfiguration(new SupplyManagerConfiguration());
            modelBuilder.ApplyConfiguration(new SupportCallConfiguration());
            modelBuilder.ApplyConfiguration(new SupportConfiguration());
            modelBuilder.ApplyConfiguration(new SupportNoteConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleConfiguration());
            modelBuilder.ApplyConfiguration(new VehicleTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VerifyCodeConfiguration());
            modelBuilder.ApplyConfiguration(new VerificationRequestConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}

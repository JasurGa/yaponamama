﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Atlas.Domain;

namespace Atlas.Application.Interfaces
{
    public interface IAtlasDbContext
    {
        DbSet<Admin> Admins { get; set; }

        DbSet<AddressToClient> AddressToClients { get; set; }

        DbSet<CardInfoToClient> CardInfoToClients { get; set; }

        DbSet<ChatMessage> ChatMessages { get; set; }

        DbSet<Client> Clients { get; set; }

        DbSet<Consignment> Consignments { get; set; }

        DbSet<Correction> Corrections { get; set; }

        DbSet<Courier> Couriers { get; set; }

        DbSet<DebitCreditStatistics> DebitCreditStatistics { get; set; }

        DbSet<DisposeToConsignment> DisposeToConsignments { get; set; }

        DbSet<FavoriteGood> FavoriteGoods { get; set; }

        DbSet<ForgotPasswordCode> ForgotPasswordCodes { get; set; }

        DbSet<Good> Goods { get; set; }

        DbSet<GoodToCart> GoodToCarts { get; set; }

        DbSet<GoodToOrder> GoodToOrders { get; set; }

        DbSet<HeadRecruiter> HeadRecruiters { get; set; }

        DbSet<Language> Languages { get; set; }

        DbSet<Notification> Notifications { get; set; }

        DbSet<NotificationAccess> NotificationAccesses { get; set; }

        DbSet<NotificationType> NotificationTypes { get; set; }

        DbSet<OfficialRole> OfficialRoles { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<OrderChat> OrderChats { get; set; }

        DbSet<OrderComment> OrderComments { get; set; }

        DbSet<OrderFeedback> OrderFeedbacks { get; set; }

        DbSet<PageVisit> PageVisits { get; set; }

        DbSet<Promo> Promos { get; set; }

        DbSet<PromoToGood> PromoToGoods { get; set; }

        DbSet<PromoAdvertise> PromoAdvertises { get; set; }

        DbSet<PromoAdvertiseGood> PromoAdvertiseGoods { get; set; }

        DbSet<PromoAdvertisePage> PromoAdvertisePages { get; set; }
        
        DbSet<PromoCategory> PromoCategories { get; set; }

        DbSet<PromoCategoryToGood> PromoCategoryToGoods { get; set; }

        DbSet<PromoUsage> PromoUsages { get; set; }

        DbSet<Provider> Providers { get; set; }

        DbSet<PhotoToGood> PhotoToGoods { get; set; }

        DbSet<ProviderPhoneNumber> ProviderPhoneNumbers { get; set; }

        DbSet<PushNotification> PushNotifications { get; set; }

        DbSet<PushNotificationRead> PushNotificationReads { get; set; }

        DbSet<Receipt> Receipts { get; set; }

        DbSet<ReceiptItem> ReceiptItems { get; set; }

        DbSet<Recommendation> Recommendations { get; set; }

        DbSet<RecommendationType> RecommendationTypes { get; set; }

        DbSet<RefreshToken> RefreshTokens { get; set; }

        DbSet<Store> Stores { get; set; }

        DbSet<StoreToGood> StoreToGoods { get; set; }

        DbSet<SupplyManager> SupplyManagers { get; set; }

        DbSet<Support> Supports { get; set; }

        DbSet<SupportCall> SupportCalls { get; set; }

        DbSet<SupportNote> SupportNotes { get; set; }

        DbSet<Transaction> Transactions { get; set; }

        DbSet<User> Users { get; set; }

        DbSet<Vehicle> Vehicles { get; set; }

        DbSet<VehicleType> VehicleTypes { get; set; }

        DbSet<VerifyCode> VerifyCodes { get; set; }

        DbSet<VerificationRequest> VerificationRequests { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

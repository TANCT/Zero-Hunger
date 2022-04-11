using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ZeroHunger.Model;

namespace ZeroHunger.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<DryFoodDonation> DryFoodDonation { get; set; }
        public DbSet<SalaryGroup> SalaryGroup { get; set; }
        public DbSet<Receiver> Receiver { get; set; }
        public DbSet<ReceiverFamily> ReceiverFamily { get; set; }
        public DbSet<ProductInNeed> ProductInNeed { get; set; }
        public DbSet<CookedFoodDonation> CookedFoodDonation { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<DeliveryItem> DeliveryItem { get; set; }
        public DbSet<CookReservation> CookReservation { get; set; }
        public DbSet<ReceiverQuestionnaire> ReceiverQuestionnaire { get; set; }
    }
}

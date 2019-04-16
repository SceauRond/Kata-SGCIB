using BookingAPI.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingAPI.Data
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            RoomSeedData(modelBuilder.Entity<Room>());
            UserSeedData(modelBuilder.Entity<User>());

        }

        private void UserSeedData(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User() { Id = 1, Name = "User 1"},
                new User() { Id = 2, Name = "User 2"},
                new User() { Id = 3, Name = "User 3"},
                new User() { Id = 4, Name = "User 4"},
                new User() { Id = 5, Name = "User 5"}
                );
        }

        private void RoomSeedData(EntityTypeBuilder<Room> builder)
        {
            builder.HasData(
                new Room() { Id = 1, Name = "room 1" },
                new Room() { Id = 2, Name = "room 2" },
                new Room() { Id = 3, Name = "room 3" },
                new Room() { Id = 4, Name = "room 4" },
                new Room() { Id = 5, Name = "room 5" },
                new Room() { Id = 6, Name = "room 6" },
                new Room() { Id = 7, Name = "room 7" },
                new Room() { Id = 8, Name = "room 8" },
                new Room() { Id = 9, Name = "room 9" },
                new Room() { Id = 10, Name = "room 10" }
                );
        }
    }
}

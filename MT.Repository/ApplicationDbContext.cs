using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MT.Data.Identity;
using MT.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MT.Repository
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderedTickets> OrderedTickets { get; set; }
        public virtual DbSet<TicketsInCart> CartTickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Cart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<TicketsInCart>()
                .HasKey(z => new { z.TicketId, z.CartId });

            builder.Entity<TicketsInCart>()
                .HasOne(z => z.Movie)
                .WithMany(z => z.CartTickets)
                .HasForeignKey(z => z.CartId);

            builder.Entity<TicketsInCart>()
                .HasOne(z => z.Cart)
                .WithMany(z => z.CartTickets)
                .HasForeignKey(z => z.TicketId);

            builder.Entity<Cart>()
                .HasOne<AppUser>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<Cart>(z => z.OwnerId);

            builder.Entity<OrderedTickets>()
                .HasKey(z => new { z.TicketId, z.OrderId });

            builder.Entity<OrderedTickets>()
                .HasOne(z => z.SelectedMovie)
                .WithMany(z => z.OrderedTickets)
                .HasForeignKey(z => z.TicketId);

            builder.Entity<OrderedTickets>()
                .HasOne(z => z.UserOrder)
                .WithMany(z => z.OrderedTickets)
                .HasForeignKey(z => z.OrderId);
        }
    }
}

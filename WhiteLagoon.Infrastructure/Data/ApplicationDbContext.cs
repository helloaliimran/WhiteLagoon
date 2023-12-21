using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteLagoon.Domain.Entities;


namespace WhiteLagoon.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Villa> villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }
        public DbSet<Amenity> Amenities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(
                 new Villa
                 {
                     Id = 1,
                     Name = "Royal Villa",
                     Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                     ImageURL = "https://placehold.co/600x400",
                     Occupancy = 4,
                     Price = 200,
                     Sqft = 550,
                 },
                 new Villa
                 {
                     Id = 2,
                     Name = "Premium Pool Villa",
                     Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                     ImageURL = "https://placehold.co/600x401",
                     Occupancy = 4,
                     Price = 300,
                     Sqft = 550,
                 },
                 new Villa
                 {
                     Id = 3,
                     Name = "Luxury Pool Villa",
                     Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                     ImageURL = "https://placehold.co/600x402",
                     Occupancy = 4,
                     Price = 400,
                     Sqft = 750,
                 }
                );
            modelBuilder.Entity<VillaNumber>().HasData(
                new VillaNumber { VillaId = 2, Villa_Number = 201 },
                new VillaNumber { VillaId = 2, Villa_Number = 202 },
                new VillaNumber { VillaId = 3, Villa_Number = 301 },
                new VillaNumber { VillaId = 3, Villa_Number = 302 },
                new VillaNumber { VillaId = 13, Villa_Number = 131 },
                new VillaNumber { VillaId = 13, Villa_Number = 132 }
            );

            modelBuilder.Entity<Amenity>().HasData(
                    new Amenity
                    {
                        Id = 1,
                        VillaId = 13,
                        Name = "Private Pool"
                    }, new Amenity
                    {
                        Id = 2,
                        VillaId = 13,
                        Name = "Microwave"
                    }, new Amenity
                    {
                        Id = 3,
                        VillaId = 13,
                        Name = "Private Balcony"
                    }, new Amenity
                    {
                        Id = 4,
                        VillaId = 13,
                        Name = "1 king bed and 1 sofa bed"
                    },

                    new Amenity
                    {
                        Id = 5,
                        VillaId = 2,
                        Name = "Private Plunge Pool"
                    }, new Amenity
                    {
                        Id = 6,
                        VillaId = 2,
                        Name = "Microwave and Mini Refrigerator"
                    }, new Amenity
                    {
                        Id = 7,
                        VillaId = 2,
                        Name = "Private Balcony"
                    }, new Amenity
                    {
                        Id = 8,
                        VillaId = 2,
                        Name = "king bed or 2 double beds"
                    },

                    new Amenity
                    {
                        Id = 9,
                        VillaId = 3,
                        Name = "Private Pool"
                    }, new Amenity
                    {
                        Id = 10,
                        VillaId = 3,
                        Name = "Jacuzzi"
                    }, new Amenity
                    {
                        Id = 11,
                        VillaId = 3,
                        Name = "Private Balcony"
                    });

        }
    }
}

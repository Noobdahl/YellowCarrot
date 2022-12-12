﻿using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.EntityFrameworkCore;
using YellowCarrot.Models;

namespace YellowCarrot.Data
{
    public class UserDbContext : DbContext
    {
        private IEncryptionProvider _provider;
        public UserDbContext()
        {
            this._provider = new GenerateEncryptionProvider("_example_encryption_key_");
        }
        public UserDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YellowCarrotUserDb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(_provider);

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    UserId = 1,
                    Name = "admin",
                    Password = "password",
                    IsAdmin = true,
                },
                new User()
                {
                    UserId = 2,
                    Name = "user",
                    Password = "password",
                    IsAdmin = false,
                },
                new User()
                {
                    UserId = 3,
                    Name = "Micke",
                    Password = "asd",
                    IsAdmin = false,
                });
        }
    }
}

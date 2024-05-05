using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace eshop.Models;

public class EshopContext : DbContext
{
/*    public EshopContext()
    {
        this.Configuration.LazyLoadingEnabled = false;
    }*/


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=eshop;User Id=Andhromede;Password=12345;TrustServerCertificate=true");

    public EshopContext(DbContextOptions options) : base(options)
    {
         /*this.ChangeTracker.LazyLoadingEnabled = false;*/
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Opinion> Opinions { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetails> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

}
